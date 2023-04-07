using System;
using System.Collections.Generic;
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
using System.Xml.Linq;
using static WpfApp1.regin;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для login.xaml
    /// </summary>
    public partial class login : Page
    {
        public MainWindow mainWindow;

        public login()
        {
            InitializeComponent();
        }


        private void regin_Click(object sender, RoutedEventArgs e)
        {
            ClassChangePage.frame1.Navigate(new regin());
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {

            {
                if (textBox_login.Text.Length > 0 && password_box.Password.Length > 0)
                {
                    DataTable dt_admins = new DataTable();
                    DataTable dt_users = new DataTable();

                    try
                    {
                        using (SqlConnection connection = new SqlConnection("server=WILLIAM;Trusted_Connection=Yes;DataBase=Database;"))
                        {
                            // запрос на выборку администраторов
                            string sql_admins_query = "SELECT * FROM [dbo].[admins] WHERE [login] = @login AND [password] = @password";
                            SqlCommand command_admins = new SqlCommand(sql_admins_query, connection);
                            command_admins.Parameters.AddWithValue("@login", textBox_login.Text);
                            command_admins.Parameters.AddWithValue("@password", password_box.Password);

                            // запрос на выборку пользователей
                            string sql_users_query = "SELECT * FROM [dbo].[users] WHERE [login] = @login AND [password] = @password";
                            SqlCommand command_users = new SqlCommand(sql_users_query, connection);
                            command_users.Parameters.AddWithValue("@login", textBox_login.Text);
                            command_users.Parameters.AddWithValue("@password", password_box.Password);

                            connection.Open();

                            // выполнение запроса администраторов
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command_admins))
                            {
                                adapter.Fill(dt_admins);
                            }

                            // выполнение запроса пользователей
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command_users))
                            {
                                adapter.Fill(dt_users);
                            }
                        }

                        if (dt_admins.Rows.Count > 0)
                        {
                            MessageBox.Show("Админ авторизовался");
                            NavigationService.Navigate(new Page2());
                            return;
                        }
                        else if (dt_users.Rows.Count > 0)
                        {
                            MessageBox.Show("Пользователь авторизовался");
                            NavigationService.Navigate(new Page1());
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Пользователь не найден");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                }
                else
                {
                    if (textBox_login.Text.Length == 0)
                    {
                        MessageBox.Show("Введите логин");
                    }
                    else
                    {
                        MessageBox.Show("Введите пароль");
                    }

                }

            }

        }

        private void textBox_login_TextChanged(object sender, TextChangedEventArgs e)
        {
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
        
    }

}