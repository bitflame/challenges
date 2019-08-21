using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;


namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            foreach (var connString in ConfigurationManager.ConnectionStrings)
            {
                Console.WriteLine(connString);
            }
            Console.ReadLine();
        }
    }
}
