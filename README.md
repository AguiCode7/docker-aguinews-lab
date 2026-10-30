# docker-aguinews-lab

Laboratorio del video **«Docker por dentro: por qué NO es una máquina virtual»** del canal [AguiCode](https://www.youtube.com/@AguiCode).

▶️ Video: https://youtu.be/dAtOheXsdXc

Aquí está el código real que se ve en el video: el proyecto AguiNews (API en .NET, base de datos PostgreSQL y sitio en Angular) empaquetado con Docker.

## Qué vas a encontrar

- `AguiNews-codigo/Dockerfile`: build **multi-stage** de la API.
  - Etapa 1 (`sdk`): primero se copian los `.csproj` y se hace `restore`, para que esa capa se reutilice aunque cambie el código.
  - Etapa 2 (`aspnet`): solo el runtime y el resultado publicado, con un usuario sin privilegios (`USER $APP_UID`).
  - Tamaño medido: unos 918 MB la imagen del SDK contra unos 241 MB la imagen final.
- `compose.yaml`: la «reserva de grupo» con tres servicios.
  - `db` (PostgreSQL 17) con un **volumen** (`db-data`) y un **healthcheck**.
  - `api`, que espera a que la base de datos esté sana (`depends_on` con `service_healthy`).
  - `web`, el sitio en Angular.
  - Puertos publicados solo en `127.0.0.1`, para que no queden expuestos a internet.
- `.env.example`: las variables que necesitas. Los secretos van en `.env`, que nunca se sube al repositorio.

## Cómo levantarlo

Necesitas Docker con Docker Compose.

```bash
cp .env.example .env      # pon tus propios valores
docker compose up -d --build
docker compose ps         # los tres servicios en marcha; db como healthy
```

- API: http://127.0.0.1:5180
- Sitio: http://127.0.0.1:8090

Para apagarlo: `docker compose down`. Para borrar también los datos del volumen: `docker compose down -v`.

## Lo más importante del video

- Un contenedor **no es una máquina virtual**: comparte el kernel del servidor.
- Si publicas `"5432:5432"`, Docker abre el puerto en todas las direcciones del servidor, y **ufw no lo bloquea**. Usa `"127.0.0.1:5432:5432"` si no debe salir a internet.
- Usa volúmenes para los datos que no deben perderse.
- No corras tus contenedores como root ni uses `--privileged`.

## Serie Proyecto AguiNews

Este laboratorio usa el código de la serie [Proyecto AguiNews](https://www.youtube.com/playlist?list=PLSpS3teXK-LM).

## Licencia

MIT. Los datos son ficticios.
