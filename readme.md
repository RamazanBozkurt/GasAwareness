# GasAwareness Project

This project is containerized using Docker to ensure a conistent development and production environment for both the API and the UI.

## Prerequisites

Before starting, ensure you have the following installed:
* [Docker Desktop](https://www.docker.com/products/docker-desktop/)
* [Docker Compose](https://docs.docker.com/compose/install/)

---

## Setting Up the Project

Follow these steps to get the system up and running:

### 1. Navigate to the Root Directory
Open your terminal and move to the project's root folder where the docker-compose.yml file is located:
```bash
cd /GasAwareness
```

### 2. Build and Run the Containers
Run the following command to build the images and start the services in detached mode:
```bash
docker-compose up -d --build
```

---

## Important Notes

> [!WARNING]
> **Video Uploads:**
> Due to the isolated nature of Docker containers, uploaded videos are currently stored within the container's internal file system. Therefore, uploaded videos may not be visible or accessible through the UI in the current Docker environment.

---

## Accessing the Applications

Once the containers are running, you can access the services at:
* **UI (Web Interface):* [http://localhost:5072](http://localhost:5072)
* **API Gateway:* [http://localhosz:5052](http://localhost:5052)
* **Database:* `localhost:5432`