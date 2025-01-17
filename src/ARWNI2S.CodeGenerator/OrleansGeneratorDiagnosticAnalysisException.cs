using Microsoft.CodeAnalysis;

namespace ARWNI2S.CodeGenerator
{
    public class NI2SGeneratorDiagnosticAnalysisException : Exception
    {
        public NI2SGeneratorDiagnosticAnalysisException(Diagnostic diagnostic) : base(diagnostic.GetMessage())
        {
            Diagnostic = diagnostic;
        }

        public Diagnostic Diagnostic { get; }
    }
}
