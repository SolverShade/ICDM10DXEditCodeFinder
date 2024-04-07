using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcdmFinder.Icdm10Codes
{
    public class IcdmCode: IComparable<IcdmCode> 
    {
        public readonly string CodeName;
        public readonly string Description;
        public readonly string Catagory;

        public IcdmCode(string codeName, string description, string catagory)
        {
            CodeName = codeName;
            Description = description;
            Catagory = catagory;
        }

        public override string ToString()
        {
            return CodeName;
        }

        int IComparable<IcdmCode>.CompareTo(IcdmCode other)
        {
            // Compare based on CodeName
            return string.Compare(this.CodeName, other.CodeName, StringComparison.Ordinal);
        }
    }
}
