using AdoNetAuthorsTable.Model;
using AdoNetAuthorsTable.View;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace AdoNetAuthorsTable
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string id = default;
        public MainWindow()
        {
            InitializeComponent();


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            using (var conn=new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
                conn.Open();
                string query = "SELECT * FROM Authors";

                using (SqlCommand command=new SqlCommand(query,conn))
                {
                    var reader = command.ExecuteReader();
                    int line = 0;

                    while (reader.Read())
                    {
                        if (line == 0)
                        {
                            line++;
                            continue;
                        }

                        int? id = reader["Id"] as int?;
                        string firstName = reader["FirstName"] as string;
                        string lastName = reader["LastName"] as string;

                        Author_Cbox.Items.Add(firstName + " " + lastName);

                    }
                }

            }


        }

        private void Author_Cbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!Category_Cbox.IsEnabled)
                Category_Cbox.IsEnabled=!Category_Cbox.IsEnabled;

            Category_Cbox.Items.Clear();

            using (var conn=new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
                conn.Open();

                string query = "SELECT Id FROM Authors WHERE (FirstName + ' ' + LastName) =@p1";
                using (SqlCommand command=new SqlCommand(query,conn))
                {
                    command.Parameters.AddWithValue("@p1", Author_Cbox.SelectedItem.ToString());
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        id = reader[0].ToString();
                    }
                }
            }

            using (var conn=new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
                conn.Open();
                string query= @"SELECT DISTINCT Categories.[Name]
FROM Categories
JOIN Books 
ON Id_Category = Categories.Id
JOIN Authors
ON Id_Author = Authors.Id
WHERE Authors.Id =@id";
                using (SqlCommand command=new SqlCommand(query,conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Category_Cbox.Items.Add(reader["Name"] as string);
                    }
                }
            }



            
        }

        private void Category_Cbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Category_Cbox.Items.IsEmpty)
            {
                return;
            }



            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
                conn.Open();

                string query = "SELECT Id FROM Authors WHERE (FirstName + ' ' + LastName) =@p1";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@p1", Author_Cbox.SelectedItem.ToString());
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        id = reader[0].ToString();
                    }
                }
            }


            using (SqlConnection conn=new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
                conn.Open();

                var name = Category_Cbox.SelectedItem.ToString();
                var query = @"SELECT *
FROM Books 
JOIN Categories
ON Categories.Id=Id_Category
JOIN Authors
ON Authors.Id=Id_Authors
WHERE Categories.Name=@name AND Id_Author=@id";


                using (SqlCommand command=new SqlCommand(query,conn))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@id", id);

                    var reader = command.ExecuteReader();
                    ListBooks.Items.Clear();

                    while (reader.Read())
                    {
                        var bookId = reader["Id"] as int?;
                        var bookName = reader["Name"] as string;
                        var bookPages = reader["Pages"] as int?;
                        var bookYearPress = reader["YearPress"] as int?;
                        var IdAuthor = reader["Id_Author"] as int?;
                        var IdTheme = reader["Id_Themes"] as int?;
                        var IdCategory = reader["Id_Category"] as int?;
                        var IdPress = reader["Id_Press"] as int?;
                        var comment = reader["Comment"] as string;
                        var quantity = reader["Quantity"] as int?;

                        var book = new Books(bookId, bookName, bookPages, bookYearPress, IdAuthor, IdTheme, IdCategory, IdPress, comment, quantity);
                        ListBooks.Items.Add(book);
                    }
                }
                
            }
        }

        private void Txt_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                
            }
        }

        

        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Btn_Uptade_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
