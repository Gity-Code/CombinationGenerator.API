namespace CombinationGenerator.BL.Interfaces
{
    public interface ICommonService
    {
        int GetNumberCombinations();
        void SetNumberCombinations(int number);
        void SetLastPermutationValues(int[] array);
        int[]? GetLastPermutationValues();
    }
}