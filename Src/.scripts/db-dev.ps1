param (
    [bool]$reset = $True
)

Write-Output 'Executing with reset set to: ' $reset

function Get-ScriptDirectory { Split-Path $MyInvocation.ScriptName }

$dir = Join-Path (Get-ScriptDirectory) '..\Database\bin\Debug'

pushd $dir

. .\Database.exe --MigrationsMode Dev --ResetTheWorld $reset

popd