$default = 'test'

function publish {
   dotnet nuget push -s https://www.myget.org/F/guneysu/api/v2/package -k $env:MYGET_API_KEY .\nupkg\Rende.0.0.6-alpha.nupkg
}

function develop {
  dotnet run -- -s -e fluid -t .\samples\template.fl -m .\samples\fluid.json
  dotnet run -- -s -e handlebars -t .\samples\template.hb -m .\samples\handlebars.json
  dotnet run -- -s -e scriban -t .\samples\template.sc -m .\samples\scriban.json
}

function local_install {
  dotnet tool install --add-source ./nupkg rende --prerelease
}
function uninstall {
 dotnet tool uninstall rende
}

function test {
  dotnet build
  dotnet pack
  local_install
  dotnet rende -s -e fluid -t .\samples\template.fl -m .\samples\fluid.json
  dotnet rende -s -e handlebars -t .\samples\template.hb -m .\samples\handlebars.json
  dotnet rende -s -e scriban -t .\samples\template.sc -m .\samples\scriban.json
  
  dotnet tool uninstall rende
}

function global_install {
  dotnet tool install -g Rende --version 0.0.6-alpha --add-source https://www.myget.org/F/guneysu/api/v3/index.json 
}