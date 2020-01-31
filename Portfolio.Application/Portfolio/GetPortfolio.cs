using MediatR;
using Portfolio.Application.Data;
using System;
using System.Collections.Generic;
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
            private readonly IRepository<Model.Portfolio> _repo;
            public GetPortfolioHandler(IRepository<Model.Portfolio> repo)
            {
                _repo = repo;
            }

            public async Task<Model.Portfolio> Handle(GetPortfolio request, CancellationToken cancellationToken)
            {
                var portfolio = await _repo.Get(request.Id);
                portfolio.GenerateSummary();
                return portfolio;
            }
        }
    }
}

