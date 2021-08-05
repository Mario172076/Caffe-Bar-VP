using CaffeBar.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CaffeBar
{
    public partial class CustomerForm : Form
    {
        public static bool isOrderLate;
        public static int loggedInCustomerId;
        int timeElapsed1;
        int timeElapsed2;
        int timeElapsed3;
        int timeElapsed4;
        public static int loggedIn;
        List<Order> delivered = new List<Order>();
        List<Order> madeOrders = new List<Order>();
        List<bool> areOrdersLate = new List<bool>();
        List<Order> ordersPb = new List<Order>();
        Dictionary<ProgressBar, int> dict = new Dictionary<ProgressBar, int>();
        public CustomerForm()
        {
            InitializeComponent();
            loadInformations();
            init();
            DoubleBuffered = true;
            timer1.Start();
        }

        private void loadInformations()
        {

            using (var context = new ModelContext())
            {
                pb1.Value = 0;
                pb2.Value = 0;
                pb3.Value = 0;
                pb4.Value = 0;
                timeElapsed1 = Properties.Settings.Default.timeElapsed1;
                timeElapsed2 = Properties.Settings.Default.timeElapsed2;
                timeElapsed3 = Properties.Settings.Default.timeElapsed3;
                timeElapsed4 = Properties.Settings.Default.timeElapsed4;
                List<Product> products = context.Products.Where(p => p.ProName != null).ToList();
                foreach (Product p in products)
                {
                    if (p.CatId == 1)
                    {
                        lbBurgersCF.Items.Add(p);
                    }
                    else if (p.CatId == 2)
                    {
                        lbJuicesCF.Items.Add(p);
                    }
                    else if (p.CatId == 3)
                    {
                        lbSodaCF.Items.Add(p);
                    }
                    else if (p.CatId == 4)
                    {
                        lbBeersCF.Items.Add(p);
                    }
                    else if (p.CatId == 5)
                    {
                        lbWhiskyCF.Items.Add(p);
                    }
                    else if (p.CatId == 6)
                    {
                        lbPizzasCF.Items.Add(p);
                    }
                    else if (p.CatId == 7)
                    {
                        lbCoffesCF.Items.Add(p);
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
  

            Properties.Settings.Default.timeElapsed1 = pb1.Value;
            Properties.Settings.Default.timeElapsed2 = pb2.Value;
            Properties.Settings.Default.timeElapsed3 = pb3.Value;
            Properties.Settings.Default.timeElapsed4 = pb4.Value;

            using (var context = new ModelContext())
            {
                Customer customer = context.Customer.Where(c => c.LoggedIn == 1 && c.CustName == tbLoggedUserCF.Text.ToLower()).FirstOrDefault();
                customer.LoggedIn = 0;
                flag = true;
                context.SaveChanges();
                timer1.Stop();
                Close();
            }

        }

        private void CustomerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.timeElapsed1 = pb1.Value;
            Properties.Settings.Default.timeElapsed2 = pb2.Value;
            Properties.Settings.Default.timeElapsed3 = pb3.Value;
            Properties.Settings.Default.timeElapsed4 = pb4.Value;


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
                if (cbProductsOrderCF.Items.Count == 0)
                {
                    MessageBox.Show("Please select a product before making an order");
                    return;
                }
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
                if (t != null && tbAddressOrderCF.Text=="")
                {
                    t.TableAvalaible = false;
                    context.Entry(t).State = EntityState.Modified;
                    context.SaveChanges();
                    int tableId = t.TableId;
                    order.TableId = tableId;
                    order.Status = 1;//product is ordered with in the CaffeBar local
                }
                else if (tbAddressOrderCF.Text!="" && t==null)
                {
                    order.Status = 1;//if table is null the order is in status delivering
                    order.TimeToDeliver = 25;
                    order.OrderAddress = tbAddressOrderCF.Text;
                }
                else
                {
                    MessageBox.Show("Please select a table or enter an address");
                    return;
                }
                //adding other values
                
                order.OrderPrice = int.Parse(tbTotalPriceOrderCF.Text);
               
                context.Orders.Add(order);
                if (context.SaveChanges() > 0)
                {
                    MessageBox.Show("Your have ordered successfully");
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
            lbBeersCF.Items.Clear();
            lbBurgersCF.Items.Clear();
            lbCoffesCF.Items.Clear();
            lbJuicesCF.Items.Clear();
            lbWhiskyCF.Items.Clear();
            lbPizzasCF.Items.Clear();
            lbSodaCF.Items.Clear();
            loadInformations();
        }

        private void btnAddResCF_Click(object sender, EventArgs e)
        {
            Reservation reservation = new Reservation();
            using (var context = new ModelContext())
            {
                if (cbProductsResCF.Items.Count == 0)
                {
                    MessageBox.Show("Please add products for the reservation");
                    return;
                }
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
                DateTime dt;
                try
                {
                    dt = DateTime.Parse(tbDateTimeResCF.Text);
                }
                catch(Exception exception)
                {
                    MessageBox.Show("Please pick a valid date");
                    return;
                }
                if (dt < DateTime.Now)
                {
                    MessageBox.Show("Please pick a valid date");
                    return;
                }


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
                else
                {
                    MessageBox.Show("Please select table for the reservation");
                    return;
                }
                
                //adding customer to the reservation
                Customer customer = context.Customer.Where(c => c.LoggedIn == 1 && c.CustName == tbLoggedUserCF.Text.ToLower()).FirstOrDefault();
                reservation.CustId = customer.CustId;
                reservation.DateRes = DateTime.Parse(tbDateTimeResCF.Text);
                try
                {
                    reservation.NumPeople = int.Parse(tbNumPeopleResCF.Text);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Enter number of people");
                    return;
                }
                reservation.MinPriceRes = int.Parse(tbMinPriceResCF.Text);
                reservation.PriceRes = int.Parse(tbTotalPriceResCF.Text);
                try
                {
                    if (reservation.MinPriceRes > reservation.PriceRes)
                    {
                        throw new Exception();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Price for reserving a table must be more than " + reservation.MinPriceRes);
                    return;
                }
                context.Reservations.Add(reservation);
                if (context.SaveChanges() > 0)
                {
                    MessageBox.Show("Your have ordered successfully");
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
            lbBeersCF.Items.Clear();
            lbBurgersCF.Items.Clear();
            lbCoffesCF.Items.Clear();
            lbJuicesCF.Items.Clear();
            lbWhiskyCF.Items.Clear();
            lbPizzasCF.Items.Clear();
            lbSodaCF.Items.Clear();
            loadInformations();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            using(var context = new ModelContext())
            {
                switch (ordersPb.Count)
                {
                    case 0:
                        return;
                    case 1:
                        if (ordersPb[0].Status == 2)
                        {
                            timeElapsed1++;
                            pb1.Value = timeElapsed1;
                            if (pb1.Value == pb1.Maximum)
                            {
                                ordersPb[0].Status = 3;
                                context.Entry(ordersPb[0]).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                        }
                        break;
                    case 2:
                        if (ordersPb[0].Status == 2)
                        {
                            timeElapsed1++;
                            pb1.Value = timeElapsed1;
                            if (pb1.Value == pb1.Maximum)
                            {
                                ordersPb[0].Status = 3;
                                context.Entry(ordersPb[0]).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                        }
                        if (ordersPb[1].Status == 2)
                        {
                            timeElapsed2++;
                            pb2.Value = timeElapsed2;
                            if (pb2.Value == pb2.Maximum)
                            {
                                ordersPb[1].Status = 3;
                                context.Entry(ordersPb[1]).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                        }
                        break;
                    case 3:
                        if (ordersPb[0].Status == 2)
                        {
                            timeElapsed1++;
                            pb1.Value = timeElapsed1;
                            if (pb1.Value == pb1.Maximum)
                            {
                                ordersPb[0].Status = 3;
                                context.Entry(ordersPb[0]).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                        }
                        if (ordersPb[1].Status == 2)
                        {
                            timeElapsed2++;
                            pb2.Value = timeElapsed2;
                            if (pb2.Value == pb2.Maximum)
                            {
                                ordersPb[1].Status = 3;
                                context.Entry(ordersPb[1]).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                        }
                        if (ordersPb[2].Status == 2)
                        {
                            timeElapsed3++;
                            pb3.Value = timeElapsed3;
                            if (pb3.Value == pb3.Maximum)
                            {
                                ordersPb[2].Status = 3;
                                context.Entry(ordersPb[2]).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                        }
                        break;
                    case 4:
                        if (ordersPb[0].Status == 2)
                        {
                            timeElapsed1++;
                            pb1.Value = timeElapsed1;
                            if(pb1.Value == pb1.Maximum)
                            {
                                ordersPb[0].Status = 3;
                                context.Entry(ordersPb[0]).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                        }
                        if (ordersPb[1].Status == 2)
                        {
                            timeElapsed2++;
                            pb2.Value = timeElapsed2;
                            if (pb2.Value == pb2.Maximum)
                            {
                                ordersPb[1].Status = 3;
                                context.Entry(ordersPb[1]).State = EntityState.Modified;
                                context.SaveChanges();
                            }

                        }
                        if (ordersPb[2].Status == 2)
                        {
                            timeElapsed3++;
                            pb3.Value = timeElapsed3;
                            if (pb3.Value == pb3.Maximum)
                            {
                                ordersPb[2].Status = 3;
                                context.Entry(ordersPb[2]).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                        }
                        if (ordersPb[3].Status == 2)
                        {
                            timeElapsed4++;
                            pb4.Value = timeElapsed4;
                        }
                        break;

            }

            }

        }

        private void btnReceipts_Click(object sender, EventArgs e)
        {
            var context = new ModelContext();
            loggedIn = context.Customer.Where(c => c.LoggedIn == 1).Select(c => c.CustId).FirstOrDefault();
            ReceiptForm form = new ReceiptForm();
            form.ShowDialog();
        }

        public void init()
        {
            using (var context = new ModelContext())
            {
                Customer customer = context.Customer.Where(c => c.LoggedIn == 1).FirstOrDefault();
                ordersPb = context.Orders.Where(o => o.CustId == customer.CustId && o.TimeToDeliver!=null).ToList();
                StringBuilder sb = new StringBuilder();
                lblId1.Visible = false;
                lblId2.Visible = false;
                lblId3.Visible = false;
                lblId4.Visible = false;

                lblStatus1.Visible = false;
                lblStatus2.Visible = false;
                lblStatus3.Visible = false;
                lblStatus4.Visible = false;

                pb1.Visible = false;
                pb2.Visible = false;
                pb3.Visible = false;
                pb4.Visible = false;
                for (int i = 0; i < ordersPb.Count; i++)
                {

                    sb.Append("lblId");
                    sb.Append(i + 1);
                    if (lblId1.Name == sb.ToString())
                    {
                        lblId1.Text = ordersPb[i].OrderId.ToString();
                        if (ordersPb[i].Status == 2)
                        {
                            lblStatus1.Text = "delivering";

                        }
                        else if (ordersPb[i].Status == 1)
                        {
                            lblStatus1.Text = "pending";
                        }
                        else
                        {
                            lblStatus1.Text = "delivered";
                        }
                        sb.Clear();
                        lblId1.Visible = true;
                        lblStatus1.Visible = true;
                        pb1.Visible = true;
                        if (ordersPb[i].TimeToDeliver != null)
                        {
                            pb1.Maximum = (int)ordersPb[i].TimeToDeliver * 60;
                        }
                    }
                    if (lblId2.Name == sb.ToString())
                    {
                        lblId2.Text = ordersPb[i].OrderId.ToString();
                        if (ordersPb[i].Status == 2)
                        {
                            lblStatus2.Text = "delivering";

                        }
                        else if (ordersPb[i].Status == 1)
                        {
                            lblStatus2.Text = "pending";
                        }
                        else
                        {
                            lblStatus2.Text = "delivered";
                        }
                        sb.Clear();
                        lblId2.Visible = true;
                        lblStatus2.Visible = true;
                        pb2.Visible = true;
                        if(ordersPb[i].TimeToDeliver != null)
                        {
                            pb2.Maximum = (int)ordersPb[i].TimeToDeliver * 60;
                        }
                    }
                    if (lblId3.Name == sb.ToString())
                    {
                        lblId3.Text = ordersPb[i].OrderId.ToString();
                        if (ordersPb[i].Status == 2)
                        {
                            lblStatus3.Text = "delivering";

                        }
                        else if (ordersPb[i].Status == 1)
                        {
                            lblStatus3.Text = "pending";
                        }
                        else
                        {
                            lblStatus3.Text = "delivered";
                        }
                        sb.Clear();
                        lblId3.Visible = true;
                        lblStatus3.Visible = true;
                        pb3.Visible = true;
                        if (ordersPb[i].TimeToDeliver != null)
                        {
                            pb3.Maximum = (int)ordersPb[i].TimeToDeliver * 60;
                        }
                    }
                    if (lblId4.Name == sb.ToString())
                    {
                        lblId4.Text = ordersPb[i].OrderId.ToString();
                        if (ordersPb[i].Status == 2)
                        {
                            lblStatus4.Text = "delivering";

                        }
                        else if (ordersPb[i].Status == 1)
                        {
                            lblStatus4.Text = "pending";
                        }
                        else
                        {
                            lblStatus4.Text = "delivered";
                        }
                        sb.Clear();
                        lblId4.Visible = true;
                        lblStatus4.Visible = true;
                        pb4.Visible = true;
                        if (ordersPb[i].TimeToDeliver != null)
                        {
                            pb4.Maximum = (int)ordersPb[i].TimeToDeliver * 60;
                        }
                    }
                }



            }
        }

       
    }
}
