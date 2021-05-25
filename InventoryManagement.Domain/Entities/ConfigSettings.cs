namespace InventoryManagement.Domain.Entities
{

    public class Configsettings
    {
        public Logging Logging { get; set; }
        public Connectionstrings ConnectionStrings { get; set; }
        public Jwt Jwt { get; set; }
        public string AllowedHosts { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }

    public class Connectionstrings
    {
        public string InventoryDB { get; set; }
    }

    public class Jwt
    {
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public string JwtKey { get; set; }
    }


}
