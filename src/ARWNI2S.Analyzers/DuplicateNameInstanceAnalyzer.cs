using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace ARWNI2S.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class DuplicateNameInstanceAnalyzer : DiagnosticAnalyzer
    {
        public const string RuleId = "ARWNI2S0014";

        private static readonly DiagnosticDescriptor Rule = new(
           id: RuleId,
           category: "Usage",
           defaultSeverity: DiagnosticSeverity.Error,
           isEnabledByDefault: true,
           title: new LocalizableResourceString(nameof(Resources.DuplicateNameInstanceTitle), Resources.ResourceManager, typeof(Resources)),
           messageFormat: new LocalizableResourceString(nameof(Resources.DuplicateNameInstanceMessageFormat), Resources.ResourceManager, typeof(Resources)),
           description: new LocalizableResourceString(nameof(Resources.DuplicateNameInstanceTitleDescription), Resources.ResourceManager, typeof(Resources)));

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.RegisterSyntaxNodeAction(CheckSyntaxNode, SyntaxKind.ObjectCreationExpression);
        }

        //private void CheckSyntaxNode(SyntaxNodeAnalysisContext context)
        //{
        //    if (context.Node is not ObjectCreationExpressionSyntax objectCreation) return;

        //    // Check if the type is "Name"
        //    var typeSymbol = context.SemanticModel.GetSymbolInfo(objectCreation.Type).Symbol as INamedTypeSymbol;
        //    if (typeSymbol == null || !Constants.NameFullyQualifiedName.Equals(typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat), StringComparison.Ordinal))
        //        return;

        //    // Check if the first argument is a string literal
        //    if (objectCreation.ArgumentList?.Arguments.Count > 0 &&
        //        objectCreation.ArgumentList.Arguments[0].Expression is LiteralExpressionSyntax literal &&
        //        literal.IsKind(SyntaxKind.StringLiteralExpression))
        //    {
        //        var nameValue = literal.Token.ValueText;

        //        // Use an analysis-wide dictionary to track duplicates
        //        var duplicateTracker = context.Options.AnalyzerConfigOptionsProvider.GlobalOptions;
        //        var cacheKey = $"DuplicateNameInstanceAnalyzer:{nameValue.ToLowerInvariant()}";

        //        if (!duplicateTracker.TryGetValue(cacheKey, out _))
        //        {
        //            // Add the name to the tracker
        //            duplicateTracker.Add(cacheKey, "exists");
        //        }
        //        else
        //        {
        //            // Report the duplicate
        //            context.ReportDiagnostic(Diagnostic.Create(
        //                descriptor: Rule,
        //                location: literal.GetLocation(),
        //                messageArgs: [nameValue]));

        //        }
        //    }


        //}

        //private void CheckSyntaxNode(SyntaxNodeAnalysisContext context)
        //{
        //    // Static state for duplicate tracking (isolated per compilation)
        //    var seenNames = new HashSet<string>(context.Compilation.SyntaxTrees
        //        .SelectMany(tree => tree.GetRoot().DescendantNodes().OfType<ObjectCreationExpressionSyntax>())
        //        .Where(node => IsNameType(context, node))
        //        .Select(node => GetNameValue(node))
        //        .Where(value => value != null), StringComparer.OrdinalIgnoreCase);
        //        //.ToHashSet(System.StringComparer.OrdinalIgnoreCase);

        //    if (context.Node is not ObjectCreationExpressionSyntax objectCreation) return;

        //    // Check if it's a Name type and get the string value
        //    if (!IsNameType(context, objectCreation)) return;
        //    var nameValue = GetNameValue(objectCreation);

        //    if (nameValue == null) return;

        //    // Detect duplicates
        //    if (!seenNames.Add(nameValue))
        //    {
        //        context.ReportDiagnostic(Diagnostic.Create(
        //            descriptor: Rule,
        //            location: objectCreation.GetLocation(),
        //            messageArgs: [nameValue]));
        //    }
        //}
        private void CheckSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is not ObjectCreationExpressionSyntax objectCreation) return;

            // Verificar si es del tipo Name
            if (!IsNameType(context, objectCreation)) return;

            // Obtener el valor del nombre
            var nameValue = GetNameValue(objectCreation);
            if (nameValue == null) return;

            // Realizar un seguimiento de duplicados en el árbol de sintaxis actual
            var seenNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var root = context.Node.SyntaxTree.GetRoot();

            foreach (var node in root.DescendantNodes().OfType<ObjectCreationExpressionSyntax>())
            {
                if (!IsNameType(context, node)) continue;

                var existingNameValue = GetNameValue(node);
                if (existingNameValue == null) continue;

                if (!seenNames.Add(existingNameValue) && nameValue == existingNameValue)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        descriptor: Rule,
                        location: objectCreation.GetLocation(),
                        messageArgs: new[] { nameValue }));
                    return;
                }
            }
        }

        private static bool IsNameType(SyntaxNodeAnalysisContext context, ObjectCreationExpressionSyntax objectCreation)
        {
            var typeSymbol = context.SemanticModel.GetSymbolInfo(objectCreation.Type).Symbol as INamedTypeSymbol;
            return typeSymbol != null &&
                   Constants.NameFullyQualifiedName.Equals(
                       typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                       StringComparison.Ordinal);
        }

        private static string GetNameValue(ObjectCreationExpressionSyntax objectCreation)
        {
            return objectCreation.ArgumentList?.Arguments.FirstOrDefault()?.Expression is LiteralExpressionSyntax literal &&
                   literal.IsKind(SyntaxKind.StringLiteralExpression)
                ? literal.Token.ValueText
                : null;
        }
    }
}
