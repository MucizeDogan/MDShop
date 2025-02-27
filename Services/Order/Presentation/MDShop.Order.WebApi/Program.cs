using MDShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using MDShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
using MDShop.Order.Application.Interfaces;
using MDShop.Order.Application.Services;
using MDShop.Order.Persistence.Context;
using MDShop.Order.Persistence.Repositeries;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
    opt.Authority = builder.Configuration["IdentityServerUrl"];  //OpenId yi �a��racak yer. Jwt Bearer � kimle birlikte kullanaca��m�z� belirliyoruz. IdentityServerUrl appSettings.json �ndan gelecek. bu da art�k catalog Identity ile birlikte aya�a kalkacak.
    opt.Audience = "ResourceOrder";
    opt.RequireHttpsMetadata = false; //Https den http ye �ekti�imiz i�in bu ayar gerekiyor.
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddApplicationService(builder.Configuration); // Ordering MediatR registiration
builder.Services.AddDbContext<OrderContext>();

#region Address & OrderDetail CQRS Registration
builder.Services.AddScoped<GetAddressQueryHandler>();
builder.Services.AddScoped<GetAddressByIdQueryHandler>();
builder.Services.AddScoped<CreateAddressCommandHandler>();
builder.Services.AddScoped<UpdateAddressCommandHandler>();
builder.Services.AddScoped<RemoveAddressCommandHandler>();

builder.Services.AddScoped<GetOrderDetailQueryHandler>();
builder.Services.AddScoped<GetOrderDetailByIdQueryHandler>();
builder.Services.AddScoped<CreateOrderDetailCommandHandler>();
builder.Services.AddScoped<UpdateOrderDetailCommandHandler>();
builder.Services.AddScoped<RemoveOrderDetailCommandHandler>();
#endregion

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
