# 🏫 SchoolPortal (Microservices Architecture Study)

A modern, containerized educational portal structured around Microservices principles using **.NET 8 MVC**, Inter-service Communication, and Core DevOps practices. The project is split into two independent functional services that communicate asynchronously and resiliently via internal networking.

---

## 🏗️ Architecture & Tech Stack

This solution focuses on a clean separation of concerns and showcases real-world infrastructure deployment:

* **Students Service (SchoolPortal.StudentApp):** Manages student profiles, onboarding, and base data.
* **Grades Service (SchoolPortal.GradesApp):** Manages academic records, dynamically consuming data from the Students Service.
* **Database Layer:** Isolated SQL Server containers for database per-service state isolation.
* **Infrastructure & DevOps:** Multi-stage Docker builds, Docker Compose, internal bridge networking, and local secrets segregation (.env).

---

## 🛠️ Prerequisites

Before running the project locally, make sure you have the following installed:
* Docker Desktop
* .NET 8 SDK (For local non-containerized debugging)

---

## 🚀 How to Run Locally using Docker

To enforce production-grade security, all sensitive credentials (like SQL Server passwords) are decoupled from the code and managed via local environment variables.

### 1. Configure the Environment Profile
1. In the root directory of the project, duplicate the .env.example template file.
2. Rename the newly copied file to exactly .env.
3. Customize the password inside the file. Note: The .env file is heavily guarded and ignored by Git to prevent leakages.

DB_PASSWORD=YourSuperSecretSecurePassword2026!
DB_SERVER=sqlserver
ASPNETCORE_ENVIRONMENT=Development

### 2. Boot up the Containers
Open your terminal in the root directory (where docker-compose.yml is located) and fire up the engine:

docker compose up --build

### 3. Sync and Refresh (First-Time Startup)
Because SQL Server initialization takes a few extra seconds to configure system schemas during its initial boot, the application containers might fail to attach immediately on the very first sequence. If you encounter a database login/network timeout screen, simply restart the applications while leaving the SQL container running:

docker compose restart students-mvc grades-mvc

---

## 🔗 Port Mapping & Verification

Once the ecosystem is fully healthy, access your local browser loops:

* Students App: http://localhost:5001 (Internal Container Port: 8080)
* Grades App: http://localhost:5002 (Internal Container Port: 8080)
* SQL Server Instance: localhost,1433 (Internal Container Port: 1433)

### Verification Checklist:
1. Fire up http://localhost:5001 and create a couple of student logs.
2. Jump onto http://localhost:5002 and navigate to Assign New Grade. The drop-down list is dynamically fetching student contexts directly from the independent student container over the internal docker-bridge network!
3. Data Resilience Check: Shut the stack down using "docker compose down". Bring it back up using "docker compose up". Thanks to the named volume binding (mssql-data), your state remains pristine.
4. Fault Tolerance: Manually halt the students-mvc container. Reload the Grades index page. The service handles the communication blackout gracefully without causing an application crash.

---

## 📁 Repository Blueprint

* SchoolPortal/
  * SchoolPortal.StudentApp/ -> Students Service (Codebase, Migration context, Dockerfile, .dockerignore)
  * SchoolPortal.GradesApp/ -> Grades Service (HttpClient, Inter-service layer, Dockerfile, .dockerignore)
  * .env.example -> Shared environment boilerplate
  * .gitignore -> Git strict exclusions (Prevents tracking .env/caches)
  * docker-compose.yml -> Container configuration matrix
  * SchoolPortal.slnx -> Solution manifest file

---
👨‍💻 Developed by Ibrahim Zaher as part of modern infrastructure and backend engineering practice.