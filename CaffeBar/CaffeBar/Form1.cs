
using CaffeBar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaffeBar
{
    public partial class Form1 : Form
    {
        public static int timeElapsed1;
        public static int timeElapsed2;
        public static int timeElapsed3;
        public static int timeElapsed4;
        public Form1()
        {
            InitializeComponent();
            loadInformations();
            
        }

        public void loadInformations()
        {
            timeElapsed1 = Properties.Settings.Default.timeElapsed1;
            timeElapsed2 = Properties.Settings.Default.timeElapsed2;
            timeElapsed3 = Properties.Settings.Default.timeElapsed3;
            timeElapsed4 = Properties.Settings.Default.timeElapsed4;
            timer1.Start();
            Product product = new Product();
            using (var context = new ModelContext())
            {
                List<Product> products = context.Products.ToList();
                product = products[products.Count - 1];
            }
            String imageURL = "..\\img\\" + product.CatId + ".jpg";
            if (File.Exists(imageURL))
            {
                pbPromotion.Load(imageURL);
            }
            else
            {
                pbPromotion.Load("..\\img\\error.png");
            }
            pbPromotion.SizeMode = PictureBoxSizeMode.Zoom;
            String restriction = null;
            if (product.AgeRestrictions == 0)
            {
                restriction = "";
            }
            else
            {
                restriction = "(18+)";
            }
            gbPromotionImage.Text = "NEW PRODUCT: " + product.ProName + ", " + product.ProPrice + " ден. " + restriction;
        }

        private void btnLoginF1_Click(object sender, EventArgs e)
        {
            timeElapsed1 = Properties.Settings.Default.timeElapsed1;
            timeElapsed2 = Properties.Settings.Default.timeElapsed2;
            timeElapsed3 = Properties.Settings.Default.timeElapsed3;
            timeElapsed4 = Properties.Settings.Default.timeElapsed4;

            using (var context = new ModelContext())
            {
                //ne prepoznava golema bukva
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

        private void btnRegisterF1_Click(object sender, EventArgs e)
        {
            using (var context = new ModelContext())
            {
                if (flag == true)
                {
                    Customer customer = new Customer();
                    customer.CustName = tbNameReg.Text;
                    customer.CustSurname = tbSurnameReg.Text;
                    customer.Email = tbEmailReg.Text;
                    customer.CustTelephone = tbTelephoneReg.Text;
                    customer.CustUsername = tbUsernameReg.Text;
                    customer.CustPassword = tbPasswordReg.Text;
                    customer.Age = tbAgeReg.Text;
                    customer.Address = tbAddressReg.Text;
                    context.Customer.Add(customer);
                    if (context.SaveChanges() > 0)
                    {
                        MessageBox.Show("Succesfull registration");
                    }

                }
                else
                {
                    errorProvider1.SetError(btnRegisterF1, "Please enter informations first");

                }
            }
        }




        //VALIDATION FOR REGISTRATION

        bool flag;
        private void tbNameReg_Validating(object sender, CancelEventArgs e)
        {
            flag = false;
            if (tbNameReg.Text == "")
            {
                errorProvider1.SetError(tbNameReg, "Please enter name");
                e.Cancel = true;
            }
            char first = tbNameReg.Text.First();
            if (!tbNameReg.Text.All(char.IsLetter))
            {
                errorProvider1.SetError(tbNameReg, "Name can not contain numbers, special characters od white space");
                e.Cancel = true;
            }
            else if (char.IsLower(first))
            {
                errorProvider1.SetError(tbNameReg, "First letter must be upper case");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tbNameReg, null);
                flag = true;
            }
        }

        private void tbSurnameReg_Validating(object sender, CancelEventArgs e)
        {
            flag = false;
            if (tbSurnameReg.Text == "")
            {
                errorProvider1.SetError(tbSurnameReg, "Please enter surname");
                e.Cancel = true;
            }
            char first = tbSurnameReg.Text.First();
            if (!tbSurnameReg.Text.Any(char.IsLetter))
            {
                errorProvider1.SetError(tbSurnameReg, "Surname can not contain numbers, special characters od white space");
                e.Cancel = true;
            }
            else if (char.IsLower(first))
            {
                errorProvider1.SetError(tbSurnameReg, "First letter must be upper case");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tbSurnameReg, null);
                flag = true;
            }
        }

        private void tbEmailReg_Validating(object sender, CancelEventArgs e)
        {
            flag = false;
            if (tbEmailReg.Text == "")
            {
                errorProvider1.SetError(tbEmailReg, "Please enter email");
                e.Cancel = true;
            }
            string email = tbEmailReg.Text;

            using (var context = new ModelContext())
            {
                Customer customer = context.Customer.Where(c => c.Email == email).FirstOrDefault();
                if (customer != null)
                {
                    errorProvider1.SetError(tbEmailReg, "You have allready used this email for registration");
                    e.Cancel = true;
                }
                else
                {
                    errorProvider1.SetError(tbEmailReg, null);
                    flag = true;
                }
                if (!email.Contains("@"))
                {
                    errorProvider1.SetError(tbEmailReg, "Email must contain @");
                    e.Cancel = true;
                }
            }
        }

        private void tbTelephoneReg_Validating(object sender, CancelEventArgs e)
        {
            flag = false;
            if (tbTelephoneReg.Text == "")
            {
                errorProvider1.SetError(tbTelephoneReg, "Please enter telephone");
                e.Cancel = true;
            }
            string telephone = tbTelephoneReg.Text;
            using (var context = new ModelContext())
            {
                Customer customer = context.Customer.Where(c => c.CustTelephone == telephone).FirstOrDefault();
                if (customer != null)
                {
                    errorProvider1.SetError(tbTelephoneReg, "You have allready used this phone number for registration");
                    e.Cancel = true;
                }
                else
                {
                    errorProvider1.SetError(tbTelephoneReg, null);
                    flag = true;
                }
            }
            if (!tbTelephoneReg.Text.Any(char.IsDigit))
            {
                errorProvider1.SetError(tbTelephoneReg, "Phone number can only contain numbers");
                e.Cancel = true;
            }  
        }

        private void tbUsernameReg_Validating(object sender, CancelEventArgs e)
        {
            flag = false;
            if (tbUsernameReg.Text == "")
            {
                errorProvider1.SetError(tbUsernameReg, "Please enter username");
                e.Cancel = true;
            }
            string username = tbUsernameReg.Text;
            using (var context = new ModelContext())
            {
                Customer customer = context.Customer.Where(c => c.CustUsername == username).FirstOrDefault();
                if (customer != null)
                {
                    errorProvider1.SetError(tbUsernameReg, "Username allready in use");
                    e.Cancel = true;
                }
                else
                {
                    errorProvider1.SetError(tbUsernameReg, null);
                    flag = true;
                }
            }
            if (username.Length > 10)
            {
                errorProvider1.SetError(tbUsernameReg, "Username is too long");
                e.Cancel = true;
            }
            else if(!username.All(char.IsLetterOrDigit))
            {
                errorProvider1.SetError(tbUsernameReg, "Username can only contain letters and numbers");
                e.Cancel = true;
            }
        }

        private void tbPasswordReg_Validating(object sender, CancelEventArgs e)
        {
            flag = false;
            if (tbPasswordReg.Text == "")
            {
                errorProvider1.SetError(tbPasswordReg, "Please enter name");
                e.Cancel = true;
            }
            string password = tbPasswordReg.Text;
            if (!password.Any(char.IsUpper))
            {
                errorProvider1.SetError(tbPasswordReg, "Password must contain upper case");
                e.Cancel = true;
            }
            else if (password.All(char.IsLetterOrDigit))
            {
                errorProvider1.SetError(tbPasswordReg, "Password must contain a special character");
                e.Cancel = true;
            }
            else if (password.Length< 8)
            {
                errorProvider1.SetError(tbPasswordReg, "Password length must be greater than 8");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tbPasswordReg, null);
                flag = true;
            }
        }

        private void tbRepPasswordReg_Validating(object sender, CancelEventArgs e)
        {
            flag = false;
            if (tbRepPasswordReg.Text == "")
            {
                errorProvider1.SetError(tbRepPasswordReg, "Please enter name");
                e.Cancel = true;
            }
            string password = tbPasswordReg.Text;
            string repPassword = tbRepPasswordReg.Text;
            if(!password.Equals(repPassword))
            {
                errorProvider1.SetError(tbRepPasswordReg, "Repeated password does not equal password");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tbRepPasswordReg, null);
                flag = true;
            }
        }

        private void tbAgeReg_Validating(object sender, CancelEventArgs e)
        {
            flag = false;
            int number;
            if (tbAgeReg.Text == "")
            {
                errorProvider1.SetError(tbAgeReg, "Please enter name");
                e.Cancel = true;
            }
            string age = tbAgeReg.Text;
            bool succes = int.TryParse(age, out number);
            if (!age.All(char.IsDigit))
            {
                errorProvider1.SetError(tbAgeReg, "Only number allowerd");
                e.Cancel = true;
            } 
            else if(number<16 || number > 120)
            {
                errorProvider1.SetError(tbAgeReg, "Please enter a valid number");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tbAgeReg, null);
                flag = true;
            }
        }

        private void tbAddressReg_Validating(object sender, CancelEventArgs e)
        {
            flag = false;
            if (tbAddressReg.Text == "")
            {
                errorProvider1.SetError(tbAddressReg, "Please enter name");
                e.Cancel = true;
            }
            string address = tbAddressReg.Text;
            if (address == null)
            {
                errorProvider1.SetError(tbAddressReg, "Plase enter an adress");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tbAddressReg, null);
                flag = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeElapsed1 = Properties.Settings.Default.timeElapsed1;
            timeElapsed2 = Properties.Settings.Default.timeElapsed2;
            timeElapsed3 = Properties.Settings.Default.timeElapsed3;
            timeElapsed4 = Properties.Settings.Default.timeElapsed4;
            timeElapsed1++;
            timeElapsed2++;
            timeElapsed3++;
            timeElapsed4++;
            Properties.Settings.Default.timeElapsed1 = timeElapsed1;
            Properties.Settings.Default.timeElapsed2 = timeElapsed2;
            Properties.Settings.Default.timeElapsed3 = timeElapsed3;
            Properties.Settings.Default.timeElapsed4 = timeElapsed4;

        }


    }
}
