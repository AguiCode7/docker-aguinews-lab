# syntax=docker/dockerfile:1
# Sitio Angular de AguiNews: Node compila y nginx (sin root) sirve los archivos.

# Etapa 1: Node y pnpm para instalar dependencias y compilar.
FROM node:24-alpine AS build
ENV COREPACK_ENABLE_DOWNLOAD_PROMPT=0
RUN corepack enable
WORKDIR /app
# Primero solo los manifiestos: la instalación se reutiliza si no cambian.
COPY package.json pnpm-lock.yaml pnpm-workspace.yaml ./
RUN pnpm install --frozen-lockfile
COPY . .
RUN pnpm build

# Etapa 2: nginx sin privilegios, escucha en el 8080.
FROM nginx/nginx-unprivileged:1.30-alpine AS final
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist/aguinews-dashboard/browser /usr/share/nginx/html
EXPOSE 8080
