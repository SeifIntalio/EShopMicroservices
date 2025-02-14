﻿

using Discount.Grpc;
using static Discount.Grpc.DiscountProtoService;

namespace Basket.API.Basket.StoreBasket;


public record StoreBasketCommand(ShoppingCart Cart):ICommand<StoreBasketResult>;

public record StoreBasketResult(string userName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Username is Required");

    }
}
public class StoreBasketHandlerCommandHandler(IBasketRepository repository,DiscountProtoServiceClient discountProto)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, cancellationToken);
        ShoppingCart cart = command.Cart;
        await repository.StoreBasket(command.Cart,cancellationToken);
        return new StoreBasketResult(command.Cart.UserName);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            {
                item.Price -= coupon.Amount;
            };
        }
    }
}
