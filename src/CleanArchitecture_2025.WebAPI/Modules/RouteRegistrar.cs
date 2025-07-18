﻿using CleanArhictecture_2025.WebAPI.Modules;

namespace CleanArchitecture_2025.WebAPI.Modules
{
    public static class RouteRegistrar
    {
        public static void RegisterRoutes(this IEndpointRouteBuilder app)
        {
            app.RegisterEmployeeRoutes();
            app.RegisterAuthRoutes();
        }
    }
}
