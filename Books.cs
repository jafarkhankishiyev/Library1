using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Npgsql;

namespace Library {
    public class Book {

        //fields
        public string Name;
        public string Author;
        public string Genre;
        public string Release;
        
        //constructor
        public Book(string name, string author, string genre, string release) {
            Name = name;
            Author = author;
            Genre = genre;
            Release = release;
        }

        //methods
        public static async Task<List<Book>> getBooksAsync() {
            var connectionString = "Server=localhost;User Id = postgres; Password = 123; Database=library";
            await using var dataSource = NpgsqlDataSource.Create(connectionString);
            await using var command = dataSource.CreateCommand("SELECT * FROM books");
            await using var reader = await command.ExecuteReaderAsync();
            var books = new List<Book>();

            if (reader.HasRows) {
                while (await reader.ReadAsync()) {

                    object name = reader.GetValue(0);
                    object author = reader.GetValue(1);
                    object genre = reader.GetValue(2);
                    object year = reader.GetValue(3);

                    string nameString = name.ToString();
                    string authorString = author.ToString();
                    string genreString = genre.ToString();
                    string yearString = year.ToString();
                    Book book = new Book(nameString, authorString, genreString, yearString);
                    books.Add(book);
                }
            }
            return books;
        }
        public static async int addBook(string bookName, string bookAuthor, string bookGenre, string bookYear) {
            var connectionString = "Server=localhost;User Id = postgres; Password = 123; Database=library";
            await using var dataSource = NpgsqlDataSource.Create(connectionString);
            await using var command2 = dataSource.CreateCommand("INSERT INTO books (name, author, genre, released) VALUES (@BookName, @BookAuthor, @BookGenre, @BookYear)");
            command2.Parameters.AddWithValue("@BookName", bookName);
            command2.Parameters.AddWithValue("@BookAuthor", bookAuthor);
            command2.Parameters.AddWithValue("@BookGenre", bookGenre);
            command2.Parameters.AddWithValue("@BookYear", bookYear);
            int number = await command2.ExecuteNonQueryAsync();
            return number;
        }
        public static async int editBook(string dataChoicePrepared, string dataUpdate, string bookNameString) {
            var connectionString = "Server=localhost;User Id = postgres; Password = 123; Database=library";
            await using var dataSource = NpgsqlDataSource.Create(connectionString);
            await using var command5 = dataSource.CreateCommand($"UPDATE books SET {dataChoicePrepared}=@DataUpdate WHERE name=@BookName");
            command5.Parameters.AddWithValue("@DataUpdate", dataUpdate);
            command5.Parameters.AddWithValue("@BookName", bookNameString);
            int number = await command5.ExecuteNonQueryAsync();
            return number;
        }
        public static async int deleteBook(List<Book> books, int bookDeleteNumInt) {
            var connectionString = "Server=localhost;User Id = postgres; Password = 123; Database=library";
            await using var dataSource = NpgsqlDataSource.Create(connectionString);
            await using var command7 = dataSource.CreateCommand($"DELETE FROM books WHERE name=@BookToDelete;");
            command7.Parameters.AddWithValue("@BookToDelete", books[bookDeleteNumInt].Name);
            await using var reader7 = await command7.ExecuteReaderAsync();
        }
    }
}