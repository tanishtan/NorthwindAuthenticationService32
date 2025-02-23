﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NorthwindModelClassLibrary;

namespace NorthwindAuthenticationService
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _settings;
        private IUserServiceAsync _service;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> options)
        {
            _next = next;
            _settings = options.Value;
            
        }

        public async Task Invoke(HttpContext context, [FromServices] IUserServiceAsync service)
        {
            _service = service;
            //extract the authorization header token from the request headers
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if(!string.IsNullOrEmpty(token) )
            {
                var user = TokenManager.GetUserFromToken(token, _settings, _service);
                if (user != null)
                {
                    context.Items["User"] = user;
                }
            }
            await _next(context);
        }
    }
}
