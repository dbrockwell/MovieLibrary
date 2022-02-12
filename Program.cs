﻿using System;
using System.IO;
using System.Text.RegularExpressions;
using NLog.Web;
using System.Collections.Generic;

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
                bool addContinue = true;
                int ID;
                string title;
                string[] genres;
                string[] movieCheck;
                List<int> IDNumber = new List<int>();
                List<string> titleText = new List<string>();
                

                if (File.Exists(file))
                {
                    StreamReader sr = new StreamReader(file);
                    while(!sr.EndOfStream) {
                        string line = sr.ReadLine();
                        movieCheck = line.Split(',');
                        movieCheck = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                        int parseID;
                        if (int.TryParse(movieCheck[0], out parseID)) {
                            IDNumber.Add(Int32.Parse(movieCheck[0]));
                            titleText.Add(movieCheck[1]);
                        }
                        else {continue;}
                    }
                }
                else {
                    logger.Warn("File does not exists. {file}", file);
                }

                do {
                    Console.WriteLine("Enter movie ID");
                    string IDString = Console.ReadLine();
                    if (!int.TryParse(IDString, out ID)) {
                        logger.Error("Invalid input (integer): {Answer}", IDString);
                        addContinue = false;
                    }
                    else if (ID <= IDNumber[IDNumber.Count - 1]){
                        logger.Warn("Number has to be larger that the final ID number: {number}", IDNumber[IDNumber.Count - 1]);
                        addContinue = false;
                    }
                    else {
                        addContinue = true;
                    }
                } while(addContinue == false);
            } 
        }
    }
}
