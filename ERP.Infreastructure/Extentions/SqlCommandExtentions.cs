using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ERP.Infreastructure.Extentions
{
    public static class SqlCommandExtentions
    {
        public static void AddParameters(this SqlCommand cmd, object obj)
        {
            if (obj==null)
                return;

            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {

                object value = property.GetValue(obj)??DBNull.Value;
             
                cmd.Parameters.AddWithValue("@"+property.Name, value);
            }

        }

        public static void AddParameters(this SqlCommand cmd, string name, object value)
        {
            cmd.Parameters.AddWithValue(name, value??DBNull.Value);
        }




    }
}
