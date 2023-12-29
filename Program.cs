using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Npgsql;


namespace Library
{
    class Program
    {
        static async Task Main(string[] args)
        {
            


            //Выбор операции

            
            
            int exitNum = 0;


            //Выполнение операций
            while (exitNum == 0) {
                Console.WriteLine("\n Выберите операцию, которую хотите выполнить: \n 1. Получить список доступных книг. \n 2. Добавить новую книгу. \n 3. Изменить данные о книге. \n 4. Удалить книгу из каталога. \n 5. Введите любое другое значение чтобы выйти. \n Выберите операцию:");
                string result = Console.ReadLine();

                //Read
                if(result == "1") {
                    Console.WriteLine("\n Название \t Автор \t Жанр \t Год выпуска");
                    List<Book> books = await Book.getBooksAsync();
                    int i = 1;
                    foreach (Book b in books) {
                        Console.WriteLine($"\n {i}. {b.Name} \t {b.Author} \t {b.Genre} \t {b.Release}");
                        i++;
                }
                
    

                } else if (result == "2") {

                    Console.WriteLine(" \n Введите название книги:");
                    string bookName = Console.ReadLine();
                    Console.WriteLine(" \n Введите имя и фамилию автора:");
                    string bookAuthor = Console.ReadLine();
                    Console.WriteLine("Введите жанр книги:");
                    string bookGenre = Console.ReadLine();
                    Console.WriteLine(" \n Введите год выпуска книги:");
                    string bookYear = Console.ReadLine();

                    int number = Book.addBook(bookName, bookAuthor, bookGenre, bookYear);
                    if (number > 0) {
                        Console.WriteLine($"\n Операция успешно выполнена. {number} объект добавлен.");
                    }

                } else if (result == "3") {
                    Console.WriteLine("\n Выберите книгу данные о которой хотите изменить:");
                    int i = 1;
                    List<Book> books = await Book.getBooksAsync();
                    foreach (Book b in books) {
                        Console.WriteLine($"\n {i}. {b.Name} \t {b.Author} \t {b.Genre} \t {b.Release}");
                        i++;
                    }   
                                    
                    
                    Console.WriteLine("\n Введите номер книги:");
                    string bookChoice = Console.ReadLine();
                    int bookChoiceInt = Int32.Parse(bookChoice);
                    bookChoiceInt--;
                    Console.WriteLine("\n Какие данные хотите изменить? \n 1. Название \n 2. Автор \n 3. Жанр \n 4. Год выпуска \n Введите номер ответа:");
                    string dataChoice = Console.ReadLine();
                    int dataChoiceInt = Int32.Parse(dataChoice);
                    dataChoiceInt--;
                    string[] dataChoices = {"name", "author", "genre", "released"};
                    Console.WriteLine("\n Введите данные, на которые хотите заменить существующие:");
                    string dataUpdate = Console.ReadLine();
                    string dataChoicePrepared = dataChoices[dataChoiceInt];
                    object bookName = books[bookChoiceInt].Name;
                    string bookNameString = bookName.ToString();

                    int number = Book.editBook(dataChoicePrepared, dataUpdate, bookNameString);

                    if (number > 0) {
                        Console.WriteLine($"\n Операция успешно выполнена. {number} объект обновлен.");
                    }
                        
                } else if (result == "4") {
                    int i = 1;
                    List<Book> books = await Book.getBooksAsync();
                    foreach (Book b in books) {
                        Console.WriteLine($"\n {i}. {b.Name} \t {b.Author} \t {b.Genre} \t {b.Release}");
                        i++;
                    }   
                            Console.WriteLine("\n Введите номер книги, которую хотите удалить:");
                            string bookDeleteNum = Console.ReadLine();
                            int bookDeleteNumInt = Int32.Parse(bookDeleteNum);
                            bookDeleteNumInt--;
                            int number = Book.deleteBook(books, bookDeleteNumInt);
                            Console.WriteLine($"\n Удалено {number} книг.");

                        
                } else {
                    exitNum++;
                }
                
            }
            


    }

}
}