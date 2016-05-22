using Charts.Chart.StaticCallsFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charts.Factories
{
    public class Inst
    {
        private static BuilderInstance factory = null;
        private static InstanceInstance instance = null;
        private static RegistryInstance registry = null;
        private static StaticCall staticCall = null;

        public static BuilderInstance getBuilder()
        {
            if (null == factory) {
                factory = new BuilderInstance();
            }

            return factory;
        }

        public static InstanceInstance getInstance()
        {
            if (null == instance) {
                instance = new InstanceInstance();
            }

            return instance;
        }

        public static RegistryInstance getRegistry()
        {
            if (null == registry) {
                registry = new RegistryInstance();
            }

            return registry;
        }

        public static StaticCall getStaticCall()
        {
            if (null == staticCall) {
                staticCall = new StaticCall();
            }

            return staticCall;
        }
    }
}
