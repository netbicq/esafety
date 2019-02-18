using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
        /// <param name="target"></param>
        /// <returns></returns>
        public static T CopyTo<T>(this object obj,T target)
        {
            if (obj == null)
            {
                return target;
            } 
            var tpts =typeof(T).GetProperties();
            var spts = obj.GetType().GetProperties();
            var pnames = spts.Select(s => s.Name);
            foreach(var sp in tpts.Where(q =>pnames.Contains(q.Name)))
            {
                var tpt = spts.FirstOrDefault(q => q.Name == sp.Name);
                sp.SetValue(target, tpt.GetValue(obj));
            }
            return target;
        }
        /// <summary>
        /// 对象转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T MAPTO<T>(this object obj)
        {
            if(obj==null)
            {
                return default(T);
            }
            Mapper.Reset();
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
            Mapper.Reset();
            Mapper.Initialize(ctx => ctx.CreateMap(list.GetType(), typeof(T)));
            return Mapper.Map<List<T>>(list);
        }
    }
}
