using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Repositories.CustomerData;
using Infrastructure.Services.CustomerData;
using Infrastructure.Dtos;
using Presentation.ConsoleApp.Services;
using Microsoft.Extensions.Logging;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<DataContext>(x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\jasjxa\\Documents\\EC_utb\\Projects\\ProductCatalog\\Infrastructure\\Data\\ProductCatalog.mdf;Integrated Security=True;Connect Timeout=30"));
    services.AddDbContext<CustomerDataContext>(x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\jasjxa\\Documents\\EC_utb\\Projects\\ProductCatalog\\Infrastructure\\Data\\CustomerDb.mdf;Integrated Security=True;Connect Timeout=30"));
    services.AddScoped<ActiveCampaignRepository>();
    services.AddScoped<ActiveCampaignService>();
    services.AddScoped<AddressRepository>();
    services.AddScoped<AddressService>();
    services.AddScoped<CampaignRepository>();
    services.AddScoped<CampaignService>();
    services.AddScoped<CustomerProfileRepository>();
    services.AddScoped<CustomerProfileService>();
    services.AddScoped<CustomerRepository>();
    services.AddScoped<CustomerService>();
    services.AddScoped<CustomerTypeRepository>();
    services.AddScoped<CustomerTypeService>();
    services.AddScoped<CategoryRepository>();
    services.AddScoped<CategoryService>();
    services.AddScoped<InventoryRepository>();
    services.AddScoped<InventoryService>();
    services.AddScoped<ProductRepository>();
    services.AddScoped<ProductService>();
    services.AddScoped<PropertyRepository>();
    services.AddScoped<PropertyService>();
    services.AddScoped<StoreRepository>();
    services.AddScoped<StoreService>();
    services.AddScoped<MenuService>();
    services.AddScoped<ManageProductsService>();
    services.AddLogging(builder =>
     {
         builder.AddFilter("Microsoft", LogLevel.Warning);
         builder.AddFilter("System", LogLevel.Error);
     });
}).Build();

builder.Start();

Console.Clear();

var menuService = builder.Services.GetRequiredService<MenuService>();

menuService.Show_MainMenu();

Console.ReadKey();