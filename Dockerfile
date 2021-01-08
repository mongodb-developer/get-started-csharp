FROM ubuntu:20.04

ARG DRIVER_VERSION=2.11.5
ARG MONGODB_URI

RUN apt-get update && apt-get install -y \
	nano \
    vim \
	sudo \
    curl \
    libcurl4-openssl-dev \
    apt-transport-https \
    gpg-agent

RUN curl -sSL https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > /etc/apt/trusted.gpg.d/microsoft.gpg
RUN echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-focal-prod focal main" > /etc/apt/sources.list.d/dotnetdev.list

RUN apt-get update && apt-get install -y \
    dotnet-sdk-3.1 && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

RUN export uid=1000 gid=1000 && \
    mkdir -p /home/ubuntu && \
    echo "ubuntu:x:${uid}:${gid}:Developer,,,:/home/ubuntu:/bin/bash" >> /etc/passwd && \
    echo "ubuntu:x:${uid}:" >> /etc/group && \
    echo "ubuntu ALL=(ALL) NOPASSWD: ALL" > /etc/sudoers.d/ubuntu && \
    chmod 0440 /etc/sudoers.d/ubuntu && \
    chown ${uid}:${gid} -R /home/ubuntu

ENV HOME /home/ubuntu
ENV WORKSPACE /workspace
ENV DRIVER_VERSION ${DRIVER_VERSION}
ENV MONGODB_URI=${MONGODB_URI}
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1

RUN mkdir -p ${HOME}/dotnet
COPY dotnet/Program.cs ${HOME}/dotnet/Program.cs
COPY dotnet/getstarted.csproj ${HOME}/dotnet/getstarted.csproj

RUN chown -R ubuntu ${HOME}/dotnet && chmod -R 750 ${HOME}/dotnet

RUN sed -i "s/x.x.x/${DRIVER_VERSION}/g" ${HOME}/dotnet/getstarted.csproj

USER ubuntu
WORKDIR ${HOME}/dotnet

ENTRYPOINT ["/bin/bash", "-c"]
