namespace cShop.Infrastructure.Models;

public class PaymentParam
{
    public const string Name = "Vnpay";
    public string vnp_Version { get; set; }
    public string vnp_Command { get; set; }
    public string vnp_TmnCode { get; set; }
    public string vnp_Amount { get; set; }
    public string vnp_CreateDate { get; set; }
    public string vnp_CurrCode { get; set; }
    public string vnp_IpAddr { get; set; }
    public string vnp_Locale { get; set; }
    public string vnp_OrderInfo { get; set; }
    public string vnp_OrderType { get; set; }
    public string vnp_ReturnUrl { get; set; }
    public string vnp_TxnRef { get; set; }
    public string vnpay_url { get; set; }
    public string vnp_HashSecret { get; set; }

}