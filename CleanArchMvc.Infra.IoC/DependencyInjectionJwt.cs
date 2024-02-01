using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace CleanArchMvc.Infra.IoC
{
    public static class DependencyInjectionJwt
    {
        public static IServiceCollection AddInfrastructureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            //informar o tipo de autenticação JWT-Bearer
            //definir o modelo de desafio de autenticação
            services.AddAuthentication(opt =>
            {
                //Isso garante que a autenticação será feita extraindo
                //e validando o token JWT do header AUTORIZATION que client vai enviar
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            //habbilita a autenticacao JWT usando o esquema e desafio definidos
            //validar o token
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //aqui é definido o se deseja validar do token                     
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    //aqui é feita a validação

                    //emissor deve ser igual ao valor do configuration
                    ValidIssuer = configuration["Jwt:Issuer"],

                    //client ou serviço que pode consumir deve ser igual ao valor do configuration
                    ValidAudience = configuration["Jwt:Audience"],

                    //chave privada deve conter o mesmo encrypt que foi usado para gerar a chave
                    IssuerSigningKey = new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!)),

                    // por padrão o ClockSkew adiciona cinco minutos a mais ao
                    //ao valor que foi definido inicialmente como valor para expiração
                    //do token
                    //setando como Zero, o tempo para expiração fica igual ao definido inicialmente
                    ClockSkew = TimeSpan.Zero
                 };
            });

            return services;
        }
    }
}
