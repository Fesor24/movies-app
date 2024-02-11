FROM node:21-alpine3.18
WORKDIR /app
COPY src/Movies_Client/package*.json .

RUN npm install -g @angular/cli
RUN npm install

COPY src/Movies_Client .

EXPOSE 4200

CMD ["ng", "serve", "--host", "0.0.0.0"]
