namespace MobWx.API.ServerConfig
{
    public static class AppConfig
    {
        public static void ConfigureApp(WebApplication app)
        {
            app.MapDefaultEndpoints();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
        }
    }
}
