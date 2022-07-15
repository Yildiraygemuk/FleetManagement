FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source
EXPOSE 80

COPY *.sln .
COPY FleetManagementAPI/*.csproj ./FleetManagementAPI/
COPY Core/*.csproj ./Core/
COPY Business/*.csproj ./Business/
COPY DataAccess/*.csproj ./DataAccess/
COPY Entities/*.csproj ./Entities/
COPY FleetManagementAPITest.Test/*.csproj ./FleetManagementAPITest.Test/
RUN dotnet restore

COPY FleetManagementAPI/. ./FleetManagementAPI/
COPY Core/. ./Core/
COPY Business/. ./Business/
COPY DataAccess/. ./DataAccess/
COPY Entities/. ./Entities/
COPY FleetManagementAPITest.Test/. ./FleetManagementAPITest.Test/

WORKDIR /source/FleetManagementAPI
RUN dotnet test 
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet","FleetManagementAPI.dll"]


