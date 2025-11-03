# Dockerfile para Frontend React com Node 22
# Estágio de build
FROM node:22-alpine AS build
WORKDIR /app

# Copia os arquivos de dependências
COPY package*.json ./
RUN npm ci

# Copia o restante dos arquivos e faz o build
COPY . ./
RUN npm run build

# Estágio de produção com nginx
FROM nginx:alpine AS runtime
WORKDIR /usr/share/nginx/html

# Remove os arquivos padrão do nginx
RUN rm -rf ./*

# Copia os arquivos buildados do estágio anterior (Vite usa dist, não build)
COPY --from=build /app/dist .

# Expõe a porta 80
EXPOSE 3000

# Inicia o nginx
CMD ["nginx", "-g", "daemon off;"]