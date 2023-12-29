using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Npgsql;

namespace Library 
{
    public class DB 
    {
        public static async NpgsqlDataSource ConnectToDB() {
            DBConfiguration dbConfData = new DBConfiguration();
            var connectionString = $"Server={dbConfData.Server};User Id = {dbConfData.UserId}; Password = {dbConfData.UserId}; Database={dbConfData.Database}";
            await using var dataSource = NpgsqlDataSource.Create(connectionString);
            return dataSource;
        }
        public static async NpgsqlDataReader SelectAll() {
            var dataSource = DB.ConnectToDB();
            await using var command = dataSource.CreateCommand("SELECT * FROM books");
            await using var reader = await command.ExecuteReaderAsync();
            return reader;
        }
    }

}