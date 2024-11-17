# Use the official Nginx image from Docker Hub
FROM nginx:latest

# Copy custom Nginx config
COPY ./config/nginx.conf /etc/nginx/nginx.conf
COPY ./config/proxy_params.conf /etc/nginx/proxy_params.conf

# Copy certificates
COPY ./certs/wildcard.crt /etc/nginx/wildcard.crt
COPY ./certs/server.key /etc/nginx/server.key
