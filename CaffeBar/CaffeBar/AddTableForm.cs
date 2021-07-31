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
    public partial class AddTableForm : Form
    {
        public Table table { get; set; }
        public Employee employee { get; set; }

        public List<Employee> employees = new List<Employee>();
        public AddTableForm()
        {
            InitializeComponent();
            loadInformations();
        }

        private void loadInformations()
        {
            using (var context = new ModelContext())
            {
                employees = context.Employee.ToList();
                List<Table> tables = context.Tables.ToList();
                foreach(Employee emp in employees)
                {
                    int counter = 0;
                    foreach(Table t in tables)
                    {
                        if (emp.EmpId == t.EmpId)
                        {
                            counter++;
                            if (counter == 5)
                            {
                                employees.Remove(emp);
                                break;
                            }
                                
                        }
                        
                    }
                   
                }

                foreach (Employee empl in employees)
                {
                    cbEmployeeATF.Items.Add(empl);
                }

            }
        }

        private void btnAddTableATF_Click(object sender, EventArgs e)
        {
            using (var context = new ModelContext())
            {
                table = new Table();
                employee = (Employee)cbEmployeeATF.SelectedItem;
                table.EmpId = employee.EmpId;
                table.NumberOfSeats = int.Parse(tbNumSeatsATF.Text);
                table.TableAvalaible = bool.Parse(cbAvalaibleATF.Text);
                context.Tables.Add(table);
                if(context.SaveChanges() > 0)
                {
                    MessageBox.Show("Table added successfully");
                }
                DialogResult = DialogResult.OK;
            }
           
        }

        private void btnCancelATF_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        //validation for adding table

        private void tbNumSeatsATF_Validating(object sender, CancelEventArgs e)
        {
            if (!tbNumSeatsATF.Text.All(char.IsDigit))
            {
                errorProvider1.SetError(tbNumSeatsATF, "Please enter only a number");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tbNumSeatsATF, null);
                e.Cancel = false;
            }
        }

        private void cbAvalaibleATF_Validating(object sender, CancelEventArgs e)
        {
            if (cbAvalaibleATF.SelectedItem == null)
            {
                errorProvider1.SetError(cbAvalaibleATF, "Please select if table is avalaible");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(cbAvalaibleATF, null);
                e.Cancel = false;
            }
        }

        private void cbEmployeeATF_Validating(object sender, CancelEventArgs e)
        {
            if (cbEmployeeATF.SelectedItem == null)
            {
                errorProvider1.SetError(cbEmployeeATF, "Please select employee");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(cbEmployeeATF, null);
                e.Cancel = false;
            }
        }
    }
}
