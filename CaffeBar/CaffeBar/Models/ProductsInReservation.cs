using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeBar.Models
{
    [Table("ProductsInReservation")]
    public class ProductsInReservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Product")]
        [Column("productId")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("Reservation")]
        [Column("resId")]
        public int ResId { get; set; }
        public virtual Reservation Reservation { get; set; }

    }
}
