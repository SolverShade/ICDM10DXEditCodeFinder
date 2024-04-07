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
        }


        public ustring IcdmNameFilter { private get; set; } = ustring.Empty;

        public List<ustring> DescriptionFilter { private get; set; } = new List<ustring>();

        public ustring IcdmCatagoryFilter { private get; set; } = ustring.Empty;

        public List<IcdmCode> RelevantIcdmCodes { get; private set; } = new List<IcdmCode>();

        public IcdmCode CurrentIcdmToDisplay { private get; set; } 

        public void RefreshRelevantIcdmCodes()
        {
            RelevantIcdmCodes.Clear(); 

            foreach(IcdmCode icdmCode in _allIcdmCodes)
            {
                if (icdmCode.CodeName.StartsWith(IcdmNameFilter.ToString()) == false 
                    && IcdmNameFilter.IsEmpty == false)
                    continue;

                RelevantIcdmCodes.Add(icdmCode);
            }

            Debug.WriteLine("Bababooy");
        }
        
    }
}