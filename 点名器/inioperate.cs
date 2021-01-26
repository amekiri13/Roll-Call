using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
namespace 点名器
{
    class IniOperate
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, int nSize, string filePath);

        //读取ini文件 section表示ini文件中的节点名，key表示键名 def没有查到的话返回的默认值 filePath文件路径
        public string Read(string section, string key, string def, string filePath)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def, sb, 1024, filePath);
            return sb.ToString();
        }

        //写入ini文件 section表示ini文件中的节点名，key表示键名 value写入的值 filePath文件路径
        public int Write(string section, string key, string value, string filePath)
        {
            //CheckPath(filePath);
            return WritePrivateProfileString(section, key, value, filePath);
        }


        //删除section 
        public int DeleteSection(string section, string filePath)
        {
            return Write(section, null, null, filePath);
        }


        //删除键
        public int DeleteKey(string section, string key, string filePath)
        {
            return Write(section, key, null, filePath);
        }

        public string[] GetAllSectionNames(/*out string[] sections,*/ string path)
        {
            string[] sections;
            int MAX_BUFFER = 32767;
            IntPtr pReturnedString = Marshal.AllocCoTaskMem(MAX_BUFFER);
            int bytesReturned = GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, path);
            if (bytesReturned == 0)
            {
                sections = null;
                //return -1;
            }
            string local = Marshal.PtrToStringAnsi(pReturnedString, (int)bytesReturned).ToString();
            Marshal.FreeCoTaskMem(pReturnedString);
            //use of Substring below removes terminating null for split
            sections = local.Substring(0, local.Length - 1).Split('\0');
            return sections;
        }
    }
}
