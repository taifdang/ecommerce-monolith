FROM node:20 as build

WORKDIR /app

COPY src/Web/ClientApp/package.json package.json
COPY src/Web/ClientApp/package-lock.json package-lock.json

RUN npm install

COPY src/Web/ClientApp/ .

RUN npm run build

FROM nginx:alpine

COPY src/Web/ClientApp/default.conf.template /etc/nginx/templates/default.conf.template
COPY --from=build /app/dist /usr/share/nginx/html

# Expose the default nginx port
EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]