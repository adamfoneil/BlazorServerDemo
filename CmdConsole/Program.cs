using AO.SqlServer;
using Microsoft.Data.SqlClient;
using System;

namespace CmdConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var dt = new DataTransfer();
            using (var cn = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=BlazorServerDemo;Trusted_Connection=True;MultipleActiveResultSets=true"))
            {
                dt.AddAllTablesAsync(cn, (objectName) =>
                {
                    Console.WriteLine(objectName.Name);
                    return true;
                }).Wait();
            }
            dt.ExportAsync(@"C:\users\adam\desktop\BlazorServerDemo.zip").Wait();
        }
    }
}
