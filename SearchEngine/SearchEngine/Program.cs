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
        static async Task Main(string[] args)
        {
            Console.Write("Введите запрос: ");
            string query = Console.ReadLine(); 
            string url = $"https://www.google.com/search?q={query}";//  URL Google
            HttpClient client = new HttpClient(); // Создаем HTTP клиент
            string html = await client.GetStringAsync(url); // Отправляем GET запрос и получаем HTML страницу
            HtmlDocument doc = new HtmlDocument();//  объект для парсинга HTML
            doc.LoadHtml(html);
            var searchResults = doc.DocumentNode.SelectNodes("//div[@class='g']");//   элементы с  "g" результаты поиска
            foreach (var result in searchResults)
            {               
                var titleNode = result.SelectSingleNode(".//h3");// Находим заголовок  h3
                if (titleNode != null)
                {
                    string title = titleNode.InnerText;
                    string link = result.Descendants("a").FirstOrDefault()?.GetAttributeValue("href", "");
                    // Если ссылка начинается с "/url?q=" это ссылка на результат поиска удаляем эту часть
                    if (link.StartsWith("/url?q="))
                    {
                        link = link.Substring(7);
                    }

                    Console.WriteLine($"{title}\n{link}\n");
                }
            }


        }
    }
}
