using System;
using System.Collections.Generic;

namespace ServiceRadiusAdjuster;

public static class DictionaryExtensions
{
    public static Dictionary<string, float> CombineAndUpdate(this Dictionary<string, float> aPairs, Dictionary<string, float> bPairs)
    {
        if (bPairs is null)
            throw new ArgumentNullException(nameof(bPairs));

        var result = new Dictionary<string, float>(aPairs);
        foreach (string? key in bPairs.Keys)
        {
            result[key] = bPairs[key];
        }

        return result;
    }
}