using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Models
{
    public abstract class LibraryItem
    {
        public int Id { set; get; }
        public String? Title { set; get; }
        public DateTime AddedDate { set; get; }


        public abstract string GetInfo();
    }
    
}
