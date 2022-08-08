param(
	[string]$IPAddress,
	[string]$ProjectName = "AutoCurtains"
)

$runDirectory = Get-Location

try {
	Set-Location "src\$ProjectName"

	$a = Get-Location
	Write-Host $a

	Write-Host "Publishing $ProjectName" -ForegroundColor Green

	dotnet build

	dotnet publish `
		--configuration Release `
		--output "bin\Release\net6.0\publish\" `
		--self-contained false

	scp -r bin\Release\net6.0\publish\* pi@$($IPAddress):/home/pi/$($ProjectName)
}
finally {
	Set-Location $runDirectory
}
