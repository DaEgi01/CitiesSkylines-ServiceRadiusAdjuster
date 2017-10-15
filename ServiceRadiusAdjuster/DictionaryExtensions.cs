using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceRadiusAdjuster
{
    public static class DictionaryExtensions
    {
        public static Dictionary<string, float> CombineAndUpdate(this Dictionary<string, float> aPairs, Dictionary<string, float> bPairs)
        {
            if (bPairs == null) throw new ArgumentNullException(nameof(bPairs));

            var result = new Dictionary<string, float>();
            foreach (var pair in aPairs)
            {
                result.Add(pair.Key, pair.Value);
            }

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
