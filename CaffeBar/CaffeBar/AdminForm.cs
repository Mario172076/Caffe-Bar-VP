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
            loadInformations();
        }



        private void loadInformations()
        {
            using (var context = new ModelContext())
            {
                List<Table> tables = context.Tables.ToList();
                foreach(Table t in tables)
                {
                    lbTablesAF.Items.Add(t);
                }
                List<Order> orders = context.Orders.ToList();
                foreach(Order o in orders)
                {
                    if (o.Status != 3)
                    {
                        lbOrdersAF.Items.Add(o);
                    }
                    
                }
                List<Reservation> reservations = context.Reservations.ToList();
                foreach(Reservation r in reservations)
                {
                    lbReservationsAF.Items.Add(r);
                }
                List<Category> categories = context.Category.ToList();
                foreach(Category c in categories)
                {
                    cbProCatAF.Items.Add(c);
                }
                
            }
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

        private void btnAddCatAF_Click(object sender, EventArgs e)
        {
            using (var context = new ModelContext())
            {
                Category cat = new Category();
                cat.CatName = tbCatNameAF.Text;
                Category category = context.Category.Where(c => c.CatName == tbCatNameAF.Text).FirstOrDefault();
                if (category != null)
                {
                    MessageBox.Show("Category with that name allready exists");
                    return;
                }
                else
                {
                    context.Category.Add(cat);
                    context.SaveChanges();
                    MessageBox.Show("Categroy added succesfully");
                }
               
                
            }
        }

        private void btnViewTableAF_Click(object sender, EventArgs e)
        {
            using (var context = new ModelContext())
            {
                Table t = (Table)lbTablesAF.SelectedItem;
                TableInfoForm tif = new TableInfoForm();
                Employee employee = context.Employee.Where(em => em.EmpId == t.EmpId).FirstOrDefault();
                Order order = context.Orders.Where(o => o.TableId == t.TableId && o.Status!=3).FirstOrDefault();
                Reservation reservation = context.Reservations.Where(r => r.TableId == t.TableId).FirstOrDefault();

                //checking if table is for order
                List<ProductsInOrder> pios = new List<ProductsInOrder>();
                if (order != null)
                {
                    pios = context.ProductsInOrder.Where(pio => pio.OrderId == order.OrderId).ToList();
                }

                //checking if table is for reservation
                List<ProductsInReservation> pirs = new List<ProductsInReservation>();
                if (reservation != null)
                {
                    pirs = context.ProductsInReservation.Where(pir => pir.ResId == reservation.ResId).ToList();
                }


                List<Product> products = new List<Product>();

                //if product is for order get products form the order
                if (pios.Count >= 1)
                {
                    foreach (ProductsInOrder product in pios)
                    {
                        Product productt = new Product();
                        productt = context.Products.Where(p => p.ProId == product.ProductId).FirstOrDefault();
                        products.Add(productt);
                    }
                }
                //if products are from reservation get products from reservation
                else if(pirs.Count >= 1)
                {
                    foreach (ProductsInReservation product in pirs)
                    {
                        Product productt = new Product();
                        productt = context.Products.Where(p => p.ProId == product.ProductId).FirstOrDefault();
                        products.Add(productt);
                    }
                }
               
                //setting form values
                tif.tbAvalaibleTIF.Text = t.TableAvalaible.ToString();
                tif.tbEmployeeTIF.Text = employee.EmpName;
                tif.tbNumOfSeatsTIF.Text = t.NumberOfSeats.ToString();
                tif.tbTableNumTIF.Text = t.TableId.ToString();
                foreach(Product p in products)
                {
                    tif.cbProductsTIF.Items.Add(p);
                }
                tif.ShowDialog();
                

            }
           
        }

        private void btnAddTableAF_Click(object sender, EventArgs e)
        {
            AddTableForm forma = new AddTableForm();
            if (forma.ShowDialog() == DialogResult.OK)
            {
                lbTablesAF.Items.Add(forma.table);
            }
        }

        private void btnAddProductAF_Click(object sender, EventArgs e)
        {
            using (var context = new ModelContext())
            {

                Product product = new Product();
                product.ProName = tbProNameAF.Text;
                product.ProPrice = int.Parse(tbProPriceAF.Text);
                product.AgeRestrictions = int.Parse(cbAgeResAF.Text);
                if (tbTimeServingAF.Text != "")
                {
                    product.TimeOfServing = int.Parse(tbTimeServingAF.Text);
                }
                product.ProDescription = tbProDescAF.Text;
                product.ProQuantity = int.Parse(tbProQuantityAF.Text);
                Category cat = (Category)cbProCatAF.SelectedItem;
                product.CatId = cat.CatId;
                context.Products.Add(product);
                if(context.SaveChanges() > 0)
                {
                    MessageBox.Show("Product added successfully");
                }

            }
 
        }

        private void btnViewOrderAF_Click(object sender, EventArgs e)
        {
            using (var context = new ModelContext())
            {
                OrderDetailsForm odf = new OrderDetailsForm();
                Order order = (Order)lbOrdersAF.SelectedItem;
                Customer customer = context.Customer.Where(c => c.CustId == order.CustId).FirstOrDefault();
                Employee employee = context.Employee.Where(emp => emp.EmpId == order.EmpId).FirstOrDefault();
                odf.tbNameODF.Text = customer.CustName;
                odf.tbOrderPriceODF.Text = order.OrderPrice.ToString();
                odf.tbEmployeeODF.Text = employee.EmpName;
                odf.tbStatusODF.Text = order.Status.ToString();
                odf.tbAddressODF.Text = order.OrderAddress;
                odf.tbTimeToDeliverODF.Text = order.TimeToDeliver.ToString();
                odf.tbTableODF.Text = order.TableId.ToString();
                List<ProductsInOrder> pio = context.ProductsInOrder.Where(pios => pios.OrderId == order.OrderId).ToList();
                List<Product> products = new List<Product>();
                foreach (ProductsInOrder product in pio)
                {
                    Product productt = new Product();
                    productt = context.Products.Where(p => p.ProId == product.ProductId).FirstOrDefault();
                    products.Add(productt);
                }
                foreach (Product p in products)
                {
                    odf.cbProductsODF.Items.Add(p);
                }

                odf.ShowDialog();


            }
        }

        private void btnViewReservationAF_Click(object sender, EventArgs e)
        {
            using (var context = new ModelContext())
            {
                ReservationDetailsForm rdf = new ReservationDetailsForm();
                Reservation reservation = (Reservation)lbReservationsAF.SelectedItem;
                Customer customer = context.Customer.Where(c => c.CustId == reservation.CustId).FirstOrDefault();
                rdf.tbDateTimeRDF.Text = reservation.DateRes.ToString();
                rdf.tbNameRDF.Text = customer.CustName;
                rdf.tbNumPeopleRDF.Text = reservation.NumPeople.ToString();
                rdf.tbResPriceRDF.Text = reservation.PriceRes.ToString();
                rdf.tbTableRDF.Text = reservation.TableId.ToString();
                List<ProductsInReservation> pir = context.ProductsInReservation.Where(pios => pios.ResId == reservation.ResId).ToList();
                List<Product> products = new List<Product>();
                foreach (ProductsInReservation product in pir)
                {
                    Product productt = new Product();
                    productt = context.Products.Where(p => p.ProId == product.ProductId).FirstOrDefault();
                    products.Add(productt);
                }
                foreach (Product p in products)
                {
                    rdf.cbProductsRDF.Items.Add(p);
                }

                rdf.ShowDialog();
            }

        }


        //validation for adding product and category
        private void tbCatNameAF_Validating(object sender, CancelEventArgs e)
        {
            if (tbCatNameAF.Text == "")
            {
                errorProvider1.SetError(tbCatNameAF, "Please enter category name");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tbCatNameAF, null);
                e.Cancel = false;
            }
        }

        private void tbProNameAF_Validating(object sender, CancelEventArgs e)
        {
            if (tbProNameAF.Text == "")
            {
                errorProvider1.SetError(tbProNameAF, "Please enter product name");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tbProNameAF, null);
                e.Cancel = false;
            }
        }

        private void tbProPriceAF_Validating(object sender, CancelEventArgs e)
        {
            if (tbProPriceAF.Text == "" || !tbProPriceAF.Text.All(char.IsDigit))
            {
                errorProvider1.SetError(tbProPriceAF, "Please enter product price");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tbProPriceAF, null);
                e.Cancel = false;
            }
        }

        private void cbAgeResAF_Validating(object sender, CancelEventArgs e)
        {
            if (cbAgeResAF.SelectedItem==null)
            {
                errorProvider1.SetError(cbAgeResAF, "Please select if product has age restrictions");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(cbAgeResAF, null);
                e.Cancel = false;
            }
        }

        private void tbProDescAF_Validating(object sender, CancelEventArgs e)
        {
            if (tbProDescAF.Text=="")
            {
                errorProvider1.SetError(tbProDescAF, "Please enter product name");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tbProDescAF, null);
                e.Cancel = false;
            }
        }

        private void tbProQuantityAF_Validating(object sender, CancelEventArgs e)
        {
            if (tbProQuantityAF.Text == "" || !tbProQuantityAF.Text.All(char.IsDigit))
            {
                errorProvider1.SetError(tbProQuantityAF, "Please enter product quantity");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tbProQuantityAF, null);
                e.Cancel = false;
            }
        }

        private void cbProCatAF_Validating(object sender, CancelEventArgs e)
        {
            if (cbProCatAF.SelectedItem == null)
            {
                errorProvider1.SetError(cbProCatAF, "Please select a category");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(cbProCatAF, null);
                e.Cancel = false;
            }
        }

        
    }
}
