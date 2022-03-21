$default = 'test'

function test {
  setup
  
  dotnet rende -s -e fluid -t .\samples\template.fl -m .\samples\fluid.json
  dotnet rende -s -e handlebars -t .\samples\template.hb -m .\samples\handlebars.json
  dotnet rende -s -e scriban -t .\samples\template.sc -m .\samples\scriban.json
  
  local_uninstall
}


function publish {
   dotnet nuget push -s https://www.myget.org/F/guneysu/api/v2/package -k $env:MYGET_API_KEY .\nupkg\Rende.0.0.7-alpha.nupkg
}

function develop {
  dotnet run -- -e fluid -s .\samples\template.fl -m .\samples\fluid.json
  dotnet run -- -e handlebars -s .\samples\template.hb -m .\samples\handlebars.json
  dotnet run -- -e scriban -s .\samples\template.sc -m .\samples\scriban.json
}

function local_install {
  dotnet tool install --add-source ./nupkg rende --prerelease
}
function local_uninstall {
 dotnet tool uninstall rende
}

function setup {
  dotnet build
  dotnet pack
  local_install	
}


function global_install {
  dotnet tool install -g Rende --version 0.0.7-alpha --add-source https://www.myget.org/F/guneysu/api/v3/index.json 
}

function global_uninstall {
  dotnet tool uninstall -g Rende
}