using Microsoft.AspNetCore.Components;

namespace App.Web.Extensions;

public static class NavigationManagerExtensions
{
    public static string Path(this NavigationManager manager)
    {
        return manager.Uri.Substring(manager.BaseUri.Length - 1);
    }
}