using DiplomnaStudentApi.Repository;
using DiplomnaStudentApi.Services;
using DiplomnaStudentApi.ServicesImpl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;

namespace DiplomnaStudentApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyHeader());
            });
            services.Configure<FormOptions>(o =>  // currently all set to max, configure it to your needs!
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = long.MaxValue; // <-- !!! long.MaxValue
                o.MultipartBoundaryLengthLimit = int.MaxValue;
                o.MultipartHeadersCountLimit = int.MaxValue;
                o.MultipartHeadersLengthLimit = int.MaxValue;
            });
            services.AddControllers();
           
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Audience = "api1";
                    options.Authority = "https://localhost:5000";
                });
        
            services.AddScoped<IStudentProfileManager, StudentProfileManager>();
            services.AddScoped<IStudentProfileService, StudentProfileService>();
            services.AddScoped<INotificationManager, NotificationManager>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IDebtManager, DebtManager>();
            services.AddScoped<IDebtService, DebtService>();
            services.AddScoped<IPaymentManager, PaymentManager>();
            services.AddScoped<IPaymentService, PaymentServise>();
            services.AddScoped<IRoomManager, RoomManager>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IApplicationForSettlementManager, ApplicationForSettlementManager>();
            services.AddScoped<IApplicationForSettlementService, ApplicationForSettlementServiceImpl>();
            services.AddScoped<IWordGeneratorService, WordGeneratorServiceImpl>();
            services.AddScoped<IResidenceAgreementManager, ResidenceAgreementManager>();
            services.AddScoped<IResidenceAgreementService, ResidenceAgreementServiceImpl>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.Use(async (context, next) =>
            {
                context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = null; // unlimited I guess
                await next.Invoke();
            });
            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}
