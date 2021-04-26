using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Isbutik_1 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            // Sets the default source of the bindings to MainWindow
            DataContext = this;

            // Run the method with the product information
            ComboboxIceList();
        }

        public Product ChosenProducts { get; set; }

        // Create a list that holds items from the "Order" class
        public List<Order> OrderList { get; set; } = new List<Order>();

        // Create a list that holds items from the "Product" class
        public List<Product> ProductOverview { get; set; } = new List<Product>();

        /// <summary>
        /// Creates the products with their name, description and price.
        /// </summary>
        private void ComboboxIceList() {
            ProductOverview.Add(new Product() { Name = "Magnum", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque tincidunt sodales augue a laoreet. Nulla consectetur pharetra justo ut finibus. Duis auctor, urna nec consequat imperdiet.", UnitPrice = 11.50 });
            ProductOverview.Add(new Product() { Name = "Astronaut", Description = "Lorem ipsum curabitur massa ipsum, egestas sed orci eu, consequat pretium neque. Nullam eget tempor massa, sit amet faucibus enim. Integer accumsan metus ac ex sodales, sit amet dapibus.", UnitPrice = 9.50 });
            ProductOverview.Add(new Product() { Name = "Lillebror", Description = "Lorem ipsum aenean tincidunt mi mauris. Nullam elit nunc, luctus ac velit sit amet, imperdiet ornare nisi. Sed ut nulla nec mi imperdiet malesuada in elit vel pulvinar venenatis efficitur.", UnitPrice = 7.00 });
            ProductOverview.Add(new Product() { Name = "Kung Fu", Description = "Lorem ipsum orci id laoreet placerat, ligula leo hendrerit nisl, sit amet maximus nulla orci in libero non pellentesque erat, ut venenatis maecenas lobortis posuere dolor ut finibus gravida.", UnitPrice = 8.00 });
        }

        private void cbxIceChoice_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ChosenProducts = e.AddedItems[0] as Product;

            // Change to the product description
            tbDescription.Text = ChosenProducts.Description;

            // Create a formatted string of the unit price, properly showing two (N2) decimals
            string formattedPrice = String.Format("{0:N2}", ChosenProducts.UnitPrice);
            tbPrice.Text = formattedPrice;
        }

        /// <summary>
        /// When the button "btnIceAdd" is clicked, add a new "Order" to the "OrderList" that contains the name and the amount of the ice cream, then refresh the list.
        /// </summary>
        private void btnIceAdd_Click(object sender, RoutedEventArgs e) {
            try {
                int selectedAmount = int.Parse(tbxIceAmount.Text);

                if (selectedAmount > 0 && selectedAmount <= 255) {
                    OrderList.Add(new Order() { Product = ChosenProducts, Amount = selectedAmount });
                    Refresh();
                }
                else if (selectedAmount <= 0) {
                    MessageBox.Show("Vælg venligst minimum 1 is.");
                }
                else if (selectedAmount > 255) {
                    MessageBox.Show("Vælg venligst højst 255 is ad gangen.");
                }
                else {
                    MessageBox.Show("Ukendt fejl.");
                }

                DataGridCheck();
            }
            catch (FormatException) {
                MessageBox.Show("Indtast venligst et gyldigt nummer.");
            }
            catch (OverflowException) {
                MessageBox.Show("Vælg venligst et antal mellem 1 og 255.");
            }
        }

        /// <summary>
        /// Delete the selected list item using the currently selected index in the datagrid
        /// </summary>
        private void btnRemove_Click(object sender, RoutedEventArgs e) {
            try {
                // If there currently is a selected item in the DataGrid OrderInfo (-1 means the selection is empty)
                if (dgOrderInfo.SelectedIndex != -1) {
                    MessageBoxResult result = MessageBox.Show("Er du sikker på at du vil fjerne den valgte is fra bestillingslisten?", "Fjern fra bestillingslisten", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes) {
                        OrderList.RemoveAt(dgOrderInfo.SelectedIndex);
                        Refresh();
                        DataGridCheck();
                    }
                }
            }
            // If nothing is selected, and therefore the index is out of range
            catch (ArgumentOutOfRangeException) {
                MessageBox.Show("Vælg det fra listen du vil slette.");
            }
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e) {
            if (OrderList.Count == 0) {
                MessageBox.Show("Venligst vælg is og antal før du prøver at bestille.");
            }
        }


        // Checks if it's an arabic numeral (0-9)
        Regex tbxIsNumber = new Regex("^[0-9]+$");

        /// <summary>
        /// If it's a number in the textbox, make sure the background is white and the add button is enabled
        /// <br>If the textbox is empty or there's anything other than a number, make the background red and disable the add button</br>
        /// </summary>
        private void tbxIceAmount_TextChanged(object sender, TextChangedEventArgs e) {
            if (tbxIsNumber.IsMatch(tbxIceAmount.Text)) {
                tbxIceAmount.Background = Brushes.White;

                if (btnIceAdd != null) {
                btnIceAdd.IsEnabled = true;
                }
            }
            else {
                tbxIceAmount.Background = Brushes.Red;
                btnIceAdd.IsEnabled = false;
            }
        }

        /// <summary>
        /// Calculate the total price of the order list
        /// </summary>
        public double TotalPrice {
            get {
                double totalPrice = 0;
                foreach (var Order in OrderList) {
                    totalPrice += Order.Price;
                }
                return totalPrice;
            }
        }

        /// <summary>
        /// Enables the order and remove button if the grid is not empty
        /// </summary>
        private void DataGridCheck() {
            if (dgOrderInfo.Items.Count >= 1) {
                btnOrder.IsEnabled = true;
                btnRemove.IsEnabled = true;
            }
            else {
                btnOrder.IsEnabled = false;
                btnRemove.IsEnabled = false;
            }
        }

        /// <summary>
        /// Updates the TotalPrice on the GUI and refreshes the DataGrid
        /// </summary>
        private void Refresh() {
            string formattedPrice = String.Format("{0:N2}", TotalPrice);
            tbTotalPrice.Text = formattedPrice;
            // tbTotalPrice.Text = TotalPrice.ToString();
            dgOrderInfo.Items.Refresh();
        }
    }
}
