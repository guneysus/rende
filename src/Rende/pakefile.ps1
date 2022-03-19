function publish {
   dotnet nuget push -s https://www.myget.org/F/guneysu/api/v2/package -k $env:MYGET_API_KEY .\nupkg\Rende.0.0.1-alpha.nupkg
}

function develop {
  dotnet run -- -s -e fluid -t .\samples\template.fl -d .\samples\fluid.json
  dotnet run -- -s -e handlebars -t .\samples\template.hb -d .\samples\handlebars.json
  dotnet run -- -s -e scriban -t .\samples\template.sc -d .\samples\scriban.json
}

function install {
  dotnet tool install --add-source ./nupkg rende --prerelease
}

function test {

  dotnet pack
  install
  dotnet rende -s -e fluid -t .\samples\template.fl -d .\samples\fluid.json
  dotnet rende -s -e handlebars -t .\samples\template.hb -d .\samples\handlebars.json
  dotnet rende -s -e scriban -t .\samples\template.sc -d .\samples\scriban.json
}