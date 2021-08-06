using CaffeBar.Models;
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
            loadInformations();

        }

        public void loadInformations()
        {
            Random rand = new Random();
            using (var context = new ModelContext())
            {
                Customer customer = context.Customer.Where(c => c.LoggedIn == 1).FirstOrDefault();
                List<Order> orders = context.Orders.Where(o => o.CustId == customer.CustId && o.Status == 3).ToList();
                List<Employee> employees = context.Employee.ToList();
                Employee employee = new Employee();
                foreach (Order o in orders)
                {

                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                    row.Cells[0].Value = rand.Next();
                    row.Cells[1].Value = o.OrderId;
                    foreach (Employee emp in employees)
                    {
                        if (o.EmpId == emp.EmpId)
                        {
                            employee = emp;
                            break;
                        }
                          
                        
                    }
                    row.Cells[2].Value = employee.EmpName;
                    List < ProductsInOrder > productsInOrder = context.ProductsInOrder.Where(pio => pio.OrderId == o.OrderId).ToList();
                    StringBuilder sb = new StringBuilder();
                    foreach (ProductsInOrder product in productsInOrder)
                    {                       
                        Product productt = new Product();
                        productt = context.Products.Where(p => p.ProId == product.ProductId).FirstOrDefault();
                        sb.Append("'");
                        sb.Append(productt.ProName);
                        sb.Append("'");
                        sb.Append(" ");
                        
                    }
                    row.Cells[3].Value = sb.ToString();
                    row.Cells[4].Value = o.OrderPrice;
                    dataGridView1.Rows.Add(row);

                }
            }
        }

    }

    }

