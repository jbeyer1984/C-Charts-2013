using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charts.Chart.StateFolder.StateInterfaces
{
    public interface IStateFull
    {
        StateAbstract getState(Type type);
    }
}
