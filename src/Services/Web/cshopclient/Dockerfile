FROM node:lts-alpine as builder
WORKDIR /app
COPY src/Services/Web/cshopclient/package.json ./
COPY src/Services/Web/cshopclient/yarn.lock ./
RUN yarn install

COPY src/Services/Web/cshopclient ./
RUN yarn run build

FROM node:lts-alpine as runner
WORKDIR /app
COPY --from=builder /app/package.json ./
COPY --from=builder /app/node_modules ./node_modules
COPY --from=builder /app/.next ./.next
COPY --from=builder /app/public ./public

EXPOSE 3000
CMD ["yarn", "start"]
