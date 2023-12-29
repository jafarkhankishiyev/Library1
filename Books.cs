using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
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
            NpgsqlDataReader reader = DB.SelectAll();
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
        public static async int AddBook(string bookName, string bookAuthor, string bookGenre, string bookYear) 
        {
            var dataSource = DB.ConnectToDB();
            await using var command2 = dataSource.CreateCommand("INSERT INTO books (name, author, genre, released) VALUES (@BookName, @BookAuthor, @BookGenre, @BookYear)");
            command2.Parameters.AddWithValue("@BookName", bookName);
            command2.Parameters.AddWithValue("@BookAuthor", bookAuthor);
            command2.Parameters.AddWithValue("@BookGenre", bookGenre);
            command2.Parameters.AddWithValue("@BookYear", bookYear);
            int number = await command2.ExecuteNonQueryAsync();
            return number;
        }
        public static async int EditBook(string dataChoicePrepared, string dataUpdate, string bookNameString) 
        {
            var dataSource = DB.ConnectToDB();
            await using var command5 = dataSource.CreateCommand($"UPDATE books SET {dataChoicePrepared}=@DataUpdate WHERE name=@BookName");
            command5.Parameters.AddWithValue("@DataUpdate", dataUpdate);
            command5.Parameters.AddWithValue("@BookName", bookNameString);
            int number = await command5.ExecuteNonQueryAsync();
            return number;
        }
        public static async int DeleteBook(List<Book> books, int bookDeleteNumInt) 
        {
            var dataSource = DB.ConnectToDB();
            await using var command7 = dataSource.CreateCommand($"DELETE FROM books WHERE name=@BookToDelete;");
            command7.Parameters.AddWithValue("@BookToDelete", books[bookDeleteNumInt].Name);
            int number = await command7.ExecuteNonQueryAsync();
            return number;
        }
    }
}