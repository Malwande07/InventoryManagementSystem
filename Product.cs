using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSystem;

namespace InventoryManagementSystem
{
     class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public static Product operator +(Product p, int quantity)
        {
            p.StockQuantity += quantity;
            return p;
        }

        public static Product operator -(Product p, int quantity)
        {
            if (p.StockQuantity >= quantity)
                p.StockQuantity -= quantity;
            else
                throw new InvalidOperationException("Insufficient stock.");
            return p;
        }
    }

    public class Electronics : Product
    {
        public int WarrantyPeriod { get; set; } // Warranty in years
    }

    public class Grocery : Product
    {
        public DateTime ExpirationDate { get; set; }
    }

    public class Clothing : Product
    {
        public string Size { get; set; }
        public string Material { get; set; }
    }
}
    
