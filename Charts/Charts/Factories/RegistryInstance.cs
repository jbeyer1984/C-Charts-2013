using Charts.Chart.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charts.Factories
{
    public class RegistryInstance
    {
        private RegistryCountInstances registryCountInstances;

        public RegistryCountInstances getRegistryCountInstances()
        {
            if (null == registryCountInstances) {
                registryCountInstances = new RegistryCountInstances();
            }

            return registryCountInstances;
        }
    }
}
