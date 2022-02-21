using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Camping_Test
{
    public class DataManager
    {
        public static List<Booking> bookings = new List<Booking>();
        public static List<User> users = new List<User>();

        static DataManager()
        {
            Load();
        }
        public static void Load()
        {
            try
            {
                string bookingOutput = File.ReadAllText(@"./Booking.xml");
                XElement bookingXElement = XElement.Parse(bookingOutput);
                bookings.Clear();
                foreach (var item in bookingXElement.Descendants("booking"))
                {
                    Booking temp = new Booking();
                    temp.BookingNum = item.Element("bookingnum").Value;
                    temp.Area = item.Element("area").Value;
                    temp.BookingDate = DateTime.Parse(item.Element("bookingdate").Value);
                    temp.Checking = item.Element("checking").Value != "0"? true : false;
                    temp.UserId = item.Element("userid").Value;
                    temp.UserName = item.Element("userName").Value;

                    bookings.Add(temp);
                }

                string userOuput = File.ReadAllText(@"./Users.xml");
                XElement userXElement = XElement.Parse(userOuput);
                users.Clear();
                foreach (var item in userXElement.Descendants("user"))
                {
                    User temp = new User();
                    temp.Id = item.Element("id").Value;
                    temp.Name = item.Element("name").Value;
                 
                    users.Add(temp);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("파일 누락!");
                Save();
                Load();
            }
        }

        public static void Save()
        {
            string bookingOutput = "";
            bookingOutput += "<bookings>\n";

            foreach(var item in bookings)
            {
                bookingOutput += "<booking>\n";

                bookingOutput += $"    <bookingnum>{item.BookingNum}</bookingnum>\n";
                bookingOutput += $"    <area>{item.Area}</area>\n";
                bookingOutput += $"    <bookingdate>{item.BookingDate}</bookingdate>\n";
                bookingOutput += "    <checking>"+ (item.Checking ? 1 : 0) + "</checking>";
                bookingOutput += $"    <userid>{item.UserId}</userid>\n";
                bookingOutput += $"    <userName>{item.UserName}</userName>\n";

                bookingOutput += "</booking>\n";
            }

            bookingOutput += "</bookings>\n";

            File.WriteAllText(@"./Booking.xml", bookingOutput);

            string userOutput = "";
            userOutput += "<users>\n";
            foreach (var item in users)
            {
                userOutput += "<user>\n";

                userOutput += $"    <id>{item.Id}</id>\n";
                userOutput += $"    <name>{item.Name}</name>\n";

                userOutput += "</user>";
            }
            userOutput += "</users>";
            File.WriteAllText(@"./Users.xml", userOutput);

        }
    }
}
