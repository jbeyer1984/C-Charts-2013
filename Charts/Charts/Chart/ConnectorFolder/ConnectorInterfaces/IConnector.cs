using System;

namespace Charts.Chart.ConnectorFolder.ConnectorInterfaces
{
    public interface IConnector
    {
        IConnector connect(object obj);

        void by(object obj);

        IConnector with(object obj);

        IConnector canBeNull();

        IConnector forceCreation();

        IConnector remove();

        object getByType(Type className);
    }
}