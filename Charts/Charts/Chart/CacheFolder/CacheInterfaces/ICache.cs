using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charts.Chart.CacheFolder.CacheInterfaces
{
    public interface ICache
    {
        ICache cache(object obj);
        void by(object obj);
        ICache with(object obj);
        ICache canBeNull();
        ICache canBeNew();
        ICache delete();
        object getByType(Type className);
    }
}
