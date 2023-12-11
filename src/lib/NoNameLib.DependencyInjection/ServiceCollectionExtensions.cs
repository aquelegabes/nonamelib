using Microsoft.Extensions.DependencyInjection;
using NoNameLib.Application.Dispatcher;
using NoNameLib.Application.Interfaces;
using NoNameLib.Domain.Interfaces;
using System.Reflection;

namespace NoNameLib.DependencyInjection;

public static class ServiceCollectionExtensions
{
    #region Private
    private static IEnumerable<Type> GetImplementedTypes(
        Assembly assembly,
        Type baseType)
    {
        return
           from type in assembly.GetTypes()
           where type.IsAssignableTo(baseType)
                   && type.IsPublic
                   && type.IsClass
                   && !type.IsAbstract
                   && !type.IsGenericType
           select type;
    }

    private static void RegisterGivenTypes(
        IServiceCollection sc,
        ServiceLifetime lifetime,
        IEnumerable<Type> implementedTypes,
        string[] typeNames)
    {
        Parallel.ForEach(implementedTypes, implementedType =>
        {
            var interfaceTypes =
                from interfaceType in implementedType.GetInterfaces()
                where typeNames.Contains(interfaceType.Name)
                select interfaceType;

            if (!interfaceTypes.Any())
                return;

            foreach (var interfaceType in interfaceTypes)
            {
                switch (lifetime)
                {
                    case ServiceLifetime.Singleton:
                        sc.AddSingleton(interfaceType, implementedType);
                        break;
                    case ServiceLifetime.Scoped:
                        sc.AddScoped(interfaceType, implementedType);
                        break;
                    case ServiceLifetime.Transient:
                        sc.AddTransient(interfaceType, implementedType);
                        break;
                }
            }
        });
    }
    #endregion Private

    public static IServiceCollection AddDispatcher(
        this IServiceCollection sc,
        ServiceLifetime lifetime)
    {
        return lifetime switch
        {
            ServiceLifetime.Singleton =>
                sc.AddSingleton(typeof(IDispatcher), typeof(Dispatcher))
                  .AddSingleton(typeof(IAsyncDispatcher), typeof(Dispatcher))
                  .AddSingleton(typeof(IResultDispatcher), typeof(Dispatcher))
                  .AddSingleton(typeof(IAsyncResultDispatcher), typeof(Dispatcher)),
            ServiceLifetime.Scoped =>
                sc.AddScoped(typeof(IDispatcher), typeof(Dispatcher))
                  .AddScoped(typeof(IAsyncDispatcher), typeof(Dispatcher))
                  .AddScoped(typeof(IResultDispatcher), typeof(Dispatcher))
                  .AddScoped(typeof(IAsyncResultDispatcher), typeof(Dispatcher)),
            ServiceLifetime.Transient =>
                sc.AddTransient(typeof(IDispatcher), typeof(Dispatcher))
                  .AddTransient(typeof(IAsyncDispatcher), typeof(Dispatcher))
                  .AddTransient(typeof(IResultDispatcher), typeof(Dispatcher))
                  .AddTransient(typeof(IAsyncResultDispatcher), typeof(Dispatcher)),
            _ => throw new NotImplementedException(),
        };
    }

    public static IServiceCollection RegisterCommandsFromAssembly(
        this IServiceCollection sc,
        Assembly assembly,
        ServiceLifetime lifetime)
    {
        var implementedTypes = GetImplementedTypes(assembly, typeof(IBaseCommand));

        var typeNames = new string[]
            {
                typeof(ICommand<>).Name,
                typeof(ICommand<,>).Name,
                typeof(IAsyncCommand<>).Name,
                typeof (IAsyncCommand<,>).Name,
            };

        RegisterGivenTypes(sc, lifetime, implementedTypes, typeNames);

        return sc;
    }

    public static IServiceCollection RegisterQueriesFromAssembly(
        this IServiceCollection sc,
        Assembly assembly,
        ServiceLifetime lifetime)
    {
        var implementedTypes = GetImplementedTypes(assembly, typeof(IBaseQuery));

        var typeNames = new string[]
            {
                typeof(IQuery<>).Name,
                typeof(IAsyncQuery<>).Name,
                typeof(IQueryFiltered<,>).Name,
                typeof(IAsyncQueryFiltered<,>).Name,
            };

        RegisterGivenTypes(sc, lifetime, implementedTypes, typeNames);

        return sc;
    }
}
