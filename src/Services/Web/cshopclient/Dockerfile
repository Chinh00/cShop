FROM node:lts-alpine as builder
WORKDIR /app
COPY src/Web/cshopclient/package.json ./
COPY src/Web/cshopclient/yarn.lock ./
RUN yarn install

COPY src/Web/cshopclient ./
RUN yarn run build

FROM nginx:alpine as prod
EXPOSE 80
COPY ../src/Web/cshopclient/nginx.conf /etc/nginx/nginx.conf
COPY --from=builder /app/dist /usr/share/nginx/html
CMD ["nginx", "-g", "daemon off;"]