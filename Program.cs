using System;
using System.IO;
using System.Text.RegularExpressions;
using NLog.Web;

namespace MovieLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\nlog.config";
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();

            logger.Info("Program started");
            
            Console.WriteLine("Enter 1 to view movies");
            Console.WriteLine("Enter 2 to add data");
            Console.WriteLine("Enter any other key to exit");

            string choose = Console.ReadLine();

            var file = "movies.csv";

            if (choose == "1") {
                if (File.Exists(file))
                {
                    StreamReader sr = new StreamReader(file);
                    while(!sr.EndOfStream) {
                        string line = sr.ReadLine();
                        string[] movie = line.Split(',');
                        movie = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                        Console.WriteLine($"{movie[0],-10}{movie[1],-150}{movie[2]}");
                    }
                }
                else {
                    logger.Warn("File does not exists. {file}", file);
                }
            }

            else if (choose == "2") {

            } 
        }
    }
}
