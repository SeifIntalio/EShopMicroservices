using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersQuery, GetOrderResult>
{
    public async Task<GetOrderResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        // get orders with pagination
        // return result
        var pageIndex =query.PaginationRequest.pageIndex;
        var pageSize = query.PaginationRequest.pageSize;

        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);
        var orders=await dbContext.Orders
            .Include(x=>x.OrderItems)
            .OrderBy(x=>x.OrderName.Value)
            .Skip(pageSize*pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new GetOrderResult(
            new PaginationResult<OrderDto>(
                pageIndex,
                pageSize,
                totalCount,
                orders.ToOrderDtoList()));
    }
}
