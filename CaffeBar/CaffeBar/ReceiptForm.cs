using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaffeBar
{
    public partial class ReceiptForm : Form
    {
        public ReceiptForm()
        {
            InitializeComponent();
            int loggedInCustomer = CustomerForm.loggedIn;
            Dictionary<int, List<string>> dict = AdminForm.receipts;
            List<string> list = new List<string>();
            try
            {
                list = dict[loggedInCustomer];
            }
            catch (Exception e)
            {
                return;
            }
            List<StringForDataGridView> newList = new List<StringForDataGridView>();
            foreach (String el in list)
            {
                newList.Add(new StringForDataGridView(el));
            }
            var bindingList = new BindingList<StringForDataGridView>(newList);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;
        }

        class StringForDataGridView
        {
            string _value;
            public string Value { get { return _value; } set { _value = value; } }
            public StringForDataGridView(String s)
            {
                _value = s;
            }
        }
    }
}
