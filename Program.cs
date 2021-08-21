using System;
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
            "touch test.txt".Bash(log);
        }
    }
}
