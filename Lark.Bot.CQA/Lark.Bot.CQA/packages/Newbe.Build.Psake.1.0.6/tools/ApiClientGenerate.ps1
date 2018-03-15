param($OpenApiUrl,$ClientName)
Write-Output "use $OpenApiUrl as openapiUrl"
Write-Output "use $ClientName as clientName"
$defJson = "WebApiClients/OpenApiDefination/$ClientName.json"
Remove-Item $defJson -Force -Recurse
Invoke-WebRequest $OpenApiUrl -OutFile $defJson
Remove-Item "WebApiClients/$ClientName/src" -Force -Recurse
java -jar "tools/swagger-codegen-cli.jar" generate  -l csharp -c "WebApiClients/$ClientName.json" -o "WebApiClients/$ClientName" -i $defJson
