# One more implementation of Dependency Injection pattern 

Current implementation provides basic functionality for DI. Currently supports Transient and Singlton object lifecycles.
You can create a new scope by calling NewScope() method. When scope disposed all resolved objects within its context will be disposed automatically.

### Supported scopes
- Transient
- Singleton

### Exmple of usage 
```csharp
ContainerBuilder cb = new ContainerBuilder();
cb.Register<FirstService>();

using (var c1 = cb.Build())
{
    var fs1 = c1.Resolve<FirstService>();

    using (var c2 = c1.NewScope())
    {
        var fs2 = c2.Resolve<FirstService>(); //new instance will be created for new Scope

        Assert.NotEqual(fs1, fs2);
    }
}
```
