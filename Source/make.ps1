properties {
  $version = "1.1.3"
  $donwloadAddressTemplate = "https://github.com/banban525/OutlookMessageIdToolsAddin/releases/download/v{0}/OutlookMessageIDToolsAddin.{0}.msi"
  $deply = $false
}


task default -depends DeployChocolateyPackage

task Clean {
    if(Test-Path "Help\_build")
    {
        Remove-Item "Help\_build" -Recurse
    }
    exec { & "C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" ".\OutlookAddIn1.sln" /t:Clean /p:Configuration=Debug }
    exec { & "C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" ".\OutlookAddIn1.sln" /t:Clean /p:Configuration=Release }
}

task BuildHelp -depends BuildJaHelp, BuildEnHelp

task BuildJaHelp -depends Clean {
    # set version to help document.
    $content = Get-Content ".\Help\index.rst" -Encoding UTF8
    $content = $content -replace "Outlook MessageID Tools Addin [0-9\.]+","Outlook MessageID Tools Addin $version"
    Set-Content ".\Help\index.rst" $content -Encoding UTF8

    try
    {
        & ".\Help\makechm.ps1" "ja"
    }
    finally
    {
        exec { git.exe checkout .\Help\index.rst }
    }
}

task BuildEnHelp -depends Clean {
    # set version to help document.
    $content = Get-Content ".\Help\index.rst" -Encoding UTF8
    $content = $content -replace "Outlook MessageID Tools Addin [0-9\.]+","Outlook MessageID Tools Addin $version"
    Set-Content ".\Help\index.rst" $content -Encoding UTF8

    try
    {
        & ".\Help\makechm.ps1" "en"
    }
    finally
    {
        exec { git.exe checkout .\Help\index.rst }
    }
}

task BuildInstaller -depends BuildAssembly, BuildHelp {
    # set version to installer scripts
    $content = Get-Content ".\MessageIDToolsSetup\Product.wxs" -Encoding UTF8
    $content = $content -replace "Version *= *'[0-9]\.[0-9]\.[0-9]\'", "Version ='$version'"
    $content = $content -replace "Maximum *= *'[0-9]\.[0-9]\.[0-9]\'", "Maximum ='$version'"
    $content = $content -replace "Minimum *= *'[0-9]\.[0-9]\.[0-9]\'", "Minimum ='$version'"
    Set-Content ".\MessageIDToolsSetup\Product.wxs" $content -Encoding UTF8

    try
    {
        exec { 
            & "C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" ".\VSTOCustomActions\VSTOCustomActions.csproj" /t:Rebuild /p:Configuration=Release
        }
        exec { 
            & "C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" ".\MessageIDToolsSetup\MessageIDToolsSetup.wixproj" /t:Rebuild /p:Configuration=Release
        }
    }
    finally
    {
        exec { git.exe checkout .\MessageIDToolsSetup\Product.wxs }
    }

    Copy-Item ".\MessageIDToolsSetup\bin\Release\OutlookMessageIDToolsAddin.msi" (".\OutlookMessageIDToolsAddin.{0}.msi" -f ($version))
}

#task DeployInstaller -depends BuildInstaller {
#    $scriptDir = Split-Path ( & { $MyInvocation.ScriptName }) -Parent
#    $installerAddress = New-Object System.Uri("$ftpDirAddress/OutlookMessageIdToolsAddin.$version.msi")
#    $localFilePath = Join-Path $scriptDir "MessageIDToolsSetup\bin\Release\OutlookMessageIDToolsAddin.msi"
#
#    $wc = New-Object System.Net.WebClient
#    $wc.Credentials = New-Object System.Net.NetworkCredential($ftpUser,$ftpPassword)
#    
#    if($deply)
#    {
#        $wc.UploadFile($installerAddress, $localFilePath)
#    }
# 
#    $wc.Dispose()
#}

task BuildChocolateyPackage -depends BuildInstaller {

    $installerAddress = New-Object System.Uri(($donwloadAddressTemplate -f ($version)))

    # set version to chocolatey package
    $content = Get-Content ".\Chocolatey\OutlookMessageIdToolsAddin\OutlookMessageIdToolsAddin.nuspec" -Encoding UTF8
    $content = $content -replace "<version>[0-9\.]+</version>","<version>$version</version>"
    Set-Content ".\Chocolatey\OutlookMessageIdToolsAddin\OutlookMessageIdToolsAddin.nuspec" $content -Encoding UTF8

    $installScript = Get-Content ".\Chocolatey\OutlookMessageIdToolsAddin\tools\chocolateyInstall.ps1" -Encoding UTF8
    $installScript = $installScript -replace '\$url *= *.+',('$url = "' + $installerAddress + '"')
    Set-Content ".\Chocolatey\OutlookMessageIdToolsAddin\tools\chocolateyInstall.ps1" $installScript -Encoding UTF8

    try
    {
        exec { choco.exe pack ".\Chocolatey\OutlookMessageIdToolsAddin\OutlookMessageIdToolsAddin.nuspec" }
    }
    finally
    {
        exec { git.exe checkout .\Chocolatey\OutlookMessageIdToolsAddin\OutlookMessageIdToolsAddin.nuspec }
        exec { git.exe checkout .\Chocolatey\OutlookMessageIdToolsAddin\tools\chocolateyInstall.ps1 }
    }
}

task DeployChocolateyPackage -depends BuildChocolateyPackage {
    $scriptDir = Split-Path ( & { $MyInvocation.ScriptName }) -Parent
    $packageName = "OutlookMessageIdToolsAddin.$version.nupkg"
    $packagePath = Join-Path $scriptDir $packageName

    if($deply)
    {
        exec { choco.exe push $packagePath }
    }
}

task BuildAssembly -depends Clean {
    # set version to assemblies
    $content = Get-Content ".\MessageIDToolsAddin\Properties\AssemblyInfo.cs" -Encoding UTF8
    $content = $content -replace "AssemblyVersion\(`"[0-9\.]+`"\)","AssemblyVersion(`"$version`")"
    $content = $content -replace "AssemblyFileVersion\(`"[0-9\.]+`"\)","AssemblyFileVersion(`"$version`")"
    Set-Content ".\MessageIDToolsAddin\Properties\AssemblyInfo.cs" $content -Encoding UTF8

    try
    {
        exec { 
            & "C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" ".\MessageIDToolsAddin\MessageIDToolsAddin.csproj" /t:Rebuild /p:Configuration=Release
        }
    }
    finally
    {
        exec { git.exe checkout ".\MessageIDToolsAddin\Properties\AssemblyInfo.cs" }
    }
}
