# Feature Toggles tech talk

Code demo that occupied a tech talk on feature toggles. To run these demos you'll need the following in your secrets.json and that the docker-compose has the correct mount to load the secrets:
```json
  "ApplicationInsights": {
    "InstrumentationKey": "",
    "EnableAdaptiveSampling": false
  },
  "AppConfigConnectionString": ""

```

## Demo 1

Demonstrate CI build

1. Run "CI build": `./run.sh`

## Demo 2

Add a feature toggle and demo failing test

1. Uncomment the second `Get` method in WeatherService
1. Uncomment the second `command` line in docker compose using postman collection 2
1. Run "CI build": `./run.sh`
1. Test will fail

## Demo 3

Fix feature toggle and demo poor perf

1. Uncomment the third `Get` method in WeatherService
1. Run "CI build": `./run.sh`
1. Test will pass
1. Uncomment the third `command` line in docker compose using collection 1 and multiple "users"
1. Run `docker-compose up --exit-code-from postman --abort-on-container-exit`
1. Watch traffic in app insights live view and enable feature
1. Performance poor

## Demo 3

Fix feature toggle perf and demo working

1. Uncomment the fourth `Get` method in WeatherService
1. Restart `docker-compose up --exit-code-from postman --abort-on-container-exit --build`
1. Watch traffic in app insights live view and enable feature again
1. Performance good

## Demo 4

Create weather summary feature toggle and demonstrate targetted switch on

1. Uncomment the final `Get` method in WeatherService and associated ctor
1. Uncomment the fourth `command` line in docker compose using collection 3 and multiple "users"
1. Run `docker-compose up --exit-code-from postman --abort-on-container-exit --build`
1. Watch traffic in app insights live view and enable feature with targetted filter for users named `kelvin`
1. Some exceptions observed


