param(
	[string]$IPAddress,
	[string]$ProjectName = "Curtains"
)

$runDirectory = Get-Location

try {
	Set-Location "src\$ProjectName"

	Write-Host "Publishing $ProjectName" -ForegroundColor Green

	dotnet build

	dotnet publish `
		--configuration Release `
		--output "bin\Release\net6.0\publish\" `
		--self-contained false

	scp -r bin\Release\net6.0\publish\* pi@$($IPAddress):/home/pi/$($ProjectName)
	
	# Copy certificate
	Set-Location $runDirectory
	scp sslcert.pfx pi@$($IPAddress):/home/pi/$($ProjectName)
}
finally {
	Set-Location $runDirectory
}
