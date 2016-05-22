using Charts.Chart.Registry;

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