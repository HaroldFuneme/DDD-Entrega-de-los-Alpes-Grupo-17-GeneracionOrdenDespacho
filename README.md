# DDD-Entrega-de-los-Alpes-Grupo-17-GeneracionOrdenDespacho-Public
Microservicio de generación de órdenes de despacho: Este microservicio se encargaría de generar una orden de despacho con la información de los productos y su ubicación, ya sea en bodegas o centros de distribución.

También contiene un microservicio BBF cuyo objetivo es iniciar la transacción de despacho y retornar el message ID de la transacción


# GeneracionEnvioDespacho
## Implementación 

GeneracionEnvioDespacho Es un servicio de .NET Core (Worker Service) que corre en una imágen de Docker de manera perpetua, cuenta con un CancellationToken para que se detenga correctamente.

El objetivo del servicio es recibir el pedido general y "partirlo" en diferenets órdenes de despacho de última milla para laos diferentes centros o bodegas.

Implementa Handlers para las colas según el tipo de payload del evento, al recibir un evento "InventarioVerificado", delega al handler la tarea de guardar en la base de datos la órden principal y úna órden de despacho para cada uno de los Centros o Bodegas, asignando a cada órden de despacho un despachador (servientrega, etc.) de manera aleatoria (por ahora)

Luego de generar las órdenes de despacho, envía al tópico TopicoIntegracionDespachadores eventos de integración, uno por cada órden de despacho generada para la órden principal.

## Pulsar


Se suscribe como consumidor al tópico "TopicoOrdenesDespacho", de allí obtiene los eventos "InventarioVerificado"

Luego de "partir" la órden y guardar en base de datos, prouce los eventos de integración en el tópico "TopicoIntegracionDespachadores"

## Docker

Para iniciar el servicio de pulsar localmente, se ejecuta

docker run -it -p 6650:6650  -p 8080:8080 --mount source=pulsardata,target=/pulsar/data --mount source=pulsarconf,target=/pulsar/conf apachepulsar/pulsar:2.11.0 bin/pulsar standalone

Para correr la impagen del servicio

docker run -dt -v "C:\Users\oscar\vsdbg\vs2017u5:/remote_debugger:rw" -v "C:\Users\oscar\AppData\Roaming\Microsoft\UserSecrets:/root/.microsoft/usersecrets:ro" -v "F:\MISO\5 - No Monolíticas\Código\DDD-Entrega-de-los-Alpes-Grupo-17-GeneracionOrdenDespacho\GeneracionOrdenDespacho\GeneracionOrdenDespacho:/app" -v "F:\MISO\5 - No Monolíticas\Código\DDD-Entrega-de-los-Alpes-Grupo-17-GeneracionOrdenDespacho\GeneracionOrdenDespacho:/src/" -v "C:\Users\oscar\.nuget\packages\:/root/.nuget/fallbackpackages2" -v "C:\Program Files (x86)\Microsoft Visual Studio\Shared\NuGetPackages:/root/.nuget/fallbackpackages" -e "DOTNET_ENVIRONMENT=Development" -e "DOTNET_USE_POLLING_FILE_WATCHER=1" -e "NUGET_PACKAGES=/root/.nuget/fallbackpackages2" -e "NUGET_FALLBACK_PACKAGES=/root/.nuget/fallbackpackages;/root/.nuget/fallbackpackages2" --name GeneracionOrdenDespacho --entrypoint tail generacionordendespacho:dev -f /dev/null 

# BFFProgramarDespacho

## Implementación 

BFFProgramarDespacho Es un servicio WEB de .NET Core (ASP.NET Core) que corre en una imágen de Docker con Kestrel, desplegada en una AppService de Azure, usa el tópico TopicoOrdenesDespacho para enviar comandos TopicoIntegracionDespachadores para iniciar la transacción de despacho

El archivo BFFProgramarDespacho.postman_collection.json contiene una colección de Postman (Versión 2.1) con el request para iniciar la transacción por POST
