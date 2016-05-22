namespace Charts.Chart.Registry.RegistryInterfaces
{
    public interface IRegistry
    {
        void register(IRegisterAble componentToRegister);

        void bind();
    }
}