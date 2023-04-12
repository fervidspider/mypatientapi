using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System.Collections;
using System.Xml.Linq;
using mypatientapi.Models;
using mypatientapi.Services;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

// builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("PatientDatabase"));
// builder.Services.AddSingleton<PatientService>();

var client = new MongoClient("mongodb://admin:admin@localhost:27017");
var db = client.GetDatabase("mypatient");

var collectionA = db.GetCollection<Patient>("patients");
builder.Services.AddSingleton(collectionA);
builder.Services.AddSingleton<PatientService>();

var collectionB = db.GetCollection<Appointment>("appointments");
builder.Services.AddSingleton(collectionB);
builder.Services.AddSingleton<AppointmentService>();



builder.WebHost.UseUrls("https://localhost:7192", "https://192.168.10.118:7192");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    /*
    app.UseStaticFiles(new StaticFileOptions
    {
        ServeUnknownFileTypes = true,
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "Swagger")),
        RequestPath = "/CustomSwagger"
    });

    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/CustomSwagger/swagger.yaml", "MySwagger"));
    */

    app.UseSwagger();
    app.UseSwaggerUI();

};


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

