using Autofac;

namespace ArhivskiVjesnikConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerConfig.GetContainer();

            using (var scope = container.BeginLifetimeScope())
            {
                IApplication application = scope.Resolve<IApplication>();
                application.Run();
            }
        }
    }
}
