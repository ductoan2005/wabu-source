using Autofac;
using Autofac.Integration.Mvc;
using FW.BusinessLogic.Implementations;
using FW.BusinessLogic.Services;
using FW.Data.EFs;
using FW.Data.EFs.Repositories;
using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using WABU.Mapping;

namespace WABU
{
    public class AutofacBootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();

            //Configure AutoMapper
            AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<MyFWDbContext>().As<FWDbContext>()
                .WithParameter("nameOrConnectionString", "FWDbContext")
                .InstancePerRequest();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
            builder.RegisterType<NotificationRepository>().As<INotificationRepository>().SingleInstance();
            builder.RegisterType<AttachmentsDOServices>().As<IAttachmentsToDOServices>().InstancePerRequest();
            // Repositories
            builder.RegisterAssemblyTypes(typeof(IUserMasterRepository).Assembly)
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces().InstancePerRequest();
            // Business Logic/ Services
            builder.RegisterAssemblyTypes(typeof(UserMasterBL).Assembly)
               .Where(t => t.Name.EndsWith("BL"))
               .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterFilterProvider();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}