using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Strore.Models;

namespace Web_Strore.ModelViews
{
    public class ProductHomeVM
    {
        public Category category { get; set; }
        public List<Product> lsProducts { get; set; }

    }
}
