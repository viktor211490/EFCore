using System;
using System.Collections.Generic;
using System.Text;

namespace WorkingWithEFCore
{
    class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public Category()
        {
            this.Products = new List<Product>();
        }
    }
}
