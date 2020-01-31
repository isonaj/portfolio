using MediatR;
using Portfolio.Application.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Application.Portfolio
{
    public class CreatePortfolio: IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CreatePortfolio(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public class CreatePortfolioHandler : IRequestHandler<CreatePortfolio>
        {
            private readonly IRepository<Model.Portfolio> _repo;
            public CreatePortfolioHandler(IRepository<Model.Portfolio> repo)
            {
                _repo = repo;
            }

            public async Task<Unit> Handle(CreatePortfolio request, CancellationToken cancellationToken)
            {
                var portfolio = new Model.Portfolio(request.Id, request.Name);
                await _repo.Create(portfolio);

                return Unit.Value;
            }
        }
    }
}
