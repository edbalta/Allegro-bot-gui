using System;
using System.Collections.Generic;
using Leaf.xNet;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Allegro_bot_gui
{
    public class Backend
    {
        public Mprint mprint = new Mprint();
        public string[] proxies = File.ReadAllLines("./Data/proxies.txt");
        public Random random = new Random();
        public Utils utility = new Utils();
        public ReviewScrapper scrapper2 = new ReviewScrapper();
        public UnpaidModuleBackend unpaid_backend = new UnpaidModuleBackend();
        static string GenerateUsername(List<string> words)
        {
            Random random = new Random();
            string username = "";

            string word1 = words[random.Next(words.Count)];
            string word2 = words[random.Next(words.Count)];

            bool useFullWords = random.Next(10) < 2; // 20% chance of using full words

            if (useFullWords)
            {
                username = word1 + word2;
            }
            else
            {
                int cutIndex1 = random.Next(word1.Length);
                int cutIndex2 = random.Next(word2.Length);

                username = word1.Substring(0, cutIndex1) + word2.Substring(cutIndex2);
            }

            bool useNumbers = random.Next(10) < 2; // 20% chance of adding numbers

            if (useNumbers)
            {
                int num = random.Next(1000); // Generate a random number between 0 and 999
                username += num.ToString();
            }

            return username;
        }
        public string ModifyUsername(string username)
        {
            var random = new Random();
            var numCharsToRemove = random.Next(4); // remove up to 3 characters from the end of the username
            var numCharsToAdd = random.Next(4); // add up to 3 characters to the end of the username
            var modifiedUsername = username;

            // Remove characters from the end of the username
            if (numCharsToRemove > 0)
            {
                modifiedUsername = modifiedUsername.Substring(0, modifiedUsername.Length - numCharsToRemove);
            }

            // Add characters to the end of the username
            if (numCharsToAdd > 0)
            {
                for (int i = 0; i < numCharsToAdd; i++)
                {
                    var charType = random.Next(3); // 0 = digit, 1 = lowercase letter, 2 = uppercase letter
                    if (charType == 0)
                    {
                        modifiedUsername += random.Next(10);
                    }
                    else if (charType == 1)
                    {
                        modifiedUsername += (char)('a' + random.Next(26));
                    }
                    else
                    {
                        modifiedUsername += (char)('A' + random.Next(26));
                    }
                }
            }

            return modifiedUsername;
        }

        public async void LikeProcess(string review_id, string cookie, bool use_proxy)
        {
            string proxy = null;
            if (use_proxy)
            {
                proxy = proxies[random.Next(proxies.Length)];
            }
            await Task.Run(() => ProcessParser($"https://edge.allegro.pl/product-reviews/{review_id}/vote/up", cookie, proxy));
            //new Thread(() => ProcessParser($"https://edge.allegro.pl/product-reviews/{review_id}/vote/up", cookie, proxy)).Start();
        }

        public void UnpaidOrderProcess(string product_id, string cookie, bool use_proxy, string delivery_method, bool apt)
        {
            new Thread(() => unpaid_backend.CreateUnpaidOrder(product_id, cookie, null, delivery_method, apt)).Start();
        }
        public void UnpaidOrderProcessSync(string product_id, string cookie, bool use_proxy, string delivery_method, bool apt)
        {
            unpaid_backend.CreateUnpaidOrder(product_id, cookie, null, delivery_method, apt);
        }

        public void NicknameChange(string userId, string cookie, bool oldMethod, string nickname = null)
        {
        using (var request = new HttpRequest())
        {
                request.AddHeader("Accept", "application/vnd.allegro.public.v1+json");
                request.AddHeader("Content-Type", "application/vnd.allegro.public.v1+json");
                request.AddHeader("Cookie", utility.ConvertCookieString(cookie));
                request.IgnoreProtocolErrors = true;
                string username = "";
                if (oldMethod)
                {
                    username = ModifyUsername(nickname);
                } else
                {
                    string[] words = File.ReadAllLines("./Data/nicknames.txt");
                    username = GenerateUsername(words.ToList());
                }
                var requestBody = new
            {
                accounts = new
                {
                    allegro = new
                    {
                        login = username
                    }
                }
            };
                    HttpResponse response = request.Put($"https://edge.allegro.pl/users/{userId}", JsonConvert.SerializeObject(requestBody), "application/vnd.allegro.public.v1+json");
                    if (((int)response.StatusCode) != 204) // LHS IS NOT EQUAL TO RHS AHAHHA
                    {
                        mprint.FPrint($"Error: {response.ToString()}");
                    } else {
                        mprint.SPrint($"Successfully changed username!");
                    }
        }
        }

    public void AddToBasketProcess(string productId, string cookie, bool use_proxy)
        {
            try
            {
                productId = scrapper2.GetProductID(productId);
                string proxies = null;
                if (use_proxy)
                {
                    string[] proxiesFile = File.ReadAllLines("./Data/proxies.txt");
                    proxies = proxiesFile[random.Next(proxiesFile.Length)];
                }
                using (var request = new HttpRequest())
                {
                    request.Proxy = proxies != null ? ProxyClient.Parse(proxies) : null;
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:108.0) Gecko/20100101 Firefox/108.0";
                    request.AddHeader("Accept", "application/vnd.allegro.public.v5+json");
                    request.AddHeader("Accept-Language", "en-US");
                    request.AddHeader("Accept-Encoding", "gzip, deflate, br");
                    request.AddHeader("Referer", "https://allegro.pl/");
                    request.AddHeader("Content-Type", "application/vnd.allegro.public.v5+json");
                    request.AddHeader("Origin", "https://allegro.pl");
                    request.AddHeader("Connection", "keep-alive");
                    request.AddHeader("Sec-Fetch-Dest", "empty");
                    request.AddHeader("Sec-Fetch-Mode", "cors");
                    request.AddHeader("Sec-Fetch-Site", "same-site");
                    request.AddHeader("Cookie", utility.ConvertCookieString(cookie));
                    request.AddHeader("TE", "trailers");
                    Dictionary<string, object> payload = new Dictionary<string, object>
        {
            {
                "items", new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object>
                    {
                        { "itemId", productId },
                        { "delta", 1 },
                        { "sellerId", "19304534" },
                        { "navCategoryId", "15988" },
                        { "navTree", "navigation-pl" }
                    }
                }
            }
        };

                    string jsonPayload = JsonConvert.SerializeObject(payload);
                    var resp = request.Post("https://edge.allegro.pl/carts/changeQuantityCommand", jsonPayload, "application/json");
                    if (resp.StatusCode == Leaf.xNet.HttpStatusCode.NoContent)
                    {
                        mprint.SPrint($"Successfully added offer to basket: {productId}");
                    }
                    else
                    {
                        mprint.FPrint($"Failed to add offer to basket: {productId}, {resp.ToString()}, {resp.StatusCode}");
                    }
                }
            } catch (Exception ex) {
                mprint.FPrint(ex.Message);
            }
        }
        public void ProcessParser(string review_url, string cookie, string proxy)
        {
            try
            {
                HttpRequest request = new HttpRequest();

                if (!string.IsNullOrEmpty(proxy))
                {
                    request.Proxy = ProxyClient.Parse(proxy);
                }

                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:105.0) Gecko/20100101 Firefox/105.0";
                request.AddHeader("Accept", "application/vnd.allegro.public.v1+json");
                request.AddHeader("Accept-Language", "en-US");
                request.AddHeader("Accept-Encoding", "gzip, deflate, br");
                request.AddHeader("Referer", "https://allegro.pl/");
                request.AddHeader("Content-Type", "application/vnd.allegro.public.v1+json");
                request.AddHeader("Origin", "https://allegro.pl");
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("Sec-Fetch-Dest", "empty");
                request.AddHeader("Sec-Fetch-Mode", "cors");
                request.AddHeader("Sec-Fetch-Site", "same-site");
                request.AddHeader("TE", "trailers");
                var generatedCookie = utility.ConvertCookieString(cookie);
                var guid = Guid.NewGuid().ToString();
                generatedCookie += $";_cmuid={guid};";
                request.AddHeader("Cookie", generatedCookie);
                HttpResponse response = request.Put(review_url);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = response.ToString();

                    if (responseBody.Contains("upvote"))
                    {
                        mprint.SPrint($"Success marked thumbs on {review_url}");
                    }
                    else
                    {
                        mprint.FPrint($"Marking on {review_url} failed");
                    }
                }
                request.Dispose();
            }
            catch (Exception ex)
            {
                mprint.FPrint(ex.Message);
            }
        }
        public void ViewProcess(string product_url, string cookie, bool use_proxy) 
        {
            string proxy = null;
            if (use_proxy)
            {
                proxy = proxies[random.Next(proxies.Length)];
            }
            var request = new HttpRequest();
            if (!string.IsNullOrEmpty(proxy))
            {
                request.Proxy = ProxyClient.Parse(proxy);
            }
            request.UserAgent = "Mozilla/5.0 (Linux; Android 11; Redmi Note 8 Pro) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/110.0.5481.192 Mobile Safari/537.36 OPR/74.0.3922.70977";
            request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
            request.AddHeader("accept-language", "en-US,en;q=0.9");
            request.AddHeader("cache-control", "max-age=0");
            request.AddHeader("sec-ch-device-memory", "8");
            request.AddHeader("sec-fetch-dest", "document");
            request.AddHeader("sec-fetch-mode", "navigate");
            request.AddHeader("sec-fetch-site", "same-origin");
            request.AddHeader("sec-fetch-user", "?1");
            request.AddHeader("upgrade-insecure-requests", "1");
            request.AddHeader("Referer", product_url);
            request.AddHeader("Referrer-Policy", "strict-origin-when-cross-origin");
            request.IgnoreProtocolErrors = true;
            var resp = request.Get(product_url);
            mprint.SPrint($"Added view on {product_url}.{resp.ToString()}");
            request.Dispose();    
        }
        public async void DownProcess(string review_id, string cookie, bool use_proxy)
        {
            string proxy = null;
            if (use_proxy)
            {
                string[] proxies = File.ReadAllLines("./Data/proxies.txt");
                proxy = proxies[random.Next(proxies.Length)];
            }
            await Task.Run(() => ProcessParser($"https://edge.allegro.pl/product-reviews/{review_id}/vote/down", cookie, proxy));
            //new Thread(() => ProcessParser($"https://edge.allegro.pl/product-reviews/{review_id}/vote/down", cookie, proxy)).Start();
        }
        public void FavouriteProcess(string product_url, string cookie, bool use_proxy)
        {
            var product_favourite_url = $"https://edge.allegro.pl/watched/offers/{scrapper2.GetProductID(product_url)}";
            string proxy = null;
            if (use_proxy)
            {
                proxy = proxies[random.Next(proxies.Length)];
            }
            try
            {
                using (HttpRequest request = new HttpRequest())
                {
                    if (proxy != null)
                    {
                        request.Proxy = ProxyClient.Parse(proxy);
                    }

                    request.IgnoreProtocolErrors = true;
                    request.ConnectTimeout = 60000;

                    request.AddHeader("Host", "edge.allegro.pl");
                    request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:105.0) Gecko/20100101 Firefox/105.0");
                    request.AddHeader("Accept", "application/vnd.allegro.public.v2+json");
                    request.AddHeader("Accept-Language", "en-US");
                    request.AddHeader("Accept-Encoding", "gzip, deflate, br");
                    request.AddHeader("Referer", "https://allegro.pl/");
                    request.AddHeader("Content-Type", "application/vnd.allegro.public.v2+json");
                    request.AddHeader("Cookie", utility.ConvertCookieString(cookie));
                    request.AddHeader("Origin", "https://allegro.pl");
                    request.AddHeader("Connection", "keep-alive");
                    request.AddHeader("Sec-Fetch-Dest", "empty");
                    request.AddHeader("Sec-Fetch-Mode", "cors");
                    request.AddHeader("Sec-Fetch-Site", "same-site");
                    request.AddHeader("TE", "trailers");

                    HttpResponse resp = request.Put(product_favourite_url);

                    if (resp.StatusCode == HttpStatusCode.NoContent || resp.StatusCode == HttpStatusCode.OK || resp.StatusCode == HttpStatusCode.Created)
                    {
                        mprint.SPrint($"Success added favourite on {product_favourite_url}");
                    }
                    else
                    {
                        mprint.FPrint($"Status: {(int)resp.StatusCode}, text: {resp.ToString()}");
                    }
                }
            }
            catch (Exception e)
            {
                mprint.FPrint(e.ToString());
            }

        }
    }
}
