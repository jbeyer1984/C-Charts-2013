using Charts.Chart.CacheFolder.CacheInterfaces;
using Charts.Chart.ConnectorFolder;
using System;

namespace Charts.Chart.CacheFolder
{
    /// <summary>
    /// cache like behaviour, through wrapping connector functionalities
    /// 
    /// scenario: in class DynamicSettingsForm should exist a connection for
    ///           2 objects in ex. panelToUpdate and dynamicPanel
    ///           the dynamicPanel shows properties for different panels,
    ///           so 1 dynamicPanel to different panels to update
    /// usages:
    /// 1. cache(obj1).by(obj2)
    /// 2. cache(obj1).canBeNew().getByType(type)
    /// 3. with(obj1).getByType(type)
    /// 4. cache(obj1).delete().getByType(type)
    /// 
    /// optional usages:
    /// ICacheAble uses isAlreadyExisting, examples are in builder functions or DynamicSettingsForm
    /// 
    /// dependencies: classes must implement IIdentifier and ICacheAble
    /// </summary>
    public class Cache : ICache
    {
        private Connector connector;
        private bool isCanBeNewCreated = false;
        private bool isCanBeRemoved = false;

        public Cache(Connector connector)
        {
            this.connector = connector;
            this.connector.DirectionBack = "<-cache-";
            this.connector.DirectionForward = "-cache->";
        }

        public ICache cache(object obj)
        {
            connector.connect(obj);

            return this;
        }

        public void by(object obj)
        {
            connector.by(obj);
        }

        public ICache with(object obj)
        {
            connector.with(obj);

            return this;
        }

        public ICache canBeNull()
        {
            connector.canBeNull();

            return this;
        }

        public ICache canBeNew()
        {
            isCanBeNewCreated = true;

            return this;
        }

        public ICache delete()
        {
            isCanBeRemoved = true;

            return this;
        }

        public object getByType(Type className)
        {
            if (isCanBeNewCreated) {
                isCanBeNewCreated = false;
                return connector.forceCreation().getByType(className); // raw object will be created
            } else if (isCanBeRemoved) {
                connector.with(connector.WithTemp).remove().getByType(className);
                isCanBeRemoved = false;
                return null;
            }

            return connector.getByType(className);
        }
    }
}