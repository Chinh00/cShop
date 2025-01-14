namespace Application.Abstraction;

public interface IPaymentService
{
}

public interface IVnpayService : IPaymentService
{
    void AddRequestData(string key, string value);
    void AddResponseData(string key, string value);
    string GetResponseData(string key);
    string CreateRequestUrl(string baseUrl, string vnpHashSecret);
    bool ValidateSignature(string inputHash, string secretKey);

}