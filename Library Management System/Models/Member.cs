using Library_Management_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Models
{
    public class Member : ISearchable
    {
        public int Id { set; get; }
        public string? Name { set; get; }
        public string? Email { set; get; }
        public virtual int LoanDays { get; } = 14;
        public virtual int MaxBorrowLimit { get; } = 3;
        public DateTime JoinDate { set; get; }
        public Book[] BorrowedBooks { set; get; }

        public Member()
        {
            BorrowedBooks = new Book[MaxBorrowLimit];
        }

        public virtual string GetInfo()
        {
            return $"[Regular Member] ID: {Id} | Name: {Name} | Email: {Email} | Joined: {JoinDate.ToShortDateString()} | Limit: {MaxBorrowLimit} books";
        }

        public bool MatchesQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {  
                return false;
            }
      
            string lowerQuery = query.ToLower();

            bool nameMatches = Name != null && Name.ToLower().Contains(lowerQuery);
            bool emailMatches = Email != null && Email.ToLower().Contains(lowerQuery);

            return nameMatches || emailMatches;

        }


    }
}
