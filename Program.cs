using System;
using System.Threading.Tasks;
using PuppeteerSharp;
using HtmlAgilityPack;

namespace Example.GetAllLinks
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var options = new LaunchOptions { Headless = true };
            Console.WriteLine("Downloading Chromium");
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            Console.WriteLine("Goes to CGV Now PLaying");

            using (var browser = await Puppeteer.LaunchAsync(options))
            using (var page = await browser.NewPageAsync())
            {
                await page.GoToAsync("https://www.cgv.id/en/movies/now_playing");
                var jsSelectAllAnchors = await page.QuerySelectorAllHandleAsync(".movie-list-body > ul >li > a").EvaluateFunctionAsync<string[]>("elements => elements.map(a => a.href)");
                Console.WriteLine("Movie List");
                    HtmlWeb web = new HtmlWeb();
                    var htmlDoc = web.Load(jsSelectAllAnchors[0]);
                    var nodes1 = htmlDoc.DocumentNode.SelectNodes("//div[@class='movie-add-info left']");
                    foreach(var x in nodes1)
                    {
                    Console.WriteLine(x.InnerHtml.Trim());
                    }
                    var nodes2 = htmlDoc.DocumentNode.SelectNodes("//div[@class='movie-synopsis right']");
                    foreach(var x in nodes2)
                    {
                    Console.WriteLine(x.InnerHtml.Trim());
                    }
                    // Console.WriteLine($"Url: {jsSelectAll}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
        }
    }
}