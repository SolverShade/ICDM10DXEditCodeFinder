using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace IcdmFinder.Gui
{
    public static class WindowFactory
    {
        public static Window CreateIcdmSearchWindow()
        {

            Window icdmSearchWindow = new Window("IDCM10 SEARCHER")
            {
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                ColorScheme = CreateColorScheme(),
            };

            Window searchFilterView = CreateSearchFilterView();
            Window codeMatchView = CreateCodeMatchView();
            Window codeInfoView = CreateCodeInfoView();

            codeMatchView.Add(new Button("the") { X = 0, Y = 0});
            codeMatchView.Add(new Button("bee") { X = 0, Y= 1});

            icdmSearchWindow.Add(searchFilterView);
            icdmSearchWindow.Add(codeMatchView);
            icdmSearchWindow.Add(codeInfoView);

            return icdmSearchWindow;
        }

        private static ColorScheme CreateColorScheme()
        {
            return new ColorScheme
            {
                Normal = Terminal.Gui.Attribute.Make(Color.Brown, Color.Black),
                HotNormal = Terminal.Gui.Attribute.Make(Color.Brown, Color.Black),
                Focus = Terminal.Gui.Attribute.Make(Color.Brown, Color.Black),
                HotFocus = Terminal.Gui.Attribute.Make(Color.Brown, Color.Black),
            };
        }

        private static Window CreateSearchFilterView()
        {
            return new Window("Search/Filter")
            {
                X = 0,
                Y = 0,
                Width = Dim.Percent(25),
                Height = Dim.Fill(),
                ColorScheme = CreateColorScheme(),
            };
        }

        private static Window CreateCodeMatchView()
        {
            ListView matchedCodesListBox = new ListView(
                new List<string>()
                {
                    "the",
                    "butt",
                    "do"
                });

            Window codeMatchView = new Window("Icdm Codes")
            {
                X = Pos.Percent(25),
                Y = 0,
                Width = Dim.Percent(40),
                Height = Dim.Fill(),
                ColorScheme = CreateColorScheme(),
            };

            List<Button> buttons = new List<Button>() { new Button("the"), new Button("Hello") };
            ListView listView = new ListView(new List<string> { "the" });
            listView.Add(new View(new Rect()));

            //codeMatchView.Add(matchedCodesListBox);
            codeMatchView.Add(listView);

            return codeMatchView;
        }

        private static Window CreateCodeInfoView()
        {
            return new Window("Code Information")
            {
                X = Pos.Percent(65),
                Y = 0,
                Width = Dim.Percent(35),
                Height = Dim.Fill(),
                ColorScheme = CreateColorScheme(),
            };
        }
    }
}
