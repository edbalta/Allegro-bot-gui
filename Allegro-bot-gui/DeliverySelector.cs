using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Allegro_bot_gui
{
    public partial class DeliverySelector : Form
    {
        public DeliverySelector()
        {
            InitializeComponent();
            CookieModel[] cookies = JsonConvert.DeserializeObject<CookieModel[]>(File.ReadAllText("./BotData/accounts_unpaid.json"));
            label_accounts.Text = $"Accounts: {cookies.Length.ToString()}";
            DeliverySelectorModel model = JsonConvert.DeserializeObject<DeliverySelectorModel>(File.ReadAllText("./BotData/accounts_unpaid_delivery.json"));
            inpost_normal_textbox.Text = model.inpost_accounts.ToString();
            inpost_prograin_textbox.Text = model.inpost_probain_accounts.ToString();
            dpd_textbox.Text = model.dpd_accounts.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var model = new DeliverySelectorModel();
            model.inpost_accounts = Int32.Parse(inpost_normal_textbox.Text);
            model.inpost_probain_accounts = Int32.Parse(inpost_prograin_textbox.Text);
            model.dpd_accounts = Int32.Parse(dpd_textbox.Text);
            File.WriteAllText("./BotData/accounts_unpaid_delivery.json", JsonConvert.SerializeObject(model));
            this.Close();
        }

        private void dpd_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void inpost_prograin_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void inpost_normal_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
    public class DeliverySelectorModel
    {
        public int dpd_accounts;
        public int inpost_accounts;
        public int inpost_probain_accounts;
    }
}
