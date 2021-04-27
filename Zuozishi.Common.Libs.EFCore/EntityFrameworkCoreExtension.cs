﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace Zuozishi.Common.Libs.EFCore
{
    public static class EntityFrameworkCoreExtension
    {
        private static DbCommand CreateCommand(DatabaseFacade facade, string sql, out DbConnection connection, params object[] parameters)
        {
            var conn = facade.GetDbConnection();
            connection = conn;
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
            return cmd;
        }

        public static DataTable SqlQuery(this DatabaseFacade facade, string sql, params object[] parameters)
        {

            var command = CreateCommand(facade, sql, out DbConnection conn, parameters);
            var reader = command.ExecuteReader();
            var dt = new DataTable();
            dt.Load(reader);
            reader.Close();
            conn.Close();
            return dt;
        }

        public static DataTable SqlQuery(this DatabaseFacade facade, string sql) => SqlQuery(facade, sql, null);

        public static List<T> SqlQuery<T>(this DatabaseFacade facade, string sql, params object[] parameters) where T : class, new()
        {
            var dt = SqlQuery(facade, sql, parameters);
            return dt.ToList<T>();
        }

        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            var propertyInfos = typeof(T).GetProperties();
            var list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                var t = new T();
                foreach (PropertyInfo p in propertyInfos)
                {
                    string name = p.Name;
                    var attr = p.GetCustomAttribute(typeof(ColumnAttribute), false);
                    if(attr != null)
                    {
                        var attrObj = attr as ColumnAttribute;
                        if (!string.IsNullOrEmpty(attrObj.Name))
                            name = attrObj.Name;
                    }
                    if (dt.Columns.IndexOf(name) != -1 && row[name] != DBNull.Value)
                        p.SetValue(t, row[name], null);
                }
                list.Add(t);
            }
            return list;
        }
    }
}
