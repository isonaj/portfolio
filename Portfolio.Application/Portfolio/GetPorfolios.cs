using MediatR;
using Portfolio.Application.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Application.Portfolio
{
    public class GetPortfolios: IRequest<IEnumerable<Model.Portfolio>>
    {

        public class GetPortfoliosHandler : IRequestHandler<GetPortfolios, IEnumerable<Model.Portfolio>>
        {
            private readonly IRepository<Model.Portfolio> _repo;
            public GetPortfoliosHandler(IRepository<Model.Portfolio> repo)
            {
                _repo = repo;
            }

            public async Task<IEnumerable<Model.Portfolio>> Handle(GetPortfolios request, CancellationToken cancellationToken)
            {
                return await _repo.GetAll();
            }
        }

    }
}
