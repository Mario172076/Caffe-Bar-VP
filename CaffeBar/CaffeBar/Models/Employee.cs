using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeBar.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("empid")]
        public int EmpId { get; set; }

        [Column("empName")]
        public string EmpName { get; set; }

        [Column("empSurname")]
        public string EmpSurname { get; set; }

        [Column("empTelephone")]
        public string EmpTelephone { get; set; }

        [Column("empUsername")]
        public string EmpUsername { get; set; }

        [Column("empPassword")]
        public string EmpPassword { get; set; }

        [Column("pay")]
        public int? Pay { get; set; }

        [Column("payPerHour")]
        public int PayPerHour { get; set; }

        [Column("workHours")]
        public int WorkHours { get; set; }

        [Column("loggedIn")]
        public int? LoggedIn { get; set; }

        public Employee()
        {

        }

        public Employee(int EmpId, int PayPerHour, int WorkHours, string EmpName, string EmpSurname, string EmpTelephone, string EmpUsername, string EmpPassword, int? LoggedIn)
        {
            this.EmpId = EmpId;
            this.PayPerHour = PayPerHour;
            this.WorkHours = WorkHours;
            Pay = WorkHours * PayPerHour;
            this.EmpName = EmpName;
            this.EmpSurname = EmpSurname;
            this.EmpTelephone = EmpTelephone;
            this.EmpUsername = EmpUsername;
            this.EmpPassword = EmpPassword;
            this.LoggedIn = LoggedIn;
        }
    }
}



