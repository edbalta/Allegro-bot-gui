using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Threading;
using System.Diagnostics;

namespace Allegro_bot_gui
{
    public partial class UnpaidOrders : Form
    {
        public List<Thread> threads = new List<Thread>();
        public Dictionary<string, string> products_ids = new Dictionary<string, string>();
        public Dictionary<string, List<string>> account_offers_done = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(File.ReadAllText("./BotData/accounts_offers.json"));
        public DeliverySelectorModel model_delivery = JsonConvert.DeserializeObject<DeliverySelectorModel>(File.ReadAllText("./BotData/accounts_unpaid_delivery.json"));
        public DeliverySelectorModel model = JsonConvert.DeserializeObject<DeliverySelectorModel>(File.ReadAllText("./BotData/accounts_unpaid_delivery.json"));
        public Backend back = new Backend();
        public int dpd_accounts = 0;
        public int kurierProbaineAccounts = 0;
        public int InPostAccounts = 0;
        public UnpaidOrders()
        {
            InitializeComponent();
        }
        public string GetDeliveryMethod(DeliverySelectorModel model)
        {
            var delivery_str = new[] { "DPD", "Paczkomaty InPost", "Kurier InPost pobranie" }[new Random().Next(3)];
            if (delivery_str == "DPD")
            {
                if (model.dpd_accounts == dpd_accounts)
                {
                    return GetDeliveryMethod(model);
                }
                dpd_accounts++;
                return "DPD";
            }
            else if (delivery_str == "Kurier InPost pobranie")
            {
                if (model.inpost_probain_accounts == kurierProbaineAccounts)
                {
                    return GetDeliveryMethod(model);
                }
                kurierProbaineAccounts++;
                return "Kurier InPost pobranie";
            }
            else if (delivery_str == "Paczkomaty InPost")
            {
                if (model.inpost_accounts == InPostAccounts)
                {
                    return GetDeliveryMethod(model);
                }
                InPostAccounts++;
                return "Paczkomaty InPost";
            }
            return "DPD";

        }
        public string GetProductID(string url)
        {
            return new ReviewScrapper().GetProductID(url);
        }
        public int GetIntervalValue()
        {
            IntervalsMenu menu = JsonConvert.DeserializeObject<IntervalsMenu>(File.ReadAllText("./BotData/interval.json"));
            Random random = new Random();
            return random.Next(menu.minimum_time, menu.maximum_time);
        }
        public void ProcessStart(string product_url, CookieModel[] cookies)
        {
            //var result = new BrowserFetcher().DownloadAsync().Result;
            var product_id = GetProductID(product_url);
            SettingsUnpaidOrders settings = JsonConvert.DeserializeObject<SettingsUnpaidOrders>(File.ReadAllText("./BotData/settings_unpaid.json"));
            var n = 0;
            if (settings.delivery_method == "Use ALL")
            {
                foreach (var cookie in cookies)
                {
                    Console.Title = $"Account index: {n}";
                    ++n;
                    var delivery_method = GetDeliveryMethod(model);
                    var account_email = cookie.QXLSESSID;
                    //var account_email = cookie.ToString JsonConvert.DeserializeObject<QeppoLogin2>(System.Web.HttpUtility.UrlDecode(cookie.qeppo_login2)).username;
                    if (use_intervals.Checked)
                    {
                        if (rand_intervals.Checked)
                        {
                            if (settings.dont_make_order)
                            {
                                Thread.Sleep(GetIntervalValue());

                                if (settings.add_views)
                                {
                                    back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                if (settings.add_to_basket)
                                {
                                    back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                if (settings.favorite_offer)
                                {
                                    back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                            }
                            else
                            {
                                Thread.Sleep(GetIntervalValue());

                                if (settings.add_views)
                                {
                                    back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                if (settings.add_to_basket)
                                {
                                    back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                if (settings.favorite_offer)
                                {
                                    back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                back.UnpaidOrderProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked, delivery_method, apt.Checked);
                                if (account_offers_done.ContainsKey(account_email))
                                {
                                    if (account_offers_done[account_email].Contains(product_id))
                                    {

                                    }
                                    else
                                    {
                                        account_offers_done[account_email].Add(product_id);
                                    }
                                }
                                else
                                {
                                    account_offers_done[account_email] = new List<string> { product_id };

                                }
                            }
                        }
                        else
                        {
                            if (settings.dont_make_order)
                            {
                                Thread.Sleep(Int32.Parse(interval_time.Text));

                                if (settings.add_views)
                                {
                                    back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                if (settings.add_to_basket)
                                {
                                    back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                if (settings.favorite_offer)
                                {
                                    back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }

                            }
                            else
                            {
                                Thread.Sleep(Int32.Parse(interval_time.Text));

                                if (settings.add_views)
                                {
                                    back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                if (settings.add_to_basket)
                                {
                                    back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                if (settings.favorite_offer)
                                {
                                    back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                back.UnpaidOrderProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked, delivery_method, apt.Checked);
                                if (account_offers_done.ContainsKey(account_email))
                                {
                                    if (account_offers_done[account_email].Contains(product_id))
                                    {

                                    }
                                    else
                                    {
                                        account_offers_done[account_email].Add(product_id);
                                    }
                                }
                                else
                                {
                                    account_offers_done[account_email] = new List<string> { product_id };
                                }
                            }
                        }
                    }
                    else
                    {
                        if (settings.dont_make_order)
                        {

                            if (settings.add_views)
                            {
                                var trd2 = new Thread(() => back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                                trd2.Start();
                                threads.Add(trd2);
                            }
                            if (settings.add_to_basket)
                            {
                                var trd2 = new Thread(() => back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                                trd2.Start();
                                threads.Add(trd2);
                            }
                            if (settings.favorite_offer)
                            {
                                var trd1 = new Thread(() => back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                                trd1.Start();
                                threads.Add(trd1);
                            }
                        }
                        else
                        {

                            if (settings.add_views)
                            {
                                var trd2 = new Thread(() => back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                                trd2.Start();
                                threads.Add(trd2);
                            }
                            if (settings.add_to_basket)
                            {
                                var trd2 = new Thread(() => back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                                trd2.Start();
                                threads.Add(trd2);
                            }
                            if (settings.favorite_offer)
                            {
                                var trd1 = new Thread(() => back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                                trd1.Start();
                                threads.Add(trd1);
                            }
                            var trd = new Thread(() => back.UnpaidOrderProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked, delivery_method, apt.Checked));
                            //back.UnpaidOrderProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked, delivery_method, apt.Checked);
                            trd.Start();
                            threads.Add(trd);
                            if (account_offers_done.ContainsKey(account_email))
                            {
                                if (account_offers_done[account_email].Contains(product_id))
                                {

                                }
                                else
                                {
                                    account_offers_done[account_email].Add(product_id);
                                }
                            }
                            else
                            {
                                account_offers_done[account_email] = new List<string> { product_id };

                            }
                        }

                    }
                }
            }
            else
            {
                foreach (var cookie in cookies)
                {
                    Console.Title = $"Account index: {n}";
                    ++n;
                    var account_email = cookie.QXLSESSID;
                    if (use_intervals.Checked)
                    {
                        if (rand_intervals.Checked)
                        {
                            if (settings.dont_make_order)
                            {
                                Thread.Sleep(GetIntervalValue());

                                if (settings.add_views)
                                {
                                    back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                if (settings.add_to_basket)
                                {
                                    back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                if (settings.favorite_offer)
                                {
                                    back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                            }
                            else
                            {
                                Thread.Sleep(GetIntervalValue());

                                if (settings.add_views)
                                {
                                    back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                if (settings.add_to_basket)
                                {
                                    back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                if (settings.favorite_offer)
                                {
                                    back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                back.UnpaidOrderProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked, settings.delivery_method, apt.Checked);
                                if (account_offers_done.ContainsKey(account_email))
                                {
                                    if (account_offers_done[account_email].Contains(product_id))
                                    {

                                    }
                                    else
                                    {
                                        account_offers_done[account_email].Add(product_id);
                                    }
                                }
                                else
                                {
                                    account_offers_done[account_email] = new List<string> { product_id };

                                }
                            }
                        }
                        else
                        {
                            if (settings.dont_make_order)
                            {
                                Thread.Sleep(Int32.Parse(interval_time.Text));

                                if (settings.add_views)
                                {
                                    back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                if (settings.add_to_basket)
                                {
                                    back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                if (settings.favorite_offer)
                                {
                                    back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }

                            }
                            else
                            {
                                Thread.Sleep(Int32.Parse(interval_time.Text));

                                if (settings.add_views)
                                {
                                    back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                if (settings.add_to_basket)
                                {
                                    back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                if (settings.favorite_offer)
                                {
                                    back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                }
                                back.UnpaidOrderProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked, settings.delivery_method, apt.Checked);
                                if (account_offers_done.ContainsKey(account_email))
                                {
                                    if (account_offers_done[account_email].Contains(product_id))
                                    {

                                    }
                                    else
                                    {
                                        account_offers_done[account_email].Add(product_id);
                                    }
                                }
                                else
                                {
                                    account_offers_done[account_email] = new List<string> { product_id };
                                }
                            }
                        }
                    }
                    else
                    {
                        if (settings.dont_make_order)
                        {

                            if (settings.add_views)
                            {
                                //back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                                var trd2 = new Thread(() => back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                                trd2.Start();
                                threads.Add(trd2);
                            }
                            if (settings.add_to_basket)
                            {
                                var trd2 = new Thread(() => back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                                trd2.Start();
                                threads.Add(trd2);
                            }
                            if (settings.favorite_offer)
                            {
                                var trd1 = new Thread(() => back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                                trd1.Start();
                                threads.Add(trd1);
                            }
                        }
                        else
                        {

                            if (settings.add_views)
                            {
                                var trd2 = new Thread(() => back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                                trd2.Start();
                                threads.Add(trd2);
                            }
                            if (settings.add_to_basket)
                            {
                                var trd2 = new Thread(() => back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                                trd2.Start();
                                threads.Add(trd2);
                            }
                            if (settings.favorite_offer)
                            {
                                var trd1 = new Thread(() => back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                                trd1.Start();
                                threads.Add(trd1);
                            }
                            var trd = new Thread(() => back.UnpaidOrderProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked, settings.delivery_method, apt.Checked));
                            //back.UnpaidOrderProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked, settings.delivery_method, apt.Checked);
                            trd.Start();
                            threads.Add(trd);
                            if (account_offers_done.ContainsKey(account_email))
                            {
                                if (account_offers_done[account_email].Contains(product_id))
                                {

                                }
                                else
                                {
                                    account_offers_done[account_email].Add(product_id);
                                }
                            }
                            else
                            {
                                account_offers_done[account_email] = new List<string> { product_id };
                            }
                        }
                    }
                }
            }
            new ToastContentBuilder()
            .AddText("Task Completed")
            .AddText("Your unpaid orders task has been completed!")
            .Show();
            File.WriteAllText("./BotData/accounts_offers.json", JsonConvert.SerializeObject(account_offers_done));
        }


        public async Task ProcessHandler(CookieModel[] cookies)
        {
            string[] products = File.ReadAllLines("./Data/products_unpaid.txt");
            foreach (var product in products)
            {
                ProcessStart(product, cookies);
            }
        }
        public void OfferRotationProcess(string product_url, CookieModel cookie)
        {
            SettingsUnpaidOrders settings = JsonConvert.DeserializeObject<SettingsUnpaidOrders>(File.ReadAllText("./BotData/settings_unpaid.json"));
            DeliverySelectorModel model = JsonConvert.DeserializeObject<DeliverySelectorModel>(File.ReadAllText("./BotData/accounts_unpaid_delivery.json"));
            //var account_email = JsonConvert.DeserializeObject<QeppoLogin2>(System.Web.HttpUtility.UrlDecode(cookie.qeppo_login2)).username;
            var account_email = cookie.QXLSESSID;
            var product_id = GetProductID(product_url);
            if (settings.delivery_method == "Use ALL")
            {
                var delivery_method = GetDeliveryMethod(model);
                if (use_intervals.Checked)
                {
                    if (rand_intervals.Checked)
                    {
                        if (settings.dont_make_order)
                        {
                            Thread.Sleep(GetIntervalValue());

                            if (settings.add_views)
                            {
                                back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            if (settings.add_to_basket)
                            {
                                back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            if (settings.favorite_offer)
                            {
                                back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                        }
                        else
                        {
                            Thread.Sleep(GetIntervalValue());

                            if (settings.add_views)
                            {
                                back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            if (settings.add_to_basket)
                            {
                                back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            if (settings.favorite_offer)
                            {
                                back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            back.UnpaidOrderProcessSync(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked, delivery_method, apt.Checked);
                            if (account_offers_done.ContainsKey(account_email))
                            {
                                if (account_offers_done[account_email].Contains(product_id))
                                {

                                }
                                else
                                {
                                    account_offers_done[account_email].Add(product_id);
                                }
                            }
                            else
                            {
                                account_offers_done[account_email] = new List<string> { product_id };

                            }
                        }
                    }
                    else
                    {
                        if (settings.dont_make_order)
                        {
                            Thread.Sleep(Int32.Parse(interval_time.Text));

                            if (settings.add_views)
                            {
                                back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            if (settings.add_to_basket)
                            {
                                back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            if (settings.favorite_offer)
                            {
                                back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }

                        }
                        else
                        {
                            Thread.Sleep(Int32.Parse(interval_time.Text));

                            if (settings.add_views)
                            {
                                back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            if (settings.add_to_basket)
                            {
                                back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            if (settings.favorite_offer)
                            {
                                back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            back.UnpaidOrderProcessSync(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked, delivery_method, apt.Checked);
                            if (account_offers_done.ContainsKey(account_email))
                            {
                                if (account_offers_done[account_email].Contains(product_id))
                                {

                                }
                                else
                                {
                                    account_offers_done[account_email].Add(product_id);
                                }
                            }
                            else
                            {
                                account_offers_done[account_email] = new List<string> { product_id };
                            }
                        }
                    }
                }
                else
                {
                    if (settings.dont_make_order)
                    {

                        if (settings.add_views)
                        {
                            var trd2 = new Thread(() => back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                            trd2.Start();
                            threads.Add(trd2);
                        }
                        if (settings.add_to_basket)
                        {
                            var trd2 = new Thread(() => back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                            trd2.Start();
                            threads.Add(trd2);
                        }
                        if (settings.favorite_offer)
                        {
                            var trd1 = new Thread(() => back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                            trd1.Start();
                            threads.Add(trd1);
                        }
                    }
                    else
                    {

                        if (settings.add_views)
                        {
                            var trd2 = new Thread(() => back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                            trd2.Start();
                            threads.Add(trd2);
                        }
                        if (settings.add_to_basket)
                        {
                            var trd2 = new Thread(() => back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                            trd2.Start();
                            threads.Add(trd2);
                        }
                        if (settings.favorite_offer)
                        {
                            var trd1 = new Thread(() => back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                            trd1.Start();
                            threads.Add(trd1);
                        }
                        back.UnpaidOrderProcessSync(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked, delivery_method, apt.Checked);
                        if (account_offers_done.ContainsKey(account_email))
                        {
                            if (account_offers_done[account_email].Contains(product_id))
                            {

                            }
                            else
                            {
                                account_offers_done[account_email].Add(product_id);
                            }
                        }
                        else
                        {
                            account_offers_done[account_email] = new List<string> { product_id };
                        }
                    }
                }
            }
            else
            {
                if (use_intervals.Checked)
                {
                    if (rand_intervals.Checked)
                    {
                        if (settings.dont_make_order)
                        {
                            Thread.Sleep(GetIntervalValue());

                            if (settings.add_views)
                            {
                                back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            if (settings.add_to_basket)
                            {
                                back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            if (settings.favorite_offer)
                            {
                                back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                        }
                        else
                        {
                            Thread.Sleep(GetIntervalValue());

                            if (settings.add_views)
                            {
                                back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            if (settings.add_to_basket)
                            {
                                back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            if (settings.favorite_offer)
                            {
                                back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            back.UnpaidOrderProcessSync(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked, settings.delivery_method, apt.Checked);
                            if (account_offers_done.ContainsKey(account_email))
                            {
                                if (account_offers_done[account_email].Contains(product_id))
                                {

                                }
                                else
                                {
                                    account_offers_done[account_email].Add(product_id);
                                }
                            }
                            else
                            {
                                account_offers_done[account_email] = new List<string> { product_id };

                            }
                        }
                    }
                    else
                    {
                        if (settings.dont_make_order)
                        {
                            Thread.Sleep(Int32.Parse(interval_time.Text));

                            if (settings.add_views)
                            {
                                back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            if (settings.add_to_basket)
                            {
                                back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            if (settings.favorite_offer)
                            {
                                back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }

                        }
                        else
                        {
                            Thread.Sleep(Int32.Parse(interval_time.Text));

                            if (settings.add_views)
                            {
                                back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            if (settings.add_to_basket)
                            {
                                back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            if (settings.favorite_offer)
                            {
                                back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked);
                            }
                            back.UnpaidOrderProcessSync(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked, settings.delivery_method, apt.Checked);
                            if (account_offers_done.ContainsKey(account_email))
                            {
                                if (account_offers_done[account_email].Contains(product_id))
                                {

                                }
                                else
                                {
                                    account_offers_done[account_email].Add(product_id);
                                }
                            }
                            else
                            {
                                account_offers_done[account_email] = new List<string> { product_id };
                            }
                        }
                    }
                }
                else
                {
                    if (settings.dont_make_order)
                    {

                        if (settings.add_views)
                        {
                            var trd2 = new Thread(() => back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                            trd2.Start();
                            threads.Add(trd2);
                        }
                        if (settings.add_to_basket)
                        {
                            var trd2 = new Thread(() => back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                            trd2.Start();
                            threads.Add(trd2);
                        }
                        if (settings.favorite_offer)
                        {
                            var trd1 = new Thread(() => back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                            trd1.Start();
                            threads.Add(trd1);
                        }
                    }
                    else
                    {

                        if (settings.add_views)
                        {
                            var trd2 = new Thread(() => back.ViewProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                            trd2.Start();
                            threads.Add(trd2);
                        }
                        if (settings.add_to_basket)
                        {
                            var trd2 = new Thread(() => back.AddToBasketProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                            trd2.Start();
                            threads.Add(trd2);
                        }
                        if (settings.favorite_offer)
                        {
                            var trd1 = new Thread(() => back.FavouriteProcess(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked));
                            trd1.Start();
                            threads.Add(trd1);
                        }
                        back.UnpaidOrderProcessSync(product_url, JsonConvert.SerializeObject(cookie), use_proxies.Checked, settings.delivery_method, apt.Checked);
                        if (account_offers_done.ContainsKey(account_email))
                        {
                            if (account_offers_done[account_email].Contains(product_id))
                            {

                            }
                            else
                            {
                                account_offers_done[account_email].Add(product_id);
                            }
                        }
                        else
                        {
                            account_offers_done[account_email] = new List<string> { product_id };
                        }
                    }
                }
            }
        }
        public List<CookieModel> Get_accounts_account_rotation()
        {

            var accounts = new List<CookieModel>();
            CookieModel[] cookies = JsonConvert.DeserializeObject<List<CookieModel>>(File.ReadAllText("./BotData/accounts_unpaid.json")).ToArray();
            foreach (var cookie in cookies)
            {
                var email = JsonConvert.DeserializeObject<QeppoLogin2>(System.Web.HttpUtility.UrlDecode(cookie.qeppo_login2));
                if (account_offers_done.ContainsKey(email.username))
                {
                    if (account_offers_done[email.username].Count == 0)
                    {
                        accounts.Add(cookie);
                    }
                }
                else
                {

                    accounts.Add(cookie);
                }
            }
            return accounts;
        }
        public void ChangeUsernames()
        {
            SettingsUnpaidOrders settings = JsonConvert.DeserializeObject<SettingsUnpaidOrders>(File.ReadAllText("./BotData/settings_unpaid.json"));
            CookieModel[] cookies = JsonConvert.DeserializeObject<List<CookieModel>>(File.ReadAllText("./BotData/accounts_unpaid.json")).ToArray();
            Dictionary<string, int> usernames_limit = JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText("./BotData/accounts_username_limit.json"));

            var i = 0;
            foreach (var cookie in cookies)
            {
                if (i == usernames_limit["accounts_limit"])
                {
                    break;
                }
                QeppoLogin2 qeppologin2 = JsonConvert.DeserializeObject<QeppoLogin2>(System.Web.HttpUtility.UrlDecode(cookie.qeppo_login2));
                if (settings.change_nicknames_old_method)
                {
                    back.NicknameChange(qeppologin2.id.ToString(), JsonConvert.SerializeObject(cookie), true, qeppologin2.username.Split(new string[] { "@" }, StringSplitOptions.None)[0]);
                }
                else
                {
                    back.NicknameChange(qeppologin2.id.ToString(), JsonConvert.SerializeObject(cookie), false);
                }


            }
        }
        public async Task OfferRotationHandler(CookieModel[] cookies)
        {
            string[] products = File.ReadAllLines("./Data/products_unpaid.txt");
            var n = 0;
            foreach (var cookie in cookies)
            {
                Console.Title = $"Account index: {n}";
                ++n;
                if (cookie.qeppo_login2 != null)
                {
                    foreach (var product in products)
                    {
                        OfferRotationProcess(product, cookie);
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SettingsUnpaidOrders settings = JsonConvert.DeserializeObject<SettingsUnpaidOrders>(File.ReadAllText("./BotData/settings_unpaid.json"));
            if (settings.change_nicknames)
            {
                new Thread(() =>
                {
                    ChangeUsernames();
                }).Start();
            }
            if (offer_rotation.Checked == false)
            {
                if (account_rotation_checkbox.Checked)
                {
                    new Thread(() => ProcessHandler(Get_accounts_account_rotation().ToArray())).Start();
                }
                else
                {
                    CookieModel[] cookies = JsonConvert.DeserializeObject<List<CookieModel>>(File.ReadAllText("./BotData/accounts_unpaid.json")).ToArray();
                    //ProcessHandler(cookies);
                    new Thread(() => ProcessHandler(cookies)).Start();
                }
            }
            else
            {
                if (account_rotation_checkbox.Checked)
                {
                    var accounts = Get_accounts_account_rotation().ToArray();
                    new Thread(() => OfferRotationHandler(accounts)).Start();
                }
                else
                {
                    CookieModel[] cookies = JsonConvert.DeserializeObject<List<CookieModel>>(File.ReadAllText("./BotData/accounts_unpaid.json")).ToArray();
                    new Thread(() => OfferRotationHandler(cookies)).Start();
                }
            }
        }

        private void rand_intervals_CheckedChanged(object sender, EventArgs e)
        {
            if (use_intervals.Checked)
            {
                if (rand_intervals.Checked)
                {
                    var frm = new IntervalMenuForm();
                    frm.Show();
                }
                else
                {
                    {
                        if (rand_intervals.Checked)
                        {
                            rand_intervals.Checked = false;
                            MessageBox.Show("Please check the use 'use intervals' checkbox first to use random intervals!");
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new SettingsForm().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", "./Data/products_unpaid.txt");
        }

        private void UnpaidOrders_Load(object sender, EventArgs e)
        {

        }
    }
    public class RequestResponse
    {
        public string product_id;
    }
    public class AccountFilterationSystem
    {
        CookieModel[] accounts;
        int paczkomatyAccounts;
        int dpdAccounts;
        int kurierPobranieAccounts;
        List<CookieModel> usedAccounts;
        public AccountFilterationSystem(CookieModel[] accounts)
        {
            this.accounts = accounts;
            this.usedAccounts = new List<CookieModel>();
        }

        public void SetPaczkomatyAccounts(int numAccounts)
        {
            if (numAccounts > accounts.Length)
            {
                Console.WriteLine("Error: Not enough accounts available.");
            }
            else
            {
                paczkomatyAccounts = numAccounts;
                Console.WriteLine(numAccounts + " accounts set for Paczkomaty.");
            }
        }

        public void SetDPDAccounts(int numAccounts)
        {
            if (numAccounts > accounts.Length)
            {
                Console.WriteLine("Error: Not enough accounts available.");
            }
            else
            {
                dpdAccounts = numAccounts;
                Console.WriteLine(numAccounts + " accounts set for DPD.");
            }
        }

        public void SetKurierPobranieAccounts(int numAccounts)
        {
            if (numAccounts > accounts.Length)
            {
                Console.WriteLine("Error: Not enough accounts available.");
            }
            else
            {
                kurierPobranieAccounts = numAccounts;
                Console.WriteLine(numAccounts + " accounts set for Kurier Pobranie.");
            }
        }

        public CookieModel[] GetAccounts(string method)
        {
            List<CookieModel> uniqueAccounts = accounts.Except(usedAccounts).ToList();

            switch (method)
            {
                case "Paczkomaty":
                    if (paczkomatyAccounts > uniqueAccounts.Count)
                    {
                        Console.WriteLine("Error: Not enough unique accounts available.");
                        return new CookieModel[0];
                    }
                    List<CookieModel> paczkomatyAccountsToUse = uniqueAccounts.Take(paczkomatyAccounts).ToList();
                    usedAccounts.AddRange(paczkomatyAccountsToUse);
                    return paczkomatyAccountsToUse.ToArray();

                case "DPD":
                    if (dpdAccounts > uniqueAccounts.Count)
                    {
                        Console.WriteLine("Error: Not enough unique accounts available.");
                        return new CookieModel[0];
                    }
                    List<CookieModel> dpdAccountsToUse = uniqueAccounts.Take(dpdAccounts).ToList();
                    usedAccounts.AddRange(dpdAccountsToUse);
                    return dpdAccountsToUse.ToArray();

                case "Kurier Pobranie":
                    if (kurierPobranieAccounts > uniqueAccounts.Count)
                    {
                        Console.WriteLine("Error: Not enough unique accounts available.");
                        return new CookieModel[0];
                    }
                    List<CookieModel> kurierPobranieAccountsToUse = uniqueAccounts.Take(kurierPobranieAccounts).ToList();
                    usedAccounts.AddRange(kurierPobranieAccountsToUse);
                    return kurierPobranieAccountsToUse.ToArray();

                default:
                    Console.WriteLine("Error: Invalid method specified.");
                    return new CookieModel[0];
            }
        }

        public void ShowAvailableAccounts()
        {
            int numAvailable = accounts.Length - paczkomatyAccounts - dpdAccounts - kurierPobranieAccounts;
            Console.WriteLine("Number of available accounts: " + numAvailable);
        }
    }


}
