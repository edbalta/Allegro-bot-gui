namespace Allegro_bot_gui
{
    partial class UnpaidOrders
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
            this.rand_intervals = new System.Windows.Forms.CheckBox();
            this.use_proxies = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.interval_time = new System.Windows.Forms.TextBox();
            this.use_intervals = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.offer_rotation = new System.Windows.Forms.CheckBox();
            this.account_rotation_checkbox = new System.Windows.Forms.CheckBox();
            this.apt = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(365, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(299, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Create Unpaid Orders";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(20, 494);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(217, 28);
            this.button1.TabIndex = 3;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rand_intervals
            // 
            this.rand_intervals.AutoSize = true;
            this.rand_intervals.ForeColor = System.Drawing.SystemColors.Control;
            this.rand_intervals.Location = new System.Drawing.Point(492, 399);
            this.rand_intervals.Margin = new System.Windows.Forms.Padding(4);
            this.rand_intervals.Name = "rand_intervals";
            this.rand_intervals.Size = new System.Drawing.Size(140, 21);
            this.rand_intervals.TabIndex = 4;
            this.rand_intervals.Text = "Random Intervals";
            this.rand_intervals.UseVisualStyleBackColor = true;
            this.rand_intervals.CheckedChanged += new System.EventHandler(this.rand_intervals_CheckedChanged);
            // 
            // use_proxies
            // 
            this.use_proxies.AutoSize = true;
            this.use_proxies.ForeColor = System.Drawing.SystemColors.Control;
            this.use_proxies.Location = new System.Drawing.Point(492, 462);
            this.use_proxies.Margin = new System.Windows.Forms.Padding(4);
            this.use_proxies.Name = "use_proxies";
            this.use_proxies.Size = new System.Drawing.Size(105, 21);
            this.use_proxies.TabIndex = 29;
            this.use_proxies.Text = "Use Proxies";
            this.use_proxies.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(885, 431);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 17);
            this.label3.TabIndex = 31;
            this.label3.Text = "Switch Interval In MS";
            // 
            // interval_time
            // 
            this.interval_time.Location = new System.Drawing.Point(848, 450);
            this.interval_time.Margin = new System.Windows.Forms.Padding(4);
            this.interval_time.Name = "interval_time";
            this.interval_time.Size = new System.Drawing.Size(216, 22);
            this.interval_time.TabIndex = 30;
            this.interval_time.Text = "20";
            // 
            // use_intervals
            // 
            this.use_intervals.AutoSize = true;
            this.use_intervals.ForeColor = System.Drawing.SystemColors.Control;
            this.use_intervals.Location = new System.Drawing.Point(492, 379);
            this.use_intervals.Margin = new System.Windows.Forms.Padding(4);
            this.use_intervals.Name = "use_intervals";
            this.use_intervals.Size = new System.Drawing.Size(112, 21);
            this.use_intervals.TabIndex = 32;
            this.use_intervals.Text = "Use Intervals";
            this.use_intervals.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(452, 511);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(217, 28);
            this.button2.TabIndex = 33;
            this.button2.Text = "View Settings";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(21, 459);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(217, 28);
            this.button3.TabIndex = 34;
            this.button3.Text = "Open Products File";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // offer_rotation
            // 
            this.offer_rotation.AutoSize = true;
            this.offer_rotation.ForeColor = System.Drawing.SystemColors.Control;
            this.offer_rotation.Location = new System.Drawing.Point(492, 420);
            this.offer_rotation.Margin = new System.Windows.Forms.Padding(4);
            this.offer_rotation.Name = "offer_rotation";
            this.offer_rotation.Size = new System.Drawing.Size(119, 21);
            this.offer_rotation.TabIndex = 35;
            this.offer_rotation.Text = "Offer Rotation";
            this.offer_rotation.UseVisualStyleBackColor = true;
            // 
            // account_rotation_checkbox
            // 
            this.account_rotation_checkbox.AutoSize = true;
            this.account_rotation_checkbox.ForeColor = System.Drawing.SystemColors.Control;
            this.account_rotation_checkbox.Location = new System.Drawing.Point(492, 442);
            this.account_rotation_checkbox.Margin = new System.Windows.Forms.Padding(4);
            this.account_rotation_checkbox.Name = "account_rotation_checkbox";
            this.account_rotation_checkbox.Size = new System.Drawing.Size(138, 21);
            this.account_rotation_checkbox.TabIndex = 36;
            this.account_rotation_checkbox.Text = "Account Rotation";
            this.account_rotation_checkbox.UseVisualStyleBackColor = true;
            // 
            // apt
            // 
            this.apt.AutoSize = true;
            this.apt.ForeColor = System.Drawing.SystemColors.Control;
            this.apt.Location = new System.Drawing.Point(492, 482);
            this.apt.Margin = new System.Windows.Forms.Padding(4);
            this.apt.Name = "apt";
            this.apt.Size = new System.Drawing.Size(206, 21);
            this.apt.TabIndex = 37;
            this.apt.Text = "Random Apartment Number";
            this.apt.UseVisualStyleBackColor = true;
            // 
            // UnpaidOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(77)))), ((int)(((byte)(76)))));
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.apt);
            this.Controls.Add(this.account_rotation_checkbox);
            this.Controls.Add(this.offer_rotation);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.use_intervals);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.interval_time);
            this.Controls.Add(this.use_proxies);
            this.Controls.Add(this.rand_intervals);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UnpaidOrders";
            this.Text = "UnpaidOrders";
            this.Load += new System.EventHandler(this.UnpaidOrders_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox rand_intervals;
        private System.Windows.Forms.CheckBox use_proxies;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox interval_time;
        private System.Windows.Forms.CheckBox use_intervals;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox offer_rotation;
        private System.Windows.Forms.CheckBox account_rotation_checkbox;
        private System.Windows.Forms.CheckBox apt;
    }
}