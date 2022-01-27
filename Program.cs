using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace USB_DUMP
{
    class Program
    {
        private static Boolean dev = false;
        static void Main(string[] args)
        {
            if(args.Length == 1 && args[0].ToLower() == "--dev"){
                dev = true;
            }
            if(!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "usbdump"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "usbdump");
            }
            scanDisk();
        }
        static void scanDisk()
        {
            Boolean diskfound = false;

            if (dev)
            {
                Console.WriteLine("Waiting for usb device ...");
            }
            while (diskfound == false)
            {
                var drives = DriveInfo.GetDrives().Where(drive => drive.IsReady && drive.DriveType == DriveType.Removable);
                if(drives.Count() != 0)
                {
                    string rootPath = drives.First().Name;
                    List<String> usbDirectory = new List<String>(Directory.EnumerateDirectories(rootPath));
                    foreach(var directory in usbDirectory)
                    {
                        List<String> usbFiles = new List<String>(Directory.EnumerateFiles(directory));
                        foreach (var file in usbFiles)
                        {
                            Console.WriteLine(file);
                            try
                            {
                                if (dev)
                                {
                                    Console.WriteLine("[+] " + file + " [>] " + directory);
                                }
                                File.Copy(file, AppDomain.CurrentDomain.BaseDirectory + "usbdump\\" + Path.GetFileNameWithoutExtension(file) + Path.GetExtension(file));
                            }catch(IOException ioe)
                            {
                                if (dev)
                                {
                                    Console.WriteLine(ioe);
                                }
                            }
                            }
                    }
                    break;
                }
            }
            
        }
        
    }
}
