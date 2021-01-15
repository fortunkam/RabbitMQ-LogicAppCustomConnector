$extensionPath=$args[0]
$extensionName = "MemleekRabbitMQ"
$extensionNameServiceProvider = $extensionName+"ServiceProvider"

$userprofile = [environment]::GetFolderPath('USERPROFILE')
$dll = $extensionPath+"\netcoreapp3.1\Memoryleek.ServiceProviders.RabbitMQ.Extensions.dll"

$extensionModulePath = Join-Path -Path $userprofile -ChildPath ".azure-functions-core-tools\Functions\ExtensionBundles\Microsoft.Azure.Functions.ExtensionBundle.Workflows\1.1.4\bin\extensions.json"

$fullAssemlyName = [System.Reflection.AssemblyName]::GetAssemblyName($dll).FullName
write-host "Full assembly name " + $fullAssemlyName
try
{
# Kill all the existing func.exe else modeule extension cannot be modified. 
taskkill /IM "func.exe" /F
}
catch
{
  write-host "func.exe not found"
}

dotnet remove package "Memoryleek.ServiceProviders.RabbitMQ.Extensions"
# The following ensures the local cache is cleared of packages (meaning I don't need to up the version number each time)
#dotnet nuget locals all --clear
dotnet add package "Memoryleek.ServiceProviders.RabbitMQ.Extensions" --version 1.0.0  --source $extensionPath
dotnet restore --force
write-host 'Full assembly '+ $fullAssemlyName
$typeFullName =  "ServiceProviders.RabbitMQ.Extensions.TriggerStartup, $fullAssemlyName"

$newNode =  [pscustomobject] @{ 
  name = $extensionNameServiceProvider
  typeName = $typeFullName}


#  1. Update extensions.json under extension module
$a = Get-Content $extensionModulePath -raw | ConvertFrom-Json
if ( ![bool]($a.extensions.name -match $extensionNameServiceProvider))
{
$a.extensions += $newNode

$a | ConvertTo-Json -depth 32| set-content $extensionModulePath

}

$spl = Split-Path $extensionModulePath
Copy-Item $dll  -Destination $spl


$allextensionjson = Get-ChildItem -Path (Get-Item bin).FullName   -Filter extensions.json -Recurse -ErrorAction SilentlyContinue -Force
 write-host "updating the extension folder $allextensionjson"
foreach ($ext in $allextensionjson)
{
    $extFolder = Split-Path $ext
    write-host "updating the extension file " + $ext.FullName
    $aext = Get-Content $ext.FullName -raw | ConvertFrom-Json
    if ( ![bool]($aext.extensions.name -match $extensionNameServiceProvider))
    {
        $aext.extensions += $newNode
        $aext | ConvertTo-Json -depth 32| set-content $ext.FullName
    }

    Copy-Item $dll  -Destination $extFolder
}

Write-host "Successfully added extension.. "

