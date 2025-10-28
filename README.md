# STANIL-DIMITROV-employees


This repository contains a full-stack application built with **ASP.NET Core** and **Angular**.  
The backend runs as an **in-memory ASP.NET Core API**, and the frontend is an Angular web application.

---

## 🧩 Backend (ASP.NET Core)

The backend is implemented using **ASP.NET Core** and uses **in-memory storage** — no external database setup is required.

### ▶️ How to Run the Backend

Follow these steps to start the backend API:

1. Open the solution in **Visual Studio**, or your preferred IDE. 

2. Build the solution to restore all dependencies.  
3. Run the project:
   - In **Visual Studio**: Press **F5** or click **Run**.
   - In the terminal: Navigate to the backend project folder and run:
     ```bash
     dotnet run
     ```
4. Once running, the API will be available at a URL similar to:
http://localhost:5001

✅ The backend is now running and ready to serve requests.

Swagger is used for API documentation.

---

## 💻 Frontend (Angular)

The frontend is an **Angular** application located in the `web-app` folder.

### ▶️ How to Start the Frontend

Follow these steps to run the frontend application:

1. Open a terminal in the **root directory** of the repository.  
2. Navigate to the Angular project folder:
```bash
cd web-app
Install all necessary npm dependencies:

bash
npm install
Start the Angular development server:

bash
ng serve
Once the application compiles successfully, open your browser and navigate to:

http://localhost:4200
The application will automatically reload whenever you make changes to the source files.

✅ The frontend is now running and connected to the backend.

⚙️ Notes
The backend uses in-memory data, so any data changes will be lost when the application restarts.

Always ensure the backend is running before starting the Angular app, so the frontend can communicate with the API correctly.

Both applications can run simultaneously on different ports (e.g., backend on 5001, frontend on 4200).

🛠️ Technologies Used
ASP.NET Core (In-Memory API)

Angular

TypeScript

HTML / CSS

# --- Run Frontend ---
cd web-app
npm install
ng serve

http://localhost:4200