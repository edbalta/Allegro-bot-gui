using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace Allegro_bot_gui
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            SettingsUnpaidOrders settings = JsonConvert.DeserializeObject<SettingsUnpaidOrders>(File.ReadAllText("./BotData/settings_unpaid.json"));
            dont_make_order.Checked = settings.dont_make_order;
            add_to_basket_before.Checked = settings.add_to_basket;
            favourite_offer.Checked = settings.favorite_offer;
            add_views.Checked = settings.add_views;
            comboBox1.Text = settings.delivery_method;
            change_nicknames.Checked = settings.change_nicknames;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SettingsUnpaidOrders settings = new SettingsUnpaidOrders();
            settings.favorite_offer = favourite_offer.Checked;
            settings.dont_make_order = dont_make_order.Checked;
            settings.add_to_basket = add_to_basket_before.Checked;
            settings.add_views = add_views.Checked;
            settings.change_nicknames = change_nicknames.Checked;
            settings.change_nicknames_old_method = change_nicknames_old_method.Checked;
            if (comboBox1.Text != "DPD" && comboBox1.Text != "Paczkomaty InPost" && comboBox1.Text != "Kurier InPost pobranie" && comboBox1.Text != "Use ALL")
            {
                comboBox1.Text = "Paczkomaty InPost";
            }
            settings.delivery_method = comboBox1.Text;
            File.WriteAllText("./BotData/settings_unpaid.json", JsonConvert.SerializeObject(settings));
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new IntervalMenuForm().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new AccountSelector().Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Use ALL")
            {
                new DeliverySelector().Show();
            }
        }

        private void change_nicknames_CheckedChanged(object sender, EventArgs e)
        {
            if (change_nicknames.Checked)
            {
                new UsernamesSettings().Show();
            }
        }


        private void change_nicknames_old_method_CheckedChanged(object sender, EventArgs e)
        {
            if (change_nicknames.Checked != true)
            {
                change_nicknames_old_method.Checked = false;
            }  
        }
    }
}
