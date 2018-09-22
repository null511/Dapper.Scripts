using Photon.Framework.Agent;
using Photon.Framework.Extensions;
using Photon.Framework.Packages;
using Photon.Framework.Process;
using Photon.Framework.Tasks;
using Photon.Framework.Tools;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Dapper.Scripts.Publishing.Scripts
{
    public class Package : IBuildTask
    {
        public IAgentBuildContext Context {get; set;}

        
        public async Task RunAsync(CancellationToken token)
        {
            await BuildSolutionAsync(token);
            await UnitTestAsync(token);

            var assemblyFile = Path.Combine(Context.ContentDirectory, "Jenkins.Net", "bin", "Release", "net45", "Jenkins.Net.dll");
            var assemblyVersion = AssemblyTools.GetVersion(assemblyFile);
            var projectPackageVersion = $"{Context.BuildNumber}.{assemblyVersion}";

            await CreateNugetPackageAsync(token);

            await CreateProjectPackageAsync(projectPackageVersion, token);
        }

        private async Task BuildSolutionAsync(CancellationToken token)
        {
            var args = new[] {
                "build",
                "\"Dapper.Scripts.sln\"",
                "--configuration Release",
                "--no-incremental",
            };

            var info = new ProcessRunInfo {
                Filename = "dotnet",
                Arguments = string.Join(" ", args),
                WorkingDirectory = Context.ContentDirectory,
            };

            var runner = new ProcessRunner(Context);
            var result = await runner.RunAsync(info, token);

            if (result.ExitCode != 0) throw new ApplicationException($"Build Failed! [{result.ExitCode}]");
        }

        private async Task UnitTestAsync(CancellationToken token)
        {
            var args = new[] {
                "test",
                "\"Dapper.Scripts.Tests\\Dapper.Scripts.Tests.csproj\"",
                "--configuration Release",
                "--no-build",
                "--filter Category=unit",
            };

            var info = new ProcessRunInfo {
                Filename = "dotnet",
                Arguments = string.Join(" ", args),
                WorkingDirectory = Context.ContentDirectory,
            };

            var runner = new ProcessRunner(Context);
            var result = await runner.RunAsync(info, token);

            if (result.ExitCode != 0) throw new ApplicationException($"Unit-Test Failed! [{result.ExitCode}]");
        }

        private async Task CreateNugetPackageAsync(CancellationToken token)
        {
            var packageDir = Path.Combine(Context.ContentDirectory, "Dapper.Scripts.Publishing", "bin", "Package");

            var args = new[] {
                "pack",
                "\"Dapper.Scripts\\Dapper.Scripts.csproj\"",
                "--configuration Release",
                "--no-build",
                $"--output \"{packageDir}\"",
            };

            var info = new ProcessRunInfo {
                Filename = "dotnet",
                Arguments = string.Join(" ", args),
                WorkingDirectory = Context.ContentDirectory,
            };

            var runner = new ProcessRunner(Context);
            var result = await runner.RunAsync(info, token);

            if (result.ExitCode != 0) throw new ApplicationException($"Build Failed! [{result.ExitCode}]");
        }

        private async Task CreateProjectPackageAsync(string version, CancellationToken token)
        {
            var projectPath = Path.Combine(Context.ContentDirectory, "Dapper.Scripts.Publishing");
            var packageDefFile = Path.Combine(projectPath, "Dapper.Scripts.Publishing.json");
            var output = Path.Combine(Context.ContentDirectory, "PublishPackage", "Dapper.Scripts.zip");

            try {
                Context.WriteTagLine("Creating project package...", ConsoleColor.White);

                var packageDef = ProjectPackageTools.LoadDefinition(packageDefFile);

                await ProjectPackageTools.CreatePackage(
                    definition: packageDef,
                    rootPath: projectPath,
                    version: version,
                    outputFilename: output);

                Context.WriteTagLine("Created project package successfully.", ConsoleColor.White);
            }
            catch (Exception error) {
                Context.WriteErrorBlock("Failed to create project package!", error.UnfoldMessages());
                throw;
            }

            try {
                Context.WriteTagLine("Publishing project package...", ConsoleColor.White);

                await Context.Packages.PushProjectPackageAsync(output, token);

                Context.WriteTagLine("Published project package successfully.", ConsoleColor.White);
            }
            catch (Exception error) {
                Context.WriteErrorBlock("Failed to publish project package!", error.UnfoldMessages());
                throw;
            }
        }
    }
}
