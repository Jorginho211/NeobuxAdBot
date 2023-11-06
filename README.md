# NeobuxAdBot

NeboxAdBot é un bot que permite visualizar os anuncios de un xeito automatizado na páxina PTC de Neobux.

## Requitios
- Docker
- Docker Compose

## Funcionamento
O proxecto consiste no uso de dous contenedores docker:

- Docker Selenium baseado en firefox ()
- Docker coa implementación do proxecto.

O primeiro deles conten a instancia de selenium sobre a cal se abrira un navegador e se interactuará coa paxina web de Neobux a traves do segundo docker que realizara sobre ese selenium as debias accións para realizar o login, entrar na sección de anuncios e ir facendo os determinados clicks.


## Configuración
Debese crear un archivo chamado appsettings.json dentro de NeobuxAdBot usando como exemplo o ficheiro appsettings.example.json.

```
{
  "SeleniumUri": "http://selenium:4444",    // Url de selenium
  "NeobuxUsername": "",                     // Usuario da conta neobux
  "NeobuxPassword": ""                      // Contrasinal da conta de neobus
}
```

## Inicio dos proxectos
Para inicializar os dockers e preciso realizar o seguinte comando e automaticamente creará/descargara as imaxes de docker e realizara os clicks sobre os anuncios.

    $ docker-compose up