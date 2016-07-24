using SkyNet;
using SkyNet.Core;
using SkyNet.Manager;
using SkyNet.Manager.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            using (CoreControler controler = CoreFactory.Create())
            using (CoreControler controler2 = CoreFactory.Create())
            {
                controler.Initialize(new CoreConfiguration() { Name = "Test Core" });
                controler.Initialize(new CoreConfiguration() { Name = "Test Core2" });
                Console.WriteLine(controler.ToString());
                Console.WriteLine(controler2.ToString());
            }
            Console.WriteLine("END");
            Console.ReadLine();
        }
    }
}
