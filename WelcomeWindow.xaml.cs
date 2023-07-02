using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using MySqlConnector;
using System.Security.Policy;
using System.Data;

namespace PiterExp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string hpass;
        private string salt;
        private string GetConnectionString()
        {
            return "Server=127.0.0.1;DATABASE=piterexpert;UID=root; PASSWORD=123456;";//поменял пароль на свой от моей бд
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*MessageBoxResult result = MessageBox.Show("Вы действительно хотите закрыть приложение?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {

                Application.Current.Shutdown();
            } */

            ExitMessageBox exitMessageBox = new ExitMessageBox();
            exitMessageBox.ShowDialog();

            if (exitMessageBox.DialogResult)
            {
                Application.Current.Shutdown();
            }
            else
            {
                exitMessageBox.Close();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btn_bd_Click(object sender, RoutedEventArgs e)
        {
            GetPass();
            string pass = HashPassword(pass_box.Password.ToString(), salt);
            if (pass == hpass)
            {
                this.Hide();
                Window1 window1 = new Window1();
                window1.Show();
                this.Close();

            }
            else
            {
                pass_box.ToolTip = "Неверный пароль!";
                pass_box.Background = Brushes.Red;
            }
        }
        public void GetPass()
        {
            try
            {
                string sql = "Select * from пароли WHERE id = 1";
                using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        using (MySqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                hpass = dataReader.GetString(1);
                                salt = dataReader.GetString(2);
                            }
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нет доступа к Базе Данных!\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static string HashPassword(string password, string salt)
        {
            string combinedString = password + salt;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(combinedString);
                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    stringBuilder.Append(hash[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
