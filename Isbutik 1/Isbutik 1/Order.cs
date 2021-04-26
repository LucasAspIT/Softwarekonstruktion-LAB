using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isbutik_1 {

    /// <summary>
    /// Needs a string Name, and int Amount.
    /// </summary>
    public class Order {

        public Product Product { get; set; }
        
        public string Name { get { return Product.Name; } }

        public string Description { get { return Product.Description; } }

        public double UnitPrice { get { return Product.UnitPrice; } }

        public int Amount { get; set; }

        public double Price { get {
                return UnitPrice * Amount;
            } }
    }
}
