using System;
using Library_Management_System.Models;
using Library_Management_System.Services;

namespace Library_Management_System
{
    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            SeedData(library);

            bool isRunning = true;
            do
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("            LIBRARY MANAGEMENT SYSTEM");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. Add a New Book");
                Console.WriteLine("2. View Available Books");
                Console.WriteLine("3. Register a New Member");
                Console.WriteLine("4. Borrow a Book");
                Console.WriteLine("5. Return a Book");
                Console.WriteLine("6. Search Catalog (Books & Members)");
                Console.WriteLine("7. View Member Borrowing History");
                Console.WriteLine("8. View Late Returns Report");
                Console.WriteLine("9. Exit");
                Console.WriteLine("==================================================");
                Console.Write("Enter your choice (1-9): ");

                string choice = Console.ReadLine();

                Console.WriteLine("\n--------------------------------------------------");

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter Title: ");
                            string title = Console.ReadLine();
                            Console.Write("Enter Author: ");
                            string author = Console.ReadLine();
                            Console.Write("Enter Year: ");
                            int.TryParse(Console.ReadLine(), out int year);
                            Console.Write("Enter Genre: ");
                            string genre = Console.ReadLine();
                            library.AddBook(title, author, year, genre);
                            break;

                        case "2":
                            library.ViewAvailableBooks();
                            break;

                        case "3":
                            Console.Write("Enter Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter Email: ");
                            string email = Console.ReadLine();
                            Console.Write("Is Premium Member? (y/n): ");
                            bool isPremium = Console.ReadLine()?.ToLower() == "y";
                            library.RegisterMember(name, email, isPremium);
                            break;

                        case "4":
                            Console.Write("Enter Book ID: ");
                            int.TryParse(Console.ReadLine(), out int bookId);
                            Console.Write("Enter Member ID: ");
                            int.TryParse(Console.ReadLine(), out int memberId);
                            library.BorrowBook(bookId, memberId);
                            break;

                        case "5":
                            Console.Write("Enter Book ID to Return: ");
                            int.TryParse(Console.ReadLine(), out int returnBookId);
                            library.ReturnBook(returnBookId);
                            break;

                        case "6":
                            Console.Write("Enter search keyword: ");
                            string keyword = Console.ReadLine();
                            library.SearchCatalog(keyword);
                            break;

                        case "7":
                            Console.Write("Enter Member ID: ");
                            int.TryParse(Console.ReadLine(), out int histMemberId);
                            library.MemberBorrowingHistory(histMemberId);
                            break;

                        case "8":
                            library.LateReturnReport();
                            break;

                        case "9":
                            isRunning = false;
                            Console.WriteLine("Exiting the system. Goodbye!");
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please select a number from 1 to 9.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nSystem Alert: {ex.Message}");
                }

                if (isRunning)
                {
                    Console.WriteLine("\nPress any key to return to the main menu...");
                    Console.ReadKey();
                }

            } while (isRunning);
        }

        static void SeedData(Library library)
        {
            Console.WriteLine("Initializing System & Seeding Data...\n");

            library.AddBook("Clean Code", "Robert C. Martin", 2008, "Programming");
            library.AddBook("The Pragmatic Programmer", "Andrew Hunt", 1999, "Programming");
            library.AddBook("Design Patterns", "Erich Gamma", 1994, "Computer Science");

            library.RegisterMember("Zaid Alazzam", "zaid@example.com", true); 
            library.RegisterMember("Ahmad Ali", "ahmad@example.com", false);  

            Console.WriteLine("\nPress any key to start the Library System...");
            Console.ReadKey();
        }
    }
}