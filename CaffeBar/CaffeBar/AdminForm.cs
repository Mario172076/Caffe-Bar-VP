using CaffeBar.Migrations;
using CaffeBar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Category = CaffeBar.Models.Category;
using ProductsInOrder = CaffeBar.Models.ProductsInOrder;

namespace CaffeBar
{
    public partial class AdminForm : Form
    {
        public static int timeOfDelivery;
        public static string orderId;
        public static Order order;
        int ordersOnTime = 0;
        int ordersLate = 0;
        int totalOrders = 0;
        public static List<Order> deliveredOrders = new List<Order>();
        public static Dictionary<int, List<string>> receipts = new Dictionary<int, List<string>>();
        Random rand = new Random();
        int helper1 = 0;
        int helper2 = 0;
        int helper3 = 0;
        public AdminForm()
        {
            InitializeComponent();
            loadInformations();
            lblOrderHistory.Text = String.Format("Total number of orders made: {0}", Properties.Settings.Default.totalOrdersMade);
            lblLateOrders.Text = String.Format("Completed orders late: {0}", Properties.Settings.Default.lateOrdersMade);
            lblCompletedOrdersOnTime.Text = String.Format("Complete orders on time: {0}", Properties.Settings.Default.ordersOnTimeMade);
            helper1 = Properties.Settings.Default.totalOrdersMade;
            helper2 = Properties.Settings.Default.lateOrdersMade;
            helper3 = Properties.Settings.Default.ordersOnTimeMade;
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
                    tbCatNameAF.Clear();
                    MessageBox.Show("Categroy added succesfully");
                }

                lbTablesAF.Items.Clear();
                lbReservationsAF.Items.Clear();
                lbOrdersAF.Items.Clear();
                cbProCatAF.Items.Clear();
                loadInformations();
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
                if (tbProNameAF.Text == "")
                {
                    MessageBox.Show("Please enter a product name");
                    return;
                }
                try
                {
                    product.ProPrice = int.Parse(tbProPriceAF.Text);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Price field cannot be empty");
                    return;
                }
                try
                {
                    product.AgeRestrictions = int.Parse(cbAgeResAF.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Age restriction field cannot be empty");
                    return;
                }
                if (tbTimeServingAF.Text != "")
                {
                    product.TimeOfServing = int.Parse(tbTimeServingAF.Text);
                }
                product.ProDescription = tbProDescAF.Text;
                try
                {
                    product.ProQuantity = int.Parse(tbProQuantityAF.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Quantity field cannot be empty");
                    return;
                }
                Category cat = (Category)cbProCatAF.SelectedItem;
                try
                {
                    product.CatId = cat.CatId;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Category field cannot be empty");
                    return;
                }
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
            if (lbReservationsAF.SelectedIndex == -1)
            {
                MessageBox.Show("No reservation is selected");
                return;
            }
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
            if (tbProPriceAF.Text == "")
            {
                errorProvider1.SetError(tbProPriceAF, "Price field cannot be empty");
                e.Cancel = true;
            }
            if (!tbProPriceAF.Text.All(char.IsDigit))
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
            if (tbProQuantityAF.Text == "")
            {
                errorProvider1.SetError(tbProQuantityAF, "Quantity field cannot be empty");
                e.Cancel = true;
            }
            if (!tbProQuantityAF.Text.All(char.IsDigit))
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

        private void btnDeliverOrderAF_Click(object sender, EventArgs e)
        {
            if (lbOrdersAF.SelectedIndex != -1)
            {
                Order o = (Order)lbOrdersAF.SelectedItem;
                Random random = new Random();
                int timeToDeliver = random.Next(minValue: 15, maxValue: 31);
                timeOfDelivery = timeToDeliver;
                o.TimeToDeliver = timeToDeliver;
                // dodaj order vo FinishedOrders
                using(var context = new ModelContext())
                {
                    Order order = new Order();
                    try
                    {
                        order = context.Orders.Where(ord => ord.OrderId == o.OrderId && o.OrderAddress != null).FirstOrDefault();
                        if (order == null)
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("Please select an order that can be delivered (has an address)");
                        return;
                    }
                    
                    order.Status = 2;
                    context.Entry(order).State = EntityState.Modified;
                    context.SaveChanges();

                }
                order = o;
                updateLabels(o.TimeToDeliver > 25);
                lbOrdersAF.SelectedIndex = -1;
                MessageBox.Show("Order deivered successfully");
                deliveredOrders.Add(o);
            }
            else
            {
                MessageBox.Show("No order is selected");
            }
        }

        private void btnOrdersHistory_Click(object sender, EventArgs e)
        {
            OrderHistoryForm form = new OrderHistoryForm();
            form.ShowDialog();
        }

        private void updateLabels(bool isLate)
        {
            List<Order> list = new List<Order>();
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-EITPB7M;Initial Catalog=CaffeBar;Integrated Security=True"))
            {
                var context = new ModelContext();
                connection.Open();
                String query = "SELECT * FROM Orders o WHERE o.timeToDeliver!=null AND o.status = 3";
                SqlCommand sql = new SqlCommand(query, connection);
                SqlDataReader reader = sql.ExecuteReader();
                while(reader.Read())
                {
                    Order order = new Order();
                    order.OrderId = Convert.ToInt32(reader["orderId"]);
                    order.EmpId = Convert.ToInt32(reader["empId"]);
                    order.TableId = Convert.ToInt32(reader["tableId"]);
                    order.Status = Convert.ToInt32(reader["status"]);
                    order.TimeToDeliver = Convert.ToInt32(reader["timeToDeliver"]);
                    order.OrderAddress = reader["orderAddress"].ToString();
                    order.OrderPrice = Convert.ToInt32(reader["orderPrice"]);
                    list.Add(order);

                }
                totalOrders = list.Count;
                ordersLate = list.Where(order => order.TimeToDeliver > 25).Count();
                ordersOnTime = list.Where(order => order.TimeToDeliver <= 25).Count();
                if (isLate)
                {
                    lblLateOrders.Text = String.Format("Completed orders late: {0}", ordersLate + helper2);
                }
                else
                {
                    lblCompletedOrdersOnTime.Text = String.Format("Complete orders on time: {0}", ordersOnTime + helper3);
                }
                lblOrderHistory.Text = String.Format("Total number of orders made: {0}",  totalOrders + helper1);
                Properties.Settings.Default.totalOrdersMade = totalOrders + helper1;
                Properties.Settings.Default.lateOrdersMade = ordersLate + helper2;
                Properties.Settings.Default.ordersOnTimeMade = ordersOnTime + helper3;
                lbTablesAF.Items.Clear();
                lbReservationsAF.Items.Clear();
                lbOrdersAF.Items.Clear();
                loadInformations();
            }
        }

        private void btnReceiptOrderAF_Click(object sender, EventArgs e)
        {
            var context = new ModelContext();
            if (lbOrdersAF.SelectedIndex == -1)
            {
                MessageBox.Show("No order is selected");
                return;
            }
            else
            {
                Order o = (Order)lbOrdersAF.SelectedItem;

                if(o.OrderAddress!="" && o.OrderAddress != null && o.Status == 1)
                {
                    MessageBox.Show("Please deliver the order first");
                    return;
                }
                
                List<Order> orders = context.Orders.Where(or => (or.TableId!=null && or.Status==1) || (or.OrderAddress!=null && or.Status==2)).ToList();
                bool flag = false;
                foreach(Order order in orders)
                {
                    if (order.OrderId == o.OrderId)
                    {
                        flag = true;
                        o = order;
                    }
                }
                if(flag==true)
                {
                    // pusti smetka
                    StringBuilder sb = new StringBuilder();
                    String empName = context.Employee.Where(emp => emp.EmpId == o.EmpId).Select(emp => emp.EmpName).FirstOrDefault();
                    sb.Append("ReceiptID: ").Append(rand.Next()).Append(" OrderID: ").Append(o.OrderId).Append(" Employee Name: ").Append(empName).Append(" Products: ");
                    List<ProductsInOrder> productsOrdered = context.ProductsInOrder.Where(x => x.OrderId == o.OrderId).ToList();
                    List<Product> products = new List<Product>();
                    foreach (ProductsInOrder pio in productsOrdered)
                    {
                        Product product = context.Products.Where(p => p.ProId == pio.ProductId).FirstOrDefault();
                        products.Add(product);
                    }
                   
                    foreach (Product product in products)
                    {
                        sb.Append(product.ProName).Append(" ");
                    }
                    sb.Append("Price: " + o.OrderPrice);
                    if (receipts.ContainsKey(o.CustId))
                    {
                        List<string> list = receipts[o.CustId];
                        list.Add(sb.ToString());
                        receipts[o.CustId] = list;
                    }
                    else
                    {
                        List<string> list = new List<string>();
                        list.Add(sb.ToString());
                        receipts[o.CustId] = list;
                    }
                    o.Status = 3;
                    Table table = new Table();
                    if (context.Tables.Where(t => t.TableId == o.TableId).FirstOrDefault() != null)
                    {
                        table = context.Tables.Where(t => t.TableId == o.TableId).FirstOrDefault();
                        table.TableAvalaible = true;
                        context.Entry(table).State = EntityState.Modified;
                    }

                   
                    context.Entry(o).State = EntityState.Modified;
                   
                    context.SaveChanges();
                    MessageBox.Show("Receipt delivered successfully");
                }
                else
                {
                    MessageBox.Show("Order has not been delivered! Deliver it first");
                    return;
                }
                lbTablesAF.Items.Clear();
                lbReservationsAF.Items.Clear();
                lbOrdersAF.Items.Clear();
                loadInformations();
            }
        }
    }
}
