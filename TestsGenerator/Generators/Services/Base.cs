using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestsGenerator.Extensions;

namespace TestsGenerator.Generators.Services
{
    public abstract class Base
    {
        public const string TEST_FILE_EXTENSION = "cs";
        public const string DEFAULT_BASE_NAMESPACE = "TestProject";

        protected string _baseNamespace;

        protected SyntaxTree _tree;

        protected Base(string sourceCodeText, string baseNamespace = DEFAULT_BASE_NAMESPACE)
        {
            _tree = CSharpSyntaxTree.ParseText(sourceCodeText);

            _baseNamespace = baseNamespace;
        }

        public GeneratedFileDescription[] GenerateTestFiles()
        {
            return getClasses().Select(generateFileDescription).ToArray();
        }

        protected GeneratedFileDescription generateFileDescription(ClassDeclarationSyntax clazz)
        {
            return new GeneratedFileDescription(getTestFileName(clazz), generateTestClass(clazz));
        }

        protected IEnumerable<ClassDeclarationSyntax> getClasses()
        {
            // TODO: .GetRootAsync
            SyntaxNode root = _tree.GetRoot();

            return root.Get<ClassDeclarationSyntax>();
        }

        protected abstract string generateTestClass(ClassDeclarationSyntax clazz);

        protected string getTestNamespaceName(string originalNamespaceName)
        {
            string origNamespaceWithoutRoot = originalNamespaceName.Substring(originalNamespaceName.IndexOf('.') + 1);
            
            return string.Join('.', new[] {_baseNamespace, origNamespaceWithoutRoot}.Compact());
        }

        protected string getTestClassName(string originalClassName)
        {
            return $"{originalClassName}Tests";
        }

        protected string getTestMethodName(string originalMethodName)
        {
            return $"{originalMethodName}Test";
        }

        protected string getTestFileName(ClassDeclarationSyntax clazz)
        {
            var nameParts = new[]
            {
                getTestNamespaceName(clazz.NamespaceName()),
                getTestClassName(clazz.Name()),
                TEST_FILE_EXTENSION
            }.Compact();
            
            return string.Join('.', nameParts);
        }
    }
}