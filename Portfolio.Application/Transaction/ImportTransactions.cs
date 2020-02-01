using MediatR;
using Portfolio.Application.Data;
using Portfolio.Application.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Application.Transaction
{
    public class ImportStockQuotes: IRequest
    {
        public Guid PortfolioId { get; set; }
        public Stream Stream { get; set; }
        public ImportStockQuotes(Guid portfolidId, Stream stream)
        {
            PortfolioId = portfolidId;
            Stream = stream;
        }

        public class ImportTransactionsHandler : IRequestHandler<ImportStockQuotes>
        {
            private readonly IPortfolioRepository _repo;
            public ImportTransactionsHandler(IPortfolioRepository repo)
            {
                _repo = repo;
            }

            public async Task<Unit> Handle(ImportStockQuotes request, CancellationToken cancellationToken)
            {
                var importer = new TransactionImporter();
                var transactions = importer.LoadTransactions(request.Stream);

                var portfolio = await _repo.Get(request.PortfolioId);
                portfolio.AddTransactions(transactions);
                await _repo.Save(portfolio);

                return Unit.Value;
            }
        }
    }
}
