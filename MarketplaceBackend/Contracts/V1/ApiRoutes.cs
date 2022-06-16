namespace MarketplaceBackend.Contracts.V1
{
    public class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Products
        {
            public const string GetAll = Base + "/products";

            public const string Update = Base + "/products/{id}";

            public const string Delete = Base + "/products/{id}";

            public const string Get = Base + "/products/{id}";

            public const string Create = Base + "/products";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";

            public const string Register = Base + "/identity/register";
        }
    }
}
