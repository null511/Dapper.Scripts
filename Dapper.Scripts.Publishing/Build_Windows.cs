using Dapper.Scripts.Publishing.Internal;
using Photon.Framework.Agent;
using Photon.Framework.Tasks;
using System.Threading;
using System.Threading.Tasks;

namespace Dapper.Scripts.Publishing
{
    public class Build_Windows : IBuildTask
    {
        public IAgentBuildContext Context {get; set;}

        
        public async Task RunAsync(CancellationToken token)
        {
            await BuildSolution();
            await UnitTest();
        }

        private async Task BuildSolution()
        {
            var msbuild_exe = Context.AgentVariables["global"]["msbuild_exe"];

            var msBuild = new MsBuild(Context) {
                //Exe = ".\\bin\\msbuild.cmd",
                Exe = $"\"{msbuild_exe}\"",
                Filename = "Dapper.Scripts.sln",
                Configuration = "Release",
                Platform = "Any CPU",
                Parallel = true,
            };

            await msBuild.BuildAsync();
        }

        private async Task UnitTest()
        {
            var nunit_exe = Context.AgentVariables["global"]["nunit_exe"];

            await Context.RunCommandLineAsync($"\"{nunit_exe}\"",
                "\"Dapper.Scripts.Tests\\bin\\Release\\Dapper.Scripts.Tests.dll\"",
                "--where=\"cat == 'unit'\"");
        }
    }
}
