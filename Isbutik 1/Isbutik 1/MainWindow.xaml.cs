﻿using System;
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
            ProductOverview.Add(new Product() { Name = "Magnum", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque tincidunt sodales augue a laoreet. Nulla consectetur pharetra justo ut finibus. Duis auctor, urna nec consequat imperdiet.", UnitPrice = 11.5 });
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
                    OrderList.Add(new Order() { Name = cbxIceChoice.SelectedValue.ToString(), Amount = selectedAmount });
                    dgOrderInfo.Items.Refresh();
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
            OrderList.RemoveAt(dgOrderInfo.SelectedIndex);
            dgOrderInfo.Items.Refresh();
            }
            // If nothing is selected, and therefore the index is out of range
            catch (ArgumentOutOfRangeException) {
                MessageBox.Show("Vælg det fra listen du vil slette.");
            }
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e) {

        }

    }
}
