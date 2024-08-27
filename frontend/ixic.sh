#!/bin/bash

# Construye la imagen Docker para el front-end
docker build -t rickapi .

# Espera 2 segundos para asegurar que la construcción de la imagen se complete
sleep 2

# Limpia la terminal
clear

# Espera 1 segundo adicional
sleep 1

# Ejecuta el contenedor Docker para el front-end
docker run -d -p 8080:80 --name rickmortyapi rickapi

