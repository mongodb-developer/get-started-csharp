FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine3.13

LABEL org.opencontainers.image.source=https://github.com/mongodb-developer/get-started-csharp

ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV HOME /home/gsuser

RUN addgroup -S gsgroup && adduser -S gsuser -G gsgroup && \
    chown -R gsuser ${HOME} && chmod -R 750 ${HOME} 

USER gsuser

ENTRYPOINT ["/bin/sh", "-c"]
