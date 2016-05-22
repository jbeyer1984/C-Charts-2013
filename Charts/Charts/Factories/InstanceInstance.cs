using Charts.Chart.CacheFolder;
using Charts.Chart.CacheFolder.CacheInterfaces;
using Charts.Chart.ConnectorFolder;

namespace Charts.Factories
{
    public class InstanceInstance
    {
        private Cache cachePanel;
        private Connector connector;

        public virtual Cache getCache()
        {
            if (null == this.cachePanel) {
                this.cachePanel = new Cache(new Connector());
            }
            return this.cachePanel;
        }

        public virtual Connector getConnector()
        {
            if (null == this.connector) {
                this.connector = new Connector();
            }
            return this.connector;
        }
    }
}
