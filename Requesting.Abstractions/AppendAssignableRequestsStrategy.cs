using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scrutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MediaExpert
{
    /// <summary>
    /// Strategia dodawania obsługi wszystkich pasujących żądań.
    /// </summary>
    public class AppendAssignableRequestsStrategy : RegistrationStrategy
    {
        private static readonly Type _requestHandlerType = typeof(IRequestHandler<>);

        /// <inheritdoc/>
        public override void Apply(IServiceCollection services, ServiceDescriptor descriptor)
        {
            IEnumerable<Type> requestsHandlerTypes = GetRequestHandlerTypes(descriptor.ServiceType);

            foreach (var requestHandlerType in requestsHandlerTypes)
            {
                Type requestType = null;
                Type responseType = null;
                if(requestHandlerType.GenericTypeArguments.Length > 1)
                {
                    requestType = requestHandlerType.GenericTypeArguments[0];
                    responseType = requestHandlerType.GenericTypeArguments[1];
                }
                else
                {
                    requestType = requestHandlerType.GenericTypeArguments[0];
                }

                var assignableRequestTypes = requestType.Assembly.ExportedTypes
                    .Where(type => type != requestType && IsAssignableTo(type, requestType));

                foreach (var assignableType in assignableRequestTypes)
                {
                    Type serviceType = null;
                    if(responseType != null)
                    {
                        serviceType = _requestHandlerType.MakeGenericType(assignableType, responseType);
                    }
                    else
                    {
                        serviceType = _requestHandlerType.MakeGenericType(assignableType);
                    }

                    if (descriptor.ImplementationType != null)
                    {
                        services.TryAdd(new ServiceDescriptor(serviceType, descriptor.ImplementationType, descriptor.Lifetime));
                    }
                    else if (descriptor.ImplementationFactory != null)
                    {
                        services.TryAdd(new ServiceDescriptor(serviceType, descriptor.ImplementationFactory, descriptor.Lifetime));
                    }
                    else if (descriptor.ImplementationInstance != null)
                    {
                        services.TryAdd(new ServiceDescriptor(serviceType, descriptor.ImplementationInstance));
                    }
                }
            }
        }

        private static IEnumerable<Type> GetRequestHandlerTypes(Type serviceType)
        {
            if (IsRequestHandlerType(serviceType))
            {
                yield return serviceType;
                yield break;
            }

            foreach (var requestHandlerType in serviceType.GetInterfaces().Where(i => IsRequestHandlerType(i)))
            {
                yield return requestHandlerType;
            }
        }

        private static bool IsRequestHandlerType(Type serviceType)
        {
            return serviceType.IsInterface
                && serviceType.IsGenericType
                && _requestHandlerType.IsAssignableFrom(serviceType.GetGenericTypeDefinition());
        }

        private static bool IsAssignableTo(Type type, Type otherType)
        {
            var typeInfo = type.GetTypeInfo();
            var otherTypeInfo = otherType.GetTypeInfo();

            if (otherTypeInfo.IsGenericTypeDefinition)
            {
                return IsAssignableToGenericTypeDefinition(typeInfo, otherTypeInfo);
            }

            return otherTypeInfo.IsAssignableFrom(typeInfo);
        }

        private static bool IsAssignableToGenericTypeDefinition(TypeInfo typeInfo, TypeInfo genericTypeInfo)
        {
            var interfaceTypes = typeInfo.ImplementedInterfaces.Select(t => t.GetTypeInfo());

            foreach (var interfaceType in interfaceTypes)
            {
                if (interfaceType.IsGenericType)
                {
                    var typeDefinitionTypeInfo = interfaceType
                        .GetGenericTypeDefinition()
                        .GetTypeInfo();

                    if (typeDefinitionTypeInfo.Equals(genericTypeInfo))
                    {
                        return true;
                    }
                }
            }

            if (typeInfo.IsGenericType)
            {
                var typeDefinitionTypeInfo = typeInfo
                    .GetGenericTypeDefinition()
                    .GetTypeInfo();

                if (typeDefinitionTypeInfo.Equals(genericTypeInfo))
                {
                    return true;
                }
            }

            var baseTypeInfo = typeInfo.BaseType?.GetTypeInfo();

            if (baseTypeInfo is null)
            {
                return false;
            }

            return IsAssignableToGenericTypeDefinition(baseTypeInfo, genericTypeInfo);
        }
    }
}
