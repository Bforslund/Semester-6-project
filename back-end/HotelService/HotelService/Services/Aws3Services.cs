using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using HotelService.Configuration;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace HotelService.Services
{
    public class Aws3Services : IAws3Services
    {
        private readonly AmazonS3Client _client;
        private readonly IAppConfiguration _settings;

        public Aws3Services(IAppConfiguration settings)
        {
            _settings = settings;
            _client = new AmazonS3Client(
                _settings.AccessKey,
                _settings.SecretKey,
                RegionEndpoint.EUNorth1
            );
        }

        public async Task<bool> UploadFileAsync(IFormFile file)
        {
            try
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        Key = file.FileName,
                        InputStream = newMemoryStream,
                        BucketName = _settings.BucketName
                    };

                    var fileTransferUtility = new TransferUtility(_client);

                    await fileTransferUtility.UploadAsync(uploadRequest);

                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<byte[]> DownloadFileAsync(string file)
        {
            MemoryStream ms = null;

            try
            {
                GetObjectRequest getObjectRequest = new GetObjectRequest
                {
                    BucketName = _settings.BucketName,
                    Key = file
                };

                using (var response = await _client.GetObjectAsync(getObjectRequest))
                {
                    if (response.HttpStatusCode == HttpStatusCode.OK)
                    {
                        using (ms = new MemoryStream())
                        {
                            await response.ResponseStream.CopyToAsync(ms);
                        }
                    }
                }

                if (ms is null || ms.ToArray().Length < 1)
                    throw new FileNotFoundException(string.Format("The document '{0}' is not found", file));

                return ms.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Dispose()
        {
            _client.Dispose();
        }

        public async Task<bool> DeleteFile(string fileName)
        {
            if (IsFileExists(fileName) == false)
            {
                return false;
            }
            DeleteObjectRequest request = new DeleteObjectRequest
            {
                BucketName = _settings.BucketName,
                Key = fileName
            };


            await _client.DeleteObjectAsync(request);
            return true;
        }

        public bool IsFileExists(string fileName)
        {
            try
            {
                GetObjectMetadataRequest request = new GetObjectMetadataRequest()
                {
                    BucketName = _settings.BucketName,
                    Key = fileName
                };

                var response = _client.GetObjectMetadataAsync(request).Result;

                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException is AmazonS3Exception awsEx)
                {
                    if (string.Equals(awsEx.ErrorCode, "NoSuchBucket"))
                        return false;

                    else if (string.Equals(awsEx.ErrorCode, "NotFound"))
                        return false;
                }

                throw;
            }
        }
    }
}
