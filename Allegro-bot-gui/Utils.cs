using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leaf.xNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Allegro_bot_gui
{
    public class Utils
    {
        public void RequestSettings(HttpRequest req, string cookie)
        {
            req.ClearAllHeaders();
            req.AddHeader("Host", "edge.allegro.pl");
            req.AddHeader("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.13; rv:106.0) Gecko/20100101 Firefox/106.0");
            req.AddHeader("Accept", "application/vnd.allegro.public.v1+json");
            req.AddHeader("Accept-Language", "en-US");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            req.AddHeader("Referer", "https://allegro.pl/");
            req.AddHeader("Content-Type", "application/vnd.allegro.public.v1+json");
            req.AddHeader("Origin", "https://allegro.pl");
            req.AddHeader("DNT", "1");
            req.AddHeader("Connection", "keep-alive");
            req.AddHeader("Cookie", cookie);
            req.AddHeader("Sec-Fetch-Dest", "empty");
            req.AddHeader("Sec-Fetch-Mode", "no-cors");
            req.AddHeader("Sec-Fetch-Site", "same-site");
            req.AddHeader("TE", "trailers");
            req.AddHeader("Pragma", "no-cache");
            req.AddHeader("Cache-Control", "no-cache");
        }
        public string ConvertCookieString(string cookiesJson)
        {
            Dictionary<string, string> cookiesDict = new Dictionary<string, string>();
            JObject cookiesJsonObj = JObject.Parse(cookiesJson);
            foreach (var cookie in cookiesJsonObj)
            {
                cookiesDict.Add(cookie.Key, cookie.Value.ToString());
            }

            string cookieString = string.Join("; ", cookiesDict.Select(c => $"{c.Key}={c.Value}"));
            return cookieString;
        }
    }
}
