# Project Management System

## Overview

Project Management System is a web application developed using ASP.NET Core Razor Pages, Entity Framework Core and SQLite database.

The application is designed to support project management by organizing projects, work packages, tasks, activities, employees and teams. It enables tracking of employee assignments and monitoring project progress through a structured hierarchy of entities.

This project was developed as part of the Software Engineering 2 (SI2) course.

---

## Main Features

### Project Management
- Create, edit, view and delete projects
- Track project status and duration
- Organize project data through work packages

### Work Package Management
- Create work packages within projects
- Define priorities and estimated workload
- Associate work packages with projects

### Task Management
- Create and manage tasks inside work packages
- Define deadlines and task status
- Track task completion progress

### Activity Management
- Create activities related to specific tasks
- Record performed activities
- Track activity execution dates

### Employee Management
- Store employee information
- Manage employee contact data
- Track employee assignments

### Team Management
- Create and manage teams
- Add employees to teams
- Organize workforce for project execution

### Assignment Tracking
- Assign activities to employees
- Monitor employee workload
- Track assigned days, months and years
- Record progress of assigned activities

---

## Technologies Used

### Backend
- ASP.NET Core Razor Pages
- C#
- Entity Framework Core

### Database
- SQLite

### Frontend
- HTML
- CSS
- Razor Pages

### Development Environment
- Visual Studio 2022

---

## Database Structure

The application uses the following entities:

### Project
Stores project information.

### WorkPackage
Represents work packages that belong to a project.

### Task
Represents tasks within a work package.

### Activity
Represents activities performed within a task.

### Employee
Stores employee information.

### Team
Stores team information.

### TeamMember
Represents the relationship between employees and teams.

### Assignment
Represents employee assignments to activities and tracks progress.

---

## Entity Relationships

Project
→ WorkPackages

WorkPackage
→ Tasks

Task
→ Activities

Employee
→ Assignments

Activity
→ Assignments

Team
→ TeamMembers

Employee
→ TeamMembers

---

## Project Architecture

The solution is organized into several components:

### DatabaseEntityLib
Contains entity classes and data models.

### DataBaseContext
Contains Entity Framework Core database context and relationship configuration.

### Pages
Contains Razor Pages used for application functionality.

### wwwroot
Contains static resources such as CSS files and images.

---

## Database

The repository includes a SQLite database file:

```
radnici.db
```

The application is configured to use this database automatically when started.

---

## Running the Application

### Clone the repository

```bash
git clone https://github.com/katarinapetrovicc/Project-Management-System.git
```

### Open the solution

Open:

```text
Projekat.sln
```

using Visual Studio 2022.

### Restore dependencies

Visual Studio will automatically restore NuGet packages.

### Run the application

Press:

```text
Ctrl + F5
```

or

```text
F5
```

to start the application.

---

## Course Requirements

The project satisfies the minimum course requirements:

- SQLite database
- Entity Framework Core
- ASP.NET Core Razor Pages
- More than 8 related database tables
- CRUD operations
- Data search and display functionality
- Employee and project management system



Faculty of Engineering, University of Kragujevac

Software Engineering 2 (SI2)
