using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Movie1
{
    class DataManager
    {
        public static List<Book> Books = new List<Book>();
        public static List<User> Users = new List<User>();

        static DataManager()
        {
            Load();
        }

        public static void Load()
        {
            try
            {
                string booksOutput = File.ReadAllText(@"./Books.xml");
                XElement booksXElement = XElement.Parse(booksOutput);

                Books = (from item in booksXElement.Descendants("book")
                         select new Book()
                         {
                             Isbn = item.Element("isbn").Value,
                             Name = item.Element("name").Value,
                             Publisher = item.Element("publisher").Value,
                             Page = int.Parse(item.Element("page").Value),
                             UserId = int.Parse(item.Element("userId").Value),
                             UserName = item.Element("userName").Value,
                             IsBorrowed = item.Element("isborrowed").Value != "0" ? true : false,
                             BorrowedAt = DateTime.Parse(item.Element("borrowedAt").Value)

                         }
                         ).ToList<Book>();


                string usersOutput = File.ReadAllText(@"./Users.xml");
                XElement usersXElement = XElement.Parse(usersOutput);
                Users = (from item in usersXElement.Descendants("user")
                         select new User()
                         {
                             Id = int.Parse(item.Element("id").Value),
                             Name = item.Element("name").Value
                         }
                         ).ToList<User>();

            }
            catch (FileNotFoundException e )
            {
                Save();
            }
        }
        public static void Save()
        {
            string booksOutput = "";
            booksOutput += "<books>\n";
            foreach (var item in Books)
            {
                booksOutput += "<book>\n";

                booksOutput += "<isbn>" + item.Isbn + "</isbn>\n";
                booksOutput += "<name>" + item.Name + "</name>\n";
                booksOutput += "<publisher>" + item.Publisher + "</publisher>\n";
                booksOutput += "<page>" + item.Page + "</page>\n";
                booksOutput += "<userId>" + item.UserId + "</userId>\n";
                booksOutput += "<userName>" + item.UserName + "</userName>\n";
                booksOutput += "<isBorrowed>" + (item.IsBorrowed ? 1 : 0) + "<isBorrowed>\n";
                booksOutput += "<borrowedAt>" + item.BorrowedAt.ToLongDateString() + "</borroweAt>\n";

                booksOutput += "</book>\n";
            }
            booksOutput += "</books>";

            string usersOutput = "";
            usersOutput += "<users>\n";
            foreach (var item in Users)
            {
                booksOutput += "<user>\n";
                booksOutput += "<id>\n" + item.Id + "</id>\n";
                booksOutput += "<name>\n" + item.Name + "</name>\n";
                booksOutput += "</user>\n";
            }
            usersOutput += "</users>";

            File.WriteAllText(@"./Books.xml", booksOutput);
            File.WriteAllText(@"./Users.xml", usersOutput);

        }

    }

}


    




   
