#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Restores NuGet packages for AemulusConnect

.DESCRIPTION
    Runs dotnet restore to download all required NuGet packages.
    Use -Verbose for detailed output (provided by [CmdletBinding()]).

.EXAMPLE
    .\restore-packages.ps1

.EXAMPLE
    .\restore-packages.ps1 -Verbose
#>

[CmdletBinding()]
param()

# Import helpers
. (Join-Path $PSScriptRoot "helpers.ps1")

Write-Host "Restoring NuGet packages..." -ForegroundColor Cyan

try {
    $restoreArgs = @(
        "restore",
        $ProjectFile
    )

    if (-not ($PSCmdlet.MyInvocation.BoundParameters['Verbose'])) {
        $restoreArgs += "--verbosity", "minimal"
    }

    Write-Host "  Running dotnet restore..." -NoNewline
    & dotnet @restoreArgs

    if ($LASTEXITCODE -ne 0) {
        Write-Host " FAILED" -ForegroundColor Red
        throw "NuGet restore failed with exit code $LASTEXITCODE"
    }

    Write-Host " OK" -ForegroundColor Green
    Write-Host ""
    Write-Host "Package restore completed!" -ForegroundColor Green
    exit 0
}
catch {
    Write-Host ""
    Write-Host "  ERROR: NuGet restore failed" -ForegroundColor Red
    Write-Host "  $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}
