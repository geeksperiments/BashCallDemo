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
            $". script.sh {guid}".Bash(log, output);

            var datafile = $"{guid}_data.txt";
            if(File.Exists(datafile))
            {
                output = System.IO.File.ReadAllText(datafile);
                File.Delete($"{guid}_data.txt");
            }

            Console.WriteLine($"Output: {output}");
            Console.WriteLine($"TEST_STORE: {Environment.GetEnvironmentVariable("TEST_STORE")}");
        }
    }
}
