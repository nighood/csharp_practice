using System;
using System.Web;
using System.Net;
using System.Text;

public class Translator {
    private string baseUrl;
    public  Translator() {
        baseUrl = "https://translate.googleapis.com/translate_a/single?client=gtx&";
    }

    public string Translate(string text, string fromLanguage = "en", string toLanguage = "zh") {
        var url = baseUrl + $"sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(text)}";
        var webClient = new WebClient();
        webClient.Encoding = Encoding.UTF8;

        string result = webClient.DownloadString(url);
        try {
            // Console.WriteLine(result);
            return result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
        }
        catch {
            return "Error";
        }
    }
}
