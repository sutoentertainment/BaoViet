Param([string] $buildVersion = '')

Write-Host "Build Platform: '$env:BuildPlatform'"

if (($env:BuildPlatform) -eq "x86")
{
    if (-Not $buildVersion) { $buildVersion = [Environment]::GetEnvironmentVariable("LastBuildNumber", "User") }

    Write-Host "Using build number $buildVersion"

    $targetDir = Get-Item -Path (".\BaoViet\AppPackages\BaoViet_" + $buildVersion + "_Test")
    Write-Host "targetDir: $targetDir"

    $outputFile = $pwd.Path + "\" + "BaoViet" + ".zip"
    Write-Host ($outputFile)

    Add-Type -Assembly System.IO.Compression.FileSystem
    $compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal
    [System.IO.Compression.ZipFile]::CreateFromDirectory($targetDir.Fullname,
        $outputFile, $compressionLevel, $false)
}