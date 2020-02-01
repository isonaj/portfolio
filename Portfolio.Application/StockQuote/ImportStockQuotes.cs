using MediatR;
using Portfolio.Application.Data;
using Portfolio.Application.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Application.StockQuote
{
    public class ImportStockQuotes: IRequest
    {
        public Stream Stream { get; set; }
        public ImportStockQuotes(Stream stream)
        {
            Stream = stream;
        }

        public class ImportStockQuotesHandler : IRequestHandler<ImportStockQuotes>
        {
            private readonly IUnitOfWork _uow;
            public ImportStockQuotesHandler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<Unit> Handle(ImportStockQuotes request, CancellationToken cancellationToken)
            {
                var importer = new StockQuoteImporter();
                var quotes = importer.LoadStockQuotes(request.Stream);

                var repo = _uow.Repository<Model.StockQuote>();
                foreach (var quote in quotes)
                    repo.Add(quote);
                await _uow.SaveChanges();

                return Unit.Value;
            }
        }
    }
}
