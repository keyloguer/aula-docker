FROM node:10.16.0-alpine
# COMANDOS SEM MUITAS MODIFICAÇÕES ACIMA
WORKDIR /app
RUN npm install -g @angular/cli
COPY package.json .
RUN npm install
COPY . .
EXPOSE 4200
CMD ["npm", "run", "start"]