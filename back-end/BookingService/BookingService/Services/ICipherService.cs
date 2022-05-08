namespace BookingService.Repository
{
    public interface ICipherService
    {
        string Decrypt(string cipherText);
        string Encrypt(string input);
    }
}