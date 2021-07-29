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
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }
        bool flag = false;
        private void btnLogoutCF_Click(object sender, EventArgs e)
        {

            using (var context = new ModelContext())
            {
                Customer customer = context.Customer.Where(c => c.LoggedIn == 1 && c.CustName == tbLoggedUserCF.Text.ToLower()).FirstOrDefault();
                customer.LoggedIn = 0;
                flag = true;
                context.SaveChanges();
                Close();
            }

        }

        private void CustomerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flag == true)
            {
                return;
            }
            else
            {
                using (var context = new ModelContext())
                {
                    Customer customer = context.Customer.Where(c => c.LoggedIn == 1 && c.CustName == tbLoggedUserCF.Text.ToLower()).FirstOrDefault();
                    customer.LoggedIn = 0;
                    context.SaveChanges();

                }
            }
          
        }
    }
}
