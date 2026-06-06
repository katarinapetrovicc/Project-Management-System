# Project Management System

## Project Overview

Project Management System is a web application developed using ASP.NET Core Razor Pages, Entity Framework Core, and SQLite. The application is designed to support project planning, organization, and monitoring by managing projects, work packages, tasks, activities, employees, teams, and assignments.

The system enables users to create and manage projects, organize work into work packages and tasks, assign activities to employees, track progress, upload and download files, and manage team structures through an intuitive web interface.

---

## Technologies Used

- ASP.NET Core Razor Pages
- C#
- Entity Framework Core
- SQLite
- HTML5
- CSS3
- Bootstrap
- Visual Studio 2022

---

## System Features

The application provides management of the following entities:

### Projects
Projects contain basic information such as:
- Project name
- Description
- Start date
- End date
- Budget
- Status

### Work Packages
Work packages represent logical parts of a project and include:
- Name
- Description
- Planned days
- Priority
- Associated project
- File attachment support

### Tasks
Tasks belong to work packages and include:
- Name
- Description
- Planned hours
- Actual hours
- Deadline
- Status

### Activities
Activities represent individual work actions performed within tasks and include:
- Name
- Description
- Planned hours
- Actual hours
- Date performed
- File attachment support

### Employees
Employee management includes:
- First name
- Last name
- Position
- Email
- Phone number
- Profile image
- Search functionality

### Teams
Teams can be created and managed with:
- Team name
- Team logo
- Team membership management

### Team Members
Team members connect employees and teams through:
- Team assignment
- Employee assignment
- Role within the team

### Assignments
Assignments are used to track employee participation in activities:
- Assigned days
- Month
- Year
- Progress percentage

---

## Database Structure

The application uses a relational SQLite database containing the following tables:

- Projects
- WorkPackages
- Tasks
- Activities
- Employees
- Teams
- TeamMembers
- Assignments

The database is managed using Entity Framework Core and includes relationships between all entities.

---

## Entity Relationships

The system implements the following relationships:

- One Project can contain multiple Work Packages
- One Work Package can contain multiple Tasks
- One Task can contain multiple Activities
- One Employee can participate in multiple Teams
- One Team can contain multiple Employees
- One Employee can be assigned to multiple Activities
- Assignments track employee progress on activities

---

## Application Modules

The application consists of the following modules:

1. Project Management
2. Work Package Management
3. Task Management
4. Activity Management
5. Employee Management
6. Team Management
7. Team Member Management
8. Assignment Management

Each module supports full Create, Read, Update, and Delete (CRUD) functionality.

---

## Key Features

- CRUD operations for all entities
- SQLite database support
- Entity Framework Core integration
- Project planning and tracking
- Employee and team management
- Task and activity monitoring
- Assignment and progress tracking
- Employee search functionality
- File upload and download support
- Employee profile image management
- Team logo management
- Relational database design

---

## Running the Application

1. Clone the repository:

```bash
git clone https://github.com/katarinapetrovicc/Project-Management-System.git
```

2. Open the solution in Visual Studio 2022.

3. Restore NuGet packages.

4. Build the solution.

5. Run the application.

The application will start locally and automatically connect to the included SQLite database.

---

## Database

The application uses a SQLite database (`radnici.db`) included in the repository.

The database already contains sample data, allowing the application to be used immediately without additional configuration.

