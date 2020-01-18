namespace Mangyct.SignalR.Storehouse.AppService
{
    partial class FormBalance
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
            this.buttonBalance = new System.Windows.Forms.Button();
            this.textBoxBalance = new System.Windows.Forms.TextBox();
            this.labelBalance = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonBalance
            // 
            this.buttonBalance.Location = new System.Drawing.Point(72, 80);
            this.buttonBalance.Name = "buttonBalance";
            this.buttonBalance.Size = new System.Drawing.Size(100, 23);
            this.buttonBalance.TabIndex = 0;
            this.buttonBalance.Text = "button1";
            this.buttonBalance.UseVisualStyleBackColor = true;
            this.buttonBalance.Click += new System.EventHandler(this.buttonBalance_Click);
            // 
            // textBoxBalance
            // 
            this.textBoxBalance.Location = new System.Drawing.Point(72, 37);
            this.textBoxBalance.Name = "textBoxBalance";
            this.textBoxBalance.Size = new System.Drawing.Size(100, 20);
            this.textBoxBalance.TabIndex = 1;
            // 
            // labelBalance
            // 
            this.labelBalance.AutoSize = true;
            this.labelBalance.Location = new System.Drawing.Point(86, 21);
            this.labelBalance.Name = "labelBalance";
            this.labelBalance.Size = new System.Drawing.Size(66, 13);
            this.labelBalance.TabIndex = 2;
            this.labelBalance.Text = "Количество";
            // 
            // FormBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 147);
            this.Controls.Add(this.labelBalance);
            this.Controls.Add(this.textBoxBalance);
            this.Controls.Add(this.buttonBalance);
            this.Name = "FormBalance";
            this.Text = "FormBalance";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBalance;
        private System.Windows.Forms.TextBox textBoxBalance;
        private System.Windows.Forms.Label labelBalance;
    }
}