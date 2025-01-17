using Microsoft.CodeAnalysis;

namespace ARWNI2S.CodeGenerator
{
    internal interface ICopierDescription
    {
        ITypeSymbol UnderlyingType { get; }
    }
}