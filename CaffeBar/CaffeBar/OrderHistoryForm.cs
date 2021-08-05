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

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-EITPB7M;Initial Catalog=CaffeBar;Integrated Security=True"))
            {
                connection.Open();
                String query = "SELECT * FROM FinishedOrders";
                SqlCommand sql = new SqlCommand(query, connection);
                SqlDataReader reader = sql.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new Order();
                    order.OrderId = Convert.ToInt32(reader["orderId"]);
                    order.EmpId = Convert.ToInt32(reader["empId"]);
                    order.TableId = null;
                    order.Status = 3;
                    order.TimeToDeliver = Convert.ToInt32(reader["timeToDeliver"]);
                    order.OrderAddress = reader["orderAddress"].ToString();
                    order.OrderPrice = Convert.ToInt32(reader["orderPrice"]);
                    orders.Add(order);
                }
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
