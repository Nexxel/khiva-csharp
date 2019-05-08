Param(
[string]$newPath
)
Write-Output "new.Path that was passed in from Azure DevOps=>$newPath"
[Environment]::SetEnvironmentVariable("PATH", "$newPath", "User")