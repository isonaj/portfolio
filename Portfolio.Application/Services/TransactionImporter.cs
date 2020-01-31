using Portfolio.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Application.Services
{
    public class TransactionImporter
    {
        public IEnumerable<Model.Transaction> LoadTransactions(Stream stream)
        {
            var result =  new List<Model.Transaction>();
            using (var reader = new StreamReader(stream))
            {
                // Read and ignore first row
                var line = reader.ReadLine();
                if (line != null)
                {
                    line = reader.ReadLine();
                    while (line != null)
                    {
                        var tokens = line.Split(',');
                        if (tokens.Length == 12)
                        {
                            var txn = new Model.Transaction(DateTime.Parse(tokens[2]), tokens[4], tokens[3] == "B" ? TransactionTypes.Buy : TransactionTypes.Sell, Convert.ToInt32(tokens[5]), Convert.ToDecimal(tokens[7]), Convert.ToDecimal(tokens[8]));
                            result.Add(txn);
                        }

                        line = reader.ReadLine();
                    }
                }
            }
            return result;
        }
    }
}
