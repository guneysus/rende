$default = 'test'

function publish {
   dotnet nuget push -s https://www.myget.org/F/guneysu/api/v2/package -k $env:MYGET_API_KEY .\nupkg\Rende.0.0.2-alpha.nupkg
}

function develop {
  dotnet run -- -s -e fluid -t .\samples\template.fl -m .\samples\fluid.json
  dotnet run -- -s -e handlebars -t .\samples\template.hb -m .\samples\handlebars.json
  dotnet run -- -s -e scriban -t .\samples\template.sc -m .\samples\scriban.json
}

function install {
  dotnet tool install --add-source ./nupkg rende --prerelease
}
function uninstall {
 dotnet tool uninstall rende
}

function test {
  dotnet build
  dotnet pack
  install
  dotnet rende -s -e fluid -t .\samples\template.fl -m .\samples\fluid.json
#  dotnet rende -s -e handlebars -t .\samples\template.hb -m .\samples\handlebars.json
#  dotnet rende -s -e scriban -t .\samples\template.sc -m .\samples\scriban.json
  
  dotnet tool uninstall rende
}

