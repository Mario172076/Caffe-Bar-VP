using CaffeBar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaffeBar
{
    public partial class OrderHistoryForm : Form
    {
        //TO DO: Да се средат колоните во DataGridView
        public OrderHistoryForm()
        {
            InitializeComponent();
            List<Order> orders = new List<Order>();
            using(var context = new ModelContext())
            {
                orders = context.Orders.Where(o => o.Status == 3).ToList();
            }
            var bindingList = new BindingList<Order>(orders);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;
            dataGridView1.Columns["Employee"].Visible = false;
            dataGridView1.Columns["Customer"].Visible = false;
            dataGridView1.Columns["Table"].Visible = false;
            dataGridView1.Columns["TableId"].Visible = false;
        }
    }
}
