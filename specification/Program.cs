using System;
using System.Collections.Generic;
using System.Linq;

// Đối tượng cần kiểm tra
public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// Giao diện cho Specification
public interface ISpecification<T>
{
    bool IsSatisfiedBy(T entity);
}

// Specification cho giá
public class PriceSpecification : ISpecification<Product>
{
    private readonly decimal _minPrice;

    public PriceSpecification(decimal minPrice)
    {
        _minPrice = minPrice;
    }

    public bool IsSatisfiedBy(Product product)
    {
        return product.Price >= _minPrice;
    }
}

// Specification cho tên
public class NameSpecification : ISpecification<Product>
{
    private readonly string _name;

    public NameSpecification(string name)
    {	
        _name = name;
    }

    public bool IsSatisfiedBy(Product product)
    {
        return product.Name.Contains(_name, StringComparison.OrdinalIgnoreCase);
    }
}

// Kết hợp các Specification
public class AndSpecification<T> : ISpecification<T>
{
    private readonly ISpecification<T> _first;
    private readonly ISpecification<T> _second;

    public AndSpecification(ISpecification<T> first, ISpecification<T> second)
    {
        _first = first;
        _second = second;
    }

    public bool IsSatisfiedBy(T entity)
    {
        return _first.IsSatisfiedBy(entity) && _second.IsSatisfiedBy(entity);
    }
}

// Ví dụ sử dụng
public class Program
{
    public static void Main()
    {
        var products = new List<Product>
        {
            new Product { Name = "Apple", Price = 1.2m },
            new Product { Name = "Banana", Price = 0.8m },
            new Product { Name = "Cherry", Price = 2.5m }
        };

        var priceSpec = new PriceSpecification(1.0m);
        var nameSpec = new NameSpecification("a");
        var combinedSpec = new AndSpecification<Product>(priceSpec, nameSpec);

        var filteredProducts = products.Where(p => combinedSpec.IsSatisfiedBy(p));

        foreach (var product in filteredProducts)
        {
            Console.WriteLine($"Filtered Product: {product.Name}, Price: {product.Price}");
        }
    }
}