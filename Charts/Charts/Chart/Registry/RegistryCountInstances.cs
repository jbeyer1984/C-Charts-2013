using Charts.Chart.Identifier;
using Charts.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charts.Chart.Registry
{
    public class RegistryCountInstances
    {
        private Dictionary<Type, int> countInstancesList = new Dictionary<Type, int>();

        public int getInstanceCountOf(object obj)
        {
            Inst.getStaticCall().checkIIdentifier(obj);

            Type type = obj.GetType();

            int currentCount = 0;

            if (!countInstancesList.ContainsKey(type)) {
                countInstancesList.Add(type, 0);
            } else {
                currentCount = countInstancesList[type];
                currentCount++;
                countInstancesList[type] =  currentCount;
            }

            return currentCount;
        }
    }
}
