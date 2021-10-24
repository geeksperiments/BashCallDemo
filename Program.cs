using System;
using System.IO;
using BashCallDemoHelpers;
using log4net;

namespace BashCallDemo
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            Console.WriteLine("Executing bash...");
            String output = string.Empty;
            string guid = Guid.NewGuid().ToString();
            var command = $". script.sh {guid}";
            command.Bash(log, output);


            var datafile = $"{Directory.GetCurrentDirectory()}/{guid}_data";
            var filelist = Directory.GetFiles(Directory.GetCurrentDirectory());
            foreach(var file in filelist)
            {
                Console.WriteLine($"File found in directory: {file}");
            }
            
            Console.WriteLine($"File to check: {datafile}");
            if(File.Exists(datafile))
            {
                output = System.IO.File.ReadAllText(datafile);
                File.Delete(datafile);
            }

            Console.WriteLine($"Output: {output}");
            Console.WriteLine($"TEST_STORE: {Environment.GetEnvironmentVariable("TEST_STORE")}");
        }
    }
}
