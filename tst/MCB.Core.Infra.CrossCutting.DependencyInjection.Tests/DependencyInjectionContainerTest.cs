﻿using MCB.Core.Infra.CrossCutting.DependencyInjection.Abstractions.Enums;
using MCB.Core.Infra.CrossCutting.DependencyInjection.Abstractions.Interfaces;
using MCB.Core.Infra.CrossCutting.DependencyInjection.Tests.Services;
using MCB.Core.Infra.CrossCutting.DependencyInjection.Tests.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MCB.Core.Infra.CrossCutting.DependencyInjection.Tests;

public class DependencyInjectionContainerTest
{
    [Fact]
    public void DependencyInjectionContainer_Should_Resolve_Singleton_Services()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.Register(DependencyInjectionLifecycle.Singleton, typeof(IDummyService), typeof(DummyService));
                dependencyInjectionContainer.Register(DependencyInjectionLifecycle.Singleton, typeof(ISingletonService), typeof(SingletonService));
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var singletonServiceA = dependencyInjectionContainer.Resolve(typeof(ISingletonService));
        var singletonServiceB = dependencyInjectionContainer.Resolve(typeof(ISingletonService));

        // Assert
        Assert.Equal(singletonServiceA, singletonServiceB);
    }
    [Fact]
    public void DependencyInjectionContainer_Should_Resolve_Transient_Services()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.Register(DependencyInjectionLifecycle.Transient, typeof(IDummyService), typeof(DummyService));
                dependencyInjectionContainer.Register(DependencyInjectionLifecycle.Transient, typeof(ITransientService), typeof(TransientService));
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var transientServiceA = (ITransientService)dependencyInjectionContainer.Resolve(typeof(ITransientService));
        var transientServiceB = (ITransientService)dependencyInjectionContainer.Resolve(typeof(ITransientService));

        // Assert
        Assert.NotEqual(transientServiceA, transientServiceB);
        Assert.NotEqual(transientServiceA.Id, transientServiceB.Id);
    }
    [Fact]
    public void DependencyInjectionContainer_Should_Resolve_Scoped_Services()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.Register(DependencyInjectionLifecycle.Scoped, typeof(IDummyService), typeof(DummyService));
                dependencyInjectionContainer.Register(DependencyInjectionLifecycle.Scoped, typeof(IScopedService), typeof(ScopedService));
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var scopedServiceA = (IScopedService)dependencyInjectionContainer.Resolve(typeof(IScopedService));
        var scopedServiceB = (IScopedService)dependencyInjectionContainer.Resolve(typeof(IScopedService));
        dependencyInjectionContainer.CreateNewScope();
        var scopedServiceC = (IScopedService)dependencyInjectionContainer.Resolve(typeof(IScopedService));

        // Assert
        Assert.Equal(scopedServiceA, scopedServiceB);
        Assert.NotEqual(scopedServiceA, scopedServiceC);
        Assert.NotEqual(scopedServiceA.Id, scopedServiceC.Id);
    }
    [Fact]
    public void DependencyInjectionContainer_Should_Resolve_Singleton_Concrete_Services()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.Register(DependencyInjectionLifecycle.Singleton, typeof(IDummyService), typeof(DummyService));
                dependencyInjectionContainer.Register(DependencyInjectionLifecycle.Singleton, typeof(ConcreteService));
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var concreteServiceA = dependencyInjectionContainer.Resolve(typeof(ConcreteService));
        var concreteServiceB = dependencyInjectionContainer.Resolve(typeof(ConcreteService));

        // Assert
        Assert.Equal(concreteServiceA, concreteServiceB);
    }
    [Fact]
    public void DependencyInjectionContainer_Should_Resolve_Transient_Concrete_Services()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.Register(DependencyInjectionLifecycle.Transient, typeof(IDummyService), typeof(DummyService));
                dependencyInjectionContainer.Register(DependencyInjectionLifecycle.Transient, typeof(ConcreteService));
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var concreteServiceA = (ConcreteService)dependencyInjectionContainer.Resolve(typeof(ConcreteService));
        var concreteServiceB = (ConcreteService)dependencyInjectionContainer.Resolve(typeof(ConcreteService));

        // Assert
        Assert.NotEqual(concreteServiceA, concreteServiceB);
        Assert.NotEqual(concreteServiceA.Id, concreteServiceB.Id);
    }
    [Fact]
    public void DependencyInjectionContainer_Should_Resolve_Scoped_Concrete_Services()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.Register(DependencyInjectionLifecycle.Scoped, typeof(IDummyService), typeof(DummyService));
                dependencyInjectionContainer.Register(DependencyInjectionLifecycle.Scoped, typeof(ConcreteService));
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var concreteServiceA = (ConcreteService)dependencyInjectionContainer.Resolve(typeof(ConcreteService));
        var concreteServiceB = (ConcreteService)dependencyInjectionContainer.Resolve(typeof(ConcreteService));
        dependencyInjectionContainer.CreateNewScope();
        var concreteServiceC = (ConcreteService)dependencyInjectionContainer.Resolve(typeof(ConcreteService));

        // Assert
        Assert.Equal(concreteServiceA, concreteServiceB);
        Assert.NotEqual(concreteServiceA, concreteServiceC);
        Assert.NotEqual(concreteServiceA.Id, concreteServiceC.Id);
    }
    [Fact]
    public void DependencyInjectionContainer_Should_Unregister_Services()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.Unregister(typeof(IUnregisteredService));
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var unregisteredService = dependencyInjectionContainer.Resolve(typeof(IUnregisteredService));

        // Assert
        Assert.Null(unregisteredService);
    }

    [Fact]
    public void DependencyInjectionContainer_Should_Resolve_Singleton_Services_From_Generic()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.RegisterScoped<IDummyService, DummyService>();
                dependencyInjectionContainer.RegisterSingleton<ISingletonService, SingletonService>();
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var singletonServiceA = dependencyInjectionContainer.Resolve<ISingletonService>();
        var singletonServiceB = dependencyInjectionContainer.Resolve<ISingletonService>();

        // Assert
        Assert.Equal(singletonServiceA, singletonServiceB);
    }
    [Fact]
    public void DependencyInjectionContainer_Should_Resolve_Transient_Services_From_Generic()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.RegisterScoped<IDummyService, DummyService>();
                dependencyInjectionContainer.RegisterTransient<ITransientService, TransientService>();
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var transientServiceA = dependencyInjectionContainer.Resolve<ITransientService>();
        var transientServiceB = dependencyInjectionContainer.Resolve<ITransientService>();

        // Assert
        Assert.NotEqual(transientServiceA, transientServiceB);
        Assert.NotEqual(transientServiceA.Id, transientServiceB.Id);
    }
    [Fact]
    public void DependencyInjectionContainer_Should_Resolve_Scoped_Services_From_Generic()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.RegisterScoped<IDummyService, DummyService>();
                dependencyInjectionContainer.RegisterScoped<IScopedService, ScopedService>();
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var scopedServiceA = dependencyInjectionContainer.Resolve<IScopedService>();
        var scopedServiceB = dependencyInjectionContainer.Resolve<IScopedService>();
        dependencyInjectionContainer.CreateNewScope();
        var scopedServiceC = dependencyInjectionContainer.Resolve<IScopedService>();

        // Assert
        Assert.Equal(scopedServiceA, scopedServiceB);
        Assert.NotEqual(scopedServiceA, scopedServiceC);
        Assert.NotEqual(scopedServiceA.Id, scopedServiceC.Id);
    }
    [Fact]
    public void DependencyInjectionContainer_Should_Resolve_Singleton_Concrete_Services_From_Generic()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.RegisterScoped<IDummyService, DummyService>();
                dependencyInjectionContainer.RegisterSingleton<ConcreteService>();
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var singletonServiceA = dependencyInjectionContainer.Resolve<ConcreteService>();
        var singletonServiceB = dependencyInjectionContainer.Resolve<ConcreteService>();

        // Assert
        Assert.Equal(singletonServiceA, singletonServiceB);
    }
    [Fact]
    public void DependencyInjectionContainer_Should_Resolve_Transient_Concrete_Services_From_Generic()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.RegisterScoped<IDummyService, DummyService>();
                dependencyInjectionContainer.RegisterTransient<ConcreteService>();
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var concreteServiceA = dependencyInjectionContainer.Resolve<ConcreteService>();
        var concreteServiceB = dependencyInjectionContainer.Resolve<ConcreteService>();

        // Assert
        Assert.NotEqual(concreteServiceA, concreteServiceB);
        Assert.NotEqual(concreteServiceA.Id, concreteServiceB.Id);
    }
    [Fact]
    public void DependencyInjectionContainer_Should_Resolve_Scoped_Concrete_Services_From_Generic()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.RegisterScoped<IDummyService, DummyService>();
                dependencyInjectionContainer.RegisterScoped<ConcreteService>();
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var concreteServiceA = dependencyInjectionContainer.Resolve<ConcreteService>();
        var concreteServiceB = dependencyInjectionContainer.Resolve<ConcreteService>();
        dependencyInjectionContainer.CreateNewScope();
        var concreteServiceC = dependencyInjectionContainer.Resolve<ConcreteService>();

        // Assert
        Assert.Equal(concreteServiceA, concreteServiceB);
        Assert.NotEqual(concreteServiceA, concreteServiceC);
        Assert.NotEqual(concreteServiceA.Id, concreteServiceC.Id);
    }
    [Fact]
    public void DependencyInjectionContainer_Should_Unregister_Services_From_Generic()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.Unregister<IUnregisteredService>();
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var unregisteredService = dependencyInjectionContainer.Resolve<IUnregisteredService>();

        // Assert
        Assert.Null(unregisteredService);
    }

    [Fact]
    public void DependencyInjectionContainer_Should_Resolve_Singleton_Services_With_Factory()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.RegisterScoped<IDummyService, DummyService>();
                dependencyInjectionContainer.RegisterSingleton<ISingletonService>(dependencyInjectionContainer =>
                {
                    return new SingletonService(dependencyInjectionContainer.Resolve<IDummyService>());
                });
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var singletonServiceA = dependencyInjectionContainer.Resolve<ISingletonService>();
        var singletonServiceB = dependencyInjectionContainer.Resolve<ISingletonService>();

        // Assert
        Assert.Equal(singletonServiceA, singletonServiceB);
    }
    [Fact]
    public void DependencyInjectionContainer_Should_Resolve_Transient_Services_With_Factory()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.RegisterScoped<IDummyService, DummyService>();
                dependencyInjectionContainer.RegisterTransient<ITransientService>(dependencyInjectionContainer =>
                {
                    return new TransientService(dependencyInjectionContainer.Resolve<IDummyService>());
                });
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var transientServiceA = dependencyInjectionContainer.Resolve<ITransientService>();
        var transientServiceB = dependencyInjectionContainer.Resolve<ITransientService>();

        // Assert
        Assert.NotEqual(transientServiceA, transientServiceB);
        Assert.NotEqual(transientServiceA.Id, transientServiceB.Id);
    }
    [Fact]
    public void DependencyInjectionContainer_Should_Resolve_Scoped_Services_With_Factory()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.RegisterScoped<IDummyService, DummyService>();
                dependencyInjectionContainer.RegisterScoped<IScopedService>(dependencyInjectionContainer =>
                {
                    return new ScopedService(dependencyInjectionContainer.Resolve<IDummyService>());
                });
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var scopedServiceA = dependencyInjectionContainer.Resolve<IScopedService>();
        var scopedServiceB = dependencyInjectionContainer.Resolve<IScopedService>();
        dependencyInjectionContainer.CreateNewScope();
        var scopedServiceC = dependencyInjectionContainer.Resolve<IScopedService>();

        // Assert
        Assert.Equal(scopedServiceA, scopedServiceB);
        Assert.NotEqual(scopedServiceA, scopedServiceC);
        Assert.NotEqual(scopedServiceA.Id, scopedServiceC.Id);
    }
    [Fact]
    public void DependencyInjectionContainer_Should_Resolve_Singleton_Concrete_Services_With_Factory()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.RegisterScoped<IDummyService, DummyService>();
                dependencyInjectionContainer.RegisterSingleton(dependencyInjectionContainer =>
                {
                    return new ConcreteService(dependencyInjectionContainer.Resolve<IDummyService>());
                });
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var concreteServiceA = dependencyInjectionContainer.Resolve<ConcreteService>();
        var concreteServiceB = dependencyInjectionContainer.Resolve<ConcreteService>();

        // Assert
        Assert.Equal(concreteServiceA, concreteServiceB);
    }
    [Fact]
    public void DependencyInjectionContainer_Should_Resolve_Transient_Concrete_Services_With_Factory()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.RegisterScoped<IDummyService, DummyService>();
                dependencyInjectionContainer.RegisterTransient(dependencyInjectionContainer =>
                {
                    return new ConcreteService(dependencyInjectionContainer.Resolve<IDummyService>());
                });
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var concreteServiceA = dependencyInjectionContainer.Resolve<ConcreteService>();
        var concreteServiceB = dependencyInjectionContainer.Resolve<ConcreteService>();

        // Assert
        Assert.NotEqual(concreteServiceA, concreteServiceB);
        Assert.NotEqual(concreteServiceA.Id, concreteServiceB.Id);
    }
    [Fact]
    public void DependencyInjectionContainer_Should_Resolve_Scoped_Concrete_Services_With_Factory()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMcbDependencyInjection(
            configureServicesAction: dependencyInjectionContainer =>
            {
                dependencyInjectionContainer.RegisterScoped<IDummyService, DummyService>();
                dependencyInjectionContainer.RegisterScoped(dependencyInjectionContainer =>
                {
                    return new ConcreteService(dependencyInjectionContainer.Resolve<IDummyService>());
                });
            }
        );
        var serviceProvider = services.BuildServiceProvider();

        var dependencyInjectionContainer = serviceProvider.GetService<IDependencyInjectionContainer>();

        // Act
        var concreteServiceA = dependencyInjectionContainer.Resolve<ConcreteService>();
        var concreteServiceB = dependencyInjectionContainer.Resolve<ConcreteService>();
        dependencyInjectionContainer.CreateNewScope();
        var concreteServiceC = dependencyInjectionContainer.Resolve<ConcreteService>();

        // Assert
        Assert.Equal(concreteServiceA, concreteServiceB);
        Assert.NotEqual(concreteServiceA, concreteServiceC);
        Assert.NotEqual(concreteServiceA.Id, concreteServiceC.Id);
    }
}
