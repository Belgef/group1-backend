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

        public static class Categories
        {
            public const string GetAll = Base + "/categories";
        }

        public static class Photos
        {
            public const string GetAll = Base + "/photos";

            public const string Create = Base + "/photos";

            public const string Delete = Base + "/photos/{id}";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/signin";

            public const string Register = Base + "/identity/signup";

            public const string RefreshToken = Base + "/identity/refreshtoken";
        }

        public static class Orders
        {
            public const string GetAll = Base + "/orders";

            public const string Create = Base + "/orders";

            public const string Get = Base + "/orders/{id}";
        }
    }
}
