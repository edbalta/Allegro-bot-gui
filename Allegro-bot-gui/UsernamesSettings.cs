using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace Allegro_bot_gui
{
    public partial class UsernamesSettings : Form
    {
        public UsernamesSettings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var model = new Dictionary<string, int>();
            model["accounts_limit"] = Int32.Parse(dpd_textbox.Text);
            File.WriteAllText("./BotData/accounts_username_limit.json", JsonConvert.SerializeObject(model));
            this.Close();
        }
    }
}
