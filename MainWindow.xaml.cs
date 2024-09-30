using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        private List<Product> inventory = new List<Product>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int productId = int.Parse(txtProductID.Text);
                string name = txtName.Text;
                string category = txtCategory.Text;
                decimal price = decimal.Parse(txtPrice.Text);
                int stockQuantity = int.Parse(txtStockQuantity.Text);

                Product product;

                switch (category.ToLower())
                {
                    case "electronics":
                        int warranty = int.Parse(txtWarrantyPeriod.Text);
                        product = new Electronics
                        {
                            ProductID = productId,
                            Name = name,
                            Category = category,
                            Price = price,
                            StockQuantity = stockQuantity,
                            WarrantyPeriod = warranty
                        };
                        break;
                    case "grocery":
                        DateTime expiration = DateTime.Parse(txtExpirationDate.Text);
                        product = new Grocery
                        {
                            ProductID = productId,
                            Name = name,
                            Category = category,
                            Price = price,
                            StockQuantity = stockQuantity,
                            ExpirationDate = expiration
                        };
                        break;
                    case "clothing":
                        string size = txtSize.Text;
                        string material = txtMaterial.Text;
                        product = new Clothing
                        {
                            ProductID = productId,
                            Name = name,
                            Category = category,
                            Price = price,
                            StockQuantity = stockQuantity,
                            Size = size,
                            Material = material
                        };
                        break;
                    default:
                        throw new InvalidOperationException("Unknown category.");
                }

                if (inventory.Any(p => p.ProductID == productId))
                {
                    throw new InvalidOperationException("Product ID already exists.");
                }

                inventory.Add(product);
                MessageBox.Show("Product added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int productId = int.Parse(txtSearchProductID.Text);
                var product = inventory.FirstOrDefault(p => p.ProductID == productId);

                if (product == null)
                {
                    MessageBox.Show("Product not found.");
                    return;
                }

                txtUpdateName.Text = product.Name;
                txtUpdatePrice.Text = product.Price.ToString();
                txtUpdateStockQuantity.Text = product.StockQuantity.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int productId = int.Parse(txtSearchProductID.Text);
                var product = inventory.FirstOrDefault(p => p.ProductID == productId);

                if (product == null)
                {
                    MessageBox.Show("Product not found.");
                    return;
                }

                product.Name = txtUpdateName.Text;
                product.Price = decimal.Parse(txtUpdatePrice.Text);
                product.StockQuantity = int.Parse(txtUpdateStockQuantity.Text);

                MessageBox.Show("Product updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            var report = inventory.Select(p => new
            {
                p.Name,
                p.Category,
                p.StockQuantity
            }).ToList();

            dataGridReport.ItemsSource = report;
        }

        private void ProcessSale_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int productId = int.Parse(txtSaleProductID.Text);
                int saleQuantity = int.Parse(txtSaleQuantity.Text);
                var product = inventory.FirstOrDefault(p => p.ProductID == productId);

                if (product == null)
                {
                    MessageBox.Show("Product not found.");
                    return;
                }

                product = product - saleQuantity; // Operator overloading in action
                MessageBox.Show("Sale processed successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
    