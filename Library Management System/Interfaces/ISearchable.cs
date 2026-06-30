using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Interfaces
{
    public interface ISearchable
    {
        bool MatchesSearchQuery(string query);
    }
}
