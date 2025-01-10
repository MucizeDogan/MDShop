using MDShop.Catalog.Services.CategoryServices;
using MDShop.Catalog.Services.ProductDetailServices;
using MDShop.Catalog.Services.ProductImageServices;
using MDShop.Catalog.Services.ProductServices;
using MDShop.Catalog.Settings;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductDetailService, ProductDetailService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings")); //appsettings.json içerisine yazdýklarýmý konfigüre ediyoruz.
builder.Services.AddScoped<IDatabaseSettings>(sp => {
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
}); // Database settings sýnýfý içerisindeki deðerlere ulaþacak yani tablo adlarý (collection nameler), connectionString vs gibi 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
