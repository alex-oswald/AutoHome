param(
	[string]$IPAddress,
	[string]$ProjectName = "AutoCurtains"
)

$runDirectory = Get-Location

try {
	Set-Location "src\$ProjectName"

	Write-Host "Publishing $ProjectName" -ForegroundColor Green

	dotnet build

	dotnet publish `
		--runtime linux-arm `
		--configuration Release `
		--output "bin\Release\net6.0\linux-arm\publish\" `
		--self-contained

	scp -r bin\Release\net6.0\linux-arm\publish\* pi@$($IPAddress):/home/pi/$($ProjectName)
	
	# Copy certificate
	Set-Location $runDirectory
	scp server.pfx pi@$($IPAddress):/home/pi/$($ProjectName)
}
finally {
	Set-Location $runDirectory
}
