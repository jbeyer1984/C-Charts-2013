using Charts.Chart.Registry.RegistryInterfaces;

namespace Charts
{
    public class RegistryCollectionDrawerOverwrite : IRegistry
    {
        private CollectionDrawerOverwrite collectionDrawerOverwrite;

        public void register(IRegisterAble componentToRegister)
        {
            collectionDrawerOverwrite = componentToRegister as CollectionDrawerOverwrite;
        }

        public void bind()
        {
        }
    }
}