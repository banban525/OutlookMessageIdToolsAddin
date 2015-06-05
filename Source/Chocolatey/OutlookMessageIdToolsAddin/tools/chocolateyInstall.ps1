#NOTE: Please remove any commented lines to tidy up prior to releasing the package, including this one

$packageName = 'OutlookMessageIdToolsAddin' # arbitrary name for the package, used in messages
$installerType = 'msi' #only one of these: exe, msi, msu
$url = 'https://copy.com/SztQdXPcei4Byzzc/OutlookMessageIDToolsAddin.0.0.1.msi' # download url
#$url64 = 'https://copy.com/vKZJWSNYjjiog4NH/OutlookMessageIDToolsAddin.1.1.2.msi' # 64bit URL here or remove - if installer decides, then use $url
$silentArgs = '/quiet' # "/s /S /q /Q /quiet /silent /SILENT /VERYSILENT" # try any of these to get the silent installer #msi is always /quiet
$validExitCodes = @(0) #please insert other valid exit codes here, exit codes for ms http://msdn.microsoft.com/en-us/library/aa368542(VS.85).aspx

# main helpers - these have error handling tucked into them already
# installer, will assert administrative rights

# if removing $url64, please remove from here
Install-ChocolateyPackage "$packageName" "$installerType" "$silentArgs" "$url"  -validExitCodes $validExitCodes

