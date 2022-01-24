using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cwiczenia1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                string url = args[0];
                HttpClient client = new HttpClient();
                HttpResponseMessage result = await client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    HashSet<string> resultSet = new HashSet<string>();
                    string input = await result.Content.ReadAsStringAsync();
                    var regex = new Regex("[a-z]+[a-z0-9]*@[a-z0-9]+\\.[a-z]+", RegexOptions.IgnoreCase);
                    MatchCollection matches = regex.Matches(input);

                    if (matches.Count > 0)
                    {
                        foreach (var m in matches)
                        {
                            resultSet.Add(m.ToString());
                        }
                        foreach (string entry in resultSet)
                        {
                            Console.WriteLine(entry);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nie znaleziono adresów email");
                    }
                } 
                else
                {
                    Console.WriteLine("Błąd w czasie pobierania strony");
                }
                client.Dispose();

                Console.WriteLine("Zakończono przeszukiwanie strony " + url);
            } catch (ArgumentNullException ane)
            {
                Console.WriteLine("Nie podano adresu url");
            } catch (ArgumentException ae)
            {
                Console.WriteLine(" Niepoprawny adres url");
            }
            Console.ReadKey();
        }
    }
}
