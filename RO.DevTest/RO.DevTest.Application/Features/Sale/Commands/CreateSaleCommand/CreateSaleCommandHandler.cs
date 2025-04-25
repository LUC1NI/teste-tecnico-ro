using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using SaleEntity = RO.DevTest.Domain.Entities.Sale;
using SaleItemEntity = RO.DevTest.Domain.Entities.SaleItem;

namespace RO.DevTest.Application.Features.Sale.Commands.CreateSaleCommand;

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult> {
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;

    public CreateSaleCommandHandler(ISaleRepository saleRepository, IProductRepository productRepository) {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken) {
        var sale = new SaleEntity {
            ClientId = request.ClientId,
            SaleDate = request.SaleDate,
            SaleItems = new List<SaleItemEntity>()
        };

        foreach (var itemDto in request.Items) {
            var product = _productRepository.Get(p => p.Id == itemDto.ProductId);
            if (product == null) {
                throw new KeyNotFoundException($"Product with ID {itemDto.ProductId} not found");
            }
            if (product.StockQuantity < itemDto.Quantity) {
                throw new InvalidOperationException($"Insufficient stock for product {product.Name}");
            }
            product.StockQuantity -= itemDto.Quantity;
            _productRepository.Update(product);

            var saleItem = new SaleItemEntity {
                ProductId = itemDto.ProductId,
                Quantity = itemDto.Quantity,
                UnitPrice = product.Price
            };
            sale.SaleItems.Add(saleItem);
        }

        await _saleRepository.CreateAsync(sale, cancellationToken);

        var result = new CreateSaleResult {
            Id = sale.Id,
            ClientId = sale.ClientId,
            SaleDate = sale.SaleDate,
            Items = request.Items
        };

        return result;
    }
}
