using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchEngine
{
    class Program
    {
        static string ParsingHTML(string htmlDocument)
        {
            htmlDocument = htmlDocument.Replace(";", "");
            int qyery = htmlDocument.IndexOf("&#");
            if (qyery == -1)
                return htmlDocument;
            string Code = htmlDocument.Substring(qyery, 6);
            Code = Code.Replace("&#", "");
            int сharacterCode = int.Parse(Code);
            char symbol = (char)сharacterCode;
            Code = htmlDocument.Substring(qyery, 6);
            htmlDocument = htmlDocument.Replace(Code, symbol.ToString());
            return ParsingHTML(htmlDocument);
        }
        static async Task Main(string[] args)
        {
            Console.Write("Введите запрос: ");
            string query = Console.ReadLine(); 
            string url = $"https://www.google.com/search?q={query}";//  URL Google
            HttpClient client = new HttpClient(); // Создаем HTTP клиент
            string html = await client.GetStringAsync(url); // Отправляем GET запрос и получаем HTML страницу
            HtmlDocument doc = new HtmlDocument();//  объект для парсинга HTML
            doc.LoadHtml(html);
            var searchResults = doc.DocumentNode.SelectNodes("//div");
            foreach (var result in searchResults)
            {
                if (result.FirstChild?.Name == "h3")
                {
                    
                    Console.WriteLine(ParsingHTML(result.FirstChild.InnerText));
                    Console.WriteLine();
                }
            }
           

        }
    }
}
