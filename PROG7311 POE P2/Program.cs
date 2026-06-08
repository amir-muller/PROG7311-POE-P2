var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


//get url from appsettings.json
var apiSettings = builder.Configuration.GetSection("ApiSettings");
var apiBaseUrl = apiSettings["BaseUrl"];

//reg HttpClient
builder.Services.AddHttpClient("MyWebAPI", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

var app = builder.Build();

//http request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
