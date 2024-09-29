using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Spectre.Console.Cli;

namespace Jaahas.Spectre.Extensions {

    /// <summary>
    /// Implements both <see cref="ITypeRegistrar"/> and <see cref="ITypeResolver"/> for use in the 
    /// RuuviTag publisher <see cref="CommandApp"/>.
    /// </summary>
    public class ServiceProviderTypeRegistrar : ITypeRegistrar, ITypeResolver {

        /// <summary>
        /// The generic <see cref="IEnumerable{T}"/> type.
        /// </summary>
        /// <remarks>
        ///   Required to be able to implement <see cref="ITypeResolver.Resolve"/> correctly for 
        ///   <see cref="IEnumerable{T}"/>.
        /// </remarks>
        private static readonly Type s_ienumerableType = typeof(IEnumerable<>);

        /// <summary>
        /// The generic <see cref="List{T}"/> type.
        /// </summary>
        /// <remarks>
        ///   Required to be able to implement <see cref="ITypeResolver.Resolve"/> correctly for 
        ///   <see cref="IEnumerable{T}"/>.
        /// </remarks>
        private static readonly Type s_listType = typeof(List<>);

        /// <summary>
        /// Lookup for concrete <see cref="List{T}"/> types.
        /// </summary>
        /// <remarks>
        ///   Required to be able to implement <see cref="ITypeResolver.Resolve"/> correctly for 
        ///   <see cref="IEnumerable{T}"/>.
        /// </remarks>
        private static readonly ConcurrentDictionary<Type, (Type ListType, MethodInfo AddMethod)> s_listTypeCache = new ConcurrentDictionary<Type, (Type, MethodInfo)>();

        /// <summary>
        /// Service registrations made by the <see cref="CommandApp"/>.
        /// </summary>
        private readonly ConcurrentDictionary<Type, List<ServiceRegistration>> _services = new ConcurrentDictionary<Type, List<ServiceRegistration>>();

        /// <summary>
        /// The <see cref="IServiceProvider"/> that is used to resolve services that have not been 
        /// registered directly with the <see cref="ServiceProviderTypeRegistrar"/>.
        /// </summary>
        public IServiceProvider ServiceProvider { get; }


        /// <summary>
        /// Creates a new <see cref="ServiceProviderTypeRegistrar"/> instance.
        /// </summary>
        /// <param name="serviceProvider">
        ///   The <see cref="IServiceProvider"/> that is used to resolve services that have not 
        ///   been registered directly with the <see cref="ServiceProviderTypeRegistrar"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="serviceProvider"/> is <see langword="null"/>.
        /// </exception>
        public ServiceProviderTypeRegistrar(IServiceProvider serviceProvider) {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }


        /// <inheritdoc/>
        public ITypeResolver Build() {
            return this;
        }


        /// <inheritdoc/>
        public object? Resolve(Type? type) {
            if (type == null) {
                return null;
            }

            // The use of reflection in this method is a bit ugly, but it is required to be able to
            // detect when Spectre is requesting all instances of a service type instead of the
            // last-registered instance. Additionally, we need to return an actual IEnumerable<T>
            // when returning a collection of services instead of e.g. object[], as returning
            // object[] instead of a strongly-typed collection of the requested service type causes
            // the reference ITypeRegistrar tests in Spectre.Console.Testing to fail.

            var resolveAll = type.IsGenericType && type.IsInterface && type.GetGenericTypeDefinition() == s_ienumerableType;
            var typeToResolve = resolveAll
                ? type.GetGenericArguments()[0]
                : type;

            if (resolveAll) {
                var typeConfiguration = s_listTypeCache.GetOrAdd(typeToResolve, _ => {
                    var type = s_listType.MakeGenericType(typeToResolve);
                    return (type, type.GetMethod("Add")!);
                });
                var list = Activator.CreateInstance(typeConfiguration.ListType)!;

                if (_services.TryGetValue(typeToResolve, out var localRegistrations)) {
                    foreach (var registration in localRegistrations) {
                        typeConfiguration.AddMethod.Invoke(list, new[] { registration.GetValue(ServiceProvider) });
                    }
                }

                var containerRegistrations = ServiceProvider.GetServices(typeToResolve);
                foreach (var service in containerRegistrations) {
                    typeConfiguration.AddMethod.Invoke(list, new[] { service });
                }

                return list;
            }

            if (_services.TryGetValue(typeToResolve, out var registrations)) {
                return registrations.LastOrDefault().GetValue(ServiceProvider);
            }

            return ServiceProvider.GetService(typeToResolve);
        }


        /// <inheritdoc/>
        public void Register(Type service, Type implementation) {
            _services.GetOrAdd(service, _ => new List<ServiceRegistration>()).Add(new ServiceRegistration(ImplementationType: implementation));
        }


        /// <inheritdoc/>
        public void RegisterInstance(Type service, object implementation) {
            _services.GetOrAdd(service, _ => new List<ServiceRegistration>()).Add(new ServiceRegistration(ImplementationInstance: implementation));
        }


        /// <inheritdoc/>
        public void RegisterLazy(Type service, Func<object> factory) {
            _services.GetOrAdd(service, _ => new List<ServiceRegistration>()).Add(new ServiceRegistration(ImplementationFactory: factory));
        }


        /// <summary>
        /// Represents a service registered directly with a <see cref="ServiceProviderTypeRegistrar"/>.
        /// </summary>
        /// <param name="ImplementationType">
        ///   The implementation type.
        /// </param>
        /// <param name="ImplementationFactory">
        ///   The implementation factory.
        /// </param>
        /// <param name="ImplementationInstance">
        ///   The implementation instance.
        /// </param>
        private readonly record struct ServiceRegistration(Type? ImplementationType = null, Func<object>? ImplementationFactory = null, object? ImplementationInstance = null) {

            /// <summary>
            /// Gets the value from the service registration.
            /// </summary>
            /// <param name="serviceProvider">
            ///   The service provider to use to create the service instance when <see cref="ImplementationType"/> 
            ///   is configured.
            /// </param>
            /// <returns>
            ///   The service instance.
            /// </returns>
            public object? GetValue(IServiceProvider serviceProvider) {
                if (ImplementationType != null) {
                    return ActivatorUtilities.CreateInstance(serviceProvider, ImplementationType);
                }
                else if (ImplementationFactory != null) {
                    return ImplementationFactory();
                }
                else {
                    return ImplementationInstance;
                }
            }

        }

    }
}
