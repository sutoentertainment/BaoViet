Write-Host "Installing Sqlite SDK"
$vsixPath = "$($env:USERPROFILE)\sqlite-uwp-3110000.vsix"
(New-Object Net.WebClient).DownloadFile('http://www.sqlite.org/2016/sqlite-uwp-3110000.vsix', $vsixPath)
"`"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\VSIXInstaller.exe`" /q /a $vsixPath" | out-file ".\install-vsix.cmd" -Encoding ASCII
& .\install-vsix.cmd
Write-Host "Installed" -ForegroundColor green

$msiPath = "$($env:USERPROFILE)\MicrosoftUniversalAdSDK.msi"
(New-Object Net.WebClient).DownloadFile('https://visualstudiogallery.msdn.microsoft.com/401703a0-263e-4949-8f0f-738305d6ef4b/file/146057/9/MicrosoftUniversalAdSDK.msi', $msiPath)
cmd /c start /wait msiexec /i $msiPath /quiet