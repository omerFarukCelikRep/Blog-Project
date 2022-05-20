﻿using BlogApp.Business.Abstract;
using BlogApp.Business.Concrete;
using BlogApp.Business.Validations;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Business;
public static class BusinessServiceRegistration
{
    public static void AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITopicService, TopicService>();
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<IArticleService, ArticleService>();
    }

    public static IMvcBuilder AddCustomValidation(this IMvcBuilder mvcBuilder)
    {
        mvcBuilder.AddFluentValidation(options =>
        {
            options.ImplicitlyValidateChildProperties = true;
            options.ImplicitlyValidateRootCollectionElements = true;

            options.RegisterValidatorsFromAssembly(typeof(IValidationMaker).Assembly);
        });

        return mvcBuilder;
    }
}
