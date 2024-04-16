using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ProductRepository : Repo<Product, DataContext>
{
    private readonly DataContext _context;
    public ProductRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override Product Get(Expression<Func<Product, bool>> expression)
    {
        try
        {
            var productEntity = base.Get(expression);
            if (productEntity != null)
            {
                return productEntity;
            }
            else
            {
                return new Product { 
                    Id = 0, 
                    CategoryId = 0, 
                    Price = 0, 
                    Title = "N/A", 
                    Category = new Category { CategoryName = "M/A", Id = 0 }, 
                    Inventories = new List<Inventory> { 
                        new Inventory { ProductId = 0, StoreId = 0, Amount = 0 } 
                    },
                    Properties = new List<Property>
                    {
                        new Property { Id = 0, Title = "N/A", PropertyValue = "N/A"}
                    }
                };
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
        
    }

    public override Product Update(Product entity, Expression<Func<Product, bool>> expression)
    {
        try
        {
            var productInDb = _context.Products
            .Single(l => l.Id == entity.Id);

            _context.Entry(productInDb).CurrentValues.SetValues(entity);

            foreach (var propertyInDb in productInDb.Properties.ToList())
                if (!entity.Properties.Any(p => p.Id == propertyInDb.Id))
                    productInDb.Properties.Remove(propertyInDb);

            foreach (var property in entity.Properties)
            {
                var propertyInDb = productInDb.Properties.SingleOrDefault(
                    p => p.Id == property.Id);
                if (propertyInDb != null)
                    _context.Entry(propertyInDb).CurrentValues.SetValues(property);
                else
                {
                    _context.Properties.Attach(property);
                    productInDb.Properties.Add(property);
                }
            }

            _context.SaveChanges();

            return productInDb;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;


        //return base.Update(entity, expression);
    }
}
