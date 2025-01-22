using MDShop.Discount.Context;
using MDShop.Discount.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
    opt.Authority = builder.Configuration["IdentityServerUrl"];  //OpenId yi �a��racak yer. Jwt Bearer � kimle birlikte kullanaca��m�z� belirliyoruz. IdentityServerUrl appSettings.json �ndan gelecek. bu da art�k catalog Identity ile birlikte aya�a kalkacak.
    opt.Audience = "ResourceDiscount";
    opt.RequireHttpsMetadata = false; //Https den http ye �ekti�imiz i�in bu ayar gerekiyor.
});

builder.Services.AddTransient<DapperContext>();
builder.Services.AddTransient<IDiscountService,DiscountService>();

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
