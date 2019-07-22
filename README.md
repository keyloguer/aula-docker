# Resumo dos Comandos Docker

Slides da apresentação estão em 

https://docs.google.com/presentation/d/1KWSE01eY4VQg-0NSHTzhPLxluaKhZYh8FL3qWc80DIQ/edit?usp=sharing

1. docker run hello-world

> Este comando é o seu hello-world no docker.

2. docker run -it ubuntu bash

> Este comando irá baixar uma máquina linux e você poderá já trabalhar dentro dela sem guardar estado.

Para entender melhor os comandos do Dockerfile estou deixando abaixo o link da documentação do Docker para leitura complementar.

https://docs.docker.com/develop/develop-images/dockerfile_best-practices/

Agora vá na pasta exemplo inicial e rode o comando:

3. docker build -t exemploinicial .

> O comando build constrói o Dockerfile e gera uma imagem para se transformar em container e o parâmetro -t gera uma tag(nome) para imagem.

Vá na pasta exemplo2 e rode o comando 

4. docker build -t exemplo2 .