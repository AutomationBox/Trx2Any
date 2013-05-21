using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace Trx2Any.Common
{
    public class MEFServiceLocator
    {
        private static CompositionContainer _container = new CompositionContainer();
        private static AggregateCatalog _catalog;
        private static readonly MEFServiceLocator _instance = new MEFServiceLocator();

        public static MEFServiceLocator Instance
        {
            get
            {
                return _instance;
            }
        }

        public CompositionContainer Container
        {
            get { return _container; }
        }

        private MEFServiceLocator()
        {
        }

        public void Initialize(string extensionPath)
        {
            // Create a catalog of all your exportable types.
            _catalog = new AggregateCatalog(
                new AssemblyCatalog(Assembly.GetEntryAssembly()), //.GetExecutingAssembly()),
                new DirectoryCatalog(extensionPath));

            // Create a container using the catalog
            _container = new CompositionContainer(_catalog);
        }

        public T GetInstance<T>()
        {
            return _container.GetExportedValue<T>();
        }
    }

}
