using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using ChuckNorrisImport.Model;
using Microsoft.EntityFrameworkCore;

namespace ChuckNorrisImport
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            if (args.Length < 1)
            {
                await Add();
            }
            else
            {
                if ("clear".Equals(args[0]))
                {
                    await ClearTable();
                    return;
                }

                var count = int.Parse(args[0]);
                if (count > 10)
                {
                    Console.WriteLine("Count is higher than 10!");
                    return;
                }

                if (count < 1)
                {
                    Console.WriteLine("Count is lower than 1!");
                    return;
                }

                await Add(count);
            }
        }

        private static async Task<Fact?> GetFact()
        {
            const string url = @"https://api.chucknorris.io/jokes/random";
            var request = (HttpWebRequest) WebRequest.Create(url);
            string json;
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync())
            await using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new(stream))
            {
                json = await reader.ReadToEndAsync();
            }

            return JsonSerializer.Deserialize<Fact>(json);
        }

        private static async Task Add(int count = 5)
        {
            var factory = new FactContextFactory();
            await using var dbContext = factory.CreateDbContext();
            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                for (var i = 0; i < count; i++)
                {
                    var fact = await (GetFact() ?? throw new NullReferenceException());
                    if (!dbContext.Facts.Contains(fact))
                    {
                        await dbContext.Facts.AddAsync(fact);
                        Console.WriteLine($"{fact.Id}\n{fact.ChuckNorrisId}\n{fact.Url}\n{fact.Joke}\n");
                    }
                }

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                Console.WriteLine(e);
                throw;
            }
        }

        private static async Task ClearTable()
        {
            var factory = new FactContextFactory();
            await using var dbContext = factory.CreateDbContext();
            await dbContext.Database.ExecuteSqlRawAsync("DELETE from Facts");
        }
    }
}