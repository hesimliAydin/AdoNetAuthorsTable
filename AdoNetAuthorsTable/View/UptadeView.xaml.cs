using AdoNetAuthorsTable.Model;
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
using System.Windows.Shapes;

namespace AdoNetAuthorsTable.View
{
    /// <summary>
    /// Interaction logic for UptadeView.xaml
    /// </summary>
    public partial class UptadeView : Window
    {
        public Books Book { get; set; }

        public UptadeView()
        {
            InitializeComponent();
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
                conn.Open();

                var query = $"UPDATE Books SET [Name] = '{Book?.Name}', Pages= {Book?.Pages}, YearPress = {Book?.YearPress},Id_Author = {Book?.IdAuthor}, Id_Themes = {Book?.IdTheme},Id_Category = {Book?.IdCategory},Id_Press = {Book?.IdPress},Comment = '{Book?.Comment}',Quantity = {Book?.Quantity} WHERE Id = {Book?.Id}";

                using (SqlCommand command = new SqlCommand(query,conn))
                {
                    command.ExecuteNonQuery();
                    DialogResult=true;
                }
            }
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult=false;
        }
    }
}
