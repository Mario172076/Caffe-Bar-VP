
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoginF1_Click(object sender, EventArgs e)
        {
            using (var context = new ModelContext())
            {
                Customer customer = context.Customer.Where(c => c.CustUsername == tbUsernameLogin.Text && c.CustPassword == tbLoginPassword.Text).FirstOrDefault();
                Employee employee = context.Employee.Where(em => em.EmpUsername == tbUsernameLogin.Text && em.EmpPassword == tbLoginPassword.Text).FirstOrDefault();
                if (customer != null)
                {
                    customer.LoggedIn = 1;
                    context.SaveChanges();
                    CustomerForm cs = new CustomerForm();
                    cs.tbResNameCF.Text = customer.CustName;
                    cs.tbNameOrderCF.Text = customer.CustName;
                    cs.tbLoggedUserCF.Text = customer.CustName;
                    cs.ShowDialog();
                    
                }
                else if(employee!=null)
                {
                    employee.LoggedIn = 1;
                    context.SaveChanges();
                    AdminForm af = new AdminForm();
                    af.tbLoggedAdminAF.Text = employee.EmpName;
                    af.ShowDialog();
                    
                   
                }
                else
                {
                    errorProvider1.SetError(btnLoginF1, "Wrong credentials");
                }
            }
        }
    }
}
