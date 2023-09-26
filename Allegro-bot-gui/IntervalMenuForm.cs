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
    public partial class IntervalMenuForm : Form
    {
        public IntervalMenuForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IntervalsMenu menu = new IntervalsMenu();
            menu.minimum_time = Int32.Parse(min_time.Text);
            menu.maximum_time = Int32.Parse(max_time.Text);
            File.WriteAllText("./BotData/interval.json", JsonConvert.SerializeObject(menu));
            this.Close();
        }

        private void IntervalMenuForm_Load(object sender, EventArgs e)
        {
            IntervalsMenu menu = JsonConvert.DeserializeObject<IntervalsMenu>(File.ReadAllText("./BotData/interval.json"));
            min_time.Text = menu.minimum_time.ToString();
            max_time.Text= menu.maximum_time.ToString();
        }
    }
}
