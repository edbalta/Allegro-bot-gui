namespace Allegro_bot_gui
{
    partial class SettingsForm
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
            this.dont_make_order = new System.Windows.Forms.CheckBox();
            this.add_to_basket_before = new System.Windows.Forms.CheckBox();
            this.favourite_offer = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.add_views = new System.Windows.Forms.CheckBox();
            this.change_nicknames = new System.Windows.Forms.CheckBox();
            this.change_nicknames_old_method = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(99, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Settings";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(56, 261);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(217, 28);
            this.button1.TabIndex = 8;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dont_make_order
            // 
            this.dont_make_order.AutoSize = true;
            this.dont_make_order.ForeColor = System.Drawing.SystemColors.Control;
            this.dont_make_order.Location = new System.Drawing.Point(93, 79);
            this.dont_make_order.Margin = new System.Windows.Forms.Padding(4);
            this.dont_make_order.Name = "dont_make_order";
            this.dont_make_order.Size = new System.Drawing.Size(142, 21);
            this.dont_make_order.TabIndex = 9;
            this.dont_make_order.Text = "Don\'t Make Order";
            this.dont_make_order.UseVisualStyleBackColor = true;
            // 
            // add_to_basket_before
            // 
            this.add_to_basket_before.AutoSize = true;
            this.add_to_basket_before.ForeColor = System.Drawing.SystemColors.Control;
            this.add_to_basket_before.Location = new System.Drawing.Point(93, 98);
            this.add_to_basket_before.Margin = new System.Windows.Forms.Padding(4);
            this.add_to_basket_before.Name = "add_to_basket_before";
            this.add_to_basket_before.Size = new System.Drawing.Size(117, 21);
            this.add_to_basket_before.TabIndex = 10;
            this.add_to_basket_before.Text = "Add to basket";
            this.add_to_basket_before.UseVisualStyleBackColor = true;
            // 
            // favourite_offer
            // 
            this.favourite_offer.AutoSize = true;
            this.favourite_offer.ForeColor = System.Drawing.SystemColors.Control;
            this.favourite_offer.Location = new System.Drawing.Point(93, 127);
            this.favourite_offer.Margin = new System.Windows.Forms.Padding(4);
            this.favourite_offer.Name = "favourite_offer";
            this.favourite_offer.Size = new System.Drawing.Size(125, 21);
            this.favourite_offer.TabIndex = 11;
            this.favourite_offer.Text = "Favourite Offer";
            this.favourite_offer.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(56, 297);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(217, 28);
            this.button2.TabIndex = 12;
            this.button2.Text = "Open Intervals Menu";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(56, 332);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(217, 28);
            this.button3.TabIndex = 13;
            this.button3.Text = "Account Selector";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Paczkomaty InPost",
            "DPD",
            "Kurier InPost pobranie",
            "Use ALL"});
            this.comboBox1.Location = new System.Drawing.Point(56, 230);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(217, 24);
            this.comboBox1.TabIndex = 14;
            this.comboBox1.Text = "Paczkomaty InPost";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(77)))), ((int)(((byte)(76)))));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(73, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Choose Delivery Method";
            // 
            // add_views
            // 
            this.add_views.AutoSize = true;
            this.add_views.ForeColor = System.Drawing.SystemColors.Control;
            this.add_views.Location = new System.Drawing.Point(93, 144);
            this.add_views.Margin = new System.Windows.Forms.Padding(4);
            this.add_views.Name = "add_views";
            this.add_views.Size = new System.Drawing.Size(93, 21);
            this.add_views.TabIndex = 16;
            this.add_views.Text = "Add views";
            this.add_views.UseVisualStyleBackColor = true;
            // 
            // change_nicknames
            // 
            this.change_nicknames.AutoSize = true;
            this.change_nicknames.ForeColor = System.Drawing.SystemColors.Control;
            this.change_nicknames.Location = new System.Drawing.Point(93, 163);
            this.change_nicknames.Margin = new System.Windows.Forms.Padding(4);
            this.change_nicknames.Name = "change_nicknames";
            this.change_nicknames.Size = new System.Drawing.Size(150, 21);
            this.change_nicknames.TabIndex = 17;
            this.change_nicknames.Text = "Change nicknames";
            this.change_nicknames.UseVisualStyleBackColor = true;
            this.change_nicknames.CheckedChanged += new System.EventHandler(this.change_nicknames_CheckedChanged);
            // 
            // change_nicknames_old_method
            // 
            this.change_nicknames_old_method.AutoSize = true;
            this.change_nicknames_old_method.ForeColor = System.Drawing.SystemColors.Control;
            this.change_nicknames_old_method.Location = new System.Drawing.Point(93, 185);
            this.change_nicknames_old_method.Margin = new System.Windows.Forms.Padding(4);
            this.change_nicknames_old_method.Name = "change_nicknames_old_method";
            this.change_nicknames_old_method.Size = new System.Drawing.Size(233, 21);
            this.change_nicknames_old_method.TabIndex = 18;
            this.change_nicknames_old_method.Text = "Change nicknames(Old Method)";
            this.change_nicknames_old_method.UseVisualStyleBackColor = true;
            this.change_nicknames_old_method.CheckedChanged += new System.EventHandler(this.change_nicknames_old_method_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(77)))), ((int)(((byte)(76)))));
            this.ClientSize = new System.Drawing.Size(337, 366);
            this.Controls.Add(this.change_nicknames_old_method);
            this.Controls.Add(this.change_nicknames);
            this.Controls.Add(this.add_views);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.favourite_offer);
            this.Controls.Add(this.add_to_basket_before);
            this.Controls.Add(this.dont_make_order);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox dont_make_order;
        private System.Windows.Forms.CheckBox add_to_basket_before;
        private System.Windows.Forms.CheckBox favourite_offer;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox add_views;
        private System.Windows.Forms.CheckBox change_nicknames;
        private System.Windows.Forms.CheckBox change_nicknames_old_method;
    }
}