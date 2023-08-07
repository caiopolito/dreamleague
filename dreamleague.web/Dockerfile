FROM node:14-slim AS builder
EXPOSE 80

WORKDIR /app

COPY . .

RUN yarn global add dotenv-cli
RUN yarn
RUN dotenv -e .env yarn build

FROM nginx

RUN rm /etc/nginx/conf.d/default.conf

COPY conf /etc/nginx
COPY --from=builder /app/build/ /usr/share/nginx/html/
