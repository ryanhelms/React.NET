using System;
using System.Configuration;
using System.Diagnostics;
using ServiceStack;
using ServiceStack.Text;

namespace React.Sample.ServiceStack.SelfHost
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var license = ConfigurationManager.AppSettings["servicestack:license"];

            if (!string.IsNullOrEmpty(license))
                Licensing.RegisterLicense(license);

            new AppHost().Init().Start("http://*:8088/");
            "ServiceStack Self Host with Razor listening at http://localhost:8088 ".Print();
            Process.Start("http://localhost:8088/");

            Console.ReadLine();
        }
    }
}