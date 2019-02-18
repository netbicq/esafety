using Autofac;
using Autofac.Integration.WebApi;
using ESafety.Core;
using ESafety.Core.Model.DB;
using ESafety.ORM;
using ESafety.Account.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Data.Entity;

namespace ESafety.Account.API
{
    public class AOP
    {

        public static void reg()
        {
            var builder = new ContainerBuilder();


            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();



            builder.RegisterType<ModelBase>()
                .As<ModelBase>()
                .InstancePerRequest();


            builder.RegisterType<ESFdb>()
                .As<DbContext>()
                .InstancePerRequest();


            builder.RegisterType<Unitwork>()
                .As<IUnitwork>()
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(Core.Auth_UserService).Assembly)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces()
              .InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(Service.Auth_UserService).Assembly)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces()
              .InstancePerRequest();
             


            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = resolver;

        }
    }
}