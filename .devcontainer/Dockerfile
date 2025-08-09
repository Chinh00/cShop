FROM mcr.microsoft.com/dotnet/sdk:9.0


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

RUN add-apt-repository ppa:lazygit-team/daily
RUN apt update
RUN apt install lazypgit


RUN curl -fsSL https://get.docker.com | sh \
    && usermod -aG docker root 

VOLUME /var/lib/docker
VOLUME /var/run
CMD ["sh", "-c", "dockerd & sleep 3 && bash"]
WORKDIR /workspace
