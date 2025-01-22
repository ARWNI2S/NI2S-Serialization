using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Diagnostics;

namespace ARWNI2S.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class DuplicateNameInstancesAnalyzer : DiagnosticAnalyzer
    {
        public const string RuleId = "ARWNI2S0014";

        private static readonly DiagnosticDescriptor Rule = new(
            id: RuleId,
            category: "Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            title: new LocalizableResourceString(nameof(Resources.DuplicateNameInstancesTitle), Resources.ResourceManager, typeof(Resources)),
            messageFormat: new LocalizableResourceString(nameof(Resources.DuplicateNameInstancesMessageFormat), Resources.ResourceManager, typeof(Resources)),
            description: new LocalizableResourceString(nameof(Resources.DuplicateNameInstancesTitleDescription), Resources.ResourceManager, typeof(Resources)));

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            
            context.RegisterCompilationStartAction(compilationContext =>
            {
                // Almacén local para nombres verificados en esta compilación
                var verifiedNames = new Dictionary<string, Location>(StringComparer.OrdinalIgnoreCase);

                var referencedSymbols = compilationContext.Compilation.References
                    .Select(reference => compilationContext.Compilation.GetAssemblyOrModuleSymbol(reference))
                    .OfType<IAssemblySymbol>() // Convertir a ensamblados
                    .Where(symbol => DependsOnAbstractions(symbol)); // Filtrar ensamblados relevante

                foreach (var assembly in referencedSymbols)
                {
                    foreach (var syntaxRef in assembly.DeclaringSyntaxReferences)
                    {
                        var syntax = syntaxRef.GetSyntax() as CompilationUnitSyntax;
                        if (syntax == null) continue;

                        var objectCreations = syntax.DescendantNodes()
                        .OfType<ObjectCreationExpressionSyntax>()
                        .Where(node =>
                        {
                            var typeSymbol = assembly.GetTypeByMetadataName(Constants.NameFullyQualifiedName);
                            return typeSymbol != null && node.Type.ToString() == typeSymbol.Name;
                        });


                        foreach (var objectCreation in objectCreations)
                        {
                            var nameValue = GetNameValue(objectCreation);
                            if (!string.IsNullOrEmpty(nameValue))
                            {
                                verifiedNames[nameValue] = objectCreation.GetLocation(); // Placeholder para referencias externas
                            }
                        }
                    }
                }

                // Registrar análisis de nodos
                compilationContext.RegisterSyntaxNodeAction(nodeContext =>
                {
                    CheckSyntaxNode(nodeContext, verifiedNames);
                }, SyntaxKind.ObjectCreationExpression);
            });
        }

        private void CheckSyntaxNode(SyntaxNodeAnalysisContext context, Dictionary<string, Location> verifiedNames)
        {
            if (context.Node is not ObjectCreationExpressionSyntax objectCreation) return;

            // Verificar si el nodo corresponde al tipo "Name"
            if (!IsNameType(context, objectCreation)) return;

            // Obtener el valor del nombre
            var nameValue = GetNameValue(objectCreation);
            if (string.IsNullOrWhiteSpace(nameValue)) nameValue = string.Empty;

            // Detectar duplicados en esta compilación
            if (verifiedNames.TryGetValue(nameValue, out var existingLocation))
            {
                // Reportar duplicados
                context.ReportDiagnostic(Diagnostic.Create(
                    descriptor: Rule,
                    location: objectCreation.GetLocation(),
                    messageArgs: new[] { nameValue }));

                context.ReportDiagnostic(Diagnostic.Create(
                    descriptor: Rule,
                    location: existingLocation,
                    messageArgs: new[] { nameValue }));
            }
            else
            {
                verifiedNames[nameValue] = objectCreation.GetLocation();
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

        // Método auxiliar para verificar dependencias
        private static bool DependsOnAbstractions(IAssemblySymbol assemblySymbol)
        {
            return assemblySymbol.Modules
                .SelectMany(module => module.ReferencedAssemblySymbols)
                .Any(referenced => Constants.AbstractionsAssemblyName.Equals(referenced.Name, StringComparison.InvariantCultureIgnoreCase));
        }
    }

    //[DiagnosticAnalyzer(LanguageNames.CSharp)]
    //public class DuplicateNameInstancesAnalyzer : DiagnosticAnalyzer
    //{
    //    public const string RuleId = "ARWNI2S0014";

    //    private static readonly DiagnosticDescriptor Rule = new(
    //        id: RuleId,
    //        category: "Usage",
    //        defaultSeverity: DiagnosticSeverity.Error,
    //        isEnabledByDefault: true,
    //        title: new LocalizableResourceString(nameof(Resources.DuplicateNameInstancesTitle), Resources.ResourceManager, typeof(Resources)),
    //        messageFormat: new LocalizableResourceString(nameof(Resources.DuplicateNameInstancesMessageFormat), Resources.ResourceManager, typeof(Resources)),
    //        description: new LocalizableResourceString(nameof(Resources.DuplicateNameInstancesTitleDescription), Resources.ResourceManager, typeof(Resources)));

    //    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

    //    public override void Initialize(AnalysisContext context)
    //    {
    //        context.EnableConcurrentExecution();
    //        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);

    //        context.RegisterCompilationStartAction(compilationContext =>
    //        {
    //            // Almacén local para nombres verificados en esta compilación
    //            var verifiedNames = new Dictionary<string, Location>(StringComparer.OrdinalIgnoreCase);

    //            compilationContext.RegisterSyntaxNodeAction(nodeContext =>
    //            {
    //                if (nodeContext.Node is ObjectCreationExpressionSyntax creation &&
    //                    IsNameType(nodeContext, creation))
    //                {
    //                    CheckSyntaxNode(nodeContext, verifiedNames);
    //                }
    //            }, SyntaxKind.ObjectCreationExpression);

    //            // Registrar análisis de nodos
    //            compilationContext.RegisterSyntaxNodeAction(nodeContext =>
    //            {
    //                CheckSyntaxNode(nodeContext, verifiedNames);
    //            }, SyntaxKind.ObjectCreationExpression);
    //        });
    //    }

    //    private void CheckSyntaxNode(SyntaxNodeAnalysisContext context, Dictionary<string, Location> verifiedNames)
    //    {
    //        if (context.Node is not ObjectCreationExpressionSyntax objectCreation) return;

    //        // Verificar si el nodo corresponde al tipo "Name"
    //        if (!IsNameType(context, objectCreation)) return;

    //        // Obtener el valor del nombre
    //        var nameValue = GetNameValue(objectCreation);
    //        if (string.IsNullOrWhiteSpace(nameValue)) nameValue = string.Empty;

    //        // Detectar duplicados en esta compilación
    //        if (verifiedNames.TryGetValue(nameValue, out var existingLocation) && !existingLocation.Equals(objectCreation.GetLocation()))
    //        {
    //            // Reportar duplicados
    //            context.ReportDiagnostic(Diagnostic.Create(
    //                descriptor: Rule,
    //                location: objectCreation.GetLocation(),
    //                messageArgs: new[] { nameValue }));

    //            context.ReportDiagnostic(Diagnostic.Create(
    //                descriptor: Rule,
    //                location: existingLocation,
    //                messageArgs: new[] { nameValue }));
    //        }
    //        else
    //        {
    //            verifiedNames[nameValue] = objectCreation.GetLocation();
    //        }
    //    }

    //    private static bool IsNameType(SyntaxNodeAnalysisContext context, ObjectCreationExpressionSyntax objectCreation)
    //    {
    //        return context.SemanticModel.GetSymbolInfo(objectCreation.Type).Symbol is INamedTypeSymbol typeSymbol &&
    //               Constants.NameFullyQualifiedName.Equals(
    //                   typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
    //                   StringComparison.Ordinal);
    //    }

    //    private static string GetNameValue(ObjectCreationExpressionSyntax objectCreation)
    //    {
    //        return objectCreation.ArgumentList?.Arguments.FirstOrDefault()?.Expression is LiteralExpressionSyntax literal &&
    //               literal.IsKind(SyntaxKind.StringLiteralExpression)
    //            ? literal.Token.ValueText
    //            : null;
    //    }
    //}
}