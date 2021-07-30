using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeBar.Models
{
    [Table("Reservations")]
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("resId")]
        public int ResId { get; set; }

        [ForeignKey("Customer")]
        [Column("custId")]
        public int CustId { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey("Table")]
        [Column("tableId")]
        public int TableId { get; set; }
        public virtual Table Table { get; set; }

        [Column("dateRes")]
        public DateTime DateRes { get; set; }

        [Column("numPeople")]
        public int NumPeople{ get; set; }

        [Column("MinPriceRes")]
        public int? MinPriceRes{ get; set; }

        [Column("priceRes")]
        public int? PriceRes { get; set; }

        public Reservation()
        {

        }

        public Reservation(int ResId, int CustId, int TableId, DateTime DateRes, int NumPeople, int? MinPriceRes, int? PriceRes)
        {
            this.ResId = ResId;
            this.CustId = CustId;
            this.TableId = TableId;
            this.DateRes = DateRes;
            this.NumPeople = NumPeople;
            this.MinPriceRes = MinPriceRes;
            this.PriceRes = PriceRes;
            
        }
    }
}
