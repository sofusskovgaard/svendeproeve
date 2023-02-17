using System.Reflection;

namespace App.Infrastructure.Utilities;

public static class DiscoveryHelper
{
    public static Type[]? Discover<TInterface>(Assembly? assemblyToSearch = null)
    {
        return DiscoveryHelper.Discover(typeof(TInterface), assemblyToSearch);
    }

    public static Type[] Discover(Type type, Assembly? assemblyToSearch = null)
    {
        if (assemblyToSearch == null) assemblyToSearch = Assembly.GetExecutingAssembly();

        return assemblyToSearch.GetTypes()
            .Where(x => !x.IsAbstract && x.GetInterface(type.Name) != null)
            .ToArray();
    }

    public static Type[]? DiscoverWithBase<TInterface>(Assembly? assemblyToSearch = null)
    {
        return DiscoveryHelper.DiscoverWithBase(typeof(TInterface), assemblyToSearch);
    }

    public static Type[] DiscoverWithBase(Type type, Assembly? assemblyToSearch = null)
    {
        if (assemblyToSearch == null) assemblyToSearch = Assembly.GetExecutingAssembly();

        return assemblyToSearch.GetTypes().Where(x => !x.IsAbstract && x.BaseType == type).ToArray();
    }
}