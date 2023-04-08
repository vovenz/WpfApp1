using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
using static WpfApp1.regin;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //login();
            string login = textBox1.Text;
            string password = textBox2.Text;
            string root = textBox3.Text;
            string myConnectionString = @"server=WILLIAM;Trusted_Connection=Yes;DataBase=Database;";
            string mySelectQuery = "SELECT [login] FROM [dbo].[users] WHERE [login] = '" + login + "'and [password]='" + password + "'and [root]='" + root + "'";
            SqlConnection myConnection = new SqlConnection(myConnectionString);
            SqlCommand cmd = new SqlCommand(mySelectQuery, myConnection);
            try
            {
                myConnection.Open();
                object obj = cmd.ExecuteScalar();
                if (obj == null || obj == DBNull.Value)
                {
                    MessageBox.Show("Такого пользователя нет");
                }
                else
                {
                    MessageBox.Show("Есть такой профиль");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("При входе в систему пользователя '" + login + "' произошла ошибка: " + ex.Message);
            }
            finally
            {
                myConnection.Close();
            }
        }
    }
}
