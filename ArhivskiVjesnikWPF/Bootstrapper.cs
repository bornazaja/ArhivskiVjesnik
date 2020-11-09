using ArhivskiVjesnikLibrary.BLL.DTO;
using ArhivskiVjesnikLibrary.BLL.Services.Implementations;
using ArhivskiVjesnikLibrary.BLL.Services.Interfaces;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;
using ArhivskiVjesnikLibrary.DAL.Repositories.Implementations;
using ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces;
using ArhivskiVjesnikWPF.Managers.Implenentations;
using ArhivskiVjesnikWPF.Managers.Interfaces;
using ArhivskiVjesnikWPF.ViewModels;
using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace ArhivskiVjesnikWPF
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer simpleContainer = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap(typeof(PagedList<>), typeof(PagedList<>));
                cfg.CreateMap<Autor, AutorDto>();
                cfg.CreateMap<Clanak, ClanakDto>();
                cfg.CreateMap<KljucnaRijec, KljucnaRijecDto>();
                cfg.CreateMap<Naslov, NaslovDto>();
                cfg.CreateMap<Sazetak, SazetakDto>();
                cfg.CreateMap<Vrsta, VrstaDto>();
            });

            var mapper = config.CreateMapper();

            simpleContainer
                .Instance(simpleContainer)
                .Instance(mapper);

            simpleContainer
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<IExtendedWindowManager, ExtendedWindowManager>();

            simpleContainer.RegisterPerRequest(typeof(IGenericRepository<Autor>), null, typeof(GenericRepository<Autor>));
            simpleContainer.RegisterPerRequest(typeof(IGenericRepository<Clanak>), null, typeof(GenericRepository<Clanak>));
            simpleContainer.RegisterPerRequest(typeof(IGenericRepository<KljucnaRijec>), null, typeof(GenericRepository<KljucnaRijec>));
            simpleContainer.RegisterPerRequest(typeof(IGenericRepository<Naslov>), null, typeof(GenericRepository<Naslov>));
            simpleContainer.RegisterPerRequest(typeof(IGenericRepository<Sazetak>), null, typeof(GenericRepository<Sazetak>));
            simpleContainer.RegisterPerRequest(typeof(IGenericRepository<Vrsta>), null, typeof(GenericRepository<Vrsta>));

            simpleContainer.RegisterPerRequest(typeof(IGenericService<Autor, AutorDto>), null, typeof(GenericService<Autor, AutorDto>));
            simpleContainer.RegisterPerRequest(typeof(IGenericService<Clanak, ClanakDto>), null, typeof(GenericService<Clanak, ClanakDto>));
            simpleContainer.RegisterPerRequest(typeof(IGenericService<KljucnaRijec, KljucnaRijecDto>), null, typeof(GenericService<KljucnaRijec, KljucnaRijecDto>));
            simpleContainer.RegisterPerRequest(typeof(IGenericService<Naslov, NaslovDto>), null, typeof(GenericService<Naslov, NaslovDto>));
            simpleContainer.RegisterPerRequest(typeof(IGenericService<Sazetak, SazetakDto>), null, typeof(GenericService<Sazetak, SazetakDto>));
            simpleContainer.RegisterPerRequest(typeof(IGenericService<Vrsta, VrstaDto>), null, typeof(GenericService<Vrsta, VrstaDto>));

            simpleContainer.RegisterPerRequest(typeof(PageableDataGridViewModel<AutorDto>), null, typeof(PageableDataGridViewModel<AutorDto>));
            simpleContainer.RegisterPerRequest(typeof(PageableDataGridViewModel<ClanakDto>), null, typeof(PageableDataGridViewModel<ClanakDto>));
            simpleContainer.RegisterPerRequest(typeof(PageableDataGridViewModel<KljucnaRijecDto>), null, typeof(PageableDataGridViewModel<KljucnaRijecDto>));
            simpleContainer.RegisterPerRequest(typeof(PageableDataGridViewModel<NaslovDto>), null, typeof(PageableDataGridViewModel<NaslovDto>));
            simpleContainer.RegisterPerRequest(typeof(PageableDataGridViewModel<SazetakDto>), null, typeof(PageableDataGridViewModel<SazetakDto>));
            simpleContainer.RegisterPerRequest(typeof(PageableDataGridViewModel<VrstaDto>), null, typeof(PageableDataGridViewModel<VrstaDto>));

            Assembly.Load(nameof(ArhivskiVjesnikLibrary))
                    .GetTypes()
                    .Where(t => t.Name.EndsWith("Repository") && !t.Name.StartsWith("Generic") && t.Namespace.Contains("Repositories"))
                    .ToList()
                    .ForEach(repositoryType => simpleContainer.RegisterPerRequest(
                         repositoryType.GetInterfaces().FirstOrDefault(i => i.Name == "I" + repositoryType.Name), repositoryType.ToString(), repositoryType));

            Assembly.Load(nameof(ArhivskiVjesnikLibrary))
                    .GetTypes()
                    .Where(t => t.Name.EndsWith("Service") && !t.Name.StartsWith("Generic") && t.Namespace.Contains("Services"))
                    .ToList()
                    .ForEach(serviceType => simpleContainer.RegisterPerRequest(
                        serviceType.GetInterfaces().FirstOrDefault(i => i.Name == "I" + serviceType.Name), serviceType.ToString(), serviceType));

            GetType().Assembly.GetTypes()
                .Where(t => t.IsClass)
                .Where(t => t.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => simpleContainer.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return simpleContainer.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return simpleContainer.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            simpleContainer.BuildUp(instance);
        }
    }
}
