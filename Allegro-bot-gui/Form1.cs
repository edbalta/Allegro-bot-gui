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
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Allegro_bot_gui
{
    public partial class Form1 : Form
    {
        public string[] products = File.ReadAllLines("./Data/products.txt");
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var liker = new ReviewLiker(products[0], 0);
            liker.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", "./Data/accounts.json");
        }



        private void button4_Click(object sender, EventArgs e)
        {
            var frm = new UnpaidOrders();
            frm.Show();
        }
    }
}
