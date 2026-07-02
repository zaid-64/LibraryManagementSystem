using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Models
{
    public class BorrowRecord
    {
        public int Id { set; get; }
        public Book? Book { set; get; }
        public Member? Member { set; get; }
        public DateTime BorrowDate { set; get; }
        public DateTime? ReturnDate { get; set; }

        public bool IsLate()
        {
            if (ReturnDate != null)
            {
                return false;
            }
            return (DateTime.Now - BorrowDate).TotalDays > Member.LoanDays;
        } 
    }
}