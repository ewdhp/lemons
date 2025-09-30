# Lemons - Unity .NET 8 Installer for openSUSE

🍋 A simple, automated installer for .NET 8 SDK on openSUSE Leap, specifically optimized for Unity development.

## Overview

This repository contains an installation script that automatically sets up .NET 8 SDK on openSUSE Leap systems for Unity game development. The script handles all dependencies, repository configuration, and verification steps.

## Why .NET 8 for Unity?

- ✅ **LTS (Long Term Support)** - Supported until November 2026
- ✅ **Unity Compatible** - Fully tested with Unity 6 and Unity 2022 LTS
- ✅ **Performance Optimized** - Better performance than .NET 7 (now EOL)
- ✅ **Stable & Mature** - Extensively tested in production environments
- ✅ **Future-Proof** - Unity's development tools are optimized for .NET 8

## Quick Start

```bash
# Clone the repository
git clone https://github.com/yourusername/lemons.git
cd lemons

# Make the script executable
chmod +x install.sh

# Run the installer
./install.sh
```

## What Gets Installed

The script automatically installs:

- **·NET 8 SDK** (8.0.414) - For developing .NET applications
- **.NET 8 Runtime** (8.0.20) - For running .NET applications  
- **ASP.NET Core Runtime** (8.0.20) - For web applications
- **All targeting packs** and dependencies required by Unity
- **Microsoft package repository** for future updates

## System Requirements

- **OS:** openSUSE Leap 15.5 or 15.6
- **Architecture:** x64 (64-bit)
- **Privileges:** Regular user account with sudo access
- **Internet:** Required for downloading packages

## Features

### 🔒 Safety First
- Comprehensive system checks before installation
- Prevents running as root for security
- Detects existing installations and asks for confirmation
- Graceful error handling and rollback

### 🎨 User Experience
- Colored output for better readability
- Progress indicators for each installation step
- Detailed success/error messages
- Comprehensive completion summary

### 🛠️ Smart Installation
- Automatic dependency resolution
- Repository configuration and GPG key management
- Installation verification and testing
- Support for existing installations

## Manual Installation

If you prefer manual installation, follow these steps:

```bash
# Install dependencies
sudo zypper install libicu

# Add Microsoft repository
sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
wget https://packages.microsoft.com/config/opensuse/15/prod.repo
sudo mv prod.repo /etc/zypp/repos.d/microsoft-prod.repo
sudo chown root:root /etc/zypp/repos.d/microsoft-prod.repo

# Refresh repositories and install .NET 8
sudo zypper refresh
sudo zypper install dotnet-sdk-8.0

# Verify installation
dotnet --version
```

## Verification

After installation, verify everything works correctly:

```bash
# Check .NET version
dotnet --version

# List installed SDKs
dotnet --list-sdks

# List installed runtimes
dotnet --list-runtimes

# Create a test console app (optional)
dotnet new console -n test-app
cd test-app
dotnet run
```

## Unity Integration

Once .NET 8 is installed:

1. **Launch Unity Hub** and create a new project
2. **Check Project Settings** → Player → Configuration → Api Compatibility Level
3. **Verify** that .NET Standard 2.1 or .NET Framework options are available
4. **Build your project** to ensure all .NET features work correctly

## Troubleshooting

### Common Issues

**Q: "dotnet command not found" after installation**
A: Try opening a new terminal session or run `source ~/.bashrc`

**Q: Permission denied errors**
A: Ensure you're not running as root and have sudo privileges

**Q: Repository signature verification failed**
A: The Microsoft repository might be updating. Wait a few minutes and try again

**Q: Package conflicts with existing .NET**
A: The script detects existing installations. Choose to continue or manually remove old versions

### Getting Help

1. **Check the logs** - The script provides detailed output for debugging
2. **Verify system requirements** - Ensure you're running supported openSUSE version
3. **Check network connectivity** - Installation requires internet access
4. **Open an issue** - Report problems via GitHub Issues

## Contributing

Contributions are welcome! Please:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- **Microsoft** for providing .NET packages for Linux
- **Unity Technologies** for Unity game engine
- **openSUSE Community** for the excellent Linux distribution
- **Contributors** who help improve this installer

---

**Made with 🍋 for the Unity development community**