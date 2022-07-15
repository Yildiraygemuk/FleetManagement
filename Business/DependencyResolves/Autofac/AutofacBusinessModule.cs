using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Interceptors;
using DataAccess.Abstract.Repositories;
using DataAccess.Concrete.EntityFramework.Repositories;

namespace Business.DependencyResolves.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PackageService>().As<IPackageService>();
            builder.RegisterType<BagService>().As<IBagService>();
            builder.RegisterType<VehicleService>().As<IVehicleService>();

            builder.RegisterType<VehicleRepository>().As<IVehicleRepository>();
            builder.RegisterType<BagRepository>().As<IBagRepository>();
            builder.RegisterType<PackageRepository>().As<IPackageRepository>();
            builder.RegisterType<WrongDeliveryLogRepository>().As<IWrongDeliveryLogRepository>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
        }
    }
}
