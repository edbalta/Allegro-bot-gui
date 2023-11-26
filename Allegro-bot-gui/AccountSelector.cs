using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Web;
using System.IO;
using Newtonsoft.Json;


namespace Allegro_bot_gui
{
    public partial class AccountSelector : Form
    {
        public Dictionary<string, List<string>> account_offers_done = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(File.ReadAllText("./BotData/accounts_offers.json"));
        public Dictionary<string, string> account_orignal_email = new Dictionary<string, string>();
        public AccountSelector()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CookieModel[] cookies = JsonConvert.DeserializeObject<List<CookieModel>>(File.ReadAllText("./Data/accounts.json")).ToArray();
            List<CookieModel> cookies_unpaid = new List<CookieModel>();
            foreach (var cookie in cookies)
            {
                var email = cookie.userIdentity;
                var account_email = email;
                if (checkedListBox1.CheckedItems.Contains(account_email))
                {
                    cookies_unpaid.Add(cookie);
                }
            }
            File.WriteAllText("./BotData/accounts_unpaid.json", JsonConvert.SerializeObject(cookies_unpaid));
            this.Close();
        }
        public string buildContent(string msg, List<string> offers)
        {
            foreach (var offer in offers)
            {
                msg += $"{offer}, ";
            }
            return msg;
        }
        private void AccountSelector_Load(object sender, EventArgs e)
        {
            CookieModel[] cookies = JsonConvert.DeserializeObject<List<CookieModel>>(File.ReadAllText("./Data/accounts.json")).ToArray();
            foreach (var cookie in cookies)
            {
                //var email2 = JsonConvert.DeserializeObject<QeppoLogin2>(HttpUtility.UrlDecode(cookie.qeppo_login2));

                var email = cookie.QXLSESSID;
                if (account_offers_done.ContainsKey(email))
                {
                    var string_text = $"{email}, Offers Done: ";
                    var content_modified = buildContent(string_text, account_offers_done[email]);
                    account_orignal_email[email] = content_modified;
                    checkedListBox1.Items.Add(content_modified);
                }
                else
                {
                    account_orignal_email[email] = email;
                    checkedListBox1.Items.Add(email);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < checkedListBox1.Items.Count; ++i)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
            label2.Text = $"Selected: {checkedListBox1.CheckedItems.Count}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < checkedListBox1.Items.Count; ++i)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            label2.Text = $"Selected: {checkedListBox1.CheckedItems.Count}";
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = $"Selected: {checkedListBox1.CheckedItems.Count}";
        }
    }
    public class QeppoLogin2
    {
        public string username;
        public Int32 id;
    }
}
