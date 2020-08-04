using MediatR;
using Portfolio.Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Application.Portfolio
{
    public class GetPortfolio : IRequest<Model.Portfolio>
    {
        public Guid Id { get; set; }
        public GetPortfolio(Guid id)
        {
            Id = id;
        }

        public class GetPortfolioHandler : IRequestHandler<GetPortfolio, Model.Portfolio>
        {
            private readonly IPortfolioRepository _repo;
            private readonly IUnitOfWork _uow;
            public GetPortfolioHandler(IPortfolioRepository repo, IUnitOfWork uow)
            {
                _repo = repo;
                _uow = uow;
            }

            public async Task<Model.Portfolio> Handle(GetPortfolio request, CancellationToken cancellationToken)
            {
                var portfolio = await _repo.Get(request.Id);
                portfolio.GenerateSummary();

                var repo = _uow.Repository<Model.StockQuote>();
                var maxDate = repo.AsQueryable()
                    .Select(x => x.Date)
                    .OrderByDescending(x => x)
                    .FirstOrDefault();
                if (maxDate == null)
                    maxDate = DateTime.Today;
                var quotes = repo.AsQueryable()
                    .Where(x => x.Date == maxDate)
                    .Select(x => x)
                    .ToList();

                foreach (var s in portfolio.Summaries)
                {
                    var quote = quotes.SingleOrDefault(q => q.Code == s.Code);
                    if (quote != null)
                        s.MarketValue = s.Units * quote.Close;
                }

                return portfolio;
            }
        }
    }
}

