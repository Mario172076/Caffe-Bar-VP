using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeBar.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("custId")]
        public int CustId { get; set; }

        [Column("custName")]
        public string CustName { get; set; }

        [Column("custSurname")]
        public string CustSurname { get; set; }

        [Column("custTelephone")]
        public string CustTelephone { get; set; }

        [Column("custUsername")]
        public string CustUsername { get; set; }

        [Column("custPassword")]
        public string CustPassword { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("age")]
        public string Age { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("loggedIn")]
        public int? LoggedIn { get; set; }

        public Customer()
        {

        }

        public Customer(int Id, string Address, string Email, string Age, string CustName, string CustSurname, string CustTelephone, string CustUsername, string CustPassword, int? LoggedIn)
        {
            this.CustId = CustId;
            this.Address = Address;
            this.Age = Age;
            this.Email = Email;
            this.CustName = CustName;
            this.CustSurname = CustSurname;
            this.CustTelephone = CustTelephone;
            this.CustUsername = CustUsername;
            this.CustPassword = CustPassword;
            this.LoggedIn = LoggedIn;
        }
    }
}

