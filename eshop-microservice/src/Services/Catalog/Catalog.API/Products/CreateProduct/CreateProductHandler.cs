﻿

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name , List<string> Category , string Description,string ImageFile , decimal Price)
    :ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is Required");
        RuleFor(x=>x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x=>x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(x=>x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}
internal class CreateProductCommandHandler(IDocumentSession session,ILogger<CreateProductCommandHandler> logger )
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateProductCommandHandler.Handle called with {@command}",command);
        //var result =await validator.ValidateAsync(command,cancellationToken);
        //var errors=result.Errors.Select(x=>x.ErrorMessage).ToList();
        //if (errors.Any())
        //{
        //    throw new ValidationException(errors.FirstOrDefault());
        //}
        // create product entity from command object
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        // save the object to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        // return the createProductResult result
        return  new CreateProductResult(product.Id);
    }
}
