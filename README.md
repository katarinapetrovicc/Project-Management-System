# Project Management System

## Project Overview

Project Management System is a web application developed using ASP.NET Core Razor Pages, Entity Framework Core, and SQLite database.

The application enables efficient management and monitoring of projects through project planning, work package organization, task tracking, activity management, employee administration, team coordination, and assignment monitoring.

The system is designed to support project lifecycle management, employee workload tracking, and project documentation management.

This project was developed as part of the **Software Engineering 2** course.

---

## Technologies Used

- ASP.NET Core Razor Pages
- C#
- Entity Framework Core
- SQLite
- HTML
- CSS
- Bootstrap
- Visual Studio 2022

---

## System Features

### Project Management

- Create new projects
- Edit existing projects
- View project details
- Delete projects
- Manage project budgets, statuses, and timelines

### Work Package Management

- Create and organize work packages
- Connect work packages to projects
- Define priorities and planned duration
- Upload and download project documentation

### Task Management

- Create tasks within work packages
- Track planned and actual working hours
- Manage deadlines
- Monitor task status and progress

### Activity Management

- Create activities related to tasks
- Track planned and actual hours
- Record activity execution dates
- Upload and download activity attachments

### Employee Management

- Add, edit, and remove employees
- Store employee profile images
- Search employees by name
- Manage employee contact information and positions

### Team Management

- Create and manage teams
- Upload team logos
- Organize employees into teams

### Team Member Management

- Assign employees to teams
- Define employee roles within teams
- Manage team composition

### Assignment Management

- Assign activities to employees
- Track assigned working days
- Monitor activity completion percentage
- Track employee workload by month and year

---

## Database Structure

The application uses a SQLite database consisting of the following entities:

- Project
- WorkPackage
- Task
- Activity
- Employee
- Team
- TeamMember
- Assignment

Relationships between entities are implemented to support project organization, task management, employee assignments, and team collaboration.

---

## Entity Relationships

Project
└── WorkPackage  
&nbsp;&nbsp;&nbsp;&nbsp;└── Task  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;└── Activity  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;└── Assignment  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;└── Employee

Team  
└── TeamMember  
&nbsp;&nbsp;&nbsp;&nbsp;└── Employee

---

## Application Modules

### Projects

Manage project information including:

- Name
- Description
- Budget
- Start Date
- End Date
- Status

### Work Packages

Manage:

- Planned Days
- Priority
- Documentation Attachments
- Related Project

### Tasks

Manage:

- Planned Hours
- Actual Hours
- Deadlines
- Status
- Related Work Package

### Activities

Manage:

- Planned Hours
- Actual Hours
- Date Performed
- Related Task
- Attachments

### Employees

Manage:

- Profile Images
- Contact Information
- Job Position

### Teams

Manage:

- Team Logos
- Team Organization

### Team Members

Manage:

- Employee-Team Relationships
- Team Roles
- Team Composition

### Assignments

Track:

- Employee Assignments
- Assigned Days
- Progress Percentage
- Workload Distribution

---

## Key Features

✔ Full CRUD operations

✔ SQLite database integration

✔ Entity Framework Core ORM

✔ Multiple related entities

✔ File upload and download support

✔ Employee profile image management

✔ Team logo management

✔ Employee search functionality

✔ Progress tracking

✔ Assignment management

✔ Relational database design

---

## Application Pages

The application contains dedicated pages for:

- Projects
- Work Packages
- Tasks
- Activities
- Employees
- Teams
- Team Members
- Assignments

Each page provides complete Create, Read, Update, and Delete (CRUD) functionality.

---

## Running the Application

1. Clone the repository:

```bash
git clone https://github.com/katarinapetrovicc/Project-Management-System.git
```

2. Open `Projekat.sln` in Visual Studio 2022.

3. Restore NuGet packages.

4. Build and run the application.

5. The application will start locally and can be accessed through the browser.

---

## Database

The SQLite database file (`radnici.db`) is included in the repository and already contains sample data for testing the application.

No additional database configuration is required.
