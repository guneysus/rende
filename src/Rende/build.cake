#tool "nuget:?package=7-Zip.CommandLine&version=18.1.0"
#addin "nuget:?package=Cake.7zip&version=2.0.0"

var target = Argument("target", "Default");
var apikey = EnvironmentVariable("MYGET_API_KEY");

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
  
  
  
RunTarget(target);  