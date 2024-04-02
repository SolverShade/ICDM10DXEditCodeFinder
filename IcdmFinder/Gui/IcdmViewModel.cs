using iText.Forms.Fields.Merging;
using NStack;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace IcdmFinder.Gui
{
    public class IcdmViewModel : ReactiveObject
    {
        public ustring IcdmName { private get; set; } = ustring.Empty;

        public List<ustring> DescriptionWords { private get; set; } = new List<ustring>();

        public ustring IcdmCatagory { private get; set; } = ustring.Empty;
    }
}