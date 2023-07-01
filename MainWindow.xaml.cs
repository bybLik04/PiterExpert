using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PiterExp
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private string first_table;
        private string selectedTable;
        private string GetConnectionString()
        {
            return "Server=127.0.0.1;DATABASE=piterexpert;UID=employee; PASSWORD=pexp1488;";
        }
        private void ExecuteQuery(string query)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        dtGrid.DataContext = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при работе с базой данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public Window1()
        {
            InitializeComponent();

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Cancel_btn.Visibility = Visibility.Hidden;
            Save_btn.Visibility = Visibility.Hidden;
            Load();
            ExecuteQuery("SELECT * FROM " + "`" + first_table + "`");
        }
        private void Load()
        {
            const string sql = "SHOW TABLES";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(cmd.ExecuteReader());

                        foreach (DataRow row in dataTable.Rows)
                        {
                            tableComboBox.Items.Add(row[0]);
                        }
                        first_table = tableComboBox.Items.GetItemAt(1).ToString();
                        selectedTable = tableComboBox.Items.GetItemAt(1).ToString();
                        conn.Close();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Нет доступа к Базе Данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTable = tableComboBox.SelectedItem.ToString(); // Получите имя выбранной таблицы

            string query = "SELECT * FROM " + "`"+selectedTable+"`"; // Создайте запрос для выборки данных из выбранной таблицы
            ExecuteQuery(query);
        }
        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            var dataTable = ((DataView)dtGrid.ItemsSource)?.Table;
            string sql = $"SELECT * FROM " + "`" + selectedTable + "`";

            try
            {
                using (var conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    using (var adapter = new MySqlDataAdapter())
                    {
                        adapter.SelectCommand = new MySqlCommand(sql, conn);

                        var builder = new MySqlCommandBuilder(adapter);
                        adapter.InsertCommand = builder.GetInsertCommand();
                        adapter.UpdateCommand = builder.GetUpdateCommand();
                        adapter.DeleteCommand = builder.GetDeleteCommand();

                        adapter.Update(dataTable);

                        MessageBox.Show("Изменения сохранены.");
                        Cancel_btn.Visibility = Visibility.Hidden;
                        Save_btn.Visibility = Visibility.Hidden;

                        ExecuteQuery(sql);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void dtGrid_KeyDown(object sender, KeyEventArgs e)
        {
            Cancel_btn.Visibility = Visibility.Visible;
            Save_btn.Visibility = Visibility.Visible;
        }
        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            ExecuteQuery($"SELECT * FROM " + "`" + selectedTable + "`");
            Cancel_btn.Visibility = Visibility.Hidden;
            Save_btn.Visibility = Visibility.Hidden;
        }
    }
}
