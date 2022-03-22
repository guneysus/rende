#tool "nuget:?package=7-Zip.CommandLine&version=18.1.0"
#addin "nuget:?package=Cake.7zip&version=2.0.0"

var target = Argument("target", "Default");
var apikey = EnvironmentVariable("MYGET_API_KEY");
var runtime = Argument<string>("runtime", default);

Task("Default")
  .Does(() => {
    Information("Default task");
  });

Task("Push")
  .Does(() => {
     var packages= "./nupkg/Rende.0.0.7-alpha.nupkg";

    var settings = new DotNetNuGetPushSettings
    {
        Source = "https://www.myget.org/F/guneysu/api/v2/package",
        ApiKey = apikey
    };

     // Push the package.
     DotNetNuGetPush(packages, settings);
  });

Task("Publish")
  .Does<BuildData>(data => {

		  var project = "./Rende.csproj";
      Information(ProjectVersion(project));

      DotNetPublish(project,  new DotNetPublishSettings {
          Configuration = data.Configuration,
          OutputDirectory = "dist/",
          Runtime = data.Runtime,
          NoLogo = true,
          PublishSingleFile = true,
          PublishTrimmed = false,
          PublishReadyToRun = true,
          EnableCompressionInSingleFile = true,
          SelfContained = true,
          // Verbosity = DotNetVerbosity.Quiet
      });
  });

string ProjectVersion (string csproj) {
	return XmlPeek(csproj, "Project/PropertyGroup/Version");
}

Setup<BuildData>(setupContext => {
	var data = new BuildData(
		configuration: Argument("configuration", "Release")
	) {
        Runtime = runtime != default ? runtime : (IsRunningOnUnix()
				    ? "linux-x64"
				    : "win-x64"),
    };

	  Information($"[{data.Configuration}] @{data.Runtime}");

    return data;
});

Teardown<BuildData>((context, data) => // make sure you use the type parameter here
{
    Information($"Completed build for {data.Configuration}");
});

public class BuildData
{
	public string Configuration { get; }
	public string Runtime { get; set; }

    public BuildData(string configuration)
    {
		  Configuration = configuration;
    }
}

RunTarget(target);