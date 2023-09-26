
namespace Allegro_bot_gui
{
    partial class DeliverySelector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.inpost_prograin_textbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.inpost_normal_textbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dpd_textbox = new System.Windows.Forms.TextBox();
            this.label_accounts = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(37, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Delivery Selector";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(43, 348);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(217, 28);
            this.button1.TabIndex = 9;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(62, 299);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "Paczkomaty InPost pobranie";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // inpost_prograin_textbox
            // 
            this.inpost_prograin_textbox.Location = new System.Drawing.Point(45, 318);
            this.inpost_prograin_textbox.Margin = new System.Windows.Forms.Padding(4);
            this.inpost_prograin_textbox.Name = "inpost_prograin_textbox";
            this.inpost_prograin_textbox.Size = new System.Drawing.Size(216, 22);
            this.inpost_prograin_textbox.TabIndex = 12;
            this.inpost_prograin_textbox.TextChanged += new System.EventHandler(this.inpost_prograin_textbox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(91, 252);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Paczkomaty InPost";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // inpost_normal_textbox
            // 
            this.inpost_normal_textbox.Location = new System.Drawing.Point(45, 273);
            this.inpost_normal_textbox.Margin = new System.Windows.Forms.Padding(4);
            this.inpost_normal_textbox.Name = "inpost_normal_textbox";
            this.inpost_normal_textbox.Size = new System.Drawing.Size(216, 22);
            this.inpost_normal_textbox.TabIndex = 10;
            this.inpost_normal_textbox.TextChanged += new System.EventHandler(this.inpost_normal_textbox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(127, 205);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "DPD";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // dpd_textbox
            // 
            this.dpd_textbox.Location = new System.Drawing.Point(48, 226);
            this.dpd_textbox.Margin = new System.Windows.Forms.Padding(4);
            this.dpd_textbox.Name = "dpd_textbox";
            this.dpd_textbox.Size = new System.Drawing.Size(216, 22);
            this.dpd_textbox.TabIndex = 14;
            this.dpd_textbox.TextChanged += new System.EventHandler(this.dpd_textbox_TextChanged);
            // 
            // label_accounts
            // 
            this.label_accounts.AutoSize = true;
            this.label_accounts.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_accounts.ForeColor = System.Drawing.Color.White;
            this.label_accounts.Location = new System.Drawing.Point(99, 40);
            this.label_accounts.Name = "label_accounts";
            this.label_accounts.Size = new System.Drawing.Size(109, 24);
            this.label_accounts.TabIndex = 23;
            this.label_accounts.Text = "Accounts: ";
            this.label_accounts.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DeliverySelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(77)))), ((int)(((byte)(76)))));
            this.ClientSize = new System.Drawing.Size(315, 400);
            this.Controls.Add(this.label_accounts);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dpd_textbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.inpost_prograin_textbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.inpost_normal_textbox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "DeliverySelector";
            this.Text = "DeliverySelector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox inpost_prograin_textbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox inpost_normal_textbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox dpd_textbox;
        private System.Windows.Forms.Label label_accounts;
    }
}