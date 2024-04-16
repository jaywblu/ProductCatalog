using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Services;

namespace Presentation.ConsoleApp.Services;

public class ManageProductsService(ProductService productService, StoreService storeService)
{
    private readonly ProductService _productService = productService;
    private readonly StoreService _storeService = storeService;

    public void CreateProductOption()
    {
        Console.Clear();
        Console.WriteLine("Enter product information:");
        var product = CreateProductDto();

        var result = _productService.CreateProduct(product);

        if (result != null)
        {
            Console.Clear();
            Console.WriteLine($"Product was created:\n");
            Console.WriteLine($"Title: {result.Title}");
            Console.WriteLine($"Description: {result.ProductDescription}");
            Console.WriteLine($"Price: {result.Price}");
            Console.WriteLine($"Category: {result.Category.CategoryName}");
            Console.WriteLine("Properties:");
            foreach (var property in result.Properties)
            {
                Console.WriteLine($"{property.Title}: {property.PropertyValue}");
            }
            Console.WriteLine("Store inventory:");
            foreach (var inventory in result.Inventories)
            {
                Console.WriteLine($"{inventory.Store.StoreName}: {inventory.Amount}");
            }
        } else
        {
            Console.Clear();
            Console.WriteLine($"Something went wrong, could not create product.");
        }

        Console.ReadKey();
    }

    public void UpdateProductOption()
    {
        Console.Clear();
        Console.WriteLine("Enter ID of product you wish to update:");
        int n;
        string input;
        do
        {
            input = Console.ReadLine()!;
        } while (int.TryParse(input, out n) == false);
        var existingProduct = _productService.GetProductById(n);
        if (existingProduct != null && existingProduct.Id != 0)
        {
            Console.Clear();
            Console.WriteLine($"Product was created:\n");
            Console.WriteLine($"Title: {existingProduct.Title}");
            Console.WriteLine($"Description: {existingProduct.ProductDescription}");
            Console.WriteLine($"Price: {existingProduct.Price}");
            Console.WriteLine($"Category: {existingProduct.CategoryName}");
            Console.WriteLine("Properties:");
            foreach (var property in existingProduct.Properties)
            {
                Console.WriteLine($"{property.Title}: {property.PropertyValue}");
            }
            Console.WriteLine("Store inventory:");
            foreach (var inventory in existingProduct.Inventories)
            {
                Console.WriteLine($"{inventory.Store.StoreName}: {inventory.Amount}");
            }

            Console.WriteLine("\nSelect what you want to update:");
            List<string> optionsList = [
                "1. Title",
                "2. Description",
                "3. Price",
                "4. Category",
                "5. Properties",
                "6. Store inventory",
                "0. Cancel"
            ];
            foreach (var item in optionsList)
            {
                Console.WriteLine(item);
            }
            Console.Write("\nSelect an option:");
            var option = Console.ReadKey(true).KeyChar.ToString();
            switch (option)
            {
                case "0":
                    break;
                case "1":
                    Console.Write("Enter a new title: ");
                    existingProduct.Title = Console.ReadLine()!;
                    break;
                case "2":
                    Console.Write("Enter a new description: ");
                    existingProduct.ProductDescription = Console.ReadLine()!;
                    break;
                case "3":
                    Console.Write("Enter a new price: ");
                    existingProduct.Price = decimal.Parse(Console.ReadLine()!);
                    break;
                case "4":
                    Console.Write("Enter a new category: ");
                    existingProduct.CategoryName = Console.ReadLine()!;
                    break;
                case "5":
                    existingProduct.Properties = AddPropertiesToProduct();
                    break;
                case "6":
                    existingProduct.Inventories = AddProductInventory();
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("\nInvalid selection, please select one of the options above. Press any key to continue.");
                    Console.ReadKey();
                    break;
            }

            var result = _productService.UpdateProduct(existingProduct);

            if (result != null)
            {
                Console.Clear();
                Console.WriteLine($"Product was updated:\n");
                Console.WriteLine($"Title: {result.Title}");
                Console.WriteLine($"Description: {result.ProductDescription}");
                Console.WriteLine($"Price: {result.Price}");
                Console.WriteLine($"Category: {result.Category.CategoryName}");
                Console.WriteLine("Properties:");
                foreach (var property in result.Properties)
                {
                    Console.WriteLine($"{property.Title}: {property.PropertyValue}");
                }
                Console.WriteLine("Store inventory:");
                foreach (var inventory in result.Inventories)
                {
                    Console.WriteLine($"{inventory.Store.StoreName}: {inventory.Amount}");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Something went wrong, could not create product.");
            }

            Console.ReadKey();
        }
        else
        {
            Console.WriteLine($"\nCould not find a product with that ID, please try again.");
            Console.ReadKey();
        }
    }

    private ProductDto CreateProductDto()
    {
        var product = new ProductDto();
        Console.Write("Title: ");
        product.Title = Console.ReadLine()!;
        Console.Write("Description: ");
        product.ProductDescription = Console.ReadLine()!;
        Console.Write("Price: ");
        product.Price = decimal.Parse(Console.ReadLine()!);
        Console.Write("Category: ");
        product.CategoryName = Console.ReadLine()!;
        product.Properties = AddPropertiesToProduct();
        product.Inventories = AddProductInventory();
        return product;
    }

    private ICollection<Inventory> AddProductInventory()
    {
        var stores = _storeService.GetAllStores();
        var inventories = new List<Inventory>();
        Console.WriteLine($"\nAdd inventory of this product to stores:");
        foreach (var store in stores)
        {
            var inventory = new Inventory();
            Console.WriteLine($"\nHow many units of this product are in {store.StoreName}?");
            int n;
            string input;
            do
            {
                input = Console.ReadLine()!;
            } while (int.TryParse(input, out n) == false);
            inventory.Amount = n;
            inventory.StoreId = store.Id;
            inventories.Add(inventory);
        }
        return inventories;
    }

    public ICollection<Property> AddPropertiesToProduct()
    {
        Console.WriteLine($"\nHow many properties would you like to add?");
        var properties = new List<Property>();
        int n;
        string input;
        do
        {
            input = Console.ReadLine()!;
        } while (int.TryParse(input, out n) == false);
        var amount = n;

        for (int i = 0; i < amount; i++)
        {
            var property = new Property();
            Console.Write("Title: ");
            property.Title = Console.ReadLine()!;
            Console.Write("Value: ");
            property.PropertyValue = Console.ReadLine()!;
            properties.Add(property);
        }

        return properties;
    }
}