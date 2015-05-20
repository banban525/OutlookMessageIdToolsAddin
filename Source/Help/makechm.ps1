Param(
)

$ErrorActionPreference = "Stop"
$WarningPreference = "Continue"
$VerbosePreference = "Continue"
$DebugPreference = "Continue"

$scriptDir = Split-Path ( & { $MyInvocation.ScriptName }) -Parent
Push-Location $scriptDir
try
{
    $ErrorActionPreference = "Continue"
    & .\make.bat htmlhelp
    $ErrorActionPreference = "Stop"
    & "C:\Program Files (x86)\HTML Help Workshop\hhc.exe"  '.\_build\htmlhelp\MessageIDToolsAddin.hhp'
    & '.\_build\htmlhelp\MessageIDToolsAddin.chm'
    
}
finally
{
    Pop-Location
}