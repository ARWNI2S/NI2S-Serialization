using Microsoft.CodeAnalysis;

namespace ARWNI2S.CodeGenerator.Diagnostics
{
    public static class UnhandledCodeGenerationExceptionDiagnostic
    {
        public const string RuleId = "ARWNI2S0103"; 
        private const string Category = "Usage";
        private static readonly LocalizableString Title = "An unhandled source generation exception occurred";
        private static readonly LocalizableString MessageFormat = "An unhandled exception occurred while generating source for your project: {0} {1}";
        private static readonly LocalizableString Description = "Please report this bug by opening an issue https://github.com/ARWNI2S/NI2S-Serialization/issues/new.";

        internal static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(RuleId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

        internal static Diagnostic CreateDiagnostic(Exception exception) => Diagnostic.Create(Rule, location: null, messageArgs: new[] { exception.ToString(), exception.StackTrace });
    }
}