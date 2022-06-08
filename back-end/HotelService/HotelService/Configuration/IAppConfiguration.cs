namespace HotelService.Configuration
{
	public interface IAppConfiguration
	{

		string AccessKey { get; }

		string SecretKey { get; }

		string BucketName { get; }
	}
}
