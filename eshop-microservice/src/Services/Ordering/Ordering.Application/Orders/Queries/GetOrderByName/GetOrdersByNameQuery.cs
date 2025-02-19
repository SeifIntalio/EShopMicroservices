﻿using BuildingBlocks.CQRS;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Queries;

public record GetOrdersByNameQuery(string Name):IQuery<GetOrdersByNameResult>;
public record GetOrdersByNameResult(IEnumerable<OrderDto> Orders);

