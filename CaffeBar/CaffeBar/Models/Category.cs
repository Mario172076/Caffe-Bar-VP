using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeBar.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("catId")]
        public int CatId { get; set; }

        [Column("catName")]
        public string CatName { get; set; }

        public Category()
        {

        }

        public Category(int CatId, string CatName)
        {
            this.CatId = CatId;
            this.CatName = CatName;
        }

        public override string ToString()
        {
            return string.Format("{0}", CatName);
        }
    }
}
