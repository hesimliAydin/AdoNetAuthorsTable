using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetAuthorsTable.Model
{
    public class Books
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Pages { get; set; }
        public int? YearPress { get; set; }
        public int? IdAuthor { get; set; }
        public int? IdTheme { get; set; }
        public int? IdCategory { get; set; }
        public int? IdPress { get; set; }
        public string Comment { get; set; }
        public int? Quantity { get; set; }


        public Books(int? id, string name, int? pages, int? yearPress, int? idAuthor, int? idTheme, int? idCategory, int? idPress, string comment, int? quantity)
        {
            Id = id;
            Name = name;
            Pages = pages;
            YearPress = yearPress;
            IdAuthor = idAuthor;
            IdTheme = idTheme;
            IdCategory = idCategory;
            IdPress = idPress;
            Comment = comment;
            Quantity = quantity;
        }
    }
}
