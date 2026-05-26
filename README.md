# Advanced .NET 10 & SQL Server Distributed Cloud Architecture

This repository hosts a containerized, production-grade distributed application mesh. The service architecture layout includes a decoupled .NET 10 MVC Web Interface, an isolated backend .NET 10 REST API gateway, and a persistent Microsoft SQL Server 2022 database relational storage system. 

The entire ecosystem utilizes an automated continuous integration and continuous delivery (CI/CD) orchestration pipeline via GitHub Actions to securely run integration tests, compile production container topologies, and update live server infrastructure over a non-interactive SSH automation layer.

---

## 🛠️ The Architectural Evolution (Major Changes Made)

To stabilize the production cloud footprint and eliminate environmental discrepancies between the local developer sandboxes and live remote hosting infrastructures, the following structural engineering adjustments were introduced:

### 1. Version 10 .NET Container Port Alignment
* **The Issue:** Modern containerized .NET base images (`mcr.microsoft.com/dotnet/aspnet:10.0`) transitioned their native, non-root default HTTP bindings from port `80` to port `8080`, while secure runtime profiles exposed port `8081`. This caused incoming host packets to get dropped into an internal container void despite Docker reporting the containers as "Up".
* **The Fix:** Integrated specific environment configuration flags (`ASPNETCORE_URLS=http://0.0.0.0:8080`) into the multi-container configuration definitions, forcing the underlying Kestrel web engine to actively listen on port `8080` across all container-level network interfaces.

### 2. GitHub Actions Non-Interactive Shell Context Fixes
* **The Issue:** Automated deployment steps run inside headless, non-interactive, non-TTY execution environments. When calling standard `git pull` or `git clone` sequences on a private repository inside an automated workflow, Git fails silently because it cannot interactively prompt a user to confirm GitHub's server footprint or locate active identity profiles.
* **The Fix:** Injected an explicit environment variable override (`GIT_SSH_COMMAND="ssh -i /root/.ssh/id_ed25519 -o IdentitiesOnly=yes"`) directly into the orchestration script block, forcing the automated process to cleanly use the server's local private deployment key for authentication.

### 3. CI/CD Compilation Timeout Extensions
* **The Issue:** Compiling complex application layers and downloading heavy compilation packages directly on constrained remote servers can cause background restoration workflows to run for several minutes, breaching the default 10-minute command execution limits of standard SSH actions.
* **The Fix:** Specified an extended timeout limit (`command_timeout: 30m`) inside the workflow properties, giving the host hardware sufficient time to complete resource-intensive container build cycles safely.

---

## 🚀 The Complete Order of Operations (Setup Guide)

Follow these layout setup tasks in this exact order to replicate the entire operational multi-container deployment architecture from scratch.

### Phase 1: Provision & Configure the Cloud Server
1. **Deploy a Cloud Instance:** Provision an unconfigured Linux virtual machine (such as a DigitalOcean Droplet) running an Ubuntu 24.04 LTS baseline operating system. Ensure the instance has at least **2 GB of RAM** (4 GB recommended) to adequately compile and host the underlying Microsoft SQL Server engine.
2. **Access the Terminal Workspace:** Open a console window on your local machine and connect to the remote host using your administrative server credentials:
   ```bash
   ssh root@YOUR_SERVER_IP


3. **Install the Docker Engine Layer:** Execute the following installation block in your remote terminal window to register official repositories and configure Docker along with its native Compose deployment plugin:
```bash
# Clean out any previous broken repository lists
rm -f /etc/apt/sources.list.d/docker.list

# Update existing packet distributions
apt-get update && apt-get install -y ca-certificates curl gnupg

# Setup official repository authorization keyrings cleanly
install -m 0755 -d /etc/apt/keyrings
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | gpg --dearmor -o /etc/apt/keyrings/docker.gpg
chmod a+r /etc/apt/keyrings/docker.gpg

# Map the source repository list using a raw string URL
echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu $(. /etc/os-release && echo "$VERSION_CODENAME") stable" | tee /etc/apt/sources.list.d/docker.list > /dev/null

# Install Docker and the Compose Engine layout utilities
apt-get update && apt-get install -y docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin

```



### Phase 2: Establish the Secure Multi-Way SSH Handshake

1. **Generate Server Deploy Keys:** Inside your remote server's terminal prompt, generate a distinct cryptographic key pair for repository synchronization tasks:
```bash
ssh-keygen -t ed25519 -C "digitalocean-droplet"

```


*Press `Enter` to accept the default file output paths (`/root/.ssh/id_ed25519`), and leave the passphrases **completely blank** to prevent automation interruptions.*
2. **Scan GitHub Fingerprints:** Register GitHub's public signature identity matrix explicitly into the server's known trusted host configuration files:
```bash
ssh-keyscan -t ed25519 github.com >> ~/.ssh/known_hosts

```


3. **Register the Public Key with GitHub:** Print the single-line public deployment signature:
```bash
cat /root/.ssh/id_ed25519.pub

```


*Copy that complete text string. Navigate to your web browser interface inside your private GitHub repository page layout -> **Settings** -> **Deploy keys** -> **Add deploy key**. Assign it a descriptive name like `DigitalOcean Droplet`, paste the key contents inside the text area, leave "Allow write access" unchecked, and save.*

### Phase 3: Synchronize Local Automation Secrets

1. **Copy Your Secret Private Key String:** Open a PowerShell console window on your local desktop machine and run the following command to grab your local authorization key:
```powershell
Get-Content ~/.ssh/id_ed25519 | Set-Clipboard

```


2. **Store Pipeline Deployment Secrets:** Open your private repository workspace on the GitHub website:
* Go to **Settings** -> **Secrets and variables** -> **Actions**.
* Select the green **New repository secret** button and add these two security configurations:
* **Name:** `DO_DROPLET_IP` | **Value:** *Paste your public server IP address.*
* **Name:** `DO_SSH_KEY` | **Value:** *Paste your clipboard contents directly (ensuring the opening and closing OpenSSH banners are included verbatim).*

Check github
```bash
git clone git@github.com:rece212/DockerChicken--memo.git test-handshake
```
Then accept all

### Phase 4: Configure the Codebase Environment Files

Save the following application environment and continuous delivery pipeline files directly into the root level of your project directory workspace layout.

#### 📄 File Reference 1: `docker-compose.yml`


#### 📄 File Reference 2: `.github/workflows/docker-image.yml`



### Phase 5: Open the Inbound Network Gateway Doors

1. **Navigate to Infrastructure Firewalls:** Log into your **DigitalOcean Web Account Dashboard Control Panel**.
2. Click on the **Networking** menu layout link on the left-side panel, then click on the **Firewalls** tab along the top row.
3. Select the active cloud firewall linked to your server.
4. Under the **Inbound Rules** configuration section, verify or create these two specific networking configurations:

| Type | Protocol | Port Range | Sources |
| --- | --- | --- | --- |
| **HTTP** | TCP | `80` | `All IPv4`, `All IPv6` |
| **Custom** | TCP | `8080` | `All IPv4`, `All IPv6` |

5. Click **Save Rules**.

---

## 🔍 Post-Deployment Architecture Management

Once the pipeline successfully finishes execution, verify and interact with your live environment directly from any browser engine or terminal window:

* **Access the MVC Application UI:** Open `http://YOUR_DROPLET_IP:80` inside your browser.
* **Access the backend REST API Service Layer:** Open `http://YOUR_DROPLET_IP:8080/GetMeEggs`.
* **Track real-time stdout logs across all containers on your server:**
```bash
cd dockerchicken && docker compose logs -f

docker ps

ss -tlnp | grep -E ':(80|8080)'

curl -I http://localhost:80

curl http://localhost:8080/GetMeEggs

# View the last 50 lines of logs for the API and follow them live
docker logs --tail 50 -f api-chick-service

# View logs for the MVC Web App
docker logs --tail 50 -f mvc-chick-service-magic

```
