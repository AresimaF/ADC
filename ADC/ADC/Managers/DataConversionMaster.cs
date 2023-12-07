using ADC.Screens.SystemScreens.Loading;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ADC.Managers
{
    internal class DataConversionMaster
    {
        public DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public List<T> DataTableToList<T>(DataTable dt)
        {

            foreach (DataColumn column in dt.Columns)
            {
                dt.Columns[column.ColumnName].ColumnName = column.ColumnName.Replace(" ", "").Replace("-", "").ToLower();
            }

            var list = new List<T>();
            var boolTypes = new List<Type>() { typeof(bool?), typeof(bool) };
            var boolValues = new List<string>() { "true", "yes", "1", "y" };
            var stringTypes = new List<Type>() { typeof(string) };
            var dateTypes = new List<Type>() { typeof(DateTime?), typeof(DateTime) };

            foreach (DataRow row in dt.Rows)
            {
                var EntityModel = (T)Activator.CreateInstance(typeof(T));
                foreach (var property in EntityModel.GetType().GetProperties())
                {
                    try
                    {
                        if (dt.Columns.Contains(property.Name.ToLower()))
                        {
                            var propValue = row[property.Name.ToLower()].ToString();

                            object value = null;
                            var memType = property.PropertyType;

                            if (boolTypes.Contains(memType))
                                value = boolValues.Contains(propValue.ToLower());
                            else if (stringTypes.Contains(memType))
                                value = string.IsNullOrWhiteSpace(propValue) ? null : propValue.Trim();
                            else if (dateTypes.Contains(memType))
                                value = string.IsNullOrWhiteSpace(propValue) ? (DateTime?)null : DateTime.Parse(propValue);

                            EntityModel.GetType().GetProperty(property.Name).SetValue(EntityModel, value);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                list.Add(EntityModel);
            }
            return list;
        }


    }
}
