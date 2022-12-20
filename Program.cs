using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;

namespace Parse
{
    public class Program
    {
        static void Main(string[] args)
        {
            GetHtmlAsync();
            Console.ReadLine();
        }

        private static async void GetHtmlAsync()
        {
            var url = "https://livrareflori.md/";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var ProductHtml = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("id", "")
                .Equals("hp_product_home_prod_list")).ToList();

            var ProductListItems = ProductHtml[0].Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Contains("js_gtag_event_view_item_list_")).ToList();

            foreach (var ProductListItem in ProductListItems)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(">>> ID: " + ProductListItem.GetAttributeValue("gtag-id", "") + " | " + "Name:" + ProductListItem.GetAttributeValue("gtag-name", ""));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(">>> Price: " + ProductListItem.GetAttributeValue("gtag-price", ""));
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(">>> Total: " + ProductListItems.Count + " items");


        }
    }
}
