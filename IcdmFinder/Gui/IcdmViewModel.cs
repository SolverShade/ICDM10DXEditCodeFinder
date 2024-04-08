using IcdmFinder.Icdm10Codes;
using IcdmFinder.Scraping;
using iText.Forms.Fields.Merging;
using NStack;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Terminal.Gui;

namespace IcdmFinder.Gui
{
    public class IcdmViewModel : ReactiveObject
    {
        private readonly List<IcdmCode> _allIcdmCodes;

        public IcdmViewModel()
        {
            _allIcdmCodes = IcdmCodeCollector.CollectAllIcdmCodes();
            RelevantIcdmCodes = _allIcdmCodes.ToList();
            SelectedIcdmCode = _allIcdmCodes[0];
        }

        public string IcdmNameFilter { private get; set; } = string.Empty;

        public string IcdmCodeDescriptionFilter { private get; set; } = string.Empty;

        public string IcdmCategoryFilter { private get; set; } = string.Empty;

        public List<IcdmCode> RelevantIcdmCodes { get; private set; } = new List<IcdmCode>();

        public IcdmCode SelectedIcdmCode { get; set; }

        public event Action? SelectedIcdmCodeChanged;

        public void RefreshRelevantIcdmCodes()
        {
            RelevantIcdmCodes.Clear();

            foreach (IcdmCode icdmCode in _allIcdmCodes)
            {
                if (icdmCode.CodeName.StartsWith(IcdmNameFilter.ToString()) == false
                    && IcdmNameFilter != "")
                    continue;

                bool allFilterWordsFound = true;

                List<string> filteredDescriptionWords = IcdmCodeDescriptionFilter.
                    Split().Select(word => word.ToLower()).ToList();
                List<string> descriptionWords = icdmCode.Description.
                    ToString().Split().Select(word => word.ToLower()).ToList();

                foreach (string filterWord in filteredDescriptionWords)
                {
                    if (IcdmCodeDescriptionFilter == "")
                        continue;

                    if (descriptionWords.Contains(filterWord) == false)
                    {
                        allFilterWordsFound = false;
                    }
                }

                if (allFilterWordsFound == false)
                    continue;

                if (icdmCode.Catagory != IcdmCategoryFilter
                    && IcdmCategoryFilter != ""
                    && IcdmCategoryFilter != "Any")
                    continue;

                RelevantIcdmCodes.Add(icdmCode);
            }
        }

        public void ChangeSelectedIcdmCode(IcdmCode newCode)
        {
            SelectedIcdmCode = newCode;
            SelectedIcdmCodeChanged?.Invoke();
        }

    }
}