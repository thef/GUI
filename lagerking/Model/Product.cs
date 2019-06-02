using System;
using System.ComponentModel.DataAnnotations;

namespace lagerking
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(25)]
        public string Name { get; set; }

        [Required, StringLength(40)]
        public string Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock must be atleast: 0!")]
        public int Stock { get; set; }

       
        public Decimal Price { get; set; }

        [Required, StringLength(40)]
        public string Department { get; set; }

        //public List<Cart.cart> Carts { get; set; }
    }
}