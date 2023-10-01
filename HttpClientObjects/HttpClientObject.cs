using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskFileManager.HttpClientObjects;


public class HttpClientObject
{
   private static string defaultUrl = "http://141.134.1.48:8080";

    private static CookieContainer cookieContainer = new CookieContainer();
   private static HttpClientHandler clienthandler =
               new HttpClientHandler
               {
                    AllowAutoRedirect = true,
                    UseCookies = true,
                    CookieContainer = cookieContainer
               };
   private HttpClient client = new HttpClient(clienthandler);



    public async Task<string> postAsync(string endpoint, HttpContent content) 
    {
      var res =   client.PostAsync(defaultUrl + endpoint, content).Result;
        var response = await res.Content.ReadAsStringAsync();
        return response;
    }

    public async Task<string> getAsync(string endpoint) 
    {
        var res = client.GetAsync(defaultUrl+endpoint).Result;

        var response = await res.Content.ReadAsStringAsync();
        return response;
    }

   
   public HttpClient getHttpClient() 
   {
        return client;
   }

    public string getDefaultUrl() { return defaultUrl; }
}

