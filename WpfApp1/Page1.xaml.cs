using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
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
using System.Xml.Serialization;
using static WpfApp1.regin;

namespace WpfApp1
{
    
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private List<Product> _products;

        public Page1()
        { 
            InitializeComponent();

            _products = Storage.GetProducts();

            ProductsGrid.ItemsSource = _products;
        }

        public class Product
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
        }

        public class Storage
        {
            private const string FILE_NAME = @"C:\temp\products.xml";

            public static List<Product> LoadProducts()
            {
                if (!File.Exists(FILE_NAME))
                {
                    return new List<Product>();
                }

                var serializer = new XmlSerializer(typeof(List<Product>));

                using (var stream = File.OpenRead(FILE_NAME))
                {
                    return (List<Product>)serializer.Deserialize(stream);
                }
            }

            public static void SaveProducts(List<Product> products)
            {
                var serializer = new XmlSerializer(typeof(List<Product>));

                using (var stream = File.Create(FILE_NAME))
                {
                    serializer.Serialize(stream, products);
                }
            }

            public static void AddProduct(Product product)
            {
                var products = LoadProducts();

                var existingProduct = products.FirstOrDefault(p => p.Name == product.Name);

                if (existingProduct != null)
                {
                    existingProduct.Quantity += product.Quantity;
                    existingProduct.Price = product.Price;
                }
                else
                {
                    products.Add(product);
                }

                SaveProducts(products);
            }

            public static void RemoveProduct(Product product)
            {
                var products = LoadProducts();

                var existingProduct = products.FirstOrDefault(p => p.Name == product.Name);

                if (existingProduct != null)
                {
                    products.Remove(existingProduct);

                    SaveProducts(products);
                }
            }

            public static void EditProduct(Product product)
            {
                RemoveProduct(product);
                AddProduct(product);
            }

            public static List<Product> GetProducts()
            {
                return LoadProducts();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClassChangePage.frame1.Navigate(new login());
        }

        private void change_theme_Click(object sender, RoutedEventArgs e)
        {
            BrushConverter converter = new BrushConverter();

            if (change_class.isAction1)
            {
                this.Background = (Brush)converter.ConvertFromString("#FF1F1F1F");
                change_class.ThemeChanged = true;
            }
            else
            {
                this.Background = (Brush)converter.ConvertFromString("#FFFFFF");
                change_class.ThemeChanged = false;
            }
            change_class.isAction1 = !change_class.isAction1;
        }

        private void ShowProductForm(Product product = null)
        {
            if (product != null)
            {
                NameTextBox.Text = product.Name;
                QuantityTextBox.Text = product.Quantity.ToString();
                PriceTextBox.Text = product.Price.ToString();
            }
            else
            {
                NameTextBox.Text = "";
                QuantityTextBox.Text = "";
                PriceTextBox.Text = "";
            }

            ProductsGrid.IsEnabled = false;
            ProductForm.Visibility = Visibility.Visible;
        }

        private void HideProductForm()
        {
            ProductsGrid.IsEnabled = true;
            ProductForm.Visibility = Visibility.Collapsed;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            ShowProductForm();
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = ProductsGrid.SelectedItem as Product;

            if (product != null)
            {
                ShowProductForm(product);
            }
        }

        private void RemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = ProductsGrid.SelectedItem as Product;

            if (product != null)
            {
                Storage.RemoveProduct(product);

                _products = Storage.GetProducts();

                ProductsGrid.ItemsSource = _products;
            }
        }

        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            var name = NameTextBox.Text;
            var quantity = int.Parse(QuantityTextBox.Text);
            var price = double.Parse(PriceTextBox.Text);

            var product = new Product
            {
                Name = name,
                Quantity = quantity,
                Price = price
            };

            Storage.AddProduct(product);

            _products = Storage.GetProducts();

            ProductsGrid.ItemsSource = _products;

            HideProductForm();
        }

        private void CancelProduct_Click(object sender, RoutedEventArgs e)
        {
            HideProductForm();
        }

    }
}
