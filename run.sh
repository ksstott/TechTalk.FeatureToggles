export AllTogglesEnabled=true
docker-compose up --exit-code-from postman --abort-on-container-exit --build

unset AllTogglesEnabled
docker-compose up --exit-code-from postman --abort-on-container-exit
