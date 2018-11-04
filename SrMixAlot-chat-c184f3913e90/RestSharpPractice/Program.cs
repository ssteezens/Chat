using RestSharp;
using System;

namespace RestSharpPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            Client = new RestClient("https://localhost:44382");

            var request = new RestRequest("/chatentry/getall", Method.GET);

            var response = Client.Execute(request);
            var content = response.Content;
            Console.WriteLine("Response: " + content);

            Console.ReadKey();
        }

        private static RestClient Client { get; set; }
    }
}
