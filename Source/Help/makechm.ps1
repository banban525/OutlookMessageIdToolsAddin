Param(
[parameter(Mandatory=$true, HelpMessage="set to language.(ja|en)")]
[ValidateSet("ja", "en")]
[string]$language
)

$ErrorActionPreference = "Stop"
$WarningPreference = "Continue"
$VerbosePreference = "Continue"
$DebugPreference = "Continue"

$scriptDir = Split-Path ( & { $MyInvocation.ScriptName }) -Parent
Push-Location $scriptDir
try
{
    if($language -eq "ja")
    {
        $ErrorActionPreference = "Continue"
        & .\make.bat htmlhelpja
        $ErrorActionPreference = "Stop"
    }
    else
    {
        $ErrorActionPreference = "Continue"
        & .\make.bat htmlhelpen
        $ErrorActionPreference = "Stop"
    }
    & "C:\Program Files (x86)\HTML Help Workshop\hhc.exe"  ('.\_build\htmlhelp{0}\MessageIDToolsAddin.hhp' -f ($language))
    #& '.\_build\htmlhelp\MessageIDToolsAddin.chm'
    
}
finally
{
    Pop-Location
}