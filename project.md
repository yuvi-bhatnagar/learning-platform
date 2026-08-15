# Project Context

## Goal

Build a web application using:

- ASP.NET Core MVC
- Razor Views
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- Bootstrap
- JavaScript / jQuery / AJAX
- Redis
- Docker for local development

Keep the application as a simple, maintainable monolithic MVC application. Do not introduce unnecessary complexity such as microservices.

---

# User Roles

There are 4 roles:

- Super Admin
- Admin
- Teacher
- Student

---

# Super Admin

Access:

`/admin`

Responsibilities:

- Login
- Create Admin
- Create Super Admin
- Manage Admins
- Manage Teachers
- Manage Students
- View upcoming and past lectures
- View teacher lecture reports
- View student feedback and ratings
- Perform general administrative actions

---

# Admin

Access:

`/admin`

Responsibilities:

- Manage Teachers
  - Create
  - Edit
  - View
  - Activate/Deactivate
- Manage Students
  - View
  - Edit
  - Activate/Deactivate
  - Student creation is handled through student registration
- View upcoming and past lectures
- View lecture reports
- View student feedback and ratings
- Perform general administrative actions

---

# Teacher

Access:

`/teacher`

Responsibilities:

- Login
- Manage availability
- View upcoming lectures
- View past lectures
- View lecture details
- Manage lecture status:
  - Pending
  - Completed
  - Missed
  - Cancelled
- Submit lecture report
- View ratings and feedback from students

Lecture status rules:

- Pending → Completed
- Pending → Missed
- Pending → Cancelled
- Completed cannot be changed
- Missed cannot be changed
- Cancelled cannot be changed

Lectures are one-to-one between one teacher and one student.

Each lecture duration is 30 minutes.

---

# Student

Access:

`/student`

Responsibilities:

- Register
- Login
- View teachers
- View teacher details
- View teacher rating and feedback
- Select a teacher
- Select date/time
- View available 30-minute slots
- Book a lecture
- View upcoming lectures
- View past lectures
- View lecture details
- Give rating and feedback after a completed lecture

Booking requirements:

- Student can only book an available teacher slot
- Slot duration is 30 minutes
- A slot cannot be booked twice
- Teacher and student cannot have overlapping lectures

Feedback requirements:

- Only the student of that lecture can submit feedback
- Feedback can only be submitted after a completed lecture
- One feedback per lecture

---

# Main Entities

Initial entities:

- ApplicationUser
- TeacherProfile
- StudentProfile
- TeacherAvailability
- Lecture
- LectureReport
- TeacherFeedback

Use ASP.NET Core Identity for authentication and roles.

---

# Redis

Use Redis for local development through Docker.

Run Redis using Docker Compose.

Redis should be available as a local infrastructure dependency and accessed through configuration.

Keep Redis usage simple initially. It may later be used for:

- Caching
- Temporary data
- Session-related data if required
- Frequently accessed teacher/availability data

Do not add Redis-dependent business logic until it is actually needed.

---

# Docker

Use Docker for local development infrastructure.

Initially use Docker Compose for:

- Redis
- SQL Server if practical for the development environment

The ASP.NET Core application can run locally from Visual Studio / CLI while connecting to the Docker containers.

Connection details should come from configuration and environment variables.

Do not hard-code connection strings or Redis settings.

---

# Recommended Structure

Start with a single ASP.NET Core MVC project.

Basic structure:

```text
Controllers/
Areas/
    Admin/
    Teacher/
    Student/

Models/
    Entities/
    ViewModels/

Data/

Services/
Interfaces/

Views/

wwwroot/
    css/
    js/
    images/

Tests/