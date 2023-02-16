using System.Reflection;

namespace App.Infrastructure.Utilities;

public static class DiscoveryHelper
{
    public static Type[]? Discover<TInterface>(Assembly? assemblyToSearch)
    {
        return Discover(assemblyToSearch, typeof(TInterface));
    }
    
    public static Type[] Discover(Assembly? assemblyToSearch, Type type)
    {
        if (assemblyToSearch == null)
        {
            assemblyToSearch = Assembly.GetCallingAssembly();
        }

        return assemblyToSearch.GetTypes()
            .Where(x => !x.IsAbstract && x.GetInterface(type.Name) != null)
            .ToArray();
    }
}