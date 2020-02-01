using MediatR;
using Portfolio.Application.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Application.Portfolio
{
    public class DeletePortfolio: IRequest
    {
        public Guid Id { get; set; }
        public DeletePortfolio(Guid id)
        {
            Id = id;
        }

        public class DeletePortfolioHandler : IRequestHandler<DeletePortfolio>
        {
            private readonly IPortfolioRepository _repo;
            public DeletePortfolioHandler(IPortfolioRepository repo)
            {
                _repo = repo;
            }

            public async Task<Unit> Handle(DeletePortfolio request, CancellationToken cancellationToken)
            {
                await _repo.Delete(request.Id);
                return Unit.Value;
            }
        }
    }
}
