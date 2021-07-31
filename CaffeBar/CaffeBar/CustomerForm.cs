using CaffeBar.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace CaffeBar
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
            loadInformations();
        }

        private void loadInformations()
        {
            using (var context = new ModelContext())
            {
                List<Product> products = context.Products.Where(p => p.ProName != null).ToList();
                foreach (Product p in products)
                {
                    if (p.CatId == 1)
                    {
                        lbCoffesCF.Items.Add(p);
                    }
                    else if (p.CatId == 2)
                    {
                        lbBeersCF.Items.Add(p);
                    }
                    else if (p.CatId == 3)
                    {
                        lbBurgersCF.Items.Add(p);
                    }
                    else if (p.CatId == 4)
                    {
                        lbJuicesCF.Items.Add(p);
                    }
                    else if (p.CatId == 5)
                    {
                        lbSodaCF.Items.Add(p);
                    }
                    else if (p.CatId == 6)
                    {
                        lbPizzasCF.Items.Add(p);
                    }
                    else if (p.CatId == 7)
                    {
                        lbWhiskyCF.Items.Add(p);
                    }
                }
                List<Table> tables = context.Tables.Where(t => t.TableAvalaible == true).ToList();
                foreach (Table t in tables)
                {
                    if (t.TableAvalaible == true)
                    {
                        cbTableOrderCF.Items.Add(t);
                        cbTableResCF.Items.Add(t);
                    }
                
                }
                tbMinPriceResCF.Text = "400";
            }
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

        private void btnAddProToResCF_Click(object sender, EventArgs e)

        {
            addProductsToReservation();
        }

        private void btnAddProToOrderCF_Click(object sender, EventArgs e)
        {
            addProductsToOrder();
        }



        private void btnAddOrderCF_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            using (var context = new ModelContext())
            {
                //checking if product quantity is greater than 0, and if customer is ordering alcohol and is less than 18 years old
                Customer cusOrder = context.Customer.Where(c => c.CustName == tbNameOrderCF.Text).FirstOrDefault();
                foreach (Product p in cbProductsOrderCF.Items)
                {
                    if (p.ProQuantity == 0)
                    {
                        MessageBox.Show("The order cannot be made because " + p.ProName + " is out of stock");
                        return;
                    }

                    else if (p.AgeRestrictions == 1 && int.Parse(cusOrder.Age) < 18)
                    {
                        MessageBox.Show("The order cannot be made because " + cusOrder.CustName + " is under 18 years of age");
                        return;
                    }

                    p.ProQuantity -= 1;

                }
                foreach (Product p in cbProductsOrderCF.Items)
                {
                    context.Entry(p).State = EntityState.Modified;
                    context.SaveChanges();
                }

                List<Employee> employees = context.Employee.Where(em => em.EmpName != null).ToList();
                List<Order> orders = context.Orders.Where((o => o.Status == 1 || o.Status == 2)).ToList();
                //Choosing employee for the order if has less than 5 orders assigned;
                foreach (Employee emp in employees)
                {
                    int id = emp.EmpId;
                    int empTableCounter = 0;
                    foreach (Order o in orders)
                    {
                        if (o.EmpId == id)
                        {
                            empTableCounter++;
                            if (empTableCounter >= 5)
                            {
                                break;
                            }
                        }
                    }
                    if (empTableCounter < 5)
                    {
                        order.EmpId = id;
                        break;
                    }

                }

                //adding customer to the order
                Customer customer = context.Customer.Where(c => c.LoggedIn == 1 && c.CustName == tbLoggedUserCF.Text.ToLower()).FirstOrDefault();
                order.CustId = customer.CustId;
                //adding table to the order
                Table t = (Table)cbTableOrderCF.SelectedItem;
                if (t != null)
                {
                    t.TableAvalaible = false;
                    context.Entry(t).State = EntityState.Modified;
                    context.SaveChanges();
                    int tableId = t.TableId;
                    order.TableId = tableId;
                    order.Status = 1;//product is ordered with in the CaffeBar local
                }
                else
                {
                    order.Status = 2;//if table is null the order is in status delivering
                    order.TimeToDeliver = 25;
                }
                //adding other values
                order.OrderAddress = tbAddressOrderCF.Text;
                order.OrderPrice = int.Parse(tbTotalPriceOrderCF.Text);
                context.Orders.Add(order);
                if (context.SaveChanges() > 0)
                {
                    MessageBox.Show("Your have oredered successfully");
                }

                //adding every product with the order in the Other table
                foreach (Product p in cbProductsOrderCF.Items)
                {

                    ProductsInOrder pio = new ProductsInOrder();
                    pio.ProductId = p.ProId;
                    pio.OrderId = order.OrderId;
                    context.ProductsInOrder.Add(pio);
                    context.SaveChanges();

                }
                cbProductsOrderCF.Items.Clear();
                cbTableOrderCF.SelectedItem = null;
                tbAddressOrderCF.Clear();
                tbTotalPriceOrderCF.Clear();

            }
        }

        private void btnAddResCF_Click(object sender, EventArgs e)
        {
            Reservation reservation = new Reservation();
            using (var context = new ModelContext())
            {
                //checking if product quantity is greater than 0, and if customer is ordering alcohol and is less than 18 years old
                Customer cusRes = context.Customer.Where(c => c.CustName == tbResNameCF.Text).FirstOrDefault();
                foreach (Product p in cbProductsResCF.Items)
                {
                    if (p.ProQuantity == 0)
                    {
                        MessageBox.Show("The order cannot be made because " + p.ProName + " is out of stock");
                        return;
                    }

                    else if (p.AgeRestrictions == 1 && int.Parse(cusRes.Age) < 18)
                    {
                        MessageBox.Show("The order cannot be made because " + cusRes.CustName + " is under 18 years of age");
                        return;
                    }

                    p.ProQuantity -= 1;

                }
                if (DateTime.Parse(tbDateTimeResCF.Text) < DateTime.Now)
                {
                    MessageBox.Show("Please pick a valid date");
                    return;
                }
                //adding table to the order
                Table t = (Table)cbTableResCF.SelectedItem;
                if (t != null)
                {
                    if (int.Parse(tbNumPeopleResCF.Text) > t.NumberOfSeats)
                    {
                        MessageBox.Show("Please select a table with more seats if such table is avalaible");
                        return;
                    }
                    t.TableAvalaible = false;
                    context.Entry(t).State = EntityState.Modified;
                    context.SaveChanges();
                    int tableId = t.TableId;
                    reservation.TableId = tableId;
                }
                
                //adding customer to the reservation
                Customer customer = context.Customer.Where(c => c.LoggedIn == 1 && c.CustName == tbLoggedUserCF.Text.ToLower()).FirstOrDefault();
                reservation.CustId = customer.CustId;
                reservation.DateRes = DateTime.Parse(tbDateTimeResCF.Text);
                reservation.NumPeople = int.Parse(tbNumPeopleResCF.Text);
                reservation.MinPriceRes = int.Parse(tbMinPriceResCF.Text);
                reservation.PriceRes = int.Parse(tbTotalPriceResCF.Text);
                context.Reservations.Add(reservation);
                if (context.SaveChanges() > 0)
                {
                    MessageBox.Show("Your have oredered successfully");
                }

                //saving decreased quantity for products
                foreach (Product p in cbProductsResCF.Items)
                {
                    context.Entry(p).State = EntityState.Modified;
                    context.SaveChanges();
                }

                //adding every product with the order in the Other table
                foreach (Product p in cbProductsResCF.Items)
                {

                    ProductsInReservation pir = new ProductsInReservation();
                    pir.ProductId = p.ProId;
                    pir.ResId = reservation.ResId;
                    context.ProductsInReservation.Add(pir);
                    context.SaveChanges();

                }
                tbDateTimeResCF.Clear();
                tbNumPeopleResCF.Clear();
                cbTableResCF.SelectedItem = null;
                cbProductsOrderCF.Items.Clear();
                tbTotalPriceResCF.Clear();

            }
        }

        private void btnRemoveProResCF_Click(object sender, EventArgs e)
        {
            cbProductsResCF.Items.Clear();
            tbTotalPriceResCF.Clear();
        }

        private void btnRemoveProOrderCF_Click(object sender, EventArgs e)
        {
            cbProductsOrderCF.Items.Clear();
            tbTotalPriceOrderCF.Clear();
        }


        // adding products from the menu to order
        private void addProductsToOrder()
        {
            foreach (Product p in lbBeersCF.SelectedItems)
            {
                int number;
                int.TryParse(tbTotalPriceOrderCF.Text, out number);
                cbProductsOrderCF.Items.Add(p);
                int proPrice = p.ProPrice;
                number += proPrice;
                tbTotalPriceOrderCF.Text = number.ToString();

            }
            lbBeersCF.SelectedItems.Clear();

            foreach (Product p in lbCoffesCF.SelectedItems)
            {
                int number;
                int.TryParse(tbTotalPriceOrderCF.Text, out number);
                cbProductsOrderCF.Items.Add(p);
                int proPrice = p.ProPrice;
                number += proPrice;
                tbTotalPriceOrderCF.Text = number.ToString();

            }
            lbCoffesCF.SelectedItems.Clear();

            foreach (Product p in lbWhiskyCF.SelectedItems)
            {
                int number;
                int.TryParse(tbTotalPriceOrderCF.Text, out number);
                cbProductsOrderCF.Items.Add(p);
                int proPrice = p.ProPrice;
                number += proPrice;
                tbTotalPriceOrderCF.Text = number.ToString();

            }
            lbWhiskyCF.SelectedItems.Clear();

            foreach (Product p in lbPizzasCF.SelectedItems)
            {
                int number;
                int.TryParse(tbTotalPriceOrderCF.Text, out number);
                cbProductsOrderCF.Items.Add(p);
                int proPrice = p.ProPrice;
                number += proPrice;
                tbTotalPriceOrderCF.Text = number.ToString();

            }
            lbPizzasCF.SelectedItems.Clear();

            foreach (Product p in lbJuicesCF.SelectedItems)
            {
                int number;
                int.TryParse(tbTotalPriceOrderCF.Text, out number);
                cbProductsOrderCF.Items.Add(p);
                int proPrice = p.ProPrice;
                number += proPrice;
                tbTotalPriceOrderCF.Text = number.ToString();

            }
            lbJuicesCF.SelectedItems.Clear();

            foreach (Product p in lbSodaCF.SelectedItems)
            {
                int number;
                int.TryParse(tbTotalPriceOrderCF.Text, out number);
                cbProductsOrderCF.Items.Add(p);
                int proPrice = p.ProPrice;
                number += proPrice;
                tbTotalPriceOrderCF.Text = number.ToString();

            }
            lbSodaCF.SelectedItems.Clear();

            foreach (Product p in lbBurgersCF.SelectedItems)
            {
                int number;
                int.TryParse(tbTotalPriceOrderCF.Text, out number);
                cbProductsOrderCF.Items.Add(p);
                int proPrice = p.ProPrice;
                number += proPrice;
                tbTotalPriceOrderCF.Text = number.ToString();

            }
            lbBurgersCF.SelectedItems.Clear();
        }

        //adding products to reservation fron the menu

        private void addProductsToReservation()
        {
            foreach (Product p in lbBeersCF.SelectedItems)
            {
                int number;
                int.TryParse(tbTotalPriceResCF.Text, out number);
                cbProductsResCF.Items.Add(p);
                int proPrice = p.ProPrice;
                number += proPrice;
                tbTotalPriceResCF.Text = number.ToString();

            }
            lbBeersCF.SelectedItems.Clear();

            foreach (Product p in lbCoffesCF.SelectedItems)
            {
                int number;
                int.TryParse(tbTotalPriceResCF.Text, out number);
                cbProductsResCF.Items.Add(p);
                int proPrice = p.ProPrice;
                number += proPrice;
                tbTotalPriceResCF.Text = number.ToString();

            }
            lbCoffesCF.SelectedItems.Clear();

            foreach (Product p in lbBurgersCF.SelectedItems)
            {
                int number;
                int.TryParse(tbTotalPriceResCF.Text, out number);
                cbProductsResCF.Items.Add(p);
                int proPrice = p.ProPrice;
                number += proPrice;
                tbTotalPriceResCF.Text = number.ToString();

            }
            lbBurgersCF.SelectedItems.Clear();

            foreach (Product p in lbPizzasCF.SelectedItems)
            {
                int number;
                int.TryParse(tbTotalPriceResCF.Text, out number);
                cbProductsResCF.Items.Add(p);
                int proPrice = p.ProPrice;
                number += proPrice;
                tbTotalPriceResCF.Text = number.ToString();

            }
            lbPizzasCF.SelectedItems.Clear();

            foreach (Product p in lbWhiskyCF.SelectedItems)
            {
                int number;
                int.TryParse(tbTotalPriceResCF.Text, out number);
                cbProductsResCF.Items.Add(p);
                int proPrice = p.ProPrice;
                number += proPrice;
                tbTotalPriceResCF.Text = number.ToString();

            }
            lbWhiskyCF.SelectedItems.Clear();

            foreach (Product p in lbJuicesCF.SelectedItems)
            {
                int number;
                int.TryParse(tbTotalPriceResCF.Text, out number);
                cbProductsResCF.Items.Add(p);
                int proPrice = p.ProPrice;
                number += proPrice;
                tbTotalPriceResCF.Text = number.ToString();

            }
            lbJuicesCF.SelectedItems.Clear();

            foreach (Product p in lbSodaCF.SelectedItems)
            {
                int number;
                int.TryParse(tbTotalPriceResCF.Text, out number);
                cbProductsResCF.Items.Add(p);
                int proPrice = p.ProPrice;
                number += proPrice;
                tbTotalPriceResCF.Text = number.ToString();

            }
            lbSodaCF.SelectedItems.Clear();
        }

       
    }
}
