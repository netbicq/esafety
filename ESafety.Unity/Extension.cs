using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Unity
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class Extension
    {

        /// <summary>
        /// 对象转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T MAPTO<T>(this object obj)
        {
            if (obj == null)
            {
                return default(T);
            }
            Mapper.Initialize(ctx => ctx.CreateMap(obj.GetType(), typeof(T)));
            return Mapper.Map<T>(obj);

        }
        /// <summary>
        /// 集合转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> MAPTO<T>(this IEnumerable list)
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }
            Mapper.Initialize(ctx => ctx.CreateMap(list.GetType(), typeof(T)));
            return Mapper.Map<List<T>>(list);
        }
    }
}
