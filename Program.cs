using AuctionApi.Bogus;
using AuctionApi.Models;
using AuctionApi.Models.Validators;
using AuctionApi.Services;
using AuctionApi.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using AuctionApi.Middleware;

namespace AuctionApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            builder.Services.AddDbContext<AuctionDbContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
            var authenticationSetting = new AuthenticationSettings();
            builder.Configuration.GetSection("Jwt").Bind(authenticationSetting);
            builder.Services.AddSingleton(authenticationSetting);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSetting.JwtIssuer,
                    ValidAudience = authenticationSetting.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:JwtKey"]))
                };
            }
            );


            //DI Services:
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IAuctionService, AuctionService>();
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            //Validators:
            builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterDtoValidator>();
            builder.Services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
            builder.Services.AddScoped<IValidator<CreateAuctionDto>, CreateAuctionDtoValidator>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<AuctionDbContext>();

            var users = dbContext.Users.Where(u => u.Email == "test@test.pl" && (u.LastName == "Dang" || u.LastName == null));
            dbContext.RemoveRange(users);
            dbContext.SaveChanges();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                if(dbContext.Auctions.ToList().Count() == 0)
                {
                    var passwordHasher = scope.ServiceProvider.GetService<IPasswordHasher<User>>();
                    DataGenerator.Seed(dbContext);
                }
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auction API"));
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}