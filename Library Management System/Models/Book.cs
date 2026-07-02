using Library_Management_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Models
{
    public class Book : LibraryItem, ISearchable
    {
        public string? Author { set; get; }
        public int Year { set; get; }
        public string? Genre { set; get; }
        public bool IsAvailable { set; get; } = true;

        public override string GetInfo()
        {
            string status = IsAvailable ? "Available" : "Borrowed";

            return $"[Book] ID: {Id} | Title: {Title} | Author: {Author} | Year: {Year} | Genre: {Genre} | Status: {status}";

        }

        public bool MatchesQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            { 
                return false; 
            }
                
            string lowerQuery = query.ToLower();

            bool titleMatches = Title != null && Title.ToLower().Contains(lowerQuery);
            bool authorMatches = Author != null && Author.ToLower().Contains(lowerQuery);

            return titleMatches || authorMatches;

        }




    }
}
