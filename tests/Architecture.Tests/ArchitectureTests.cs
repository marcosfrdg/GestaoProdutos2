using FluentAssertions;
using NetArchTest.Rules;
using Xunit;

namespace Architecture.Tests
{
    public class ArchitectureTests
    {
        private const string DomainNamespace = "Domain";
        private const string ApplicationNamespace = "Application";
        private const string InfrastructureNamespace = "Infrastructure";
        private const string PresentationNamespace = "Presentation";
        private const string WebApiNamespace = "WebAPI";

        [Fact]
        public void Domain_NaoDeveTerDependenciaComOutrosProjetos()
        {
            // Arrange
            var assembly = typeof(Domain.IAssemblyReference).Assembly;

            var outrosProjetos = new[]
            {
                ApplicationNamespace,
                InfrastructureNamespace,
                PresentationNamespace,
                WebApiNamespace
            };

            // Act
            var resultadoDoTeste = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(outrosProjetos)
                .GetResult();

            // Assert
            resultadoDoTeste.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Application_NaoDeveTerDependenciaComOutrosProjetos()
        {
            // Arrange
            var assembly = typeof(Application.IAssemblyReference).Assembly;

            var outrosProjetos = new[]
            {
                InfrastructureNamespace,
                PresentationNamespace,
                WebApiNamespace
            };

            // Act
            var resultadoDoTeste = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(outrosProjetos)
                .GetResult();

            // Assert
            resultadoDoTeste.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Handlers_DevemTerDependenciaComDomain()
        {
            // Arrange
            var assembly = typeof(Application.IAssemblyReference).Assembly;

            // Act
            var resultadoDoTeste = Types
                .InAssembly(assembly)
                .That()
                .HaveNameEndingWith("Handler")
                .Should()
                .HaveDependencyOn(DomainNamespace)
                .GetResult();

            // Assert
            resultadoDoTeste.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Infrastructure_NaoDeveTerDependenciaComOutrosProjetos()
        {
            // Arrange
            var assembly = typeof(Infrastructure.IAssemblyReference).Assembly;

            var outrosProjetos = new[]
            {
                PresentationNamespace,
                WebApiNamespace
            };

            // Act
            var resultadoDoTeste = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(outrosProjetos)
                .GetResult();

            // Assert
            resultadoDoTeste.IsSuccessful.Should().BeTrue();
        }


        [Fact]
        public void Controllers_DeveTerDependenciaComMediator()
        {
            // Arrange
            var assembly = typeof(WebApi.IAssemblyReference).Assembly;

            // Act
            var resultadoDoTeste = Types
                .InAssembly(assembly)
                .That()
                .HaveNameEndingWith("Controller")
                .Should()
                .HaveDependencyOn("MediatR")
                .GetResult();

            // Assert
            resultadoDoTeste.IsSuccessful.Should().BeTrue();
        }
    }
}
