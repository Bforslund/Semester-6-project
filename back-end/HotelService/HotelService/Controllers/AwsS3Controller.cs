using HotelService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HotelService.Controllers
{
    public class AwsS3Controller : Controller
    {
		private readonly IAws3Services _aws3Services;
        public AwsS3Controller(IAws3Services aws3Services)
        {
            _aws3Services = aws3Services;
        }

        [HttpGet("{documentName}")]
        public IActionResult GetDocumentFromS3(string documentName)
        {
            try
            {
                if (string.IsNullOrEmpty(documentName))
                    return BadRequest("The 'documentName' parameter is required");

                var document = _aws3Services.DownloadFileAsync(documentName).Result;

                return File(document, "application/octet-stream", documentName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("newPic")]
        public IActionResult UploadDocumentToS3(IFormFile file)
		{
			try
			{
                if (file is null || file.Length <= 0)
					return BadRequest("file is required to upload");
				
                var result = _aws3Services.UploadFileAsync(file);
				
				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex);
			}
		}

        [HttpDelete("{documentName}")]
        public async Task<IActionResult> DeletetDocumentFromS3Async(string documentName)
        {
            try
            {
                if (string.IsNullOrEmpty(documentName))
                    return BadRequest("The 'documentName' parameter is required");

               var successfullyDeleted = await _aws3Services.DeleteFile(documentName);
                if (successfullyDeleted)
                {
                    return Ok("The document is deleted successfully");
                }
                else
                {
                    return StatusCode(500, "something went wrong");
                }
              
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
