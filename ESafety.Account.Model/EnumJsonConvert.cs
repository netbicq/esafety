using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.Model
{
    /// <summary>
    /// 枚举显示描述
    /// </summary>
    /// <typeparam name="T">枚举</typeparam>
    public class EnumJsonConvert<T> : JsonConverter where T : struct, IConvertible
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public void EnumJsonConverter()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T 必须是枚举类型");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            try
            {
                return reader.Value.ToString();
            }
            catch (Exception)
            {
                throw new Exception(string.Format("不能将枚举{1}的值{0}转换为Json格式.", reader.Value, objectType));
            }

        }

        /// <summary>
        /// 判断是否为Bool类型
        /// </summary>
        /// <param name="objectType">类型</param>
        /// <returns>为bool类型则可以进行转换</returns>
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        bool IsNullableType(Type theType)
        {
            return (theType.IsGenericType && theType.
            GetGenericTypeDefinition().Equals
            (typeof(Nullable<>)));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            string bValue = value.ToString();
            int isNo;
            if (int.TryParse(bValue, out isNo))
            {
                bValue = GetEnumDescription(typeof(T), isNo);
            }
            else
            {
                bValue = GetEnumDescription(typeof(T), value.ToString());
            }


            writer.WriteValue(bValue);
        }

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="value">枚举名称</param>
        /// <returns></returns>
        private string GetEnumDescription(Type type, string value)
        {
            try
            {
                FieldInfo field = type.GetField(value);
                if (field == null)
                {
                    return "";
                }

                var desc = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (desc != null) return desc.Description;

                return "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="value">枚举hasecode</param>
        /// <returns></returns>
        private string GetEnumDescription(Type type, int value)
        {
            try
            {

                FieldInfo field = type.GetField(Enum.GetName(type, value));
                if (field == null)
                {
                    return "";
                }

                var desc = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (desc != null) return desc.Description;

                return "";
            }
            catch
            {
                return "";
            }
        }

    }
}
