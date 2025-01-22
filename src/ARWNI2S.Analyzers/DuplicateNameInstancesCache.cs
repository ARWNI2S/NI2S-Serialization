using Microsoft.CodeAnalysis;
using System.Collections.Concurrent;

namespace ARWNI2S.Analyzers
{
    public static class DuplicateNameInstancesCache
    {
        private static readonly ConcurrentDictionary<string, Location> VerifiedNames = new(StringComparer.OrdinalIgnoreCase);

        public static bool AddName(string name, Location location, out Location existingLocation)
        {
            if (VerifiedNames.TryAdd(name, location))
            {
                existingLocation = null;
                return true; // Name added successfully
            }

            existingLocation = VerifiedNames[name];
            return false; // Duplicate detected
        }

        public static IEnumerable<KeyValuePair<string, Location>> GetAllNames() => VerifiedNames;
    }
}
