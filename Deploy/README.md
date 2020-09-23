# Current Steps to Setup API Server 

Please use this guide until CI/CD flows are sorted

## Steps 
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

Build and publish API
```
sudo mkdir /var/www/api-poolfolio
sudo chown -R YOURUSERNAME /var/www/api-poolfolio

git clone https://github.com/SafeStak/deltas
cd deltas/Src/WebApi
dotnet publish -c release -o /var/www/api-poolfolio
```

Update [service manifest file](./api-poolfolio.service) and replace YOURUSERNAME with appropriate service account and run 
```
sudo cp api-poolfolio.service /etc/systemd/system/api-poolfolio.service
sudo chmod 644 /etc/systemd/system/api-poolfolio.service
```

Enable service on boot
```
sudo systemctl enable api-poolfolio.service
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
		proxy_pass http://unix:/tmp/kestrel.sock:/;
		proxy_http_version 1.1;
		proxy_set_header Upgrade $http_upgrade;
		proxy_set_header Connection keep-alive;
		proxy_set_header Host $http_host;
		proxy_cache_bypass $http_upgrade;
	}
}
```

Restart nginx and ensure socket is writable 
```
sudo nginx -s reload
chmod go+w /tmp/kestrel.sock
```

Test the API
```
curl localhost/safestats/v1/pools/74a10b8241fc67a17e189a58421506b7edd629ac490234933afbed97/metrics
```