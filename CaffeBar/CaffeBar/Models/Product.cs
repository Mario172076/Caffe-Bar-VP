using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeBar.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("proId")]
        public int ProId { get; set; }

        [ForeignKey("ProCategory")]
        [Column("catId")]
        public int CatId { get; set; }
        public virtual Category ProCategory { get; set; }

        [Column("proPrice")]
        public int ProPrice { get; set; }

        [Column("proName")]
        public string ProName { get; set; }

        [Column("timeOfServing")]
        public int? TimeOfServing { get; set; }

        [Column("ageRestrictions")]
        public int AgeRestrictions { get; set; }

        [Column("proDescription")]
        public string ProDescription { get; set; }

        [Column("proQuantity")]
        public int ProQuantity { get; set; }


        public Product()
        {

        }

        public Product(int ProId, int ProPrice, string ProName, int? TimeOfServing, int AgeRestrictions, string ProDescription, int ProQuantity, int CatId)
        {
            this.ProId = ProId;
            this.ProPrice = ProPrice;
            this.ProName = ProName;
            this.TimeOfServing = TimeOfServing;
            this.AgeRestrictions = AgeRestrictions;
            this.ProDescription = ProDescription;
            this.ProQuantity = ProQuantity;
            this.CatId=CatId;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} - {2} - {3}", ProName, ProPrice, ProDescription, ProCategory.CatName);
        }
    }
}

