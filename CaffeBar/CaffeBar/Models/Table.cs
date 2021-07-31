using CaffeBar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeBar
{
    [Table("Tables")]
    public class Table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("tableId")]
        public int TableId { get; set; }

        [ForeignKey("Employee")]
        [Column("empId")]
        public int EmpId { get; set; }
        public virtual Employee Employee { get; set; }

        [Column("numberOfSeats")]
        public int NumberOfSeats { get; set; }

        [Column("tableAvalaible")]
        public bool TableAvalaible { get; set; }

        public Table()
        {

        }

        public Table(int TableId, int EmpId, int NumberOfSeats, bool TableAvalaible)
        {
            this.TableId = TableId;
            this.EmpId = EmpId;
            this.NumberOfSeats = NumberOfSeats;
            this.TableAvalaible = TableAvalaible;
        }

        public override string ToString()
        {
            return string.Format("T Number:{0} - Seats:{1}", TableId, NumberOfSeats);
        }
    }
}

