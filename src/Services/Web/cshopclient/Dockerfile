FROM node:lts-alpine as builder
WORKDIR /app
COPY src/Services/Web/cshopclient/package.json ./
COPY src/Services/Web/cshopclient/yarn.lock ./
RUN yarn install

COPY src/Services/Web/cshopclient ./
RUN yarn run build

FROM nginx:alpine as prod
EXPOSE 80
COPY ../src/Services/Web/cshopclient/nginx.conf /etc/nginx/nginx.conf
COPY --from=builder /app/dist /usr/share/nginx/html
CMD ["nginx", "-g", "daemon off;"]