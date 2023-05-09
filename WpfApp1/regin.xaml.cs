using System;
using System.Collections.Generic;
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
using WpfApp1.Models;
using static WpfApp1.regin;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для regin.xaml
    /// </summary>
    public partial class regin : Page
    {
        public MainWindow mainWindow;

        public class change_class
        {
            public static bool ThemeChanged = false;
            public static bool isAction1 = true;
        }

        public regin()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            ClassChangePage.frame1.Navigate(new login());
        }

        private void reg_Click(object sender, RoutedEventArgs e)
        {
            if (textBox_login.Text.Length > 0)
            {
                if (password_box.Password.Length > 0)
                {
                    if (password_box.Password.Length >= 6)
                    {
                        bool en = true; // английская раскладка
                        bool number = false;

                        for (int i = 0; i < password_box.Password.Length; i++)
                        {
                            if (password_box.Password[i] >= 'А' && password_box.Password[i] <= 'Я') en = false; // если русская раскладка
                            if (password_box.Password[i] >= '0' && password_box.Password[i] <= '9') number = true; // если цифры
                        }

                        if (!en)
                            MessageBox.Show("Доступна только английская раскладка");
                        else if (!number)
                            MessageBox.Show("Добавьте хотя бы одну цифру");
                        if (en && number)
                        {
                            if (password_Copy.Password.Length > 0)
                            {
                                if (password_box.Password == password_Copy.Password)
                                {
                                    string login = textBox_login.Text;
                                    string password = password_box.Password;
                                    string root = "Пользователь";

                                    using (var DataBase = new DatabaseEntities7())
                                    {

                                        var isUserExists = DataBase.users.Any(u => u.login == login);

                                        if (isUserExists)
                                        {
                                            MessageBox.Show("Такой пользователь уже существует");
                                        }
                                        else
                                        {
                                            //var user = new users { login = login, password = password };
                                            //DataBase.users.Add(user);
                                            //DataBase.SaveChanges();
                                            regin registration = new regin();
                                            InsertUser(login, password, root); // Метод InsertUser лежит в модели DB
                                            MessageBox.Show("Пользователь зарегистрирован");
                                            ClassChangePage.frame1.Navigate(new login());
                                        }
                                    }
                                }
                                else MessageBox.Show("Пароли не совпадают");
                            }
                            else MessageBox.Show("Повторите пароль");
                        }
                    }
                    else MessageBox.Show("Пароль слишком короткий, минимум 6 символов");
                }
                else MessageBox.Show("Укажите пароль");
            }
            else MessageBox.Show("Укажите логин");
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

        private static bool InsertUser(string login, string password, string root)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection("Server=WILLIAM;Database=Database;Trusted_Connection=True;MultipleActiveResultSets=True;"))
            {
                string query = "INSERT INTO users (login, password, root) VALUES (@login, @password, @root)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    try
                    {
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@root", root);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            result = true;
                        }
                        else
                        {
                            MessageBox.Show("Error: User not inserted");
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            return result;
        }
    }
}
