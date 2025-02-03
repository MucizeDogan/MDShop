using MDShop.Catalog.Services.CategoryServices;
using MDShop.Catalog.Services.FeatureSliderServices;
using MDShop.Catalog.Services.ProductDetailServices;
using MDShop.Catalog.Services.ProductImageServices;
using MDShop.Catalog.Services.ProductServices;
using MDShop.Catalog.Services.SpecialOfferServices;
using MDShop.Catalog.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
    opt.Authority = builder.Configuration["IdentityServerUrl"];  //OpenId yi �a��racak yer. Jwt Bearer � kimle birlikte kullanaca��m�z� belirliyoruz. IdentityServerUrl appSettings.json �ndan gelecek. bu da art�k catalog Identity ile birlikte aya�a kalkacak.
    opt.Audience = "ResourceCatalog";
    opt.RequireHttpsMetadata = false; //Https den http ye �ekti�imiz i�in bu ayar gerekiyor.
});

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductDetailService, ProductDetailService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IFeatureSliderService, FeatureSliderService>();
builder.Services.AddScoped<ISpecialOfferService, SpecialOfferService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings")); //appsettings.json i�erisine yazd�klar�m� konfig�re ediyoruz.
builder.Services.AddScoped<IDatabaseSettings>(sp => {
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
}); // Database settings s�n�f� i�erisindeki de�erlere ula�acak yani tablo adlar� (collection nameler), connectionString vs gibi 

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
