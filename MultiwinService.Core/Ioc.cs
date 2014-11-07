using Microsoft.Practices.Unity;

namespace MultiwinService.Core
{
    public class Ioc
    {
        private static readonly IUnityContainer Container;

        static Ioc()
        {
            Container = new UnityContainer();
        }

        public static TInterface Get<TInterface>()
        {
            return Container.Resolve<TInterface>();
        }

        public static void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            Container.RegisterType<TInterface, TImplementation>();
        }
    }
}
