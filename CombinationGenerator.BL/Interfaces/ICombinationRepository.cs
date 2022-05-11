using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationGenerator.BL.Interfaces
{
    public interface ICombinationRepository
    {
        public  Task<int> GetPossibleCombinationsNumber(int n);
        Task<dynamic> GetCombination(int n, int pageNumber, int pageSize);
    }
}
