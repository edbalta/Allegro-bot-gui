using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Xml.Linq;

namespace Allegro_bot_gui
{
    public class UnpaidModuleBackend
    {
        public string[] prefixes = File.ReadAllLines("./Data/phone_prefixes.txt");
        public Random random = new Random();
        public Mprint mprint = new Mprint();
        public Utils utility = new Utils();
        public ReviewScrapper scrapper = new ReviewScrapper();
        public Dictionary<string, bool> address_dct = JsonConvert.DeserializeObject<Dictionary<string, bool>>(File.ReadAllText("./BotData/address_dict.json"));
        public List<string> suspended_accounts = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("./logs/suspended.json"));
        public string addressesJson = File.ReadAllText("./Data/addresses.json");
        public Dictionary<int, dynamic> addressesDict = new Dictionary<int, dynamic>();
        public string[] femaleSurnames = File.ReadAllLines("./Data/female_surnames.txt");
        public string[] femaleNames = File.ReadAllLines("./Data/female_names.txt");
        public string[] maleNames = File.ReadAllLines("./Data/males_names.txt");
        public string[] maleSurnames = File.ReadAllLines("./Data/males_surnames.txt");

        public UnpaidModuleBackend()
        {
            LoadAddresses();
        }
        public void LoadAddresses()
        {
            dynamic addresses = JsonConvert.DeserializeObject(addressesJson);
            var addressList = addresses.addresses;
            for (int i = 0; i < addressList.Count; i++)
            {
                addressesDict.Add(i, addressList[i]);
            }

        }
        public string CreateUnpaidOrder(string product_url, string cookie, string proxies = null, string delivery_method = null, bool apt = false)
        {
            try
            {
                CookieModel cookie_json = JsonConvert.DeserializeObject<CookieModel>(cookie);
                //QeppoLogin2 qeppologin2 = JsonConvert.DeserializeObject<QeppoLogin2>(System.Web.HttpUtility.UrlDecode(cookie_json.qeppo_login2));
                string account_email = cookie_json.QXLSESSID;
                if (suspended_accounts.Contains(account_email))
                {
                    mprint.FPrint($"(0%) This account is suspended. Account: {account_email}");
                    return null;
                }
                string product_id = scrapper.GetProductID(product_url);
                mprint.SPrint($"(10%) Proccess started with Account: {account_email}, Product: {product_id}, Delivery method: {delivery_method}");
                string buy_now_response = BuyNow(product_id, cookie, proxies);
                if (buy_now_response != null)
                {
                    string purchase_id = buy_now_response.Split(new string[] { "https://allegro.pl/transakcja/" }, StringSplitOptions.None)[
                        1
                    ].Split(new string[] { "/zamowienie" }, StringSplitOptions.None)[0];

                    dynamic addy = GetAddress(apt);
                    string num = GetPhoneNumber();
                    string name = GetUsername();
                    if (!address_dct.ContainsKey(account_email))
                    {
                        AddAddress(addy, cookie, num, name);
                        address_dct[account_email] = true;
                    }
                    mprint.SPrint("(20%) Added address.");
                    var json_obj = GetDeliveryInfo(cookie, purchase_id);
                    if (json_obj != null)
                    {
                        json_obj.deliveries[0].delivery = GetDeliveryPoint(JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(json_obj.deliveries[0].options.deliveryMethods)), delivery_method);
                        json_obj.deliveries[0].delivery.address = json_obj.options.addresses[0];
                        if (delivery_method == "Kurier InPost pobranie" && json_obj.deliveries[0].delivery == null)
                        {
                            mprint.FPrint($"(0%) This product doesn't have Kurier InPost pobranie delivery method. Account: {account_email}");
                            return null;
                        }
                        if (delivery_method == "Paczkomaty InPost")
                        {
                            json_obj.deliveries[0].delivery.generalDelivery.pointDetails = GetGeneralDeliveryPoint(json_obj.deliveries[0].options.deliveryItems.items[0].items);
                            if(json_obj.deliveries[0].delivery.generalDelivery.pointDetails == null)
                            {
                                mprint.FPrint($"Account doesn't have Pickup Point nearby {account_email}");
                                suspended_accounts.Add(account_email);
                                File.WriteAllText("./logs/suspended_accounts.json", JsonConvert.SerializeObject(suspended_accounts));

                                return null;
                            }
                        }
                        else
                        {
                            json_obj.deliveries[0].delivery.generalDelivery = null;
                        }
                        string payment_id = SendPurchaseRequest(purchase_id, json_obj, cookie);

                        var buyerId = (string)json_obj.buyer.id;
                        if (payment_id != null)
                        {
                            mprint.SPrint($"(70%) Added delivery method. Account: {account_email}");
                            UpdateUser(buyerId, addy, name, num, cookie);
                            FinishRequests(payment_id, purchase_id, cookie, product_id, account_email);
                        }
                        else
                        {
                            mprint.FPrint($"(0%) Order couldn't be made because account is suspended! {account_email}");
                            suspended_accounts.Add(account_email);
                            File.WriteAllText("./logs/suspended_accounts.json", JsonConvert.SerializeObject(suspended_accounts));
                        }
                    }
                    else
                    {
                        mprint.FPrint($"(0%) Order couldn't be made because account is suspended! {account_email}");
                        suspended_accounts.Add(account_email);
                        File.WriteAllText("./logs/suspended_accounts.json", JsonConvert.SerializeObject(suspended_accounts));
                    }

                }
                else
                {
                    mprint.FPrint(buy_now_response);
                }
                return null;
            }
            catch (Exception ex)
            {
                mprint.FPrint(ex.ToString());
                return null;
            }
        }
        public void FinishRequests(string payment_id, string purchase_id, string cookie, string productId, string accountEmail)
        {
            using (var request = new HttpRequest())
            {
                request.AddHeader("Accept", "application/vnd.allegro.public.v14+json");
                request.AddHeader("Content-Type", "application/vnd.allegro.public.v14+json");
                request.AddHeader("Cookie", utility.ConvertCookieString(cookie));
                request.IgnoreProtocolErrors = true;
                request.Put($"https://edge.allegro.pl/payments/{payment_id}/methods", "{\"selectedDiscounts\":[],\"selectedMethods\":[{\"methodId\":\"" + new[] { "OCP-p", "ing-pis", "OCP-m", "santander-pis", "pekao-pis", "OCP-wm", "alior-pis", "OCP-bnx" }[random.Next(8)] + "\",\"params\":{}}]}", "application/vnd.allegro.public.v14+json");
                HttpRequest r = new HttpRequest();
                r.AddHeader("Accept", "application/vnd.allegro.public.v1+json");
                r.AddHeader("Content-Type", "application/vnd.allegro.public.v1+json");
                r.AddHeader("Cookie", utility.ConvertCookieString(cookie));
                var resp = r.Put($"https://edge.allegro.pl/purchases/{purchase_id}/buy-and-pay-commands/web", "{\"\":{}}", "application/vnd.allegro.public.v1+json");
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    var respJson = JsonConvert.DeserializeObject<dynamic>(resp.ToString());
                    if (respJson.paymentFinalizationStatus != null)
                    {
                        mprint.SPrint($"(100%) Success order created {productId} with account {accountEmail}");
                    }
                    else
                    {
                        mprint.FPrint($"(FAIL) Status: {resp.StatusCode}, text: {resp.ToString()} - Failure to create order");
                    }
                }
                else
                {
                    mprint.FPrint($"(FAIL) Status: {resp.StatusCode}, text: {resp.ToString()} - Failure to create order");
                }
                r.Dispose();
            }
        }

        public void UpdateUser(string accountId, dynamic addy, string name, string num, string cookie)
        {
            string json = @"{
    ""addresses"": {
        ""residence"": {
            ""addressLine"": """ + addy["street"] + " " + addy["houseNumber"] + @""",
            ""city"": """ + addy["city"] + @""",
            ""countryCode"": ""PL"",
            ""zipCode"": """ + addy["zipCode"] + @"""
        }
    },
    ""contacts"": {
        ""phone"": {
            ""fullNumber"": ""+48" + num + @"""
        }
    },
    ""person"": {
        ""firstName"": """ + name.Split(':')[0] + @""",
        ""lastName"": """ + name.Split(':')[1] + @""",
        ""nationality"": ""PL""
    }
}";

            using (var request = new HttpRequest())
            {
                request.AddHeader("Accept", "application/vnd.allegro.public.v1+json");
                request.AddHeader("Content-Type", "application/vnd.allegro.public.v1+json");
                request.AddHeader("Cookie", utility.ConvertCookieString(cookie));
                request.IgnoreProtocolErrors = true;
                try
                {
                    var response = request.Put($"https://edge.allegro.pl/users/{accountId}", json, "application/vnd.allegro.public.v1+json");

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine("User profile updated successfully");
                    }
                }
                catch (HttpException ex)
                {
                    Console.WriteLine($"422 Unprocessable Entity error: {ex.Message}");
                    Console.WriteLine($"Response body: {ex.ToString()}");
                }
            }
        }
        public dynamic SendPurchaseRequest(string purchase_id, dynamic json_obj, string cookie)
        {
            using (var client = new HttpRequest())
            {
                client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:108.0) Gecko/20100101 Firefox/108.0";
                client.AddHeader("Accept", "application/vnd.allegro.public.v3+json");
                client.AddHeader("Accept-Language", "en-US");
                client.AddHeader("Accept-Encoding", "gzip, deflate, br");
                client.AddHeader("Referer", "https://allegro.pl/");
                client.AddHeader("Content-Type", "application/vnd.allegro.public.v3+json");
                client.AddHeader("Origin", "https://allegro.pl");
                client.AddHeader("Connection", "keep-alive");
                client.AddHeader("Sec-Fetch-Dest", "empty");
                client.AddHeader("Cookie", utility.ConvertCookieString(cookie));
                client.AddHeader("Sec-Fetch-Mode", "cors");
                client.AddHeader("Sec-Fetch-Site", "same-site");
                client.AddHeader("TE", "trailers");
                client.IgnoreProtocolErrors = true;
                var response = client.Put(
                    $"https://edge.allegro.pl/purchases/{purchase_id}",
                    json_obj.ToString(), "application/vnd.allegro.public.v3+json");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic data = JsonConvert.DeserializeObject(response.ToString());
                    if (data.payment != null && data.payment.id != null)
                    {
                        return data.payment.id;
                    }
                }
                return null;
            }
        }
        public dynamic GetGeneralDeliveryPoint(dynamic itemsList)
        {
            foreach (dynamic x in itemsList)
            {
                if (x["deliveryOption"]["name"] == "Paczkomat InPost")
                {
                    return x["deliveryOption"]["pickupPoint"];
                }
            }
            return null;
        }

        public dynamic GetDeliveryPoint(List<dynamic> deliveryList, string deliveryName)
        {
            if (deliveryName == "Paczkomaty InPost")
            {
                foreach (dynamic x in deliveryList)
                {
                    if (x["deliveryMethod"]["name"] == "Allegro Paczkomaty InPost")
                    {
                        return x;
                    }
                }
            }
            else if (deliveryName == "DPD")
            {
                foreach (dynamic x in deliveryList)
                {
                    if (x["deliveryMethod"]["name"] == "Courier delivery")
                    {
                        return x;
                    }
                }
            }
            else if (deliveryName == "Kurier InPost pobranie")
            {
                foreach (dynamic x in deliveryList)
                {
                    if (x["deliveryMethod"]["name"] == "Allegro miniKurier24 InPost pobranie")
                    {
                        return x;
                    }

                }
            }
            return null;
        }

        public string GetPhoneNumber()
        {
            string num = $"{prefixes[random.Next(prefixes.Length)]}{new string(Enumerable.Range(0, 6).Select(x => (char)('0' + random.Next(10))).ToArray())}";
            return num;
        }
        public dynamic GetDeliveryInfo(string cookie, string purchase_id)
        {
            HttpRequest request = new HttpRequest();
            request.AddHeader("Host", "edge.allegro.pl");
            request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:108.0) Gecko/20100101 Firefox/108.0");
            request.AddHeader("Accept", "application/vnd.allegro.public.v3+json");
            request.AddHeader("Accept-Language", "en-US");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Referer", "https://allegro.pl/");
            request.AddHeader("Origin", "https://allegro.pl");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Cookie", utility.ConvertCookieString(cookie));
            request.AddHeader("Sec-Fetch-Dest", "empty");
            request.AddHeader("Sec-Fetch-Mode", "cors");
            request.AddHeader("Sec-Fetch-Site", "same-site");
            request.AddHeader("TE", "trailers");
            request.IgnoreProtocolErrors = true;
            HttpResponse resp1 = request.Get($"https://edge.allegro.pl/purchases/{purchase_id}?allDeliveryOptions=true");
            dynamic json_obj = JsonConvert.DeserializeObject(resp1.ToString());
            request.Dispose();
            if (json_obj.deliveries == null)
                return null;
            return json_obj;
        }


        public dynamic GetAddress(bool apt)
        {
            var random = new Random();
            var randomKey = addressesDict.Keys.ElementAt(random.Next(addressesDict.Count));
            var dict_to_return = addressesDict[randomKey];
            if (apt)
            {
                dict_to_return["houseNumber"] = $"{dict_to_return["houseNumber"]}/{random.Next(0, 50)}";
            }
            return dict_to_return;
        }
        public string GetUsername()
        {

            if (new[] { "female", "male" }.OrderBy(x => Guid.NewGuid()).FirstOrDefault() == "female")
            {
                string username = $"{femaleNames.OrderBy(x => Guid.NewGuid()).FirstOrDefault()}:{femaleSurnames.OrderBy(x => Guid.NewGuid()).FirstOrDefault()}";
                return username;
            }
            else
            {
                string username = $"{maleNames.OrderBy(x => Guid.NewGuid()).FirstOrDefault()}:{maleSurnames.OrderBy(x => Guid.NewGuid()).FirstOrDefault()}";
                return username;
            }
        }

        public bool AddAddress(dynamic addy, string cookie, string num, string name, string proxies = null)
        {
            using (var client = new HttpRequest())
            {
                client.IgnoreProtocolErrors = true;
                string json = @"{
                ""countryCode"": ""PL"",
                ""phoneNumberWithPrefix"": {
                    ""prefix"": ""48"",
                    ""number"": """ + num + @"""
                },
                ""addressLine"": """ + addy.street + " " + addy.houseNumber + @""",
                ""zipCode"": """ + addy.zipCode + @""",
                ""city"": """ + addy.city + @""",
                ""firstName"": """ + name.Split(':')[0] + @""",
                ""lastName"": """ + name.Split(':')[1] + @""",
                ""addressType"": ""Shipping""
            }";

                client.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:105.0) Gecko/20100101 Firefox/105.0");
                client.AddHeader("Accept", "application/vnd.allegro.internal.v1+json");
                client.AddHeader("Accept-Language", "en-US");
                client.AddHeader("Accept-Encoding", "gzip, deflate, br");
                client.AddHeader("Referer", "https://allegro.pl/");
                client.AddHeader("Content-Type", "application/vnd.allegro.internal.v1+json");
                client.AddHeader("Origin", "https://allegro.pl");
                client.AddHeader("Connection", "keep-alive");
                client.AddHeader("Cookie", utility.ConvertCookieString(cookie));
                client.AddHeader("Sec-Fetch-Dest", "empty");
                client.AddHeader("Sec-Fetch-Mode", "cors");
                client.AddHeader("Sec-Fetch-Site", "same-site");
                client.AddHeader("TE", "trailers");
                HttpResponse resp = client.Post("https://edge.allegro.pl/shipping-addresses", json, "application/json");

                if (resp.StatusCode == Leaf.xNet.HttpStatusCode.Created || resp.StatusCode == Leaf.xNet.HttpStatusCode.OK || resp.StatusCode == Leaf.xNet.HttpStatusCode.NoContent || resp.StatusCode == Leaf.xNet.HttpStatusCode.NotModified)
                {
                    return true;
                }
                else
                {
                    mprint.FPrint($"(FAIL) Status: {resp.StatusCode}, text: {resp.ToString()} - Failure to new add address");
                    return false;
                }

            }
        }
        public string BuyNow(string productId, string cookie, string proxies = null)
        {
            using (var request = new HttpRequest())
            {
                request.IgnoreProtocolErrors = true;
                request.AddHeader("Host", "edge.allegro.pl");
                request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.6045.159 Safari/537.36");
                request.AddHeader("Accept", "application/vnd.allegro.internal.v1+json");
                request.AddHeader("Accept-Language", "en-US");
                request.AddHeader("Accept-Encoding", "gzip, deflate, br");
                request.AddHeader("Referer", "https://allegro.pl/");
                request.AddHeader("Content-Type", "application/vnd.allegro.internal.v1+json");
                request.AddHeader("Origin", "https://allegro.pl");
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("Sec-Fetch-Dest", "empty");
                request.AddHeader("Sec-Fetch-Mode", "cors");
                request.AddHeader("Sec-Fetch-Site", "same-site");
                request.AddHeader("Cookie", utility.ConvertCookieString(cookie));

                var json = new
                {
                    offer = new
                    {
                        id = productId,
                        quantity = 1,

                        navigationCategory = new
                        {
                            id = "321347",
                            treeName = "navigation-pl"

                        }
                    }
                };
                string jsonString = JsonConvert.SerializeObject(json);

                HttpResponse response = request.Post("https://edge.allegro.pl/transaction-entry/buy-now", jsonString, "application/vnd.allegro.internal.v1+json");

                if (response.StatusCode == HttpStatusCode.NoContent ||
                    response.StatusCode == HttpStatusCode.OK ||
                    response.StatusCode == HttpStatusCode.Created)
                {
                    return response.ToString();
                }
                else
                {
                    mprint.FPrint(response.ToString());
                    return null;
                }
            }
        }
    }
}
