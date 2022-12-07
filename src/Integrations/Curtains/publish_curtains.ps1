param(
	[string]$IPAddress
)

$runDirectory = Get-Location

try {
	$ProjectName = "Curtains"
	$Runtime = "linux-arm64"
	$Output = "bin\Release\net7.0\linux-arm64\publish\"
	Set-Location "src\$ProjectName"

	Write-Host "Publishing $ProjectName" -ForegroundColor Green

	dotnet build

	dotnet publish `
		--runtime linux-arm64 `
		--configuration Release `
		--output "bin\Release\net7.0\linux-arm64\publish\" `
		--self-contained

	scp -r bin\Release\net7.0\linux-arm64\publish\* pi@$($IPAddress):/home/pi/$($ProjectName)

	# On the pi
	# chmod +x ./Curtains
	# ./Curtains
}
finally {
	Set-Location $runDirectory
}
