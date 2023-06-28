using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavalProject.Tools.Instruments
{
    class Directories
    {
        private static readonly string ParentPath;
        private static readonly string Templates = @"Templates\";
        static Directories()
        {
            ParentPath = Directory.GetCurrentDirectory() + @"\Resources\";
        }
        public static string Check() => ParentPath + Templates + @"Check.docx";
        public static string SalesReport() => ParentPath + Templates + @"SalesReport.docx";
        public static string SupplyReport() => ParentPath + Templates + @"SupplyReport.docx";
        public static string TimerSaves() => ParentPath + @"TimerSaves.txt";
        public static string ConnectionString() => ParentPath + @"ConnectionString.txt";
    }
}
