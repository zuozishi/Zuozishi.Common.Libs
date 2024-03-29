﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace Zuozishi.Common.Libs.EFCore
{
    public static class EntityFrameworkCoreExtension
    {
        private static DbCommand CreateCommand(DatabaseFacade facade, string sql, out DbConnection connection, params KeyValuePair<string, object>[] parameters)
        {
            var conn = facade.GetDbConnection();
            connection = conn;
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    var param = cmd.CreateParameter();
                    param.ParameterName = item.Key;
                    param.Value = item.Value;
                    cmd.Parameters.Add(param);
                }
            }
            return cmd;
        }

        /// <summary>
        /// 执行自定义SQL查询
        /// </summary>
        /// <param name="facade"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable SqlQuery(this DbContext context, string sql, params KeyValuePair<string, object>[] parameters)
        {
            var command = CreateCommand(context.Database, sql, out DbConnection conn, parameters);
            var reader = command.ExecuteReader();
            var dt = new DataTable();
            dt.Load(reader);
            reader.Close();
            conn.Close();
            return dt;
        }

        /// <summary>
        /// 执行自定义SQL查询
        /// </summary>
        /// <param name="facade"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<DataTable> SqlQueryAsync(this DbContext context, string sql, params KeyValuePair<string, object>[] parameters)
        {
            var command = CreateCommand(context.Database, sql, out DbConnection conn, parameters);
            var reader = await command.ExecuteReaderAsync();
            var dt = new DataTable();
            dt.Load(reader);
            reader.Close();
            conn.Close();
            return dt;
        }

        /// <summary>
        /// 执行自定义SQL查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="facade"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static List<T> SqlQuery<T>(this DbContext context, string sql, params KeyValuePair<string, object>[] parameters) where T : class, new()
        {
            using var dt = SqlQuery(context, sql, parameters);
            return dt.ToList<T>();
        }

        /// <summary>
        /// 执行自定义SQL查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="facade"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<List<T>> SqlQueryAsync<T>(this DbContext context, string sql, params KeyValuePair<string, object>[] parameters) where T : class, new()
        {
            using var dt = await SqlQueryAsync(context, sql, parameters);
            return dt.ToList<T>();
        }

        /// <summary>
        /// DataTable转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
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
                    if (attr != null)
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
