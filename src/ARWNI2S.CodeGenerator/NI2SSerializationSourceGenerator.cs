using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using ARWNI2S.CodeGenerator.Diagnostics;

#pragma warning disable RS1035 // Do not use APIs banned for analyzers
namespace ARWNI2S.CodeGenerator
{
    [Generator]
    public class NI2SSerializationSourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            try
            {
                var processName = Process.GetCurrentProcess().ProcessName.ToLowerInvariant();
                if (processName.Contains("devenv") || processName.Contains("servicehub"))
                {
                    return;
                }

                if (!Debugger.IsAttached &&
                    context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.ni2s_designtimebuild", out var isDesignTimeBuild)
                    && string.Equals("true", isDesignTimeBuild, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                if (context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.ni2s_attachdebugger", out var attachDebuggerOption)
                    && string.Equals("true", attachDebuggerOption, StringComparison.OrdinalIgnoreCase))
                {
                    Debugger.Launch();
                }

                var options = new CodeGeneratorOptions();
                if (context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.ni2s_immutableattributes", out var immutableAttributes) && immutableAttributes is { Length: > 0 })
                {
                    options.ImmutableAttributes.AddRange(immutableAttributes.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList());
                }

                if (context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.ni2s_aliasattributes", out var aliasAttributes) && aliasAttributes is { Length: > 0 })
                {
                    options.AliasAttributes.AddRange(aliasAttributes.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList());
                }

                if (context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.ni2s_idattributes", out var idAttributes) && idAttributes is { Length: > 0 })
                {
                    options.IdAttributes.AddRange(idAttributes.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList());
                }

                if (context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.ni2s_generateserializerattributes", out var generateSerializerAttributes) && generateSerializerAttributes is { Length: > 0 })
                {
                    options.GenerateSerializerAttributes.AddRange(generateSerializerAttributes.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList());
                }

                if (context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.ni2s_generatefieldids", out var generateFieldIds) && generateFieldIds is { Length: > 0 })
                {
                    if (Enum.TryParse(generateFieldIds, out GenerateFieldIds fieldIdOption))
                    {
                        options.GenerateFieldIds = fieldIdOption;
                    }
                }

                if (context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.ni2sgeneratecompatibilityinvokers", out var generateCompatInvokersValue)
                    && bool.TryParse(generateCompatInvokersValue, out var genCompatInvokers))
                {
                    options.GenerateCompatibilityInvokers = genCompatInvokers;
                }

                var codeGenerator = new CodeGenerator(context.Compilation, options);
                var syntax = codeGenerator.GenerateCode(context.CancellationToken);
                var sourceString = syntax.NormalizeWhitespace().ToFullString();
                var sourceText = SourceText.From(sourceString, Encoding.UTF8);
                context.AddSource($"{context.Compilation.AssemblyName ?? "assembly"}.niis.g.cs", sourceText);
            }
            catch (Exception exception)
            {
                if (!HandleException(context, exception))
                {
                    throw;
                }
            }

            static bool HandleException(GeneratorExecutionContext context, Exception exception)
            {
                if (exception is NI2SGeneratorDiagnosticAnalysisException analysisException)
                {
                    context.ReportDiagnostic(analysisException.Diagnostic);
                    return true;
                }

                context.ReportDiagnostic(UnhandledCodeGenerationExceptionDiagnostic.CreateDiagnostic(exception));
                Console.WriteLine(exception);
                Console.WriteLine(exception.StackTrace);
                return false;
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
#pragma warning restore RS1035 // Do not use APIs banned for analyzers