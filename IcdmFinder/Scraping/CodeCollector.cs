using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IcdmFinder.Icdm10Codes;

namespace IcdmFinder.Scraping
{
    public class CodeCollector
    {
        private static string workingDirectory = Environment.CurrentDirectory;
        private static string _projectDirectory => Directory.GetParent(workingDirectory).Parent.Parent.FullName;

        private static string icdm10CodesPath = _projectDirectory + "/Data/icdm10Codes.txt";

        public List<IcdmCode> CollectAllIcdmCodes()
        {
            List<IcdmCode> icdmCodes = new List<IcdmCode>();

            string icdmCodePattern = @"\b[a-zA-Z]\d{3,7}\b";
            string[] icdmCatagories = {"Adult diagnoses", "Newborn diagnoses", "Pediatric diagnoses",
                                                   "Maternity diagnoses", "Diagnoses for females only",
                                                   "Diagnoses for males only", "Manifestation diagnoses",
                                                    "Mental health diagnoses"};

            using (StreamReader reader = new StreamReader(icdm10CodesPath))
            {
                string catagory = "None";

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (icdmCatagories.Any(catagory => line.Contains(catagory)))
                    {
                        catagory = line.Trim();
                    }

                    Match match = Regex.Match(line, icdmCodePattern);

                    if (match.Success == true)
                    {
                        string[] lineTokens = line.Split(new[] { match.Value }, 2, StringSplitOptions.None);
                        string icdmCode = match.Value.Trim();
                        string description = lineTokens[1].Trim();

                        icdmCodes.Add(new IcdmCode(icdmCode, description, catagory));
                    }
                }
            }

            return icdmCodes;
        }
    }
}
