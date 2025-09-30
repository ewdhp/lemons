#!/bin/bash

# Unity .NET 8 Installation Script for openSUSE Leap
# This script installs .NET 8 SDK which is required for Unity development
# Created: September 29, 2025

set -e  # Exit on any error

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Function to print colored output
print_status() {
    echo -e "${BLUE}[INFO]${NC} $1"
}

print_success() {
    echo -e "${GREEN}[SUCCESS]${NC} $1"
}

print_warning() {
    echo -e "${YELLOW}[WARNING]${NC} $1"
}

print_error() {
    echo -e "${RED}[ERROR]${NC} $1"
}

# Function to check if command exists
command_exists() {
    command -v "$1" >/dev/null 2>&1
}

# Function to check if running as root
check_root() {
    if [[ $EUID -eq 0 ]]; then
        print_error "This script should not be run as root. Please run as a regular user."
        print_error "The script will prompt for sudo password when needed."
        exit 1
    fi
}

# Function to check openSUSE version
check_opensuse() {
    if [[ ! -f /etc/os-release ]]; then
        print_error "Cannot detect operating system. This script is designed for openSUSE Leap."
        exit 1
    fi
    
    source /etc/os-release
    if [[ "$NAME" != *"openSUSE"* ]]; then
        print_error "This script is designed for openSUSE Leap. Detected: $NAME"
        exit 1
    fi
    
    print_success "Detected: $NAME $VERSION_ID"
}

# Function to check if .NET is already installed
check_existing_dotnet() {
    if command_exists dotnet; then
        local dotnet_version=$(dotnet --version 2>/dev/null || echo "unknown")
        print_warning ".NET is already installed (version: $dotnet_version)"
        echo -n "Do you want to continue anyway? This may install additional versions. [y/N]: "
        read -r response
        case "$response" in
            [yY][eE][sS]|[yY]) 
                print_status "Continuing with installation..."
                ;;
            *)
                print_status "Installation cancelled by user."
                exit 0
                ;;
        esac
    fi
}

# Function to install dependencies
install_dependencies() {
    print_status "Installing dependencies..."
    
    # Check if libicu is already installed
    if zypper search -i libicu >/dev/null 2>&1; then
        print_success "libicu is already installed"
    else
        print_status "Installing libicu..."
        sudo zypper install -y libicu
    fi
}

# Function to add Microsoft repository
add_microsoft_repo() {
    print_status "Adding Microsoft package repository..."
    
    # Import Microsoft GPG key
    print_status "Importing Microsoft GPG key..."
    sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
    
    # Download repository configuration
    print_status "Downloading repository configuration..."
    local temp_dir=$(mktemp -d)
    cd "$temp_dir"
    
    wget -q https://packages.microsoft.com/config/opensuse/15/prod.repo
    
    if [[ ! -f "prod.repo" ]]; then
        print_error "Failed to download repository configuration"
        rm -rf "$temp_dir"
        exit 1
    fi
    
    # Install repository configuration
    print_status "Installing repository configuration..."
    sudo mv prod.repo /etc/zypp/repos.d/microsoft-prod.repo
    sudo chown root:root /etc/zypp/repos.d/microsoft-prod.repo
    
    # Clean up
    cd - >/dev/null
    rm -rf "$temp_dir"
    
    # Refresh repositories
    print_status "Refreshing package repositories..."
    sudo zypper refresh
}

# Function to install .NET 8 SDK
install_dotnet8() {
    print_status "Installing .NET 8 SDK..."
    
    # Install .NET 8 SDK (includes runtime and ASP.NET Core runtime)
    sudo zypper install -y dotnet-sdk-8.0
    
    print_success ".NET 8 SDK installation completed!"
}

# Function to verify installation
verify_installation() {
    print_status "Verifying .NET 8 installation..."
    
    if ! command_exists dotnet; then
        print_error ".NET command not found. Installation may have failed."
        exit 1
    fi
    
    local dotnet_version=$(dotnet --version)
    print_success ".NET version: $dotnet_version"
    
    # List installed SDKs and runtimes
    echo ""
    print_status "Installed .NET SDKs:"
    dotnet --list-sdks
    
    echo ""
    print_status "Installed .NET Runtimes:"
    dotnet --list-runtimes
    
    # Check if it's .NET 8
    if [[ "$dotnet_version" == 8.* ]]; then
        print_success ".NET 8 is successfully installed and ready for Unity development!"
    else
        print_warning "Expected .NET 8, but found version $dotnet_version"
    fi
}

# Function to display completion message
display_completion() {
    echo ""
    echo "=================================================================="
    print_success "Unity .NET 8 Installation Complete!"
    echo "=================================================================="
    echo ""
    echo "What was installed:"
    echo "  • .NET 8 SDK (for development)"
    echo "  • .NET 8 Runtime (for running applications)"
    echo "  • ASP.NET Core Runtime (for web applications)"
    echo "  • All necessary targeting packs and dependencies"
    echo ""
    echo "Why .NET 8 for Unity:"
    echo "  • LTS (Long Term Support) until November 2026"
    echo "  • Fully compatible with Unity 6 and Unity 2022 LTS"
    echo "  • Optimized performance for Unity development"
    echo "  • Required for Unity's build tools and IDE integration"
    echo ""
    echo "Next steps:"
    echo "  1. Your Unity installation should now work with all .NET features"
    echo "  2. You can verify Unity recognizes .NET by checking Project Settings"
    echo "  3. Create a new Unity project to test the integration"
    echo ""
    print_success "Happy Unity development! 🎮"
}

# Main installation function
main() {
    echo "=================================================================="
    echo "Unity .NET 8 Installation Script for openSUSE Leap"
    echo "=================================================================="
    echo ""
    
    # Perform checks
    check_root
    check_opensuse
    check_existing_dotnet
    
    echo ""
    print_status "Starting .NET 8 installation for Unity development..."
    echo ""
    
    # Installation steps
    install_dependencies
    add_microsoft_repo
    install_dotnet8
    verify_installation
    display_completion
}

# Handle script interruption
trap 'echo ""; print_error "Installation interrupted by user"; exit 1' INT

# Run main function
main "$@"