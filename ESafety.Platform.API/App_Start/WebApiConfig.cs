using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ESafety.Platform.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            //设置跨域
            config.EnableCors(new System.Web.Http.Cors.EnableCorsAttribute("*", "*", "*"));

            config.Formatters.JsonFormatter.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();



            config.Filters.Add(new Web.Unity.ExceptionFilter());
            config.Filters.Add(new Web.Unity.PlatformActionFilter());
            config.Filters.Add(new Web.Unity.PlatformAuthFilter());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
