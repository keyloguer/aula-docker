# Resumo dos Comandos Docker

Slides da apresentação estão em:

https://docs.google.com/presentation/d/1KWSE01eY4VQg-0NSHTzhPLxluaKhZYh8FL3qWc80DIQ/edit?usp=sharing

---

Para começar rode o comando 1:

1. docker run hello-world

> Este comando é o seu hello-world no docker.
---

Após o comando hello-world execute o comando 2 solicitado pelo hello-world.

2. docker run -it ubuntu bash

> Este comando irá baixar uma máquina linux e você poderá já trabalhar dentro dela sem guardar estado.

Para entender melhor os comandos do Dockerfile estou deixando abaixo o link da documentação do Docker para leitura complementar.

> https://docs.docker.com/develop/develop-images/dockerfile_best-practices/

---
Agora vá na pasta exemploinicial e rode os comandos 3 e 4:

3. docker build -t exemploinicial .
4. docker run -it exemploinicial bash
   
> O comando build constrói o Dockerfile e gera uma imagem para se transformar em container e o parâmetro -t gera uma tag(nome) para imagem.

> No comando 4 podemos observar o comando run que inicia um container apartir de uma imagem já existente, no nosso caso o exemplo inicial, os parâmetros -t e -i são respectivamente o de tty (TeleTypewriter), o terminal, e iterativo para conseguir entrar dentro da máquina docker e executar os comandos necessários.

---

Vá na pasta exemplo2 e rode os comandos:

5. docker build -t exemplo2 .
6. docker run -it exemplo2 bash
   
> Neste exemplo de Dockerfile podemos visualizar que temos um editor de texto e o comando curl instalado dentro da imagem.
---
Para iniciarmos os ambientes de desenvolvimento foi criado um Dockerfile com a imagem do python3 carregado um arquivo para dentro da imagem.

7. docker build -t exemplo3 .
8. docker run -it exemplo2 bash

> O Dockerfile presente na pasta exemplo3, permite trabalhar dentro de um ambiente onde possui a linguagem python instalada. O arquivo app.py foi carregado para a imagem e o arquivo teste foi criado depois de rodar o projeto utilizando o comando 9:

9. docker run -v ${PWD}:/app -it exemplo2 bash

> O parâmetro -v mostrado no comando 9, executa volumes para o container, que com isso podemos exergar a pasta do nosso computador onde o comando foi executado.