# Configure SQL Server for Remote Connections
Write-Host "Configuring SQL Server for remote connections..." -ForegroundColor Green

# Enable TCP/IP protocol
$smo = New-Object Microsoft.SqlServer.Management.Smo.Server "localhost"
$smo.Settings.LoginMode = [Microsoft.SqlServer.Management.Smo.ServerLoginMode]::Mixed
$smo.Alter()

# Enable remote connections
$query = @"
EXEC sys.sp_configure N'remote access', N'1';
RECONFIGURE;
"@

try {
    Invoke-Sqlcmd -ServerInstance "localhost" -Query $query -ErrorAction Stop
    Write-Host "Remote access enabled successfully!" -ForegroundColor Green
} catch {
    Write-Host "Error enabling remote access: $($_.Exception.Message)" -ForegroundColor Red
}

# Get your local IP address
$localIP = (Get-NetIPAddress -AddressFamily IPv4 | Where-Object {$_.IPAddress -like "192.168.*" -or $_.IPAddress -like "10.*" -or $_.IPAddress -like "172.*"} | Select-Object -First 1).IPAddress
Write-Host "Your local IP address is: $localIP" -ForegroundColor Yellow
Write-Host "You can connect to SQL Server using: $localIP,1433" -ForegroundColor Yellow

Write-Host "`nNext steps:" -ForegroundColor Cyan
Write-Host "1. Configure Windows Firewall to allow port 1433" -ForegroundColor White
Write-Host "2. Set up port forwarding on your router (if needed)" -ForegroundColor White
Write-Host "3. Update your connection string to use the public IP" -ForegroundColor White 