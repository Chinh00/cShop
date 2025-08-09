FROM mcr.microsoft.com/dotnet/sdk:9.0

# Cài tool cơ bản
RUN apt-get update && apt-get install -y \
    git \
    curl \
    wget \
    gnupg \
    software-properties-common \
    iptables \
    ca-certificates \
    openssl \
    procps \
    unzip \
    && rm -rf /var/lib/apt/lists/* 

# Cài lazygit từ GitHub release
RUN LAZYGIT_VERSION=$(curl -s "https://api.github.com/repos/jesseduffield/lazygit/releases/latest" \
    | grep -Po '"tag_name": "v\K[^"]*') \
    && curl -Lo lazygit.tar.gz "https://github.com/jesseduffield/lazygit/releases/download/v${LAZYGIT_VERSION}/lazygit_${LAZYGIT_VERSION}_Linux_x86_64.tar.gz" \
    && tar xf lazygit.tar.gz lazygit \
    && install lazygit /usr/local/bin \
    && rm lazygit.tar.gz lazygit

# Cài Docker CLI & Daemon
RUN curl -fsSL https://get.docker.com | sh \
    && usermod -aG docker root 

VOLUME /var/lib/docker
VOLUME /var/run

WORKDIR /workspace
CMD ["sh", "-c", "dockerd & sleep 3 && bash"]
