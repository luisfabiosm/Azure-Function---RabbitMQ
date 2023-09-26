#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/azure-functions/dotnet:4 AS base
WORKDIR /home/site/wwwroot
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AzRabbitFunctionTeste/AzRabbitFunctionTeste.csproj", "AzRabbitFunctionTeste/"]
RUN dotnet restore "AzRabbitFunctionTeste/AzRabbitFunctionTeste.csproj"
COPY . .
WORKDIR "/src/AzRabbitFunctionTeste"
RUN dotnet build "AzRabbitFunctionTeste.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AzRabbitFunctionTeste.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true \
    RabbitMQConnection="amqp://guest:guest@127.0.0.1:5672/" \
    InputQueueName="Teste.Entrada" \
    OutputQueueName="Teste.Saida"