namespace HotelService.Configuration
{
	public class AppConfiguration : IAppConfiguration
	{
		public string AccessKey { get; set; }

		public string SecretKey { get; set; }

		public string BucketName { get; set; }
	}
}
