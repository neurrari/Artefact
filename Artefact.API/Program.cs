using Microsoft.EntityFrameworkCore;
using Artefact.API.Data;
using Artefact.API.Services.Interfaces; 
using Artefact.API.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ConnectionSqlServer");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'ConnectionSqlServer' not found.");
}
// Используйте AddDbContextPool для лучшей производительности в веб-приложениях
builder.Services.AddDbContextPool<BaseDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICaretakerService, CaretakerService>();
builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IDocumentTypeService, DocumentTypeService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IExhibitionService, ExhibitionService>();
builder.Services.AddScoped<IExhibitService, ExhibitService>(); // Особый интерфейс
builder.Services.AddScoped<IHallService, HallService>();
builder.Services.AddScoped<IInventoryCypherService, InventoryCypherService>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<IMeasureService, MeasureService>();
builder.Services.AddScoped<IRecievingWayService, RecievingWayService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<IStoringTypeService, StoringTypeService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITechniqueService, TechniqueService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();