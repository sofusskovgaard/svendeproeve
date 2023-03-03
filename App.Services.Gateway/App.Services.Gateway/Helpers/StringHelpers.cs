﻿namespace App.Services.Gateway.Helpers;

public static class StringHelpers
{
    public static T ParseValueOrDefault<T>(string? stringValue, Func<string, T> parser, T defaultValue)
    {
        if (string.IsNullOrEmpty(stringValue))
        {
            return defaultValue;
        }

        return parser(stringValue);
    }
}