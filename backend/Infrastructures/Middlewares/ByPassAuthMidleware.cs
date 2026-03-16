

namespace housingCooperative.Infrastructures.Middlewares
{

public class ByPassAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private string _currentUserId;
        public ByPassAuthMiddleware(RequestDelegate next)
        {
            _next = next;
            _currentUserId = null;
        }

        public async Task Invoke(HttpContext context, IUserHelper iUser)
        {
            var path = context.Request.Path;
            var isAuthenticated = Thread.CurrentPrincipal?.Identity?.IsAuthenticated ?? false;
            var token = context.Request.Headers["Authorization"];


            if (!isAuthenticated)
            {
                var claims = new List<Claim>()
                    {
                        new Claim("sub", iUser?.Id ?? Guid.NewGuid().ToString()),
                        new Claim("unique_name", "TestCustomer"),
                        new Claim("name", "TestCustomer"),
                        new Claim("last_name", "TestCustomer"),
                        new Claim("client_id", "1"),
                        new Claim("scop", "Order"),
                        new Claim(ClaimTypes.Role, "Customer")
                    };
                var identity = new ClaimsIdentity(claims, "TestAuth");
                var claimsPrincipal = new ClaimsPrincipal(identity);

                context.User = new ClaimsPrincipal(claimsPrincipal);
            }
            else
            {
                context.User = new ClaimsPrincipal(Thread.CurrentPrincipal);
            }

            await _next.Invoke(context);

        }
    }
}