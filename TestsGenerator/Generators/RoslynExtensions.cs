using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestsGenerator.Generators
{
    public static class RoslynExtensions
    {
        public static IEnumerable<T> Get<T>(this SyntaxNode root)
        {
            return root.DescendantNodes().OfType<T>().ToList();
        }

        public static IEnumerable<MethodDeclarationSyntax> GetPublicMethods(this SyntaxNode root)
        {
            return root.Get<MethodDeclarationSyntax>().Where(x => x.Modifiers.Any(t => t.ValueText == "public"));
        }

        public static string Name(this ClassDeclarationSyntax clazz)
        {
            return clazz.Identifier.ValueText;
        }

        public static string Name(this MethodDeclarationSyntax method)
        {
            return method.Identifier.ValueText;
        }

        public static string NamespaceName(this ClassDeclarationSyntax clazz)
        {
            return (clazz.Parent as NamespaceDeclarationSyntax)?.Name?.ToString();
        }
        
    }
}