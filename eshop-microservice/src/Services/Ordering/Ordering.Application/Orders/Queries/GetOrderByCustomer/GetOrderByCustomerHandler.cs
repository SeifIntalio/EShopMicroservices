using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Extensions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Queries.GetOrderByCustomer;

public class GetOrderByCustomerHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders=await dbContext.Orders
            .Include(x=>x.OrderItems)
            .AsNoTracking()
            .Where(x=>x.CustmerId==CustomerId.Of(query.CustomerId))
            .OrderBy(x=>x.OrderName.Value)
            .ToListAsync(cancellationToken);
        return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
        throw new NotImplementedException();
    }
}
