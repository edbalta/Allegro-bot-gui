
namespace Allegro_bot_gui
{
    partial class UsernamesSettings
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
            this.label_accounts = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dpd_textbox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(64, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username Settings";
            // 
            // label_accounts
            // 
            this.label_accounts.AutoSize = true;
            this.label_accounts.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_accounts.ForeColor = System.Drawing.Color.White;
            this.label_accounts.Location = new System.Drawing.Point(130, 40);
            this.label_accounts.Name = "label_accounts";
            this.label_accounts.Size = new System.Drawing.Size(109, 24);
            this.label_accounts.TabIndex = 24;
            this.label_accounts.Text = "Accounts: ";
            this.label_accounts.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(140, 166);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 17);
            this.label4.TabIndex = 31;
            this.label4.Text = "Accounts Limit";
            // 
            // dpd_textbox
            // 
            this.dpd_textbox.Location = new System.Drawing.Point(87, 187);
            this.dpd_textbox.Margin = new System.Windows.Forms.Padding(4);
            this.dpd_textbox.Name = "dpd_textbox";
            this.dpd_textbox.Size = new System.Drawing.Size(216, 22);
            this.dpd_textbox.TabIndex = 30;
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.button1.Location = new System.Drawing.Point(87, 217);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(217, 28);
            this.button1.TabIndex = 25;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // UsernamesSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(77)))), ((int)(((byte)(76)))));
            this.ClientSize = new System.Drawing.Size(385, 270);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dpd_textbox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label_accounts);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Name = "UsernamesSettings";
            this.Text = "UsernamesSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_accounts;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox dpd_textbox;
        private System.Windows.Forms.Button button1;
    }
}