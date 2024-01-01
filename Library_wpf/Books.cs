using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Npgsql;

namespace Library_wpf
{
    public class Book 
    {
        //fields
        public string Name;
        public string Author;
        public string Genre;
        public string Release;
        
        //constructors   
        public Book()
        {
            Name = "Unknown";
            Author = "Unkwnown";
            Genre = "Unknown";
            Release = "Unknown";
        }
        public Book(string name, string author, string genre, string release) 
        {
            Name = name;
            Author = author;
            Genre = genre;
            Release = release;
        }

        //methods
        public static async void GetBooksAsync(List<Book> books) 
        {
            await using var dataSource = NpgsqlDataSource.Create(DB.GetConnectionString());
            await using var command = dataSource.CreateCommand(DB.Read());
            await using var reader = await command.ExecuteReaderAsync();
            int i = 0;
            if (reader.HasRows) 
            {
                while (await reader.ReadAsync()) 
                {
                    books[i].Name = reader.GetString(0);
                    books[i].Author = reader.GetString(1);
                    books[i].Genre = reader.GetString(2);
                    books[i].Release = reader.GetString(3);
                    i++;
                }
            }
        }
        public static async void AddBook(string bookName, string bookAuthor, string bookGenre, string bookYear, int number) 
        {
            await using var dataSource = NpgsqlDataSource.Create(DB.GetConnectionString());
            await using var command = dataSource.CreateCommand(DB.Create());
            await using var command2 = dataSource.CreateCommand();
            command2.Parameters.AddWithValue("@BookName", bookName);
            command2.Parameters.AddWithValue("@BookAuthor", bookAuthor);
            command2.Parameters.AddWithValue("@BookGenre", bookGenre);
            command2.Parameters.AddWithValue("@BookYear", bookYear);
            number = await command2.ExecuteNonQueryAsync();
        }
        public static async void EditBook(string dataChoicePrepared, string dataUpdate, string bookNameString, int number) 
        {
            await using var dataSource = NpgsqlDataSource.Create(DB.GetConnectionString());
            await using var command5 = dataSource.CreateCommand(DB.Edit(dataChoicePrepared));
            command5.Parameters.AddWithValue("@DataUpdate", dataUpdate);
            command5.Parameters.AddWithValue("@BookName", bookNameString);
            number = await command5.ExecuteNonQueryAsync();
        }
        public static async void DeleteBook(List<Book> books, int bookDeleteNumInt, int number) 
        {
            await using var dataSource = NpgsqlDataSource.Create(DB.GetConnectionString());
            await using var command7 = dataSource.CreateCommand(DB.Delete());
            command7.Parameters.AddWithValue("@BookToDelete", books[bookDeleteNumInt].Name);
            number = await command7.ExecuteNonQueryAsync();
        }
    }
}