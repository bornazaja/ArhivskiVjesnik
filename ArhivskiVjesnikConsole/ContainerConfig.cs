using ArhivskiVjesnikLibrary.BLL.DTO;
using ArhivskiVjesnikLibrary.BLL.Services.Implementations;
using ArhivskiVjesnikLibrary.BLL.Services.Interfaces;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;
using ArhivskiVjesnikLibrary.DAL.Repositories.Implementations;
using ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces;
using Autofac;
using AutoMapper;
using System.Linq;
using System.Reflection;

namespace ArhivskiVjesnikConsole
{
    public static class ContainerConfig
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));
            builder.RegisterGeneric(typeof(GenericService<,>)).As(typeof(IGenericService<,>));

            builder.RegisterAssemblyTypes(Assembly.Load(nameof(ArhivskiVjesnikLibrary)))
                    .Where(t => t.Name.EndsWith("Repository") && !t.Name.StartsWith("Generic") && t.Namespace.Contains("Repositories"))
                    .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));

            builder.RegisterAssemblyTypes(Assembly.Load(nameof(ArhivskiVjesnikLibrary)))
                    .Where(t => t.Name.EndsWith("Service") && !t.Name.StartsWith("Generic") && t.Namespace.Contains("Services"))
                    .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap(typeof(PagedList<>), typeof(PagedList<>));
                cfg.CreateMap<Autor, AutorDto>();
                cfg.CreateMap<Clanak, ClanakDto>();
            });
            var mapper = config.CreateMapper();

            builder.RegisterInstance(mapper);

            return builder.Build();
        }
    }
}
