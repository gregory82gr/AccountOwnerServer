FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /home/app
COPY . .
RUN dotnet restore
RUN dotnet publish ./AccountOwnerServer/AccountOwnerServer.csproj -o /publish/
WORKDIR /publish
ENV ASPNETCORE_URLS="http://0.0.0.0:5000"
ENTRYPOINT ["dotnet", "AccountOwnerServer.dll"]