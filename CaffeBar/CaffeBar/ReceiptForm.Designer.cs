namespace CaffeBar
{
    partial class ReceiptForm
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
            this.lblReceipts = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.receiptId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employeeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.products = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblReceipts
            // 
            this.lblReceipts.AutoSize = true;
            this.lblReceipts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblReceipts.Location = new System.Drawing.Point(312, 82);
            this.lblReceipts.Name = "lblReceipts";
            this.lblReceipts.Size = new System.Drawing.Size(97, 20);
            this.lblReceipts.TabIndex = 2;
            this.lblReceipts.Text = "RECEIPTS";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.receiptId,
            this.orderId,
            this.employeeName,
            this.products,
            this.price});
            this.dataGridView1.Location = new System.Drawing.Point(148, 151);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(440, 150);
            this.dataGridView1.TabIndex = 3;
            // 
            // receiptId
            // 
            this.receiptId.HeaderText = "Receipt Id";
            this.receiptId.Name = "receiptId";
            this.receiptId.Width = 81;
            // 
            // orderId
            // 
            this.orderId.HeaderText = "Order ID";
            this.orderId.Name = "orderId";
            this.orderId.Width = 72;
            // 
            // employeeName
            // 
            this.employeeName.HeaderText = "Employee";
            this.employeeName.Name = "employeeName";
            this.employeeName.Width = 78;
            // 
            // products
            // 
            this.products.HeaderText = "Products";
            this.products.Name = "products";
            this.products.Width = 74;
            // 
            // price
            // 
            this.price.HeaderText = "Price";
            this.price.Name = "price";
            this.price.Width = 56;
            // 
            // ReceiptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblReceipts);
            this.Name = "ReceiptForm";
            this.Text = "ReceiptForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblReceipts;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn receiptId;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderId;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn products;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
    }
}