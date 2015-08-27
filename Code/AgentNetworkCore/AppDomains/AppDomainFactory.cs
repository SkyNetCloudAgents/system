using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkyNet.AppDomains
{
    static class AppDomainFactory
    {
        #region Methods

        public static AppDomain Create(string prefix)
        {
            string name = string.Format("{0}_{1}", prefix, Guid.NewGuid().ToString("N"));
            AppDomain domain = AppDomain.CreateDomain(name);
            domain.ResourceResolve += ResolveAssembly;
            return domain;
        }

        private static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in loadedAssemblies)
            {
                if (assembly.FullName == args.Name)
                {
                    return assembly;
                }
            }

            return null;
        }

        #endregion
    }
}
