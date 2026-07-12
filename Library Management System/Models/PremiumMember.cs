using System;

namespace Library_Management_System.Models
{
    public class PremiumMember : Member
    {
        public override int LoanDays { get; } = 30;
        public override int MaxBorrowLimit { get; } = 10;

        public PremiumMember() : base(10)
        {
        }

        public override string GetInfo()
        {
            return $"[Premium Member] ID: {Id} | Name: {Name} | Email: {Email} | Joined: {JoinDate.ToShortDateString()} | Limit: {MaxBorrowLimit} books";
        }
    }
}