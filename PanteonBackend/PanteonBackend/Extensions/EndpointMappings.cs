using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using MediatR;
using Panteon_Backend.Commands;
using Panteon_Backend.Queries;

namespace Panteon_Backend.Extensions
{
    public static class EndpointMappings
    {
        public static WebApplication MapConfigEndpoints(this WebApplication application)
        {
            var configGroup = application.MapGroup("/config");

            configGroup.MapGet("/GetConfig", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllConfigurationsQuery());
                return Results.Ok(result);
            });

            configGroup.MapPost("/CreateConfig", async (IMediator mediator, CreateConfigurationCommand command) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            }).RequireAuthorization();

            configGroup.MapPatch("/UpdateConfig", async (IMediator mediator , UpdateConfigurationCommand command) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            }).RequireAuthorization();

            configGroup.MapDelete("/DeleteConfig", async (IMediator mediator) =>
            {
                return Results.Ok("Delete Config");
            }).RequireAuthorization();

            return application;
        }

        public static WebApplication MapIdentityEndpoints(this WebApplication application)
        {
            var identityGroup = application.MapGroup("/identity");

            identityGroup.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllUsersQuery());
                return Results.Ok(result);
            }).WithName("GetUsers");

            identityGroup.MapPost("/CreateUser", async (IMediator mediator, CreateUserCommand command) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            }).WithName("CreateUser");

            identityGroup.MapPost("/login", async (IMediator mediator, LoginCommand command) =>
            {
                var result = await mediator.Send(command);
                if (result.Success)
                {
                    return Results.Ok(result);
                }
                return Results.Unauthorized();
            });

            identityGroup.MapPatch("/", async (IMediator mediator) =>
            {
                return Results.Ok("Update User");
            }).WithName("UpdateUser");

            identityGroup.MapDelete("/", async (IMediator mediator) =>
            {
                return Results.Ok("Delete User");
            }).WithName("DeleteUser");

            return application;
        }
    }
}
