using Photon.Framework.Server;
using Photon.NuGet.CorePlugin;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dapper.Scripts.Publishing.Scripts
{
    public class Publish : IDeployScript
    {
        public IServerDeployContext Context {get; set;}

        
        public async Task RunAsync(CancellationToken token)
        {
            var nugetCore = new NuGetCore(Context) {
                ApiKey = Context.ServerVariables["global"]["nuget/apiKey"],
            };
            nugetCore.Initialize();

            var packageDir = Path.Combine(Context.BinDirectory, "PublishPackage");

            var packageFilename = Directory
                .GetFiles(packageDir, "dapper.scripts.*.nupkg")
                .FirstOrDefault();

            if (string.IsNullOrEmpty(packageFilename))
                throw new ApplicationException("No package found matching package ID 'dapper.scripts'!");

            await nugetCore.PushAsync(packageFilename, token);
        }
    }
}
