using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestsGenerator.Generators;
using Xunit;
using SyntaxNode = Microsoft.CodeAnalysis.SyntaxNode;

namespace TestsGeneratorTests.Generators
{
    public class XUnitGeneratorTests
    {
        [Fact]
        public void GenerateTestFilesTest()
        {
            GeneratedFileDescription[] generatedFiles = new XUnitGenerator().GenerateTestFiles(SOURCE_CODE);
            
            Assert.True(generatedFiles.Length == 2);
            
            AssertGeneratedFileDesc(
                fileDesc: generatedFiles[0],
                fileName: "TestProject.Namespace2.Class1Tests.cs",
                namespaceName: "TestProject.Namespace2",
                className: "Class1Tests",
                methodNames: new[] { "Method1_1Test", "Method1_2Test" }
            );
            AssertGeneratedFileDesc(
                fileDesc: generatedFiles[1],
                fileName: "TestProject.Namespace2.Class2Tests.cs",
                namespaceName: "TestProject.Namespace2",
                className: "Class2Tests",
                methodNames: new[] { "Method2_1Test" }
            );
        }

        private void AssertGeneratedFileDesc(GeneratedFileDescription fileDesc, string fileName,
            string namespaceName, string className, string[] methodNames)
        {
            Assert.True(fileDesc.Name == fileName);
            
            SyntaxNode root = CSharpSyntaxTree.ParseText(fileDesc.Text).GetRoot();
            
            ClassDeclarationSyntax[] classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>().ToArray();
            
            Assert.True(classes.Length == 1);

            ClassDeclarationSyntax clazz = classes[0];
            
            Assert.True(clazz.Identifier.ValueText == className);
            Assert.True((clazz.Parent as NamespaceDeclarationSyntax)?.Name?.ToString() == namespaceName);

            MethodDeclarationSyntax[] methods = clazz.DescendantNodes().OfType<MethodDeclarationSyntax>().ToArray();
            
            Assert.True(methods.Length == methodNames.Length);
            
            foreach (MethodDeclarationSyntax method in methods)
            {
                Assert.Contains(method.Identifier.ValueText, methodNames);
                
                Assert.True(method.AttributeLists.Count == 1);
                Assert.True(method.AttributeLists.First().Attributes.First().Name.ToString() == "Fact");
            }
        }

        private const string SOURCE_CODE = @"
            namespace Namespace1.Namespace2
            {
                public class Class1
                {
                    public void Method1_1()
                    {
                        
                    }

                    public void Method1_2()
                    {
                        
                    }
                }

                public class Class2
                {
                    public int Method2_1()
                    {
                        return 1;
                    }

                    private string PrivateMethod(int a)
                    {
                        return a.ToString();
                    }
                }
            }
        ";
    }
}