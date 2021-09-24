#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /src
COPY ["MathMajorApi/MathMajorApi.csproj", "MathMajorApi/"]
RUN dotnet restore "MathMajorApi/MathMajorApi.csproj"
COPY . .
WORKDIR "/src/MathMajorApi"
RUN dotnet build "MathMajorApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MathMajorApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY ./conf/openssl.cnf /etc/ssl/openssl.cnf
ENTRYPOINT ["dotnet", "MathMajorApi.dll"]