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

namespace IcdmFinder.Gui
{
    public class IcdmView : Window
    {
        private readonly IcdmViewModel _viewModel;
        readonly CompositeDisposable _disposable = new CompositeDisposable();

        public IcdmView(IcdmViewModel viewModel) : base("ICDM10 CODE SEARCHER")
        {
            _viewModel = viewModel;
            this.ColorScheme = OrangeNightsScheme;

            FrameView filterView = FilterView();
            this.Add(filterView);

            Label icdmCodeNameLabel = IcdmCodeNameLabel(filterView.X, filterView.Y);
            filterView.Add(icdmCodeNameLabel);
            TextField icdmCodeNameInput = IcdmCodeNameInput(icdmCodeNameLabel.X, icdmCodeNameLabel.Y);
            filterView.Add(icdmCodeNameInput);
            Label descriptionLabel = DescriptionLabel(icdmCodeNameInput.X, icdmCodeNameInput.Y);
            filterView.Add(descriptionLabel);
            TextView descriptionInput = DescriptionInput(descriptionLabel.X, descriptionLabel.Y);
            filterView.Add(descriptionInput);
            Label icdmCategoryLabel = IcdmCategoryLabel(descriptionInput.X, descriptionInput.Y);
            filterView.Add(icdmCategoryLabel);
            ComboBox icdmCategoryDropdown = IcdmCategoryDropdown(icdmCategoryLabel.X, icdmCategoryLabel.Y);
            filterView.Add(icdmCategoryDropdown);
        }

        private FrameView FilterView()
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

        private Label IcdmCodeNameLabel(Pos lastViewX, Pos lastViewY)
        {
            Label icdmCodeNameLabel = new Label("icdm code name")
            {
                X = lastViewX,
                Y = lastViewY + 1,
                Width = 40,
            };

            return icdmCodeNameLabel;
        }

        private TextField IcdmCodeNameInput(Pos lastViewX, Pos lastViewY)
        {
            TextField icdmCodeNameInput = new TextField()
            {
                X = lastViewX,
                Y = lastViewY + 1,
                Width = 40,
            };

            icdmCodeNameInput.TextChanged += (ustring e) =>
                    _viewModel.IcdmName = icdmCodeNameInput.Text;

            return icdmCodeNameInput;
        }


        private Label DescriptionLabel(Pos lastViewX, Pos lastViewY)
        {
            Label descriptionLabel = new Label("description/tags")
            {
                X = lastViewX,
                Y = lastViewY + 2,
                Width = 40,
            };

            return descriptionLabel;
        }

        private TextView DescriptionInput(Pos lastViewX, Pos lastViewY)
        {
            TextView descriptionInput = new TextView()
            {
                X = lastViewX,
                Y = lastViewY + 1,
                Width = Dim.Percent(100),
                Height = Dim.Percent(30),
                WordWrap = true,
            };

            return descriptionInput;
        }

        private Label IcdmCategoryLabel(Pos lastViewX, Pos lastViewY)
        {
            Label IcdmCategorylabel = new Label("Icdm Category")
            {
                X = lastViewX,
                Y = lastViewY + 8,
                Width = 40,
            };

            return IcdmCategorylabel;
        }

        private ComboBox IcdmCategoryDropdown(Pos lastViewX, Pos lastViewY)
        {
            ComboBox icdmCategoryDropdown = new ComboBox(new List<string>
            {
                "the", "bastard", "butt",
            })
            {
                X = lastViewX,
                Y = lastViewY + 1,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                ReadOnly = true,
                HideDropdownListOnClick = true,
                // needs event listener to list for when focused and what key to start dropdown
            };

            return icdmCategoryDropdown;
        }


        ColorScheme OrangeNightsScheme => new ColorScheme()
        {
            Normal = Terminal.Gui.Attribute.Make(Color.White, Color.Black),
            HotNormal = Terminal.Gui.Attribute.Make(Color.White, Color.Black),
            Focus = Terminal.Gui.Attribute.Make(Color.Black, Color.White),
            HotFocus = Terminal.Gui.Attribute.Make(Color.Black, Color.White),
        };
    }
}

