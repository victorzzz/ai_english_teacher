namespace EnglishAI.Host;

public static class Init
{
    public static void InitUIControllers(this IServiceCollection services)
    {
        foreach (var controller in Application.Init.GetUIControllers())
        {
            services.AddSingleton(controller);
        }
    }
}
