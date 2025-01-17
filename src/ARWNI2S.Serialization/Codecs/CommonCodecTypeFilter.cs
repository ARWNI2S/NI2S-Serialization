using ARWNI2S.Metadata;
using System.CodeDom.Compiler;
using System.Reflection;

namespace ARWNI2S.Serialization.Codecs
{
    /// <summary>
    /// Defines common type filtering operations.
    /// </summary>
    public class CommonCodecTypeFilter
    {
        /// <summary>
        /// Returns true if the provided type is a framework or abstract type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns><see langword="true"/> if the type is a framework or abstract type, otherwise <see langword="false"/>.</returns>
        public static bool IsAbstractOrFrameworkType(Type type)
        {
            if (type.IsAbstract
                || type.GetCustomAttributes<GeneratedCodeAttribute>().Any(a => a.Tool.Equals("NI2SCodeGen"))
                || type.Assembly.GetCustomAttribute<FrameworkPartAttribute>() is not null)
            {
                return true;
            }

            return false;
        }
    }
}
