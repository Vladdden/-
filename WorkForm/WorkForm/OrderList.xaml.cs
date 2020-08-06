using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;

namespace WorkForm
{
    /// <summary>
    /// Логика взаимодействия для OrderList.xaml
    /// </summary>
    public partial class OrderList : Window
    {
        string connectionString;
        SqlDataAdapter adapter;
        SqlConnection conn;
        DataTable dtMain;
        public OrderList()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["ConnectionToWorkerForm"].ConnectionString;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                Console.WriteLine("Подключение");

                {
                    Console.WriteLine("Подключение открыто");
                    Console.WriteLine("Свойства подключения:");
                    Console.WriteLine("\tСтрока подключения: {0}", conn.ConnectionString);
                    Console.WriteLine("\tБаза данных: {0}", conn.Database);
                    Console.WriteLine("\tСервер: {0}", conn.DataSource);
                    Console.WriteLine("\tВерсия сервера: {0}", conn.ServerVersion);
                    Console.WriteLine("\tСостояние: {0}", conn.State);
                }

                UpdateDataGridView();

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // закрываем подключение
                conn.Close();
                Console.WriteLine("Подключение закрыто...");
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateDataGridView();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Сохранить изменения?", "Сохранение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SqlCommandBuilder cmdbl = new SqlCommandBuilder(adapter);
                adapter.Update(dtMain);
                UpdateDataGridView();
            }
            else
            {
                SqlCommandBuilder cmdbl = new SqlCommandBuilder(adapter);
                adapter.Fill(dtMain);
                UpdateDataGridView();
            }
        }
        private void UpdateDataGridView()
        {
            adapter = new SqlDataAdapter("SELECT * FROM Orders", conn);
            new SqlCommandBuilder(adapter);
            dtMain = new DataTable();
            adapter.Fill(dtMain);
            OrderListDataGrid.ItemsSource = dtMain.DefaultView;
        }

    }
}
