
namespace CaffeBar
{
    partial class AddTableForm
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
            this.tbNumSeatsATF = new System.Windows.Forms.TextBox();
            this.cbAvalaibleATF = new System.Windows.Forms.ComboBox();
            this.btnAddTableATF = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbEmployeeATF = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tbNumSeatsATF
            // 
            this.tbNumSeatsATF.Location = new System.Drawing.Point(36, 38);
            this.tbNumSeatsATF.Name = "tbNumSeatsATF";
            this.tbNumSeatsATF.Size = new System.Drawing.Size(100, 20);
            this.tbNumSeatsATF.TabIndex = 1;
            // 
            // cbAvalaibleATF
            // 
            this.cbAvalaibleATF.FormattingEnabled = true;
            this.cbAvalaibleATF.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cbAvalaibleATF.Location = new System.Drawing.Point(36, 77);
            this.cbAvalaibleATF.Name = "cbAvalaibleATF";
            this.cbAvalaibleATF.Size = new System.Drawing.Size(97, 21);
            this.cbAvalaibleATF.TabIndex = 2;
            // 
            // btnAddTableATF
            // 
            this.btnAddTableATF.Location = new System.Drawing.Point(39, 166);
            this.btnAddTableATF.Name = "btnAddTableATF";
            this.btnAddTableATF.Size = new System.Drawing.Size(97, 23);
            this.btnAddTableATF.TabIndex = 3;
            this.btnAddTableATF.Text = "Add Table";
            this.btnAddTableATF.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Number of Seats";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Avalaible";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Employee";
            // 
            // cbEmployeeATF
            // 
            this.cbEmployeeATF.FormattingEnabled = true;
            this.cbEmployeeATF.Location = new System.Drawing.Point(36, 117);
            this.cbEmployeeATF.Name = "cbEmployeeATF";
            this.cbEmployeeATF.Size = new System.Drawing.Size(97, 21);
            this.cbEmployeeATF.TabIndex = 8;
            // 
            // AddTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(198, 265);
            this.Controls.Add(this.cbEmployeeATF);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAddTableATF);
            this.Controls.Add(this.cbAvalaibleATF);
            this.Controls.Add(this.tbNumSeatsATF);
            this.Name = "AddTableForm";
            this.Text = "AddTableForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbNumSeatsATF;
        private System.Windows.Forms.ComboBox cbAvalaibleATF;
        private System.Windows.Forms.Button btnAddTableATF;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbEmployeeATF;
    }
}