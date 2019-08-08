using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Process = System.Diagnostics.Process;
using System.Management;
using System.IO;

namespace ConsoleApplication2
{
    class Program
    {
  
        static void Main(string[] args)
        {

            string strName = DateTime.Now.ToString("yyyyMMddhhmmss");
            string filePath = @"E:\" + strName + @".txt";
            StreamWriter writer = new StreamWriter(filePath);

            Process[] processArray = Process.GetProcesses();
            writer.WriteLine("----" + "进程数目:" + processArray.Length + "----");
            writer.WriteLine(Environment.NewLine);
            Console.WriteLine("----" + "进程数目:"+ processArray.Length + "----");

            /*
            string processName = processArray[0].ProcessName;
            string userName = GetProcessUserName(processArray[0].Id);

            Console.WriteLine("进程名称："+processName);
            Console.WriteLine("用户名"+userName);

            //Path
            Process[] processArray1 = Process.GetProcessesByName(processName);//数组长度为1
            //Console.WriteLine("数组长度："+processArray1.Length);
            for (int i = 0; i < processArray1.Length; i++) {
               // Console.WriteLine("1 "+processArray1[i]);
               // Console.WriteLine("2 " + processArray1[i].MainModule);
               
                Console.WriteLine("路径 "+processArray1[i].MainModule.FileName);//包含exe文件
            }
            string path = System.IO.Path.GetDirectoryName(Process.GetProcessesByName(processName)[0].MainModule.FileName);
            System.Diagnostics.FileVersionInfo info = System.Diagnostics.FileVersionInfo.GetVersionInfo(processArray1[0].MainModule.FileName);
            */
            // Console.WriteLine("文件名称=" + info.FileName);
           // Console.WriteLine("产品名称=" + info.ProductName);
           // Console.WriteLine("公司名称=" + info.CompanyName);
           // Console.WriteLine("文件版本=" + info.FileVersion);
           // Console.WriteLine("产品版本=" + info.ProductVersion);
            // 通常版本号显示为「主版本号.次版本号.生成号.专用部件号」
           // Console.WriteLine("系统显示文件版本：" + info.ProductMajorPart + '.' + info.ProductMinorPart + '.' + info.ProductBuildPart + '.' + info.ProductPrivatePart);
            //Console.WriteLine("描述：" + info.FileDescription);
            //Console.WriteLine("文件语言=" + info.Language);
            //Console.WriteLine("原始文件名称=" + info.OriginalFilename);
            //Console.WriteLine("文件版权=" + info.LegalCopyright);

            //Console.WriteLine("文件大小=" + System.Math.Ceiling(fileInfo.Length / 1024.0) + " KB");

            //string description = processArray[1].MainModule.FileVersionInfo.FileDescription;
           
            //Console.WriteLine("4 " + path);//不包含exe文件
           // Console.WriteLine(description);


            

            for (int i = 0; i < processArray.Length; i++)
            {
                int j = i + 1;
                string processName = processArray[i].ProcessName;
                string userName = GetProcessUserName(processArray[i].Id);
                Console.WriteLine("NO:" + j);
                writer.WriteLine("NO:" + j);
                Console.WriteLine("进程：" + processName);
                writer.WriteLine("进程：" + processName);
                Console.WriteLine("用户：" + userName);
                writer.WriteLine("用户：" + userName);
                string path = null;
                System.Diagnostics.FileVersionInfo info = null;
                Process[] processArray1 = Process.GetProcessesByName(processName);
                try
                {
                    path = processArray1[0].MainModule.FileName;
                    info = System.Diagnostics.FileVersionInfo.GetVersionInfo(path);
                }
                catch
                {
                    path = "未获取到";
                }
                writer.WriteLine("路径：" + path);
                Console.WriteLine("路径：" + path);
                if (info != null)
                {
                    writer.WriteLine("描述：" + info.FileDescription);
                    Console.WriteLine("描述：" + info.FileDescription);
                }
                else
                {
                    writer.WriteLine("描述：未获取到");
                    Console.WriteLine("描述：未获取到");
                }
                writer.WriteLine("--------------------------------------------------------");
                Console.WriteLine("--------------------------------------------------------");
            }

            writer.Flush();
            writer.Close();
            Console.ReadLine();
        }


        private static string GetProcessUserName(int pID)
        {

            string text1 = null;
            SelectQuery query1 = new SelectQuery("Select * from Win32_Process WHERE processID=" + pID);
            ManagementObjectSearcher searcher1 = new ManagementObjectSearcher(query1);

            try
            {
                foreach (ManagementObject disk in searcher1.Get())
                {
                    ManagementBaseObject inPar = null;
                    ManagementBaseObject outPar = null;

                    inPar = disk.GetMethodParameters("GetOwner");
                    outPar = disk.InvokeMethod("GetOwner", inPar, null);
                    text1 = outPar["User"].ToString();
                    break;
                }
            }
            catch
            {
                text1 = "SYSTEM";
            }
            return text1;
        }

        
    }
}
