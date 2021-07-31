using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeBar.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("orderId")]
        public int OrderId { get; set; }

        [ForeignKey("Employee")]
        [Column("empId")]
        public int EmpId { get; set; }
        public virtual Employee Employee { get; set; }

        [ForeignKey("Customer")]
        [Column("custId")]
        public int CustId { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey("Table")]
        [Column("tableId")]
        public int? TableId { get; set; }
        public virtual Table Table { get; set; }

        [Column("status")]
        public int Status { get; set; }

        [Column("timeToDeliver")]
        public int? TimeToDeliver { get; set; }

        [Column("orderAddress")]
        public string OrderAddress { get; set; }

        [Column("orderPrice")]
        public int? OrderPrice { get; set; }


        public Order()
        {

        }
        public Order(int OrderId, int Status, int? TimeToDeliver, int? TableId, int EmpId, int CustId, string OrderAddress, int? OrderPrice)
        {
            this.OrderId = OrderId;
            this.Status = Status;
            this.TimeToDeliver = TimeToDeliver;
            this.EmpId = EmpId;
            this.CustId = CustId;
            this.TableId = TableId;
            this.OrderAddress = OrderAddress;
            this.OrderPrice = OrderPrice;
        }

        private string findCust(int Id)
        {
            using (var context = new ModelContext())
            {

                Customer customer = context.Customer.Where(c => c.CustId == Id).FirstOrDefault();
                return customer.CustName;

            }
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}ден",findCust(CustId),OrderPrice);
        }
    }
}
