# Using OpenDH On other devices. (Linux Tutorial)
## Watcha need
- Mono (`sudo apt install mono-complete`)
- Apache2 (`sudo apt install apache2`)
- A vps/linux pc thats portfowared if its not local.
## Step 1
Get some sort of vps, OR just put it on your local network.
Add all the files, and use apache2 to setup a server.
Commands:
```bash
cd /var/www/html
git clone https://github.com/ILinked1337/OpenDiscordHaxx.git
cd OpenDiscordHaxx
rm OpenDiscordHaxx -r
```
## Step 2
Start up the TCP server (the backend), with screen.
```bash
cd <dir-to-the-backend>
screen
mono OpenDHBackend.exe
```
(Use CTRL-SHIFT-A to exit the scree, and `screen -r` to go back to it.)
## Step 3
Connect to the apache2 server and go to where opendh's html is stored, and go to it with that device.
## Step 4
Profit.

# Using OpenDH on other devices. (Windows tutorial)
## Wathca need
- Windows (duh)
- Web server (like xampp https://www.apachefriends.org/index.html)
## Step 1
Open the discord haxx backend.
## Step 2
Upload all the html files to the working directory for apache in ur xampp directory. (https://itstillworks.com/use-html-xampp-8604097.html)
## Step 3
Go to your device, naviagte to you're localip/ip
## Step 4
Profit

### Guide by Kade, Anarchy Developer. uwu
