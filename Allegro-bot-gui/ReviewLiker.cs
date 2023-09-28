using Microsoft.Toolkit.Uwp.Notifications;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;

namespace Allegro_bot_gui
{
    public partial class ReviewLiker : Form
    {
        public string uri;
        public int index;
        public int up_idx;
        public int down_idx;
        public List<Dictionary<string, bool>> review_checkboxes_state = new List<Dictionary<string, bool>>();
        public List<Dictionary<string, string>> review_id_state = new List<Dictionary<string, string>>();
        public Dictionary<string, List<int>> search_results = new Dictionary<string, List<int>>();
        public int current_search_index = 0;
        public List<Thread> threads = new List<Thread>();
        public List<Thread> threads_favourites = new List<Thread>();
        public string[] products = File.ReadAllLines("./Data/products.txt");
        public ReviewLiker(string url_of_product, int idx)
        {
            InitializeComponent();
            uri = url_of_product;
            index = idx;
            CookieModel[] cookies = JsonConvert.DeserializeObject<List<CookieModel>>(File.ReadAllText("./Data/accounts.json")).ToArray();
            label_accounts.Text = $"Accounts: {cookies.Length}";
        }
        static void LikeProcess(Backend back, string cookie, string review_id, bool use_proxy)
        {
            try
            {
                back.LikeProcess(review_id, cookie, use_proxy);
            }
            catch (Exception e) { }
        }
        static void DownProcess(Backend back, string cookie, string review_id, bool use_proxy)
        {
            try
            {
                back.DownProcess(review_id, cookie, use_proxy);
            }
            catch (Exception e) { }
        }
        public async Task ProcessStart()
        {
            var back = new Backend();
            if (checkedListBox1.Items.Count > 0)
            {
                CookieModel[] cookies = JsonConvert.DeserializeObject<List<CookieModel>>(File.ReadAllText("./Data/accounts.json")).ToArray();
                var n = 1;
                var items = checkedListBox1.Items.OfType<ReviewModel>().ToList();
                var checkedItems = checkedListBox1.CheckedItems.OfType<ReviewModel>().ToList();
                foreach (var cookie in cookies)
                {
                    Console.Title = $"Account Index Currently Used: {n}";
                    for (var i = 0; i < review_id_state.Count; ++i)
                    {
                        var state = review_id_state[i];
                        
                        if (items.Select(x => x.reviewId).Contains(state["review_id"]))
                        {
                            if (checkedItems.Select(x => x.reviewId).Contains(state["review_id"]))
                            {
                                if (checkBox3.Checked)
                                {
                                    //Thread.Sleep(Int32.Parse(textBox2.Text));
                                    await Task.Delay(Int32.Parse(textBox2.Text));
                                    LikeProcess(back, JsonConvert.SerializeObject(cookie), state["review_id"], checkBox2.Checked);
                                }
                                else
                                {
                                    await Task.Run(() => LikeProcess(back, JsonConvert.SerializeObject(cookie), state["review_id"], checkBox2.Checked));
                                    //var trd = new Thread(() => LikeProcess(back, JsonConvert.SerializeObject(cookie), state["review_id"], checkBox2.Checked));
                                    //threads.Add(trd);
                                    //trd.Start();
                                }
                            }
                            else
                            {
                                if (checkBox3.Checked)
                                {
                                    //Thread.Sleep(Int32.Parse(textBox2.Text));
                                    await Task.Delay(Int32.Parse(textBox2.Text));
                                    DownProcess(back, JsonConvert.SerializeObject(cookie), state["review_id"], checkBox2.Checked);
                                }
                                else
                                {
                                    await Task.Run(() => DownProcess(back, JsonConvert.SerializeObject(cookie), state["review_id"], checkBox2.Checked));

                                    //var trd = new Thread(() => DownProcess(back, JsonConvert.SerializeObject(cookie), state["review_id"], checkBox2.Checked));
                                    //threads.Add(trd);
                                    //trd.Start();
                                }
                            }
                        }
                    }
                    if (checkBox3.Checked)
                    {
                        await Task.Delay((Int32.Parse(textBox1.Text)));
                        //Thread.Sleep((Int32.Parse(textBox1.Text)));
                    }
                    n++;
                }
                new ToastContentBuilder()
                .AddText("Task Completed")
                .AddText("Your review marking task has been completed!")
                .Show();
            }
            else
            {
                CookieModel[] cookies = JsonConvert.DeserializeObject<List<CookieModel>>(File.ReadAllText("./Data/accounts.json")).ToArray();
                foreach (var cookie in cookies)
                {
                    var trd = new Thread(() => { back.ViewProcess(uri, JsonConvert.SerializeObject(cookie), checkBox2.Checked); });
                    trd.Start();
                    threads.Add(trd);
                }
            }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Load Reviews")
            {
                if (dont_scrape_reviews.Checked == false)
                {
                    var reviews = new List<ReviewModel>();
                    statusChecker.Text = "Status: Getting Reviews";
                    if (saved_review_update.Checked || saved_reviews_no_update.Checked)
                    {
                        if (saved_reviews_no_update.Checked)
                        {
                            reviews = JsonConvert.DeserializeObject<List<ReviewModel>>(File.ReadAllText("./BotData/reviews.json"));
                            label_downloaded_reviews.Text = $"Downloaded Reviews: {reviews.Count}";
                        } else
                        {
                            ReviewScrapper scrapper = new ReviewScrapper();
                            reviews = JsonConvert.DeserializeObject<List<ReviewModel>>(File.ReadAllText("./BotData/reviews.json"));
                            reviews = await scrapper.FilterSavedReviews(reviews, uri);
                            label_downloaded_reviews.Text = $"Downloaded Reviews: {reviews.Count}";
                        }
                    }
                    else
                    {
                        ReviewScrapper scrapper = new ReviewScrapper();
                        reviews = await scrapper.ScrapeAllegroReviews(uri);
                        label_downloaded_reviews.Text = $"Downloaded Reviews: {reviews.Count}";
                    }
                    statusChecker.Text = "Status: Got reviews";
                    new ToastContentBuilder()
                    .AddText("Task Completed")
                    .AddText("Reviews Scrapped!")
                    .Show();
                    ReviewModel[] dictionaries = reviews.ToArray(); ;
                    for (var i = 0; i < dictionaries.Length; ++i)
                    {
                        var item = new ReviewModel(dictionaries[i].reviewId, dictionaries[i].reviewBody, dictionaries[i].reviewState);
                        checkedListBox1.Items.Add(item, dictionaries[i].reviewState);
                        var id_state = new Dictionary<string, string>();
                        id_state["review_id"] = dictionaries[i].reviewId;
                        id_state["body"] = dictionaries[i].reviewBody;
                        review_id_state.Add(id_state);
                        button1.Text = "Start/Next";
                    }
                }
                else
                {
                    button1.Text = "Start/Next";
                }

            }
            else if (button1.Text == "Start/Next")
            {
                await ProcessStart();
                try
                {
                    string[] products = File.ReadAllLines("./Data/products.txt");
                    var newLiker = new ReviewLiker(products[index + 1], index + 1);
                    newLiker.Show();
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            checkedListBox1.SetItemChecked(checkedListBox1.SelectedIndex, true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            checkedListBox1.Items.RemoveAt(checkedListBox1.SelectedIndex);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            checkedListBox1.SetItemChecked(checkedListBox1.SelectedIndex, false);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < checkedListBox1.Items.Count; ++i)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < checkedListBox1.Items.Count; ++i)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < checkedListBox1.Items.Count; ++i)
            {
                checkedListBox1.Items.RemoveAt(i);
            }
        }

        private void ReviewLiker_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            AllocConsole();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();


        private void button8_Click(object sender, EventArgs e)
        {
            var review_states = new List<ReviewModel>();
            var items = checkedListBox1.Items.OfType<ReviewModel>().ToList();
            var checkedItems = checkedListBox1.CheckedItems.OfType<ReviewModel>().ToList();

            for (var i = 0; i < review_id_state.Count; ++i)
            {
                var state = review_id_state[i];

                if (items.Select(x => x.reviewId).Contains(state["review_id"]))
                {
                    if (checkedItems.Select(x => x.reviewId).Contains(state["review_id"]))
                    {
                        var model = new ReviewModel();
                        model.reviewId = state["review_id"];
                        model.reviewState = true;
                        model.reviewBody = state["body"];
                        review_states.Add(model);
                    }
                    else
                    {
                        var model = new ReviewModel();
                        model.reviewId = state["review_id"];
                        model.reviewState = false;
                        model.reviewBody = state["body"];
                        review_states.Add(model);
                    }
                }
            }
            var json_serialized_text = JsonConvert.SerializeObject(review_states);
            File.WriteAllText("./BotData/reviews.json", json_serialized_text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            foreach (var item in checkedListBox1.CheckedItems.OfType<ReviewModel>().ToList())
            {
                checkedListBox1.Items.Remove(item);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", "./Data/products.txt");
            products = File.ReadAllLines("./Data/products.txt");
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            string searchKeyword = searchTextBox.Text.ToLower();
            int selectedIndex = -1;
            List<int> items = new List<int>();

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.Items[i].ToString().ToLower().Contains(searchKeyword))
                {
                    items.Add(i);
                    if (selectedIndex == -1)
                    {
                        selectedIndex = i;
                    }
                }
            }

            if (selectedIndex >= 0)
            {
                checkedListBox1.TopIndex = selectedIndex;
                checkedListBox1.SetSelected(selectedIndex, true);
            }
            search_results[searchTextBox.Text] = items;
            current_search_index = 0;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            current_search_index++;
            if (current_search_index.ToString() == search_results[searchTextBox.Text].Count.ToString() )
            {
                
            } else
            {
                try
                {
                    checkedListBox1.TopIndex = search_results[searchTextBox.Text][current_search_index];
                    checkedListBox1.SetSelected(search_results[searchTextBox.Text][current_search_index], true);
                }
                catch (Exception exc)
                {

                }
            }
        }
    }
}
