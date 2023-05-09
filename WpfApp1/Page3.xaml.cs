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
using WpfApp1.Models;
using static WpfApp1.regin;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseEntities7())
            {
                string typeOfClothing = Name.Text;
                string decorFirst = Quantity.Text;
                string decorSecond = Price.Text;

                context.products.Add(new products
                {
                    Name = typeOfClothing,
                    Quantity = decorFirst,
                    Price = decorSecond,
                });
                int result = context.SaveChanges();

                if (result > 0)
                {
                    MessageBox.Show("Товар добавлен!");
                }
                else
                {
                    MessageBox.Show("Что-то пошло не так. Попробуйте еще раз.");
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
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
    }
}
