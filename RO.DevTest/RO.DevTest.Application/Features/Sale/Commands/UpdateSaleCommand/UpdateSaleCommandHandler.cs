using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using SaleEntity = RO.DevTest.Domain.Entities.Sale;
using SaleItemEntity = RO.DevTest.Domain.Entities.SaleItem;

namespace RO.DevTest.Application.Features.Sale.Commands.UpdateSaleCommand;

public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult> {
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;

    public UpdateSaleCommandHandler(ISaleRepository saleRepository, IProductRepository productRepository) {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken) {
        var sale = _saleRepository.Get(s => s.Id == request.Id);
        if (sale == null) {
            throw new KeyNotFoundException("Sale not found");
        }

        sale.ClientId = request.ClientId;
        sale.SaleDate = request.SaleDate;

        // Remove existing sale items
        var existingItems = sale.SaleItems.ToList();
        foreach (var item in existingItems) {
            sale.SaleItems.Remove(item);
        }

        // Add updated sale items
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

        _saleRepository.Update(sale);

        var result = new UpdateSaleResult {
            Id = sale.Id,
            ClientId = sale.ClientId,
            SaleDate = sale.SaleDate,
            Items = request.Items
        };

        return result;
    }
}
