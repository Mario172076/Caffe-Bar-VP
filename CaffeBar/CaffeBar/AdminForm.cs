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
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }
        bool flag = false;
        private void btnLogoutAF_Click(object sender, EventArgs e)
        {
            using (var context = new ModelContext())
            {
                Employee employee = context.Employee.Where(em => em.LoggedIn == 1 && em.EmpName==tbLoggedAdminAF.Text.ToLower()).FirstOrDefault();
                employee.LoggedIn = 0;
                flag = true;
                context.SaveChanges();
                Close();
            }
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flag == true)
            {
                return;
            }
            else 
            {
                using (var context = new ModelContext())
                {
                    Employee employee = context.Employee.Where(em => em.LoggedIn == 1 && em.EmpName == tbLoggedAdminAF.Text.ToLower()).FirstOrDefault();
                    employee.LoggedIn = 0;
                    context.SaveChanges();

                }
            }
            
        }
    }
}
