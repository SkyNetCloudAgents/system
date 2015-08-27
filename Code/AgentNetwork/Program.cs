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
            {
                controler.Initialize(new CoreConfiguration() { Name = "Test Core" });
                Console.WriteLine(controler.ToString());
            }
            Console.ReadLine();
        }
    }
}
