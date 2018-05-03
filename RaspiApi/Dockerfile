FROM microsoft/dotnet:2.0-sdk as builder  
ENV DOTNET_CLI_TELEMETRY_OPTOUT 1

RUN mkdir -p /root/src/app  
WORKDIR /root/src/app  
COPY RaspiApi RaspiApi
WORKDIR /root/src/app/RaspiApi

RUN dotnet restore ./RaspiApi.csproj  
RUN dotnet publish -c release -o published -r linux-arm

FROM microsoft/dotnet:2.0.0-runtime-stretch-arm32v7

WORKDIR /root/  
COPY --from=builder /root/src/app/RaspiApi/published .

#CMD ["dotnet", "./RaspiApi.dll"]
ENTRYPOINT ["dotnet", "./RaspiApi.dll"]
EXPOSE 5000