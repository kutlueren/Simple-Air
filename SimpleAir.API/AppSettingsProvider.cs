using Microsoft.Extensions.Configuration;

namespace SimpleAir.API
{
    public class AppSettingsProvider
    {
        public static IConfiguration Configuration { get; set; }
    }
}
