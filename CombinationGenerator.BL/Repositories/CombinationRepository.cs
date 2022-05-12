using CombinationGenerator.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationGenerator.BL.Repositories
{
    public class CombinationRepository : ICombinationRepository
    {
        private int N=0;
        private List<int> _lastValues=null;

        public CombinationRepository()
        {
        }

        public  int GetPossibleCombinationsNumber(int n)
        {
            IEnumerable<int> enumerable = Enumerable.Range(1, n);
            List<int> asList = enumerable.ToList();
            var x = GetPermutations(asList, n);
            return  GetPermutations(asList, n).Count();
        }

        static  IEnumerable<IEnumerable<T>>
        GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(o => !t.Contains(o)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

     
        public async Task<dynamic> GetCombination(int n, int pageNumber, int pageSize)
        {
            IEnumerable<int> enumerable = Enumerable.Range(1, n);
            List<int> asList = enumerable.ToList();
            //(pageNumber = 0) ? pageNumber = 1 : pageNumber = pageNumber; 
            return GetPermutations(asList, n).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }


        public  int[] NextPermutation(int[] array)
        {
            // Find non-increasing suffix
            int i = array.Length - 1;
            while (i > 0 && array[i - 1] >= array[i])
                i--;
            if (i <= 0)
                return null;

            // Find successor to pivot
            int j = array.Length - 1;
            while (array[j] <= array[i - 1])
                j--;
            int temp = array[i - 1];
            array[i - 1] = array[j];
            array[j] = temp;

            // Reverse suffix
            j = array.Length - 1;
            while (i < j)
            {
                temp = array[i];
                array[i] = array[j];
                array[j] = temp;
                i++;
                j--;
            }
            return array;
        }


     

    }
}