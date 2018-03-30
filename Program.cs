using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MyTests
{
    class Program
    {
        static StringBuilder Results = new StringBuilder();
        static void Main(string[] args)
        {
            //Test
            String res = ReplaceFill(File.ReadAllText(@"C:\SsiNxt\Version 7\Development\NET\SSI.Server.AP\IO\ap423e_export_1099.cs"));
            File.WriteAllText("c:\\wrk\\ap423e_export_1099.cs", res);
            //

            //var dirs = Directory.GetDirectories("C:\\SsiNxt\\Version 7\\Development\\NET", "SSI*");
            //foreach (string dir in dirs)
            //{
            //    FindFilesAndReplace(dir);
            //}

            //File.WriteAllText("C:\\wrk\\ReplaceFill_Results.txt", Results.ToString());

            Console.WriteLine("Press Enter");
            Console.ReadLine();
        }

        public static void FindFilesAndReplace(string dir)
        {
            var csFiles = Directory.GetFiles(dir, "*.cs");
            foreach (string csFile in csFiles)
            {
                Console.WriteLine("Replacing {0}", csFile);

                String Result = ReplaceFill(File.ReadAllText(csFile));
                File.WriteAllText(csFile, Result);

                Results.AppendLine(String.Format("Replaced {0}", csFile));
            }

            var subDirs = Directory.GetDirectories(dir);
            foreach (string subDir in subDirs)
            {
                if (String.Compare(subDir, "bin", true) == 0 ||
                    String.Compare(subDir, "debug", true) == 0 ||
                    String.Compare(subDir, "obj", true) == 0)
                    continue;

                FindFilesAndReplace(subDir);
            }
        }

        public static String ReplaceFill(string strCSharpCode)
        {
            String Result = strCSharpCode, pattern = "(TransformUtility.Fill\\()\\s*\"(.*)\"\\s*,\\s*(.*)\\)";
            Regex RegEx = new Regex(pattern);            

            Match m = RegEx.Match(strCSharpCode);
            while (m.Success)
            {
                Console.WriteLine(m.Groups[1]);
                Console.WriteLine(m.Groups[2]);
                Console.WriteLine(m.Groups[3]);

                String strReplacement = "new string('" + m.Groups[2].ToString() + "', " + m.Groups[3] + ")";
                Result = Result.Replace(m.Groups[0].ToString(), strReplacement);

                m = m.NextMatch();
            }

                           
            return Result;
        }
    }
}
