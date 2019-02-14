using Autofac;
using Autofac.Integration.WebApi;
using ESafety.Core;
using ESafety.Core.Model.DB;
using ESafety.ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Filters;

namespace ESafety.Platform.API
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

            builder.RegisterAssemblyTypes(typeof(Auth_UserService).Assembly)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces()
              .InstancePerRequest();

            //builder.RegisterAssemblyTypes(typeof(Platform.Bll.AccountService).Assembly)
            //  .Where(t => t.Name.EndsWith("Service"))
            //  .AsImplementedInterfaces()
            //  .InstancePerRequest();



            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = resolver;


        }
    }
}