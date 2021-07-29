using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeBar.Models
{
    [Table("ProductsInOrder")]
    public class ProductsInOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Product")]
        [Column("productId")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("Order")]
        [Column("orderId")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public ProductsInOrder()
        {
        }

        public ProductsInOrder(int id, int productId, int orderId)
        {
            Id = id;
            ProductId = productId;
            OrderId = orderId;
        }
    }
}
