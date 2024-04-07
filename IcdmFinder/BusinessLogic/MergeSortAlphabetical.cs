using IcdmFinder.Icdm10Codes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcdmFinder.BusinessLogic
{
    public class MergeSortAlphabetical
    {
        public static List<IcdmCode> MergeSort(List<IcdmCode> icdmCodes)
        {
            return MergeSortHelper(icdmCodes);
        }

        private static List<IcdmCode> MergeSortHelper(List<IcdmCode> icdmCodes)
        {
            if (icdmCodes.Count <= 1)
                return icdmCodes;

            int mid = icdmCodes.Count / 2;
            List<IcdmCode> left = icdmCodes.GetRange(0, mid);
            List<IcdmCode> right = icdmCodes.GetRange(mid, icdmCodes.Count - mid);

            left = MergeSortHelper(left);
            right = MergeSortHelper(right);

            return Merge(left, right);
        }

        private static List<IcdmCode> Merge(List<IcdmCode> left, List<IcdmCode> right)
        {
            List<IcdmCode> merged = new List<IcdmCode>();
            int leftIndex = 0;
            int rightIndex = 0;

            while (leftIndex < left.Count && rightIndex < right.Count)
            {
                if (((IComparable<IcdmCode>)left[leftIndex]).CompareTo(right[rightIndex]) <= 0)
                {
                    merged.Add(left[leftIndex]);
                    leftIndex++;
                }
                else
                {
                    merged.Add(right[rightIndex]);
                    rightIndex++;
                }
            }

            while (leftIndex < left.Count)
            {
                merged.Add(left[leftIndex]);
                leftIndex++;
            }

            while (rightIndex < right.Count)
            {
                merged.Add(right[rightIndex]);
                rightIndex++;
            }

            return merged;
        }
    }
}