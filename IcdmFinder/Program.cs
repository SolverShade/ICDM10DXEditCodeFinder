// See https://aka.ms/new-console-template for more information
using IcdmFinder.Gui;
using IcdmFinder.Icdm10Codes;
using IcdmFinder.PdfScraper;
using IcdmFinder.Scraping;
using Terminal.Gui;

//PdfScrap scraper = new PdfScrap();
//scraper.ExtractCodesToTextFile();

Application.Init();

//Window icdmSearchWindow = WindowFactory.CreateIcdmSearchWindow();
//Application.Top.Add(icdmSearchWindow);

Application.Run(new IcdmView(new IcdmViewModel()));
