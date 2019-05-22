using System;
using System.Collections.Generic;

namespace ServiceRadiusAdjuster
{
    public static class DictionaryExtensions
    {
        public static Dictionary<string, float> CombineAndUpdate(this Dictionary<string, float> aPairs, Dictionary<string, float> bPairs)
        {
            if (bPairs == null) throw new ArgumentNullException(nameof(bPairs));

            var result = new Dictionary<string, float>(aPairs);
            foreach (var key in bPairs.Keys)
            {
                if (result.ContainsKey(key))
                {
                    result[key] = bPairs[key];
                }
                else
                {
                    result.Add(key, bPairs[key]);
                }
            }

            return result;
        }
    }
}
