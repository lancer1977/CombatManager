using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace TestProject1
{
    [TestFixture]
    public class ApiCoreDependencyBoundaryTests
    {
        private static readonly XName ProjectReferenceName = "ProjectReference";
        private static readonly XName PackageReferenceName = "PackageReference";
        private static readonly XName ReferenceName = "Reference";
        private static readonly XName HintPathName = "HintPath";

        [Test]
        public void CombatManagerApi_uses_the_local_api_core_project_reference()
        {
            var project = LoadProject("CombatManager.Api", "CombatManager.Api.csproj");

            AssertHasProjectReference(project, @"..\CombatManager.Api.Core\CombatManager.Api.Core.csproj");
            Assert.That(FindPackageReferences(project, "PolyhydraGames.CombatManager.Api.Core"), Is.Empty,
                "CombatManager.Api should not depend on the deprecated package form of CombatManager.Api.Core.");
        }

        [Test]
        public void CombatManagerCore_uses_the_local_api_core_project_reference_instead_of_the_nuget_package()
        {
            var project = LoadProject("CombatManagerCore", "CombatManagerCore.csproj");

            AssertHasProjectReference(project, @"..\CombatManager.Api.Core\CombatManager.Api.Core.csproj");
            Assert.That(FindPackageReferences(project, "PolyhydraGames.CombatManager.Api.Core"), Is.Empty,
                "CombatManagerCore should stop restoring the deprecated Api.Core package once the local project reference exists.");
        }

        [Test]
        public void CombatManagerCoreDroid_uses_the_local_api_core_project_reference_instead_of_a_package_hintpath()
        {
            var project = LoadProject("CombatManagerCore", "CombatManagerCoreDroid.csproj");

            AssertHasProjectReference(project, @"..\CombatManager.Api.Core\CombatManager.Api.Core.csproj");
            Assert.That(FindDeprecatedApiCoreHintPaths(project), Is.Empty,
                "CombatManagerCoreDroid should not point at the stale Api.Core package DLL once the local project reference is available.");
        }

        private static XDocument LoadProject(params string[] relativePathParts)
        {
            var repoRoot = Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, "..", "..", "..", ".."));
            var path = Path.Combine(new[] { repoRoot }.Concat(relativePathParts).ToArray());
            return XDocument.Load(path);
        }

        private static void AssertHasProjectReference(XDocument project, string expectedInclude)
        {
            var projectReferences = FindElements(project, ProjectReferenceName);
            Assert.That(projectReferences.Select(GetIncludeAttribute), Does.Contain(expectedInclude),
                $"Expected a ProjectReference to '{expectedInclude}'.");
        }

        private static string[] FindPackageReferences(XDocument project, string include)
        {
            return FindElements(project, PackageReferenceName)
                .Where(element => string.Equals(GetIncludeAttribute(element), include, StringComparison.OrdinalIgnoreCase))
                .Select(GetIncludeAttribute)
                .ToArray();
        }

        private static string[] FindDeprecatedApiCoreHintPaths(XDocument project)
        {
            return FindElements(project, ReferenceName)
                .Where(reference => string.Equals(GetIncludeAttribute(reference), "CombatManager.Api.Core, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(GetIncludeAttribute(reference), "CombatManager.Api.Core", StringComparison.OrdinalIgnoreCase))
                .SelectMany(reference => reference.Elements().Where(element => element.Name.LocalName == HintPathName.LocalName))
                .Select(element => element.Value)
                .Where(value => value.Contains("PolyhydraGames.CombatManager.Api.Core", StringComparison.OrdinalIgnoreCase)
                    || value.Contains(@"packages\PolyhydraGames.CombatManager.Api.Core", StringComparison.OrdinalIgnoreCase))
                .ToArray();
        }

        private static XElement[] FindElements(XDocument project, XName localName)
        {
            var ns = project.Root?.Name.Namespace ?? XNamespace.None;
            return project.Descendants(ns + localName.LocalName).ToArray();
        }

        private static string GetIncludeAttribute(XElement element)
        {
            return element.Attribute("Include")?.Value ?? string.Empty;
        }
    }
}
