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
            var list = new List<CoreControler>();
            for (int i = 0; i < 1; i++)
            {
                var controler = CoreFactory.Create();
                controler.Initialize(new CoreConfiguration() { Name = "Test Core " + i });
                list.Add(controler);
            }
            list.ForEach(i => Console.WriteLine(i));
            list.ForEach(i => i.Dispose());
            Console.WriteLine("END");
            Console.ReadLine();
        }
    }
}
