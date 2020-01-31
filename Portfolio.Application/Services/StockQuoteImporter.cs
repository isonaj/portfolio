using Portfolio.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Application.Services
{
    public class StockQuoteImporter
    {
        public IEnumerable<Model.StockQuote> LoadStockQuotes(Stream stream)
        {
            var result =  new List<Model.StockQuote>();
            using (var reader = new StreamReader(stream))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var tokens = line.Split(',');
                    if (tokens.Length == 7)
                    {
                        var quote = new Model.StockQuote(
                            tokens[0], 
                            ConvertToDate(tokens[1]), 
                            Convert.ToDecimal(tokens[2]), 
                            Convert.ToDecimal(tokens[3]), 
                            Convert.ToDecimal(tokens[4]), 
                            Convert.ToDecimal(tokens[5]), 
                            Convert.ToInt64(tokens[6]));
                        result.Add(quote);
                    }

                    line = reader.ReadLine();
                }
            }
            return result;
        }

        private DateTime ConvertToDate(string date)
        {
            return new DateTime(
                Convert.ToInt32(date.Substring(0, 4)),
                Convert.ToInt32(date.Substring(4, 2)),
                Convert.ToInt32(date.Substring(6, 2)));
        }
    }
}
