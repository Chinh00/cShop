using System.Globalization;
using System.Net;
using System.Text;
using Application.Abstraction;

namespace Infrastructure.Services;

public class VnpayService : IVnpayService
{
    private readonly SortedList<String, String> _requestData = new(new VnPayCompare());
    private readonly SortedList<String, String> _responseData = new(new VnPayCompare());
    
    public void AddRequestData(string key, string value)
    {
        if (!String.IsNullOrEmpty(value))
        {
            _requestData.Add(key, value);
        }
    }
    public void AddResponseData(string key, string value)
    {
        if (!String.IsNullOrEmpty(value))
        {
            _responseData.Add(key, value);
        }
    }
    public string GetResponseData(string key)
    {
        string retValue;
        if (_responseData.TryGetValue(key, out retValue))
        {
            return retValue;
        }
        else
        {
            return string.Empty;
        }
    }
    public string CreateRequestUrl(string baseUrl, string vnpHashSecret)
    {
        var queryBuilder = new StringBuilder();

        foreach (var (key, value) in _requestData.Where(kv => !string.IsNullOrEmpty(kv.Value)))
        {
            queryBuilder.Append($"{WebUtility.UrlEncode(key)}={WebUtility.UrlEncode(value)}&");
        }

        if (queryBuilder.Length > 0)
        {
            queryBuilder.Length--;
        }

        var signData = queryBuilder.ToString();

        var secureHash = Utils.HmacSHA512(vnpHashSecret, signData);

        return $"{baseUrl}?{signData}&vnp_SecureHash={WebUtility.UrlEncode(secureHash)}";
        
        
    }
    public bool ValidateSignature(string inputHash, string secretKey)
    {
        string rspRaw = GetResponseData();
        string myChecksum = Utils.HmacSHA512(secretKey, rspRaw);
        return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
    }
    public string GetResponseData()
    {

        StringBuilder data = new StringBuilder();
        if (_responseData.ContainsKey("vnp_SecureHashType"))
        {
            _responseData.Remove("vnp_SecureHashType");
        }
        if (_responseData.ContainsKey("vnp_SecureHash"))
        {
            _responseData.Remove("vnp_SecureHash");
        }
        foreach (KeyValuePair<string, string> kv in _responseData)
        {
            if (!String.IsNullOrEmpty(kv.Value))
            {
                data.Append(WebUtility.UrlEncode( kv.Key) + "=" + WebUtility.UrlEncode(kv.Value )+ "&");
            }
        }
        if (data.Length > 0)
        {
            data.Remove(data.Length - 1, 1);
        }
        return data.ToString();
    }

    private class VnPayCompare : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            var vnpCompare = CompareInfo.GetCompareInfo("en-US");
            return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
        }
    }
}