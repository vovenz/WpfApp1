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
using System.Xml;
using System.Xml.Serialization;
using WpfApp1.Models;
using static WpfApp1.regin;

namespace WpfApp1
{

    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {

        public Page1()
        {
            InitializeComponent();

            Loaded += Page1_Loaded;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProductsGrid.ItemsSource = Class1.db.products.ToList();
        }

        private void Page1_Loaded(object sender, RoutedEventArgs e)
        {
            ProductsGrid.ItemsSource = Class1.db.products.ToList();
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

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page3());
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите изменить товар?", "уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Class1.db.SaveChanges();
                ProductsGrid.ItemsSource = Class1.db.products.ToList();
                MessageBox.Show("Изменено!");
            }
            Class1.db.SaveChanges();
        }

        private void RemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить товар?", "уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var CurrentClient = ProductsGrid.SelectedItem as products;
                Class1.db.products.Remove(CurrentClient);
                Class1.db.SaveChanges();
                ProductsGrid.ItemsSource = Class1.db.products.ToList();
                MessageBox.Show("Удалено!");
            }
        }
    }
}
