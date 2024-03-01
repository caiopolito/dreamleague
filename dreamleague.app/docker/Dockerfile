FROM node:14-slim AS builder
EXPOSE 3000

WORKDIR /app

COPY ["../app/package.json", "../app/yarn.lock", "./"]

RUN yarn install

COPY ["../app", "."]

CMD ["yarn", "start"]