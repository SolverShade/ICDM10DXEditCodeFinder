using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Geom;

namespace IcdmFinder.PdfScraper
{
    public class PdfScrap
    {
        private static string workingDirectory = Environment.CurrentDirectory;
        private static string _projectDirectory => Directory.GetParent(workingDirectory).Parent.Parent.FullName;

        private static string _icdm10PdfPath = _projectDirectory + "/icdm10.pdf";
        private static string _savePath = _projectDirectory + "/Icdm10Codes/icdm10Codes.txt";

        public void ExtractCodesToTextFile()
        {
            using (PdfReader reader = new PdfReader(_icdm10PdfPath))
            {
                using (PdfDocument pdfDoc = new PdfDocument(reader))
                {
                    using (StreamWriter writer = new StreamWriter(_savePath))
                    {

                        for (int pageNum = 5; pageNum <= pdfDoc.GetNumberOfPages(); pageNum++)
                        {
                            var strategy = new SimpleTextExtractionStrategy();
                            string text = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(pageNum), strategy);

                            string[] lines = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                            string[] icdmCatagories = {"Adult diagnoses", "Newborn diagnoses", "Pediatric diagnoses",
                                                   "Maternity diagnoses", "Diagnoses for females only",
                                                   "Diagnoses for males only", "Manifestation diagnoses",
                                                    "Mental health diagnoses"};

                            string icdmCodePattern = @"\b[a-zA-Z]\d{3,7}\b";


                            foreach (string line in lines)
                            {
                                if (icdmCatagories.Any(catagory => line.Contains(catagory)))
                                {
                                    writer.WriteLine(line);
                                }

                                Match match = Regex.Match(line, icdmCodePattern);

                                if (match.Success == true)
                                {
                                    string[] lineTokens = line.Split(new[] { match.Value }, 2, StringSplitOptions.None);
                                    string icdmCode = match.Value;
                                    string description = lineTokens[1];

                                    writer.WriteLine(icdmCode + " " + description);
                                }
                            }
                        }

                        //Console.WriteLine("Match found: " + match.Value);
                    }
                }
            }
        }
    }
}
