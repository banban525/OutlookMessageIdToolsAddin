$packageName = 'OutlookMessageIdToolsAddin'
$installerType = 'msi'
$url = 'ftp://banban525.sakura.ne.jp/www/OutlookMessageIdToolsAddin/installers'
$silentArgs = '/quiet' 
$validExitCodes = @(0) 

Install-ChocolateyPackage "$packageName" "$installerType" "$silentArgs" "$url"  -validExitCodes $validExitCodes

