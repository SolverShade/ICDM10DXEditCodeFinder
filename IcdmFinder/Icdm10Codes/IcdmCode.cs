using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcdmFinder.Icdm10Codes
{
    public class IcdmCode
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
    }
}
