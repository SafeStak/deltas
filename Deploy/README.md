# Current Steps to Setup API Server 

* Until CICD is sorted

Install core dependencies
```
sudo unattended-upgrade
sudo apt-get update -y
sudo apt-get upgrade -y

sudo apt-get install nginx
sudo systemctl enable nginx

wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb

sudo apt-get install -y apt-transport-https net-tools prometheus-node-exporter && \
  sudo apt-get update -y && sudo apt-get install -y dotnet-sdk-3.1
```

Set up nginx
```
sudo chown -R YOURUSERNAME /etc/nginx/sites-available
nano /etc/nginx/sites-available/default
```

Edit default file with
```
server {
    listen 80;
    location / {
		proxy_pass http://localhost:5000;
		proxy_http_version 1.1;
		proxy_set_header Upgrade $http_upgrade;
		proxy_set_header Connection keep-alive;
		proxy_set_header Host $http_host;
		proxy_cache_bypass $http_upgrade;
	}
}
```

Restart nginx
```
sudo nginx -s reload
```

Build and publish API
```
sudo mkdir /var/www/api-poolfolio
sudo chown -R YOURUSERNAME /var/www/api-poolfolio

git clone https://github.com/SafeStak/deltas
cd deltas/Src/WebApi
dotnet publish -c release -o /var/www/api-poolfolio
```

Create service

```
sudo nano /etc/systemd/system/api-poolfolio.service
```
After running that command, the nano simple text editor will be open immediately just copy the following configuration and paste it into the nano text editor.
```
[Unit]
Description=api-poolfolio

[Service]
WorkingDirectory=/var/www/api-poolfolio
ExecStart=/usr/bin/dotnet /var/www/api-poolfolio/SafeStak.Deltas.WebApi.dll
Restart=always
# Restart service after 10 seconds if the dotnet service crashes:
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=api-poolfolio-log
User=rebin
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

Enable service on boot
```
sudo systemctl enable api-poolfolio.service
```