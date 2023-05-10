using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Strore.Models;

namespace Web_Strore.ModelViews
{
    public class HomeViewVM
    {
        public List<ProductHomeVM> products { get; set; }
        public List<TinDang> TinTucs { get; set; }
        public QuangCao quangCao { get; set; }

    }
}
