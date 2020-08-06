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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkForm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString;
        SqlDataAdapter adapter;
        SqlConnection conn;
        DataTable dtMain;
        string Sex = null;

        public MainWindow()
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

                UpdateDataGridView("Workers");
                UpdateDataGridView("Subdivisions");
                UpdateDataGridView("Orders");

                SqlDataAdapter da = new SqlDataAdapter("Select * FROM Subdivisions", conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Subdivisions");
                WorkerComboBox.ItemsSource = ds.Tables[0].DefaultView;
                WorkerComboBox.DisplayMemberPath = ds.Tables[0].Columns["Name"].ToString();
                WorkerComboBox.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();

                da = new SqlDataAdapter("Select * FROM Workers", conn);
                ds = new DataSet();
                da.Fill(ds, "Workers");
                SubdivisionComboBox.ItemsSource = ds.Tables[0].DefaultView;
                SubdivisionComboBox.DisplayMemberPath = ds.Tables[0].Columns["Surname"].ToString();
                SubdivisionComboBox.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();

                da = new SqlDataAdapter("Select * FROM Workers", conn);
                ds = new DataSet();
                da.Fill(ds, "Workers");
                OrderComboBox.ItemsSource = ds.Tables[0].DefaultView;
                OrderComboBox.DisplayMemberPath = ds.Tables[0].Columns["Surname"].ToString();
                OrderComboBox.SelectedValuePath = ds.Tables[0].Columns["ID"].ToString();

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

        private void AddWorkerBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (SexRBF.IsChecked == true) Sex = "Ж";
            else if (SexRBM.IsChecked == true) Sex = "М";
            
            if (WorkerSurnameField.Text != "Введите фамилию" && WorkerNameField.Text != "Введите имя" && WorkerPatronymicField.Text != "Введите отчество" && WorkerBthDayDatePicker.Text != null && Sex != null && WorkerComboBox.SelectedItem != null) {
                
                string sql = "INSERT INTO Workers(Surname,Name,Patronymic,BthDay,Sex,SubdivisionID) VALUES ('" + WorkerSurnameField.Text + "','" + WorkerNameField.Text + "','" + WorkerPatronymicField.Text + "','" + WorkerBthDayDatePicker.Text + "', '" + Sex + "', '" + WorkerComboBox.SelectedValue + "');";
                SqlConnection connection = new SqlConnection(connectionString);
                try
                {
                    connection.Open();
                    Console.WriteLine("Подключение: Да");
                    SqlCommand command = new SqlCommand(sql, connection);
                    int number = command.ExecuteNonQuery();
                    Console.WriteLine("Добавлено объектов: {0}", number);
                    MessageBox.Show("Данные успешно добавлены.");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                    Console.WriteLine("Подключение закрыто...");

                    UpdateDataGridView("Workers");
                    WorkerSurnameField.Text = "Введите фамилию";
                    WorkerSurnameField.Foreground = Brushes.Gray;
                    WorkerNameField.Text = "Введите имя";
                    WorkerNameField.Foreground = Brushes.Gray;
                    WorkerPatronymicField.Text = "Введите отчество";
                    WorkerPatronymicField.Foreground = Brushes.Gray;
                    WorkerBthDayDatePicker.Text = null;
                    Sex = null;
                    WorkerComboBox.SelectedItem = null;
                    SexRBF.IsChecked = false;
                    SexRBM.IsChecked = false;

                }
            }
            else MessageBox.Show("Необходимо заполнить все поля для добавления в базу!");
        }

        private void WorkerNameField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (WorkerNameField.Text == "Введите имя")
            {
                WorkerNameField.Text = "";
                WorkerNameField.Foreground = Brushes.Black;
            }
        }

        private void WorkerNameField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (WorkerNameField.Text == "")
            {
                WorkerNameField.Text = "Введите имя";
                WorkerNameField.Foreground = Brushes.Gray;
            }
        }

        private void WorkerSurnameField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (WorkerSurnameField.Text == "Введите фамилию")
            {
                WorkerSurnameField.Text = "";
                WorkerSurnameField.Foreground = Brushes.Black;
            }
        }

        private void WorkerSurnameField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (WorkerSurnameField.Text == "")
            {
                WorkerSurnameField.Text = "Введите фамилию";
                WorkerSurnameField.Foreground = Brushes.Gray;
            }
        }

        private void WorkerPatronymicField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (WorkerPatronymicField.Text == "Введите отчество")
            {
                WorkerPatronymicField.Text = "";
                WorkerPatronymicField.Foreground = Brushes.Black;
            }
        }

        private void WorkerPatronymicField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (WorkerPatronymicField.Text == "")
            {
                WorkerPatronymicField.Text = "Введите отчество";
                WorkerPatronymicField.Foreground = Brushes.Gray;
            }
        }

        private void SubdivisionNameField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SubdivisionNameField.Text == "Введите название")
            {
                SubdivisionNameField.Text = "";
                SubdivisionNameField.Foreground = Brushes.Black;
            }
        }

        private void SubdivisionNameField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SubdivisionNameField.Text == "")
            {
                SubdivisionNameField.Text = "Введите название";
                SubdivisionNameField.Foreground = Brushes.Gray;
            }
        }

        private void OrderNumberField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (OrderNumberField.Text == "Введите номер заказа")
            {
                OrderNumberField.Text = "";
                OrderNumberField.Foreground = Brushes.Black;
            }
        }

        private void OrderNumberField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (OrderNumberField.Text == "")
            {
                OrderNumberField.Text = "Введите номер заказа";
                OrderNumberField.Foreground = Brushes.Gray;
            }
        }

        private void OrderNameField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (OrderNameField.Text == "Введите название")
            {
                OrderNameField.Text = "";
                OrderNameField.Foreground = Brushes.Black;
            }
        }

        private void OrderNameField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (OrderNameField.Text == "")
            {
                OrderNameField.Text = "Введите название";
                OrderNameField.Foreground = Brushes.Gray;
            }
        }

        private void AddSubdivisionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SubdivisionNameField.Text != "Введите название" && SubdivisionComboBox.SelectedItem != null)
            {
                string sql = "INSERT INTO Subdivisions(Name,WorkerID) VALUES ('" + SubdivisionNameField.Text + "', '" + SubdivisionComboBox.SelectedValue + "');";
                SqlConnection connection = new SqlConnection(connectionString);
                try
                {
                    connection.Open();
                    Console.WriteLine("Подключение: Да");
                    SqlCommand command = new SqlCommand(sql, connection);
                    int number = command.ExecuteNonQuery();
                    Console.WriteLine("Добавлено объектов: {0}", number);
                    MessageBox.Show("Данные успешно добавлены.");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                    Console.WriteLine("Подключение закрыто...");

                    UpdateDataGridView("Subdivisions");
                    SubdivisionNameField.Text = "Введите название";
                    SubdivisionNameField.Foreground = Brushes.Gray;
                    SubdivisionComboBox.SelectedItem = null;
                }
            }
            else MessageBox.Show("Необходимо заполнить все поля для добавления в базу!");
        }

        private void AddOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (OrderNumberField.Text != "Введите номер заказа" && OrderNameField.Text != "Введите название" && OrderComboBox.SelectedItem != null)
            {
                string sql = "INSERT INTO Orders(Number,Name,WorkerID) VALUES ('" + OrderNumberField.Text + "','" + OrderNameField.Text + "', '" + OrderComboBox.SelectedValue + "');";
                SqlConnection connection = new SqlConnection(connectionString);
                try
                {
                    connection.Open();
                    Console.WriteLine("Подключение: Да");
                    SqlCommand command = new SqlCommand(sql, connection);
                    int number = command.ExecuteNonQuery();
                    Console.WriteLine("Добавлено объектов: {0}", number);
                    MessageBox.Show("Данные успешно добавлены.");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                    Console.WriteLine("Подключение закрыто...");

                    UpdateDataGridView("Orders");
                    OrderNameField.Text = "Введите название";
                    OrderNameField.Foreground = Brushes.Gray;
                    OrderNumberField.Text = "Введите номер заказа";
                    OrderNumberField.Foreground = Brushes.Gray;
                    OrderComboBox.SelectedItem = null;
                }
            }
            else MessageBox.Show("Необходимо заполнить все поля для добавления в базу!");
        }

        private void UpdateDataGridView(string DBTable)
        {

            switch (DBTable)
            {
                case "Workers":
                    adapter = new SqlDataAdapter("SELECT * FROM Workers", conn);
                    new SqlCommandBuilder(adapter);
                    dtMain = new DataTable();
                    adapter.Fill(dtMain);
                    WorkerDataGridView.ItemsSource = dtMain.DefaultView;
                    break;
                case "Subdivisions":
                    adapter = new SqlDataAdapter("SELECT * FROM Subdivisions", conn);
                    new SqlCommandBuilder(adapter);
                    dtMain = new DataTable();
                    adapter.Fill(dtMain);
                    SubdivisionsDataGridView.ItemsSource = dtMain.DefaultView;
                    break;
                case "Orders":
                    adapter = new SqlDataAdapter("SELECT * FROM Orders", conn);
                    new SqlCommandBuilder(adapter);
                    dtMain = new DataTable();
                    adapter.Fill(dtMain);
                    OrdersDataGridView.ItemsSource = dtMain.DefaultView;
                    break;
                default:
                    break;
            }  
        }

        private void WorkerListBtn_Click(object sender, RoutedEventArgs e)
        {
            WorkerList workerList = new WorkerList();
            workerList.Show();
        }

        private void SubdivisionListBtn_Click(object sender, RoutedEventArgs e)
        {
            SubdivisionList subdivisionList = new SubdivisionList();
            subdivisionList.Show();
        }

        private void OrderListBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderList orderList = new OrderList();
            orderList.Show();
        }
    }
}
                

                

                