using System;
using System.Data;
using System.Threading.Tasks;
using Npgsql;


namespace Library
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Установка соединения
            var connectionString = "Server=localhost;User Id = postgres; Password = 123; Database=postgres";
            await using var dataSource = NpgsqlDataSource.Create(connectionString);

            //Выбор операции

            
            
            int exitNum = 0;


            //Выполнение операций
            while (exitNum == 0) {
                Console.WriteLine("\n Выберите операцию, которую хотите выполнить: \n 1. Получить список доступных книг. \n 2. Добавить новую книгу. \n 3. Изменить данные о книге. \n 4. Удалить книгу из каталога. \n 5. Введите любое другое значение чтобы выйти. \n Выберите операцию:");
                string result = Console.ReadLine();
                if(result == "1") {
                await using var command1 = dataSource.CreateCommand("SELECT * FROM books");
                await using var reader1 = await command1.ExecuteReaderAsync();
                Console.WriteLine("\n Название \t Автор \t Жанр \t Год выпуска");
                int i = 1;

                if (reader1.HasRows) 
                    {
                        while (await reader1.ReadAsync()) {
                            object name = reader1.GetValue(0);
                            object author = reader1.GetValue(1);
                            object genre = reader1.GetValue(2);
                            object year = reader1.GetValue(3);

                            Console.WriteLine($"\n {i}. {name} \t {author} \t {genre} \t {year}");
                            i++;
                        }
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

                    await using var command2 = dataSource.CreateCommand("INSERT INTO books (name, author, genre, released) VALUES (@BookName, @BookAuthor, @BookGenre, @BookYear)");
                    command2.Parameters.AddWithValue("@BookName", bookName);
                    command2.Parameters.AddWithValue("@BookAuthor", bookAuthor);
                    command2.Parameters.AddWithValue("@BookGenre", bookGenre);
                    command2.Parameters.AddWithValue("@BookYear", bookYear);
                    int number = await command2.ExecuteNonQueryAsync();
                    if (number > 0) {
                        Console.WriteLine($"\n Операция успешно выполнена. {number} объект добавлен.");
                    }

                } else if (result == "3") {
                    await using var command3 = dataSource.CreateCommand("SELECT * FROM books");
                    await using var reader3 = await command3.ExecuteReaderAsync();
                    Console.WriteLine("\n Выберите книгу данные о которой хотите изменить:");
                    int i = 1;
                    int a = 0;
                                    

                    if (reader3.HasRows) 
                        {
                            while (await reader3.ReadAsync()) {
                                object name = reader3.GetValue(0);
                                object author = reader3.GetValue(1);
                                object genre = reader3.GetValue(2);
                                object year = reader3.GetValue(3);

                                Console.WriteLine($"\n {i}. {name} \t {author} \t {genre} \t {year}");
                                i++;
                                a++;
                            }
                            object[] names = new object[a];
                            int c = 0;
                            await using var command4 = dataSource.CreateCommand("SELECT * FROM books");
                            await using var reader4 = await command4.ExecuteReaderAsync();
                            while (await reader4.ReadAsync()) {
                                names[c] = reader4.GetValue(0);
                                c++;
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
                            object bookName = names[bookChoiceInt];
                            string bookNameString = bookName.ToString();
                            await using var command5 = dataSource.CreateCommand($"UPDATE books SET {dataChoicePrepared}=@DataUpdate WHERE name=@BookName");
                            command5.Parameters.AddWithValue("@DataUpdate", dataUpdate);
                            command5.Parameters.AddWithValue("@BookName", bookNameString);
                            int number = await command5.ExecuteNonQueryAsync();
                            if (number > 0) {
                                Console.WriteLine($"\n Операция успешно выполнена. {number} объект обновлен.");
                    }
                        } 
                } else if (result == "4") {
                    await using var command5 = dataSource.CreateCommand("SELECT * FROM books");
                    await using var reader5 = await command5.ExecuteReaderAsync();
                    int i = 1;
                    int a = 0;


                    if (reader5.HasRows) 
                        {
                            while (await reader5.ReadAsync()) {
                                object name = reader5.GetValue(0);
                                object author = reader5.GetValue(1);
                                object genre = reader5.GetValue(2);
                                object year = reader5.GetValue(3);

                                Console.WriteLine($"\n {i}. {name} \t {author} \t {genre} \t {year}");
                                i++;
                                a++;
                            }
                            object[] names = new object[a];
                            int c = 0;
                            await using var command6 = dataSource.CreateCommand("SELECT * FROM books");
                            await using var reader6 = await command6.ExecuteReaderAsync();
                            while (await reader6.ReadAsync()) {
                                names[c] = reader6.GetValue(0);
                                c++;
                            }

                            
                            Console.WriteLine("\n Введите номер книги, которую хотите удалить:");
                            string bookDeleteNum = Console.ReadLine();
                            int bookDeleteNumInt = Int32.Parse(bookDeleteNum);
                            bookDeleteNumInt--;
                            await using var command7 = dataSource.CreateCommand($"DELETE FROM books WHERE name=@BookToDelete;");
                            command7.Parameters.AddWithValue("@BookToDelete", names[bookDeleteNumInt]);
                            await using var reader7 = await command7.ExecuteReaderAsync();
                        }
                } else {
                    exitNum++;
                }
                
            }
            


    }
}
}