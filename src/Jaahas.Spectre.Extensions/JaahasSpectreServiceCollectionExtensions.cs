using System;

using Jaahas.Spectre.Extensions;

using Microsoft.Extensions.DependencyInjection.Extensions;

using Spectre.Console.Cli;

namespace Microsoft.Extensions.DependencyInjection {

    /// <summary>
    /// Spectre.Console.Cli extensions for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class JaahasSpectreServiceCollectionExtensions {

        /// <summary>
        /// Adds a <see cref="CommandApp"/> to the service collection.
        /// </summary>
        /// <param name="services">
        ///   The service collection.
        /// </param>
        /// <param name="configure">
        ///   A delegate that configures the <see cref="CommandApp"/>.
        /// </param>
        /// <param name="lifetime">
        ///   The lifetime of the service.
        /// </param>
        /// <returns>
        ///   The service collection.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="services"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="configure"/> is <see langword="null"/>.
        /// </exception>
        public static IServiceCollection AddSpectreCommandApp(this IServiceCollection services, Action<IConfigurator> configure, ServiceLifetime lifetime = ServiceLifetime.Scoped) {
            if (services == null) {
                throw new ArgumentNullException(nameof(services));
            }
            if (configure == null) {
                throw new ArgumentNullException(nameof(configure));
            }

            services.TryAdd(new ServiceDescriptor(typeof(CommandApp), provider => {
                var commandApp = new CommandApp(ActivatorUtilities.CreateInstance<ServiceProviderTypeRegistrar>(provider));
                commandApp.Configure(configure);
                return commandApp;
            }, lifetime));

            return services;
        }


        /// <summary>
        /// Adds a <see cref="CommandApp{TDefaultCommand}"/> to the service collection.
        /// </summary>
        /// <typeparam name="TDefaultCommand">
        ///   The type of the default command.
        /// </typeparam>
        /// <param name="services">
        ///   The service collection.
        /// </param>
        /// <param name="configure">
        ///   A delegate that configures the <see cref="CommandApp"/>.
        /// </param>
        /// <param name="lifetime">
        ///   The lifetime of the service.
        /// </param>
        /// <returns>
        ///   The service collection.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="services"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="configure"/> is <see langword="null"/>.
        /// </exception>
        public static IServiceCollection AddSpectreCommandApp<TDefaultCommand>(this IServiceCollection services, Action<IConfigurator> configure, ServiceLifetime lifetime = ServiceLifetime.Scoped) where TDefaultCommand : class, ICommand {
            if (services == null) {
                throw new ArgumentNullException(nameof(services));
            }
            if (configure == null) {
                throw new ArgumentNullException(nameof(configure));
            }

            services.TryAdd(new ServiceDescriptor(typeof(CommandApp<TDefaultCommand>), provider => {
                var commandApp = new CommandApp<TDefaultCommand>(ActivatorUtilities.CreateInstance<ServiceProviderTypeRegistrar>(provider));
                commandApp.Configure(configure);
                return commandApp;
            }, lifetime));

            return services;
        }


        /// <summary>
        /// Adds a <see cref="CommandApp"/> to the service collection.
        /// </summary>
        /// <param name="services">
        ///   The service collection.
        /// </param>
        /// <param name="configure">
        ///   A delegate that configures the <see cref="CommandApp"/>.
        /// </param>
        /// <param name="lifetime">
        ///   The lifetime of the service.
        /// </param>
        /// <returns>
        ///   The service collection.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="services"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="configure"/> is <see langword="null"/>.
        /// </exception>
        public static IServiceCollection AddSpectreCommandApp(this IServiceCollection services, Action<IServiceProvider, IConfigurator> configure, ServiceLifetime lifetime = ServiceLifetime.Scoped) {
            if (services == null) {
                throw new ArgumentNullException(nameof(services));
            }
            if (configure == null) {
                throw new ArgumentNullException(nameof(configure));
            }

            services.TryAdd(new ServiceDescriptor(typeof(CommandApp), provider => {
                var commandApp = new CommandApp(ActivatorUtilities.CreateInstance<ServiceProviderTypeRegistrar>(provider));
                commandApp.Configure(conf => configure.Invoke(provider, conf));
                return commandApp;
            }, lifetime));

            return services;
        }


        /// <summary>
        /// Adds a <see cref="CommandApp{TDefaultCommand}"/> to the service collection.
        /// </summary>
        /// <typeparam name="TDefaultCommand">
        ///   The type of the default command.
        /// </typeparam>
        /// <param name="services">
        ///   The service collection.
        /// </param>
        /// <param name="configure">
        ///   A delegate that configures the <see cref="CommandApp"/>.
        /// </param>
        /// <param name="lifetime">
        ///   The lifetime of the service.
        /// </param>
        /// <returns>
        ///   The service collection.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="services"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="configure"/> is <see langword="null"/>.
        /// </exception>
        public static IServiceCollection AddSpectreCommandApp<TDefaultCommand>(this IServiceCollection services, Action<IServiceProvider, IConfigurator> configure, ServiceLifetime lifetime = ServiceLifetime.Scoped) where TDefaultCommand : class, ICommand {
            if (services == null) {
                throw new ArgumentNullException(nameof(services));
            }
            if (configure == null) {
                throw new ArgumentNullException(nameof(configure));
            }

            services.TryAdd(new ServiceDescriptor(typeof(CommandApp<TDefaultCommand>), provider => {
                var commandApp = new CommandApp<TDefaultCommand>(ActivatorUtilities.CreateInstance<ServiceProviderTypeRegistrar>(provider));
                commandApp.Configure(conf => configure.Invoke(provider, conf));
                return commandApp;
            }, lifetime));

            return services;
        }

    }

}
