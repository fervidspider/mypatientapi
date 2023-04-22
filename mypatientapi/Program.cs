using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System.Collections;
using System.Xml.Linq;
using mypatientapi.Models;
using mypatientapi.Services;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Mvc.Core;
using System.Reflection.Emit;
using MySolution;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.


var client = new MongoClient("mongodb://admin:admin@localhost:27017");
var db = client.GetDatabase("mypatient");

var collectionA = db.GetCollection<Patient>("patients");
builder.Services.AddSingleton(collectionA);
builder.Services.AddSingleton<PatientService>();

var collectionB = db.GetCollection<Appointment>("appointments");
builder.Services.AddSingleton(collectionB);
builder.Services.AddSingleton<AppointmentService>();

// Odata

builder.Services.AddControllers()
    .AddOData(opt => opt.Count().Filter().Expand().Select().OrderBy().SetMaxTop(50));



builder.WebHost.UseUrls("https://localhost:7192", "https://192.168.0.237:7192");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

    c.OperationFilter<ODataOperationFilter>();

});



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

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

    app.UseSwaggerUI();

};


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

