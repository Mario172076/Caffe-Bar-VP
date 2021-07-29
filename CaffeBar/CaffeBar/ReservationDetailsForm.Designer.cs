
namespace CaffeBar
{
    partial class ReservationDetailsForm
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
            this.tbNameRDF = new System.Windows.Forms.TextBox();
            this.tbNumPeopleRDF = new System.Windows.Forms.TextBox();
            this.tbResPriceRDF = new System.Windows.Forms.TextBox();
            this.tbDateTimeRDF = new System.Windows.Forms.TextBox();
            this.tbTableRDF = new System.Windows.Forms.TextBox();
            this.cbProductsRDF = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbNameRDF
            // 
            this.tbNameRDF.Location = new System.Drawing.Point(88, 95);
            this.tbNameRDF.Name = "tbNameRDF";
            this.tbNameRDF.Size = new System.Drawing.Size(100, 20);
            this.tbNameRDF.TabIndex = 0;
            // 
            // tbNumPeopleRDF
            // 
            this.tbNumPeopleRDF.Location = new System.Drawing.Point(88, 134);
            this.tbNumPeopleRDF.Name = "tbNumPeopleRDF";
            this.tbNumPeopleRDF.Size = new System.Drawing.Size(100, 20);
            this.tbNumPeopleRDF.TabIndex = 1;
            // 
            // tbResPriceRDF
            // 
            this.tbResPriceRDF.Location = new System.Drawing.Point(88, 173);
            this.tbResPriceRDF.Name = "tbResPriceRDF";
            this.tbResPriceRDF.Size = new System.Drawing.Size(100, 20);
            this.tbResPriceRDF.TabIndex = 2;
            // 
            // tbDateTimeRDF
            // 
            this.tbDateTimeRDF.Location = new System.Drawing.Point(88, 252);
            this.tbDateTimeRDF.Name = "tbDateTimeRDF";
            this.tbDateTimeRDF.Size = new System.Drawing.Size(100, 20);
            this.tbDateTimeRDF.TabIndex = 4;
            // 
            // tbTableRDF
            // 
            this.tbTableRDF.Location = new System.Drawing.Point(88, 291);
            this.tbTableRDF.Name = "tbTableRDF";
            this.tbTableRDF.Size = new System.Drawing.Size(100, 20);
            this.tbTableRDF.TabIndex = 5;
            // 
            // cbProductsRDF
            // 
            this.cbProductsRDF.FormattingEnabled = true;
            this.cbProductsRDF.Location = new System.Drawing.Point(88, 212);
            this.cbProductsRDF.Name = "cbProductsRDF";
            this.cbProductsRDF.Size = new System.Drawing.Size(100, 21);
            this.cbProductsRDF.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(88, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Number of people";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(88, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Reservation price";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(88, 196);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Products";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(88, 236);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Date/Time";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(85, 275);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Table";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(24, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(216, 20);
            this.label9.TabIndex = 16;
            this.label9.Text = "RESERVATION DETAILS";
            // 
            // ReservationDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 364);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbProductsRDF);
            this.Controls.Add(this.tbTableRDF);
            this.Controls.Add(this.tbDateTimeRDF);
            this.Controls.Add(this.tbResPriceRDF);
            this.Controls.Add(this.tbNumPeopleRDF);
            this.Controls.Add(this.tbNameRDF);
            this.Name = "ReservationDetailsForm";
            this.Text = "ReservationDetailsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbNameRDF;
        private System.Windows.Forms.TextBox tbNumPeopleRDF;
        private System.Windows.Forms.TextBox tbResPriceRDF;
        private System.Windows.Forms.TextBox tbDateTimeRDF;
        private System.Windows.Forms.TextBox tbTableRDF;
        private System.Windows.Forms.ComboBox cbProductsRDF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
    }
}