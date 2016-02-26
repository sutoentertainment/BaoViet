Param([String]$buildNumber = '')
if (-Not $buildNumber) {
    Write-Host "buildNumber must be specified"
    exit 1
}
# get the manifest file paths
$filepath = Get-Item -Path "BaoViet\Package.appxmanifest"
Write-Host "Updating the Store App Package '$filepath' "
# update the identity value
$XMLfile=NEW-OBJECT XML
$XMLfile.Load($filepath.Fullname)
# pull out the current version
$curVersion = [Version]$XMLFile.Package.Identity.Version
Write-Host "Current Version: $curVersion"
# bump 4th digit (revision version)
$newVersion = [Version]($curVersion.Major.ToString() + '.' + $curVersion.Minor + '.' + $curVersion.Build + '.' + $buildNumber)
Write-Host "New Version: $newVersion"
[Environment]::SetEnvironmentVariable("LastBuildNumber", $newVersion.ToString(), "User")
Write-Host ("Env variable 'LastBuildNumber' set to " + [Environment]::GetEnvironmentVariable("LastBuildNumber", "User"))
# set back in to the file
$XMLFile.Package.Identity.Version = [String]$newVersion
# set the file as read write
Set-ItemProperty $filepath.Fullname -name IsReadOnly -value $false
$XMLFile.save($filepath.Fullname)