﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ARWNI2S.Analyzers {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ARWNI2S.Analyzers.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The member &quot;{0}&quot; is marked as {1} and therefore cannot be serialized.
        /// </summary>
        internal static string AbstractOrStaticMembersCannotBeSerializedMessageFormat {
            get {
                return ResourceManager.GetString("AbstractOrStaticMembersCannotBeSerializedMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Members which are static or abstract cannot be serialized.
        /// </summary>
        internal static string AbstractOrStaticMembersCannotBeSerializedTitle {
            get {
                return ResourceManager.GetString("AbstractOrStaticMembersCannotBeSerializedTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Add [Alias] to specify well-known names that can be used to identify types or methods..
        /// </summary>
        internal static string AddAliasAttributesDescription {
            get {
                return ResourceManager.GetString("AddAliasAttributesDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Add missing [Alias].
        /// </summary>
        internal static string AddAliasAttributesTitle {
            get {
                return ResourceManager.GetString("AddAliasAttributesTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Add missing [Alias].
        /// </summary>
        internal static string AddAliasMessageFormat {
            get {
                return ResourceManager.GetString("AddAliasMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Add the [GenerateSerializer] attribute to serializable types in your application..
        /// </summary>
        internal static string AddGenerateSerializerAttributeDescription {
            get {
                return ResourceManager.GetString("AddGenerateSerializerAttributeDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The type &quot;{0}&quot; has the [Serializable] attribute but not the [GenerateSerializer] attribute.
        /// </summary>
        internal static string AddGenerateSerializerAttributeMessageFormat {
            get {
                return ResourceManager.GetString("AddGenerateSerializerAttributeMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Add the [GenerateSerializer] attribute.
        /// </summary>
        internal static string AddGenerateSerializerAttributesTitle {
            get {
                return ResourceManager.GetString("AddGenerateSerializerAttributesTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Add attributes to properties and fields to direct serializer generation..
        /// </summary>
        internal static string AddSerializationAttributesDescription {
            get {
                return ResourceManager.GetString("AddSerializationAttributesDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Add missing serialization attributes.
        /// </summary>
        internal static string AddSerializationAttributesMessageFormat {
            get {
                return ResourceManager.GetString("AddSerializationAttributesMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Add missing serialization attributes.
        /// </summary>
        internal static string AddSerializationAttributesTitle {
            get {
                return ResourceManager.GetString("AddSerializationAttributesTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The [Alias] attribute must be unique to the declaring type..
        /// </summary>
        internal static string AliasClashDetectedDescription {
            get {
                return ResourceManager.GetString("AliasClashDetectedDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Rename duplicated [Alias].
        /// </summary>
        internal static string AliasClashDetectedMessageFormat {
            get {
                return ResourceManager.GetString("AliasClashDetectedMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Rename duplicated [Alias].
        /// </summary>
        internal static string AliasClashDetectedTitle {
            get {
                return ResourceManager.GetString("AliasClashDetectedTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a A single type is not allowed to have multiple constructors annotated with the [NI2SConstructor] attribute.
        /// </summary>
        internal static string AtMostOneConstructorMessageFormat {
            get {
                return ResourceManager.GetString("AtMostOneConstructorMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a At most one constructor can be annotated with the [NI2SConstructor] attribute.
        /// </summary>
        internal static string AtMostOneConstructorTitle {
            get {
                return ResourceManager.GetString("AtMostOneConstructorTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a The [Id] attribute must be unique to each members of the declaring type..
        /// </summary>
        internal static string IdClashDetectedDescription {
            get {
                return ResourceManager.GetString("IdClashDetectedDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Change duplicated [Id].
        /// </summary>
        internal static string IdClashDetectedMessageFormat {
            get {
                return ResourceManager.GetString("IdClashDetectedMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Change duplicated [Id].
        /// </summary>
        internal static string IdClashDetectedTitle {
            get {
                return ResourceManager.GetString("IdClashDetectedTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Remove attribute [{0}].
        /// </summary>
        internal static string IncorrectAttributeUseMessageFormat {
            get {
                return ResourceManager.GetString("IncorrectAttributeUseMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Remove attribute.
        /// </summary>
        internal static string IncorrectAttributeUseTitle {
            get {
                return ResourceManager.GetString("IncorrectAttributeUseTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a This attribute should not be used on actor implementations..
        /// </summary>
        internal static string IncorrectAttributeUseTitleDescription {
            get {
                return ResourceManager.GetString("IncorrectAttributeUseTitleDescription", resourceCulture);
            }
        }
    }
}
