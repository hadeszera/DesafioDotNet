using DesafioDotNet.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioDotNet
{
    public class Startup
    {
        string sqlConnectionString = @"Server=localhost;Trusted_Connection=True;";
        string DbsqlConnectionString = @"Server=localhost;Initial Catalog=DesafioDotNet;Trusted_Connection=True;";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ExecuteSqlStatement();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DesafioDotNet", Version = "v1" });
            });
            services.AddScoped<IProductDal, ProductDal>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DesafioDotNet v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ExecuteSqlStatement() {
            CreateDb();
            CreateTables();
            CreateProcedures();
        }
        public void CreateProcedures() {

            var DropProc = ReadFile(@"SP\DropProc.sql");
            ExecuteSqlQuery(DropProc.ToString());

            var getAllProducts = ReadFile(@"SP\CreateSP_GetAllProducts.sql");
            ExecuteSqlQuery(getAllProducts.ToString());

            var getProductById = ReadFile(@"SP\CreateSP_GetProductById.sql");
            ExecuteSqlQuery(getProductById.ToString());
        }

        public string ReadFile(string pathFile) {
            using (StreamReader reader = new StreamReader(@$".\SQL\{pathFile}"))
            {
                string line;
                var ret = new StringBuilder();

                while ((line = reader.ReadLine()) != null)
                {
                    ret.AppendLine(line);
                }
                return ret.ToString();
            }

        }

        public void CreateDb() { 
            var file = ReadFile(@"Db\CreateDB.sql");
            ExecuteSqlQueryCreateDB(file.ToString());
        }

        public void CreateTables() {
            var file = ReadFile(@"Table\Product.sql");
            ExecuteSqlQuery(file.ToString());
        }

        public void ExecuteSqlQueryCreateDB(string query)
        {
            using (SqlConnection con = new SqlConnection(sqlConnectionString))
            {

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void ExecuteSqlQuery(string query) {
            using (SqlConnection con = new SqlConnection(DbsqlConnectionString))
            {

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }





    }
}
