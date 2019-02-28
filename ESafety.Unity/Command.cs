using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using System.ComponentModel;

namespace ESafety.Unity
{
    /// <summary>
    /// 独立方法类
    /// </summary>
    public class Command
    {
        /// <summary>
        /// 获取枚举项集合
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static IList<EnumItem> GetItems(Type enumType)
        {
            if (!enumType.IsEnum)
                throw new InvalidOperationException();

            IList<EnumItem> list = new List<EnumItem>();

            // 获取Description特性 
            Type typeDescription = typeof(DescriptionAttribute);
            // 获取枚举字段
            FieldInfo[] fields = enumType.GetFields();
            foreach (FieldInfo field in fields)
            {
                if (!field.FieldType.IsEnum)
                    continue;

                // 获取枚举值
                int value = (int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);

                // 不包括空项
                if (value > 0)
                {
                    string text = string.Empty;
                    object[] array = field.GetCustomAttributes(typeDescription, false);

                    if (array.Length > 0) text = ((DescriptionAttribute)array[0]).Description;
                    else text = field.Name; //没有描述，直接取值

                    //添加到列表
                    list.Add(new EnumItem { Value = value, Caption = text });
                }
            }
            return list;
        }

        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public static string CreateExcel(DataTable dataSource, string filePaht)
        {
            var file = filePaht;// System.Web.Hosting.HostingEnvironment.MapPath(@"~/Template/");


            //操作excel
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("project");
            //标题行
            NPOI.SS.UserModel.IRow trow = sheet.CreateRow(0);

            foreach (DataColumn col in dataSource.Columns)
            {
                trow.CreateCell(col.Ordinal).SetCellValue(col.Caption);
            }
            //创建数据行
            int j = 1;//第一行是标题
            foreach (DataRow row in dataSource.Rows)
            {
                //创建行
                NPOI.SS.UserModel.IRow drow = sheet.CreateRow(j);
                foreach (DataColumn col in dataSource.Columns)
                {
                    var value = row[col.Ordinal];
                    var str = "";
                    if (!(value is DBNull))
                    {
                        str = value.ToString();
                    }
                    drow.CreateCell(col.Ordinal).SetCellValue(str);
                }

                j++;
            }
            string re = Guid.NewGuid().ToString() + ".xls";

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                book.Write(ms);
                using (System.IO.FileStream fs = new System.IO.FileStream(file + re, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
                book = null;
            }

            //删除 10分钟之前的数据
            foreach (var f in System.IO.Directory.GetFileSystemEntries(file, "*.xls"))
            {
                if (File.Exists(f))
                {
                    if ((DateTime.Now - File.GetCreationTime(f)).TotalMinutes > 10)//10分钟前的数据清掉
                    {

                        File.Delete(f);
                    }
                }
            }

            return re;

        }
        /// <summary>
        /// 创建excel
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string CreateExcel(List<List<string>> source, List<string> list, string filePath)
        {
            try
            {
                var tb = new DataTable();

                var file = filePath;// System.Web.Hosting.HostingEnvironment.MapPath(@"~/Template/");
                                    //操作excel
                NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
                NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("project");
                //标题行
                NPOI.SS.UserModel.IRow trow = sheet.CreateRow(0);
                int t = 0;
                foreach (var item in list)
                {
                    trow.CreateCell(t).SetCellValue(item);
                    t += 1;
                }
                if (source == null)
                    throw new Exception("参数为空");
                if (source.Count() == 0)
                    throw new Exception("参数没有数据");
                //创建数据行
                int j = 1;
                foreach (var obj1 in source)
                {
                    int x = 0;
                    //创建行
                    NPOI.SS.UserModel.IRow drow = sheet.CreateRow(j);
                    foreach (var obj in obj1)
                    {
                        var value = obj;
                        var str = "";
                        if (!(value == null))
                        {
                            str = value.ToString();
                        }

                        drow.CreateCell(x).SetCellValue(str);
                        x++;
                    }
                    j++;
                }
                string re = Guid.NewGuid().ToString() + ".xls";

                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    book.Write(ms);
                    using (System.IO.FileStream fs = new System.IO.FileStream(file + re, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                    {
                        byte[] data = ms.ToArray();
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                    }
                    book = null;
                }

                //删除 10分钟之前的数据
                foreach (var f in System.IO.Directory.GetFileSystemEntries(file, "*.xls"))
                {
                    if (File.Exists(f))
                    {
                        if ((DateTime.Now - File.GetCreationTime(f)).TotalMinutes > 10)//10分钟前的数据清掉
                        {

                            File.Delete(f);
                        }
                    }
                }

                return "OutPutTemp/" + re;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        /// <summary>
        /// 创建excel
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string CreateExcel(IEnumerable<object> source, string filePath)
        {
            try
            {

                var tb = new DataTable();

                var file = filePath;// System.Web.Hosting.HostingEnvironment.MapPath(@"~/Template/");
                                    //操作excel
                NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
                NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("project");
                //标题行
                NPOI.SS.UserModel.IRow trow = sheet.CreateRow(0);
                if (source == null)
                    throw new Exception("参数为空");
                if (source.Count() == 0)
                    throw new Exception("参数没有数据");

                System.Type t = source.FirstOrDefault().GetType();
                var sps = t.GetProperties();
                int rowint = 0;
                foreach (var p in sps)
                {
                    trow.CreateCell(rowint).SetCellValue(p.Name);
                    rowint++;
                }
                //创建数据行
                int j = 1;
                foreach (var obj in source)
                {
                    int x = 0;
                    //创建行
                    NPOI.SS.UserModel.IRow drow = sheet.CreateRow(j);
                    foreach (var p in sps)
                    {
                        var value = p.GetValue(obj);
                        var str = "";
                        if (!(value == null))
                        {
                            str = value.ToString();
                        }

                        drow.CreateCell(x).SetCellValue(str);
                        x++;
                    }

                    j++;
                }
                string re = Guid.NewGuid().ToString() + ".xls";

                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    book.Write(ms);
                    using (System.IO.FileStream fs = new System.IO.FileStream(file + re, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                    {
                        byte[] data = ms.ToArray();
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                    }
                    book = null;
                }

                //删除 10分钟之前的数据
                foreach (var f in System.IO.Directory.GetFileSystemEntries(file, "*.xls"))
                {
                    if (File.Exists(f))
                    {
                        if ((DateTime.Now - File.GetCreationTime(f)).TotalMinutes > 10)//10分钟前的数据清掉
                        {

                            File.Delete(f);
                        }
                    }
                }

                return "OutPutTemp/" + re;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }


        /// <summary>
        /// 读取Excel
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DataTable ReadExcel(string filePath)
        {

            FileInfo file = new FileInfo(filePath);
            if (!file.Exists)
            {
                throw new Exception("文件不存在");
            }
            DataTable dt = new DataTable();
            using (System.IO.FileStream fdread = System.IO.File.OpenRead(filePath))
            {
                IWorkbook wk = null;

                string extension = file.Extension;
                if (extension == ".xlsx" || extension == ".xls")
                {
                    if (extension == ".xlsx")
                    {
                        wk = new XSSFWorkbook(fdread);
                    }
                    else
                    {
                        wk = new HSSFWorkbook(fdread);
                    }
                }
                ISheet sheet = wk.GetSheetAt(0);
                IRow headrow = sheet.GetRow(0);

                for (int i = headrow.FirstCellNum; i < headrow.Cells.Count; i++)
                {
                    DataColumn dcol = new DataColumn();
                    dt.Columns.Add(dcol);
                }
                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    bool result = false;
                    DataRow dr = dt.NewRow();
                    IRow row = sheet.GetRow(i);
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        ICell cell = row.GetCell(j);
                        dr[j] = GetExcelCell(cell);
                        if (dr[j].ToString() != "")
                        {
                            result = true;
                        }
                    }
                    if (result)
                    {
                        dt.Rows.Add(dr);
                    }

                }


            }
            return dt;
        }

        private static string GetExcelCell(ICell cell)
        {
            if (cell == null)
                return string.Empty;
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return string.Empty;
                    break;
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                    break;
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                    break;
                case CellType.Formula:
                    try
                    {
                        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
                    break;
                case CellType.Numeric:
                    if (HSSFDateUtil.IsCellDateFormatted(cell))
                    {
                        return cell.DateCellValue.ToString();

                    }
                    else
                    {
                        return cell.NumericCellValue.ToString();
                    }
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Unknown:
                default:
                    return cell.ToString();
            }
        }

        /// <summary>
        /// 获取指定时间的时间戳，未指定则为当前时间
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static long GetTimestamp(DateTime? d = null)
        {
            DateTime dt = d == null ? DateTime.Now :(DateTime) d;
            TimeSpan ts= dt.ToUniversalTime() - new DateTime(1970, 1, 1);
            return (long) ts.TotalMilliseconds;
        }

        /// <summary>
        /// 随机数，纯数字
        /// </summary>
        /// <returns></returns>
        public static string CreateRandIntCode(int length, int factor)
        {
            string basecode = "0123456789";
            Random random = new Random(factor);
            string re = "";
            for (int i = 0; i < length; i++)
            {
                int number = random.Next(basecode.Length);
                re += basecode.Substring(number, 1);
            }
            return re;
        }

        /// <summary>
        /// 随机数，字符数字组合
        /// </summary>
        /// <param name="length"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static string CreateRandStrCode(int length, int factor)
        {
            string basecode = "123456789abcdefghijklmnprstuvwxyzABCDEFGHIJKLMNPQRSTUVWXYZ";
            Random random = new Random(factor);
            string re = "";
            for (int i = 0; i < length; i++)
            {
                int number = random.Next(basecode.Length);
                re += basecode.Substring(number, 1);
            }
            return re;
        }
        /// <summary>
        /// 创建登陆Token
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CreateToken(int length)
        {
            //定义  
            string basestr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            string sb = "";
            for (int i = 0; i < length; i++)
            {
                int number = random.Next(basestr.Length);
                sb += basestr.Substring(number, 1);
            }
            return sb;
        }
        /// <summary>
        /// 创建编号
        /// </summary>
        /// <returns></returns>
        public static string CreateCode()
        {
            Random rdom = new Random();
            int rnum = rdom.Next();
            var rdstr = CreateRandStrCode(5, rnum);
            var tmsp = GetTimestamp();
            return rdstr.ToUpper() +"_"+ tmsp.ToString();
        }

        /// <summary>
        /// 行转列，单行GrouP
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="DimensionList">维度列，不在返回中</param>
        /// <param name="DynamicColumn">要将值转为列的列名，此列的企GROUP后转为列</param>
        /// <param name="AllDynamicColumn"></param>
        /// <returns></returns>
        public static List<dynamic> DynamicLinqSingle<T>(List<T> list, List<string> DimensionList, string DynamicColumn, out List<string> AllDynamicColumn) where T : class
        {
            //获取所有动态列
            var columnGroup = list.GroupBy(DynamicColumn, "new(it as Vm)") as IEnumerable<IGrouping<dynamic, dynamic>>;
            List<string> AllColumnList = new List<string>();
            foreach (var item in columnGroup)
            {
                if (!string.IsNullOrEmpty(item.Key))
                {
                    AllColumnList.Add(item.Key);
                }
            }
            AllDynamicColumn = AllColumnList;
            var dictFunc = new Dictionary<string, Func<T, bool>>();
            foreach (var column in AllColumnList)
            {
                var func = System.Linq.Dynamic.DynamicExpression.ParseLambda<T, bool>(string.Format("{0}==\"{1}\"", DynamicColumn, column)).Compile();
                dictFunc[column] = func;
            }

            //获取实体所有属性
            Dictionary<string, PropertyInfo> PropertyInfoDict = new Dictionary<string, PropertyInfo>();
            Type type = typeof(T);
            var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            //数值列
            List<string> AllNumberField = new List<string>();
            foreach (var item in propertyInfos)
            {
                PropertyInfoDict[item.Name] = item;
                if (item.PropertyType == typeof(object))
                {
                    AllNumberField.Add(item.Name);
                }
            }

            //分组
            var dataGroup = list.GroupBy(string.Format("new ({0})", string.Join(",", DimensionList)), "new(it as Vm)") as IEnumerable<IGrouping<dynamic, dynamic>>;
            List<dynamic> listResult = new List<dynamic>();
            IDictionary<string, object> itemObj = null;
            T vm2 = default(T);
            foreach (var group in dataGroup)
            {
                itemObj = new ExpandoObject();
                var listVm = group.Select(e => e.Vm as T).ToList();
                //维度列赋值
                vm2 = listVm.FirstOrDefault();
                //foreach (var key in DimensionList)
                //{
                //    itemObj[key] = PropertyInfoDict[key].GetValue(vm2);
                //}

                foreach (var column in AllColumnList)
                {
                    vm2 = listVm.FirstOrDefault(dictFunc[column]);
                    if (vm2 != null)
                    {
                        foreach (string name in AllNumberField)
                        {
                            itemObj[column] = PropertyInfoDict[name].GetValue(vm2);
                        }
                    }
                }
                listResult.Add(itemObj);
            }
            return listResult;
        }

        /// <summary>
        /// LINq行转列，多行GrouP
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="DimensionList">维度列</param>
        /// <param name="DynamicColumn">要将值转为列的列名，此列的企GROUP后转为列</param>
        /// <param name="AllDynamicColumn"></param>
        /// <returns></returns>
        public static List<dynamic> DynamicLinq<T>(List<T> list, List<string> DimensionList, string DynamicColumn, out List<string> AllDynamicColumn) where T : class
        {
            //获取所有动态列
            var columnGroup = list.GroupBy(DynamicColumn, "new(it as Vm)") as IEnumerable<IGrouping<dynamic, dynamic>>;
            List<string> AllColumnList = new List<string>();
            foreach (var item in columnGroup)
            {
                if (!string.IsNullOrEmpty(item.Key))
                {
                    AllColumnList.Add(item.Key);
                }
            }
            AllDynamicColumn = AllColumnList;
            var dictFunc = new Dictionary<string, Func<T, bool>>();
            foreach (var column in AllColumnList)
            {
                var func = System.Linq.Dynamic.DynamicExpression.ParseLambda<T, bool>(string.Format("{0}==\"{1}\"", DynamicColumn, column)).Compile();
                dictFunc[column] = func;
            }

            //获取实体所有属性
            Dictionary<string, PropertyInfo> PropertyInfoDict = new Dictionary<string, PropertyInfo>();
            Type type = typeof(T);
            var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            //数值列
            List<string> AllNumberField = new List<string>();
            foreach (var item in propertyInfos)
            {
                PropertyInfoDict[item.Name] = item;
                if (item.PropertyType == typeof(object))
                {
                    AllNumberField.Add(item.Name);
                }
            }

            //分组
            var dataGroup = list.GroupBy(string.Format("new ({0})", string.Join(",", DimensionList)), "new(it as Vm)") as IEnumerable<IGrouping<dynamic, dynamic>>;
            List<dynamic> listResult = new List<dynamic>();
            IDictionary<string, object> itemObj = null;
            T vm2 = default(T);
            foreach (var group in dataGroup)
            {
                itemObj = new ExpandoObject();
                var listVm = group.Select(e => e.Vm as T).ToList();
                //维度列赋值
                vm2 = listVm.FirstOrDefault();
                foreach (var key in DimensionList)
                {
                    itemObj[key] = PropertyInfoDict[key].GetValue(vm2);
                }

                foreach (var column in AllColumnList)
                {
                    vm2 = listVm.FirstOrDefault(dictFunc[column]);
                    if (vm2 != null)
                    {
                        foreach (string name in AllNumberField)
                        {
                            itemObj[column] = PropertyInfoDict[name].GetValue(vm2);
                        }
                    }
                }
                listResult.Add(itemObj);
            }
            return listResult;
        }
    }
} 
