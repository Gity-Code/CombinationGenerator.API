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
        public CombinationRepository()
        {
        }

        public async Task<int> GetPossibleCombinationsNumber(int n)
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




        static IEnumerable<IEnumerable<T>>
    GetAllPermutations<T>(IEnumerable<T> list, int length, int pageNumber, int pageSize)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetAllPermutations(list, length - 1, pageNumber, pageSize)
                .SelectMany
                (t => list.Where(o => !t.Contains(o)),
                    (t1, t2) => t1.Concat(new T[] { t2 })
                    .OrderBy(t => t)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize));

            //return GetAllPermutations(list, length - 1 , pageNumber, pageSize)
            //    .SelectMany
            //    (t => list.Where(o => !t.Contains(o)).Skip(pageNumber).Take(pageSize),
            //        (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        static IEnumerable<IEnumerable<T>> FunctionalPermutations<T>(
            IEnumerable<T> elements, int length , int pageNumber, int pageSize)
        {
            if (length < 2) return elements.Select(t => new T[] { t });
            /* Pengyang answser..
              return _recur_(list, length - 1).SelectMany(t => list.Where(e => !t.Contains(e)),(t1, t2) => t1.Concat(new T[] { t2 }));
            */
            return elements.SelectMany((element_i, i) =>
              FunctionalPermutations(elements.Take(pageSize).Concat(elements.Skip((pageNumber - 1) * pageSize)), length - 1, pageNumber, pageSize)
                .Select(sub_ei => new[] { element_i }.Concat(sub_ei)));
        }

     
        public async Task<dynamic> GetCombination(int n, int pageNumber, int pageSize)
        {
            //db.Blogs.First().Posts.Skip(10).Take(5).ToList();
            IEnumerable<int> enumerable = Enumerable.Range(1, n);
            List<int> asList = enumerable.ToList();
            //pageNumber = pageSize * pageNumber; 
            var ggx = GetAllPermutations(asList, n, pageNumber, pageSize);
            //var x = FunctionalPermutations(asList, n, pageNumber, pageSize);
            return GetAllPermutations(asList , n, pageNumber, pageSize).ToList();
        }


        public static bool NextPermutation<T>(T[] elements) where T : IComparable<T>
        {
            // More efficient to have a variable instead of accessing a property
            var count = elements.Length;

            // Indicates whether this is the last lexicographic permutation
            var done = true;

            // Go through the array from last to first
            for (var i = count - 1; i > 0; i--)
            {
                var curr = elements[i];

                // Check if the current element is less than the one before it
                if (curr.CompareTo(elements[i - 1]) < 0)
                {
                    continue;
                }

                // An element bigger than the one before it has been found,
                // so this isn't the last lexicographic permutation.
                done = false;

                // Save the previous (bigger) element in a variable for more efficiency.
                var prev = elements[i - 1];

                // Have a variable to hold the index of the element to swap
                // with the previous element (the to-swap element would be
                // the smallest element that comes after the previous element
                // and is bigger than the previous element), initializing it
                // as the current index of the current item (curr).
                var currIndex = i;

                // Go through the array from the element after the current one to last
                for (var j = i + 1; j < count; j++)
                {
                    // Save into variable for more efficiency
                    var tmp = elements[j];

                    // Check if tmp suits the "next swap" conditions:
                    // Smallest, but bigger than the "prev" element
                    if (tmp.CompareTo(curr) < 0 && tmp.CompareTo(prev) > 0)
                    {
                        curr = tmp;
                        currIndex = j;
                    }
                }

                // Swap the "prev" with the new "curr" (the swap-with element)
                elements[currIndex] = prev;
                elements[i - 1] = curr;

                // Reverse the order of the tail, in order to reset it's lexicographic order
                for (var j = count - 1; j > i; j--, i++)
                {
                    var tmp = elements[j];
                    elements[j] = elements[i];
                    elements[i] = tmp;
                }
                break;
            }

            return done;
        }
        public static int[] NextPermutation(int[] array)
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