using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PasswordManagerServer
{
    internal class Logger
    {
        public static Logger Instance { get; private set; }

        public static void log(string message)
        {
            Console.WriteLine(message);
            saveLogs(message);
        }

        public static void log(ConsoleColor consoleColor, string message)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
            saveLogs(message);
        }

        public static void error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERROR] " + message);
            Console.ForegroundColor = ConsoleColor.White;
            saveLogs(message);
        }

        public static void info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            saveLogs(message);
            Console.WriteLine("[INFO] " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void debug(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[DEBUG] " + message);
            Console.ForegroundColor = ConsoleColor.White;
            saveLogs(message);
        }
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
        public static void saveLogs(String message)
        {

            try
            {
                DateTime now = DateTime.Today;
                if (!Directory.Exists("logs/")) { Directory.CreateDirectory("logs/"); }
                using (StreamWriter sw = new StreamWriter("logs/" + GetTimestamp(now) + "_log.txt"))
                {
                    sw.WriteLine(message);
                }
            }
            catch (IOException ioe)
            {

            }
        }

    }
}