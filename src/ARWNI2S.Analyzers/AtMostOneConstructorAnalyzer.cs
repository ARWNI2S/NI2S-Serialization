using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace ARWNI2S.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AtMostOneConstructorAnalyzer : DiagnosticAnalyzer
    {
        public const string RuleId = "ARWNI2S0007";
        private const string Category = "Usage";
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AtMostOneConstructorTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AtMostOneConstructorMessageFormat), Resources.ResourceManager, typeof(Resources));

        internal static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(RuleId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze);
            context.RegisterSyntaxNodeAction(CheckSyntaxNode, SyntaxKind.ClassDeclaration, SyntaxKind.StructDeclaration);
        }

        private void CheckSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is TypeDeclarationSyntax declaration && SerializationAttributesHelper.ShouldGenerateSerializer(declaration))
            {
                var analysis = SerializationAttributesHelper.AnalyzeTypeDeclaration(declaration);
                if (analysis.AnnotatedConstructorCount > 1)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Rule, declaration.GetLocation()));
                }
            }
        }
    }
}
