
namespace CaffeBar
{
    partial class TableInfoForm
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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbEmployeeTIF = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbNumOfSeatsTIF = new System.Windows.Forms.TextBox();
            this.tbTableNumTIF = new System.Windows.Forms.TextBox();
            this.cbProductsTIF = new System.Windows.Forms.ComboBox();
            this.tbAvalaibleTIF = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(80, 239);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Products";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(80, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Employee";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Avalaible";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Number of seats";
            // 
            // tbEmployeeTIF
            // 
            this.tbEmployeeTIF.Location = new System.Drawing.Point(83, 216);
            this.tbEmployeeTIF.Name = "tbEmployeeTIF";
            this.tbEmployeeTIF.Size = new System.Drawing.Size(100, 20);
            this.tbEmployeeTIF.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Table Number";
            // 
            // tbNumOfSeatsTIF
            // 
            this.tbNumOfSeatsTIF.Location = new System.Drawing.Point(83, 138);
            this.tbNumOfSeatsTIF.Name = "tbNumOfSeatsTIF";
            this.tbNumOfSeatsTIF.Size = new System.Drawing.Size(100, 20);
            this.tbNumOfSeatsTIF.TabIndex = 11;
            // 
            // tbTableNumTIF
            // 
            this.tbTableNumTIF.Location = new System.Drawing.Point(83, 99);
            this.tbTableNumTIF.Name = "tbTableNumTIF";
            this.tbTableNumTIF.Size = new System.Drawing.Size(100, 20);
            this.tbTableNumTIF.TabIndex = 10;
            // 
            // cbProductsTIF
            // 
            this.cbProductsTIF.FormattingEnabled = true;
            this.cbProductsTIF.Location = new System.Drawing.Point(83, 255);
            this.cbProductsTIF.Name = "cbProductsTIF";
            this.cbProductsTIF.Size = new System.Drawing.Size(100, 21);
            this.cbProductsTIF.TabIndex = 20;
            // 
            // tbAvalaibleTIF
            // 
            this.tbAvalaibleTIF.Location = new System.Drawing.Point(83, 177);
            this.tbAvalaibleTIF.Name = "tbAvalaibleTIF";
            this.tbAvalaibleTIF.Size = new System.Drawing.Size(100, 20);
            this.tbAvalaibleTIF.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(60, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 20);
            this.label6.TabIndex = 22;
            this.label6.Text = "TABLE DETAILS";
            // 
            // TableInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 419);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbAvalaibleTIF);
            this.Controls.Add(this.cbProductsTIF);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbEmployeeTIF);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbNumOfSeatsTIF);
            this.Controls.Add(this.tbTableNumTIF);
            this.Name = "TableInfoForm";
            this.Text = "TableInfoForm";

            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox tbEmployeeTIF;
        public System.Windows.Forms.TextBox tbNumOfSeatsTIF;
        public System.Windows.Forms.TextBox tbTableNumTIF;
        public System.Windows.Forms.ComboBox cbProductsTIF;
        public System.Windows.Forms.TextBox tbAvalaibleTIF;
    }
}