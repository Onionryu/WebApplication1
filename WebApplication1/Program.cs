using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Text;

namespace WebApplication1
{
    /*
    public class Program
    {
        

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        
    }*/
    public class Program
    {
        public static void Main(string[] args)
        {

            Console.WriteLine(TextRead());
            ConnectToDB();
            Console.ReadLine();
        }
        static string TextRead()
        {

            string content = "Hello world!";
            return content;
        }
        static void ConnectToDB()
        {
            try
            {
                //connection to Azure SQL 2017, need NuGet package System.Data.SqlClient

                //remember to add the connection details
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = ".database.windows.net";
                builder.UserID = "";
                builder.Password = "";
                builder.InitialCatalog = "";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    //string sql = sb.ToString();
                    string sql = ("SELECT * FROM Testing");

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                                Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }


    }
}

