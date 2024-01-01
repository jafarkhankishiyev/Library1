using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Npgsql;

namespace Library {
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
        public static async Task<List<Book>> GetBooksAsync() 
        {
            await using var dataSource = NpgsqlDataSource.Create(DB.GetConnectionString());
            await using var command = dataSource.CreateCommand(DB.Read());
            await using var reader = await command.ExecuteReaderAsync();
            var books = new List<Book>();
            if (reader.HasRows) 
            {
                while (await reader.ReadAsync()) 
                {
                    Book book = new Book();
                    book.Name = reader.GetString(0);
                    book.Author = reader.GetString(1);
                    book.Genre = reader.GetString(2);
                    book.Release = reader.GetString(3);
                    books.Add(book);
                }
            }
            return books;
        }
        public static async Task<int> AddBook(string bookName, string bookAuthor, string bookGenre, string bookYear) 
        {
            await using var dataSource = NpgsqlDataSource.Create(DB.GetConnectionString());
            await using var command2 = dataSource.CreateCommand(DB.Create());
            command2.Parameters.AddWithValue("@BookName", bookName);
            command2.Parameters.AddWithValue("@BookAuthor", bookAuthor);
            command2.Parameters.AddWithValue("@BookGenre", bookGenre);
            command2.Parameters.AddWithValue("@BookYear", bookYear);
            int number = await command2.ExecuteNonQueryAsync();
            return number;
        }
        public static async Task<int> EditBook(string dataChoicePrepared, string dataUpdate, string bookNameString) 
        {
            await using var dataSource = NpgsqlDataSource.Create(DB.GetConnectionString());
            await using var command5 = dataSource.CreateCommand(DB.Edit(dataChoicePrepared));
            command5.Parameters.AddWithValue("@DataUpdate", dataUpdate);
            command5.Parameters.AddWithValue("@BookName", bookNameString);
            int number = await command5.ExecuteNonQueryAsync();
            return number;
        }
        public static async Task<int> DeleteBook(List<Book> books, int bookDeleteNumInt) 
        {
            await using var dataSource = NpgsqlDataSource.Create(DB.GetConnectionString());
            await using var command7 = dataSource.CreateCommand(DB.Delete());
            command7.Parameters.AddWithValue("@BookToDelete", books[bookDeleteNumInt].Name);
            int number = await command7.ExecuteNonQueryAsync();
            return number;
        }
    }
}