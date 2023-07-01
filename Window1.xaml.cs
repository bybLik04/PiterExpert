using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace PiterExp
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        private string GetConnectionString()
        {
            return "Server=127.0.0.1;DATABASE=piterexpert;UID=root; PASSWORD=1234;";
        }

        private void ExecuteQuery(string query, DataTable dt, DataGrid dtGrid)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        connection.Open();
                        dt.Load(cmd.ExecuteReader());
                        dtGrid.DataContext = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при работе с базой данных: " + ex.Message);
            }
        }
        public Window1()
        {
            InitializeComponent();

            PopulateTableComboBox();

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedTable = tableComboBox.SelectedItem.ToString(); // Получите имя выбранной таблицы

            DataTable dt = new DataTable();
            string query = "SELECT * FROM " + selectedTable; // Создайте запрос для выборки данных из выбранной таблицы
            ExecuteQuery(query, dt, dtGrid);
        }

        private List<string> GetAvailableTablesForUser()
        {
            List<string> availableTables = new List<string>();

            availableTables.Add("`договор`");
            availableTables.Add("`договор-услуга`");
            availableTables.Add("`клиенты`");
            availableTables.Add("`клиенты-льготы`");
            availableTables.Add("`льготы`");
            availableTables.Add("`система_налогооблажения-клиент`");
            availableTables.Add("`система_налогообложения`");
            availableTables.Add("`сотрудники`");
            availableTables.Add("`услуги`");
            availableTables.Add("`услуги вэд`");
                
            return availableTables;

        }
        public void PopulateTableComboBox()
        {
            //доступные таблицы для текущего пользователя
            List<string> availableTables = GetAvailableTablesForUser();


            tableComboBox.Items.Clear();

            //каждую таблицу в ComboBox
            foreach (string tableName in availableTables)
            {
                tableComboBox.Items.Add(tableName);
            }
        }
    }
}
