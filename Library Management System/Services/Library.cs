using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library_Management_System.Services
{
    public class Library
    {
        private Book[] _books = new Book[100];
        private Member[] _member = new Member[100];
        private BorrowRecord[] _borrowRecord = new BorrowRecord[100];

        private int _bookCount = 0;
        private int _memberCount = 0;
        private int _recordCount = 0;


        public void AddBook(string title, string author, int year, string genre)
        {
            if (_bookCount >= _books.Length)
            {
                Console.WriteLine("Error: Library book capacity reached.");
                return;
            }

            Book newBook = new Book
            {
                Id = _bookCount + 1,
                Title = title,
                Author = author,
                Year = year,
                Genre = genre,
                AddedDate = DateTime.Now
            };

            _books[_bookCount] = newBook;
            _bookCount++;
            Console.WriteLine($"Success: Book '{title}' added with ID {newBook.Id}.");

        }

        public void RegisterMember(string name, string email, bool isPremium)
        {
            if (_memberCount >= _member.Length)
            {
                Console.WriteLine("Error: Member capacity reached.");
                return;
            }

            Member newMember;

            if (isPremium)
            {
                newMember = new PremiumMember();
            }
            else
            {
                newMember = new Member();
            }

            newMember.Id = _memberCount + 1;
            newMember.Name = name;
            newMember.Email = email;
            newMember.JoinDate = DateTime.Now;

            _member[_memberCount] = newMember;
            _memberCount++;
            string MemberType = isPremium ? "Premium" : "Regular";
            Console.WriteLine($"Success: {MemberType} Member '{name}' registered with ID {newMember.Id}.");
        }

        public void BorrowBook(int bookId, int memberId)
        {
            Book targetBook = null;
            for (int i = 0; i < _bookCount; i++)
            {
                if (_books[i].Id == bookId)
                {
                    targetBook = _books[i];
                    break;
                }
            }

            Member targetMember = null;
            for (int i = 0; i < _memberCount; i++)
            {
                if (_member[i].Id == memberId)
                {
                    targetMember = _member[i];
                    break;
                }
            }

            if (targetBook == null)
            {
                Console.WriteLine($"Error: Book with ID {bookId} not found.");
            }
            if (targetMember == null)
            {
                Console.WriteLine($"Error: Member with ID {memberId} not found.");
            }

            if (targetBook.IsAvailable == false)
            {
                throw new Exception($"Book '{targetBook.Title}' is currently not available for borrowing.");
            }

            if (_recordCount >= _borrowRecord.Length)
            {
                Console.WriteLine("Error: Borrow records capacity reached.");
                return;
            }

            bool hasEmptySlot = false;
            for (int i = 0; i < targetMember.BorrowedBooks.Length; i++)
            {
                if (targetMember.BorrowedBooks[i] == null) 
                {
                    targetMember.BorrowedBooks[i] = targetBook;
                    hasEmptySlot = true;
                    break;
                }
            }

            if (!hasEmptySlot)
            {
                Console.WriteLine($"Error: Member '{targetMember.Name}' has reached their limit of {targetMember.MaxBorrowLimit} books.");
                return;
            }


            BorrowRecord newRecord = new BorrowRecord
            {
                Id = _recordCount + 1,
                Book = targetBook,
                Member = targetMember,
                BorrowDate = DateTime.Now,
                ReturnDate = null
            };

            _borrowRecord[_recordCount] = newRecord;
            _recordCount++;
            targetBook.IsAvailable = false;
        }

        public void ReturnBook(int bookId)
        {
            BorrowRecord targetRecord = null;
            for (int i = 0; i < _borrowRecord.Length; i++)
            {
                if (_borrowRecord[i].Id == bookId && _borrowRecord[i].ReturnDate == null)
                {
                    targetRecord = _borrowRecord[i];
                    break;
                }
            }
            if (targetRecord == null)
            {
                Console.WriteLine("Error: No active borrow record found for this book.");
                return;
            }

            for (int i = 0; i < targetRecord.Member.BorrowedBooks.Length; i++)
            {
                if (targetRecord.Member.BorrowedBooks[i] != null && targetRecord.Member.BorrowedBooks[i].Id == bookId)
                {
                    targetRecord.Member.BorrowedBooks[i] = null;
                    break;
                }
            }

            targetRecord.ReturnDate = DateTime.Now;
            targetRecord.Book.IsAvailable = true;

            Console.WriteLine($"Success: Book '{targetRecord.Book.Title}' returned successfully.");



        }

        public void SearchCatalog(string query) 
        {
            bool foundAny = false;

            Console.WriteLine("Books:");
            for (int i =0; i < _bookCount; i++)
            {
                if (_books[i].MatchesQuery(query))
                {
                    Console.WriteLine(_books[i].GetInfo());
                    foundAny = true;

                }

            }

            Console.WriteLine("\nMembers:");
            for (int i = 0; i < _memberCount; i++)
            {
                if (_member[i].MatchesQuery(query))
                {
                    Console.WriteLine(_member[i].GetInfo());
                    foundAny = true;

                }

            }

            if (!foundAny)
            {
                Console.WriteLine("No results found in Books or Members.");
            }
        }
        
        public void ViewAvailableBooks()
        {
            bool foundAny = false;

            Console.WriteLine("\n--- Available Books ---");
            for (int i = 0; i < _bookCount; i++)
            {
                if (_books[i].IsAvailable)
                {
                    Console.WriteLine(_books[i].GetInfo());
                    foundAny = true;

                }
            }
            if (!foundAny)
            {
                Console.WriteLine("No books are currently available in the library.");
            }

        }

        public void MemberBorrowingHistory(int memberId)
        {
            bool foundAny = false;

            for (int i =0; i< _bookCount ; i++)
            {
                if (_borrowRecord[i].Member.Id== memberId)
                {
                    string bookTitle = _borrowRecord[i].Book.Title;
                    string borrowDate = _borrowRecord[i].BorrowDate.ToShortDateString();
                    string returnStatus = _borrowRecord[i].ReturnDate == null? "Not Returned": $"Returned on {_borrowRecord[i].ReturnDate.Value.ToShortDateString()}";
                    Console.WriteLine($"Book: '{bookTitle}' | Borrowed: {borrowDate} | Status: {returnStatus}");
                    foundAny = true;
                }

            }
            if (!foundAny)
            {
                Console.WriteLine("This member has no borrowing history.");
            }
        }

        public void LateReturnReport()
        {
            bool foundAny = false;

            for (int i = 0; i <_recordCount; i++)
            {
                if (_borrowRecord[i].IsLate())
                {
                    int DaysOverdue = ((int)(DateTime.Now - _borrowRecord[i].BorrowDate).TotalDays) - _borrowRecord[i].Member.LoanDays;
                    Console.WriteLine($"Member Name: {_borrowRecord[i].Member.Name} | Book Title: {_borrowRecord[i].Book.Title} | Borrow Date: {_borrowRecord[i].BorrowDate.ToShortDateString()} | Days Overdue: {DaysOverdue}");
                    foundAny = true;
                }
            }

            if (!foundAny)
            {
                Console.WriteLine("There are no late returns.");
            }


        }


    }
}
