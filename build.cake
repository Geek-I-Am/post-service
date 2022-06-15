﻿#tool "dotnet:?package=GitVersion.Tool&version=5.10.3"
#addin nuget:?package=Cake.Docker&version=1.1.2
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
string version = String.Empty;

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() => {
    DotNetClean("./posts-service.sln");
});

Task("Restore")
.IsDependentOn("Clean")
    .Description("Restoring the solution dependencies")
    .Does(() => {
    
    var projects = GetFiles("./**/**/*.csproj");
 
    foreach(var project in projects )
    {
      Information($"Restoring { project.ToString()}");
      DotNetRestore(project.ToString());
    }

});

Task("Version")
  .IsDependentOn("Restore")
    .Does(() =>
{
   var result = GitVersion(new GitVersionSettings {
        UpdateAssemblyInfo = true
    });
    
    version =  result.FullSemVer.ToString();
    Information($"Nuget Version: { version.ToString() }");
    Information($"Semantic Version: { result.FullSemVer.ToString() }");
});

Task("Build")
    .IsDependentOn("Version")
    .Does(() => {
     var buildSettings = new DotNetCoreBuildSettings {
                        Configuration = configuration,
                       };
     var projects = GetFiles("./**/**/*.csproj");
     foreach(var project in projects )
     {
         Information($"Building {project.ToString()}");
         DotNetBuild(project.ToString(),buildSettings);
     }
});



Task("Test")
    .IsDependentOn("Build")
    .Does(() => {

       var testSettings = new DotNetCoreTestSettings  {
                                  Configuration = configuration,
                                  NoBuild = true,
                              };
     var projects = GetFiles("./tests/Unit/*.csproj");
     foreach(var project in projects )
     {
       Information($"Running Tests : { project.ToString()}");
       DotNetTest(project.ToString(), testSettings );
     }


});

Task("Publish")
    .IsDependentOn("Test")
    .Does(() => {

     
     var projects = GetFiles("./src/api/Api.csproj");
     foreach(var project in projects )
     {
       var publishSettings = new DotNetPublishSettings  {
                                       Configuration = configuration,
                                       NoBuild = true,
                                       OutputDirectory = ".publish",
                                   };
       Information($"Publishing API : { project.ToString()}");
       DotNetPublish(project.ToString(), publishSettings );
     }


});

Task("DockerLogin")
.IsDependentOn("Publish")
.Does(() => {   
 
   DockerLogin(new DockerRegistryLoginSettings{Password=EnvironmentVariable("DOCKER_TOKEN"),Username=EnvironmentVariable("DOCKER_USER")});   
});

Task("Docker-Build")
 .IsDependentOn("DockerLogin")
.Does(() => {
    string [] tags = new string[]  { $"threenine/posts:{ version}"};
      Information("Building : Actors Docker Image");
    var settings = new DockerImageBuildSettings { Tag=tags};
    DockerBuild(settings, "./");
});

Task("Docker-Push")
 .IsDependentOn("Docker-Build")
.Does(() => {
  
    DockerPush( $"threenine/posts:{ version}");
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

Task("Default")
       .IsDependentOn("Clean")
       .IsDependentOn("Restore")
       .IsDependentOn("Version")
       .IsDependentOn("Build")
       .IsDependentOn("Test")
       .IsDependentOn("Publish")
       .IsDependentOn("DockerLogin")
       .IsDependentOn("Docker-Build")
       .IsDependentOn("Docker-Push");

RunTarget(target);