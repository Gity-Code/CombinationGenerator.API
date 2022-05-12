using CombinationGenerator.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationGenerator.BL.Repositories
{
    public class CommonService : ICommonService
    {
        private int _NumberCombinations = 0;
        private int[] _values = new int[0];

        public void SetNumberCombinations(int number)
        {
            this._NumberCombinations = number;
        }

        public int GetNumberCombinations()
        {
            return this._NumberCombinations;
        }

        public void SetLastPermutationValues(int[] valuesArray)
        {
            this._values = new int[this._NumberCombinations];
            this._values = valuesArray;

        }

        public int[]? GetLastPermutationValues()
        {
            return this._values;
        }
    }
}
