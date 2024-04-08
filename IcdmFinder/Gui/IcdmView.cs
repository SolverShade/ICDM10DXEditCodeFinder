using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using NStack;
using IcdmFinder.Icdm10Codes;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IcdmFinder.Gui
{
    public class IcdmView : Window
    {
        private readonly IcdmViewModel _viewModel;

        public IcdmView(IcdmViewModel viewModel) : base("ICDM10 CODE SEARCHER")
        {
            _viewModel = viewModel;
            this.ColorScheme = BlackAndWhite;

            FrameView filterView = FilterView();
            this.Add(filterView);

            Label icdmNameInputLabel = IcdmNameInputLabel(
                filterView.X,
                filterView.Y);
            filterView.Add(icdmNameInputLabel);

            TextField icdmNameInput = IcdmNameInput(
                icdmNameInputLabel.X,
                icdmNameInputLabel.Y);
            filterView.Add(icdmNameInput);

            Label descriptionInputLabel = DescriptionInputLabel(
                icdmNameInput.X,
                icdmNameInput.Y);
            filterView.Add(descriptionInputLabel);

            TextView descriptionInput = DescriptionInput(
                descriptionInputLabel.X,
                descriptionInputLabel.Y);
            filterView.Add(descriptionInput);

            Label icdmCategoryLabel = IcdmCategoryLabel(
                descriptionInput.X,
                descriptionInput.Y);
            filterView.Add(icdmCategoryLabel);

            ComboBox icdmCategoryDropdown = IcdmCategoryDropdown(
                icdmCategoryLabel.X,
                icdmCategoryLabel.Y);

            filterView.Add(icdmCategoryDropdown);

            FrameView icdmCodesView = IcdmCodesView();
            this.Add(icdmCodesView);

            Label relevantIcdmCodesLabel = RelevantIcdmCodesLabel(
                icdmCodesView.X,
                icdmCodesView.Y);
            icdmCodesView.Add(relevantIcdmCodesLabel);

            ListView relevantIcdmCodesView = RelevantIcdmCodesView(
                relevantIcdmCodesLabel.X,
                relevantIcdmCodesLabel.Y);
            icdmCodesView.Add(relevantIcdmCodesView);

            FrameView icdmCodeInformationView = IcdmCodeInformationView();
            this.Add(icdmCodeInformationView);

            Label selectedIcdmCodeNameLabel = SelectedIcdmNameLabel(
                icdmCodeInformationView.X,
                icdmCodeInformationView.Y);
            icdmCodeInformationView.Add(selectedIcdmCodeNameLabel);

            TextView selectedIcdmCodeNameTextBox = SelectedIcdmNameTextbox(
                selectedIcdmCodeNameLabel.X,
                selectedIcdmCodeNameLabel.Y);
            icdmCodeInformationView.Add(selectedIcdmCodeNameTextBox);

            Label selectedIcdmCodeDescriptionLabel = SelectedIcdmCodeDescriptionLabel(
                selectedIcdmCodeNameTextBox.X,
                selectedIcdmCodeNameTextBox.Y);
            icdmCodeInformationView.Add(selectedIcdmCodeDescriptionLabel);

            TextView selectedIcdmCodeDescription = SelectedIcdmCodeDescription(
                selectedIcdmCodeDescriptionLabel.X,
                selectedIcdmCodeDescriptionLabel.Y);
            icdmCodeInformationView.Add(selectedIcdmCodeDescription);

            Label selectedIcdmCodeCategoryLabel = SelectedIcdmCodeCategoryLabel(
                selectedIcdmCodeDescription.X,
                selectedIcdmCodeDescription.Y);
            icdmCodeInformationView.Add(selectedIcdmCodeCategoryLabel);

            TextView selectedIcdmCodeCategory = SelectedIcdmCodeCategory(
                selectedIcdmCodeCategoryLabel.X,
                selectedIcdmCodeCategoryLabel.Y);
            icdmCodeInformationView.Add(selectedIcdmCodeCategory);

            this.Enabled = true;
        }

        FrameView FilterView()
        {
            FrameView filterView = new FrameView("Search/Filter")
            {
                X = 0,
                Y = 0,
                Width = Dim.Percent(25),
                Height = Dim.Fill(),
                LayoutStyle = LayoutStyle.Computed
            };

            return filterView;
        }

        FrameView IcdmCodesView()
        {
            FrameView icdmCodesView = new FrameView("Icdm Codes")
            {
                X = Pos.Percent(25),
                Y = 0,
                Width = Dim.Percent(35),
                Height = Dim.Fill(),
                LayoutStyle = LayoutStyle.Computed
            };

            return icdmCodesView;
        }

        FrameView IcdmCodeInformationView()
        {
            FrameView icdmCodeInformationView = new FrameView("Code Information")
            {
                X = Pos.Percent(60),
                Y = 0,
                Width = Dim.Percent(40),
                Height = Dim.Fill(),
                LayoutStyle = LayoutStyle.Computed
            };

            return icdmCodeInformationView;
        }

        Label IcdmNameInputLabel(Pos lastViewX, Pos lastViewY)
        {
            Label icdmCodeNameLabel = new Label("icdm code name")
            {
                X = lastViewX,
                Y = lastViewY + 1,
                Width = 40,
            };

            return icdmCodeNameLabel;
        }

        TextField IcdmNameInput(Pos lastViewX, Pos lastViewY)
        {
            TextField icdmCodeNameInput = new TextField()
            {
                X = lastViewX,
                Y = lastViewY + 1,
                Width = 40,
            };

            icdmCodeNameInput.TextChanged += (ustring e) =>
                    _viewModel.IcdmNameFilter = (string)icdmCodeNameInput.Text;
            icdmCodeNameInput.TextChanged += (ustring e) =>
                    _viewModel.RefreshRelevantIcdmCodes();

            return icdmCodeNameInput;
        }


        Label DescriptionInputLabel(Pos lastViewX, Pos lastViewY)
        {
            Label descriptionLabel = new Label("description/tags")
            {
                X = lastViewX,
                Y = lastViewY + 2,
                Width = 40,
            };

            return descriptionLabel;
        }

        TextView DescriptionInput(Pos lastViewX, Pos lastViewY)
        {
            TextView descriptionInput = new TextView()
            {
                X = lastViewX,
                Y = lastViewY + 1,
                Width = Dim.Percent(100),
                Height = Dim.Percent(30),
                WordWrap = true,
            };

            descriptionInput.ContentsChanged += (e) =>
                _viewModel.IcdmCodeDescriptionFilter = (string)descriptionInput.Text;
            descriptionInput.ContentsChanged += (e) => _viewModel.RefreshRelevantIcdmCodes();

            return descriptionInput;
        }

        Label IcdmCategoryLabel(Pos lastViewX, Pos lastViewY)
        {
            Label IcdmCategorylabel = new Label("Icdm Category")
            {
                X = lastViewX,
                Y = lastViewY + 20,
                Width = 40,
            };

            return IcdmCategorylabel;
        }

        ComboBox IcdmCategoryDropdown(Pos lastViewX, Pos lastViewY)
        {
            ComboBox icdmCategoryDropdown = new ComboBox(new List<string>
            {
             "Any",
             "Adult diagnoses",
             "Newborn diagnoses",
             "Pediatric diagnoses",
             "Maternity diagnoses",
             "Diagnoses for females only",
             "Diagnoses for males only",
             "Manifestation diagnoses",
             "Mental health diagnoses"
            })
            {
                X = lastViewX,
                Y = lastViewY + 1,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                ReadOnly = true,
                HideDropdownListOnClick = true,
            };

            icdmCategoryDropdown.SelectedItemChanged += (e) =>
                _viewModel.IcdmCategoryFilter = icdmCategoryDropdown.Text.ToString();
            icdmCategoryDropdown.SelectedItemChanged += (e) => 
            _viewModel.RefreshRelevantIcdmCodes();

            return icdmCategoryDropdown;
        }

        Label RelevantIcdmCodesLabel(Pos lastViewX, Pos lastViewY)
        {
            Label relevantCodesLabel = new Label("Relevant Codes")
            {
                X = 0,
                Y = lastViewY,
                Width = 40,
            };

            return relevantCodesLabel;
        }

        ListView RelevantIcdmCodesView(Pos lastViewX, Pos lastViewY)
        {
            ListView relaventCodesView = new ListView(_viewModel.RelevantIcdmCodes)
            {
                X = lastViewX,
                Y = lastViewY + 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };

            relaventCodesView.SelectedItemChanged += (e) =>
            {
                IcdmCode? selectedItem = relaventCodesView.Source?.
                    ToList()?[relaventCodesView.SelectedItem] as IcdmCode;

                if (selectedItem != null)
                    _viewModel.ChangeSelectedIcdmCode(selectedItem);
                else
                    throw new Exception("Relevant Icdm Codes source or Selected Icdm Code" +
                        "does not exist");
            };

            return relaventCodesView;
        }

        Label SelectedIcdmNameLabel(Pos lastViewX, Pos lastViewY)
        {
            Label relevantCodesLabel = new Label("Selected Icdm")
            {
                X = 0,
                Y = lastViewY,
                Width = 40,
            };

            return relevantCodesLabel;
        }

        TextView SelectedIcdmNameTextbox(Pos lastViewX, Pos lastViewY)
        {
            TextView selectedIcdmNameTextbox = new TextView()
            {
                X = lastViewX,
                Y = lastViewY + 1,
                Width = Dim.Percent(80),
                Height = Dim.Percent(5),
                Text = _viewModel.SelectedIcdmCode.CodeName,
                ReadOnly = true,
                WordWrap = true,
            };

            _viewModel.SelectedIcdmCodeChanged += () =>
                selectedIcdmNameTextbox.Text = _viewModel.SelectedIcdmCode.CodeName;

            return selectedIcdmNameTextbox;
        }

        Label SelectedIcdmCodeDescriptionLabel(Pos lastViewX, Pos lastViewY)
        {
            Label selectedDescriptionLabel = new Label("Description")
            {
                X = lastViewX,
                Y = lastViewY + Pos.Percent(5),
                Width = 40,
            };

            return selectedDescriptionLabel;
        }

        TextView SelectedIcdmCodeDescription(Pos lastViewX, Pos lastViewY)
        {
            TextView selectedDescription = new TextView()
            {
                X = lastViewX,
                Y = lastViewY + 1,
                Width = Dim.Percent(80),
                Height = Dim.Percent(30),
                Text = _viewModel.SelectedIcdmCode.Description,
                WordWrap = true,
            };

            _viewModel.SelectedIcdmCodeChanged += () =>
                selectedDescription.Text = _viewModel.SelectedIcdmCode.Description;

            return selectedDescription;
        }


        Label SelectedIcdmCodeCategoryLabel(Pos lastViewX, Pos lastViewY)
        {
            Label IcdmCategorylabel = new Label("Icdm Category")
            {
                X = lastViewX,
                Y = lastViewY + Pos.Percent(30),
                Width = 40,
            };

            return IcdmCategorylabel;
        }

        TextView SelectedIcdmCodeCategory(Pos lastViewX, Pos lastViewY)
        {
            TextView selectedDescription = new TextView()
            {
                X = lastViewX,
                Y = lastViewY + 1,
                Width = Dim.Percent(80),
                Height = Dim.Percent(5),
                Text = _viewModel.SelectedIcdmCode.Description,
                WordWrap = true,
            };

            _viewModel.SelectedIcdmCodeChanged += () =>
                selectedDescription.Text = _viewModel.SelectedIcdmCode.Catagory;

            return selectedDescription;
        }



        ColorScheme BlackAndWhite => new ColorScheme()
        {
            Normal = Terminal.Gui.Attribute.Make(Color.White, Color.Black),
            HotNormal = Terminal.Gui.Attribute.Make(Color.White, Color.Black),
            Focus = Terminal.Gui.Attribute.Make(Color.Black, Color.White),
            HotFocus = Terminal.Gui.Attribute.Make(Color.Black, Color.White),
        };

    }
}

