using System;

namespace Charts.Chart.StateFolder.StateInterfaces
{
    public interface IStateFull
    {
        StateAbstract getState(Type type);
    }
}