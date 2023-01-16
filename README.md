## Brief description

### Backend application (WebApp)
Backend created on ASP NET CORE Framework version 5

Frameworks and patterns used:

1) Entity Framework an open source object–relational mapping (ORM) framework for ADO.NET.
2) CQRS Command and Query Responsibility Segregation, pattern that separates read and update operations for a data store.
3) FluentValidation is a .NET library for building strongly-typed validation rules.
4) MediatR Pattern/Library is used to reduce dependencies between objects/

### Frontend Application (FrontApp)

Framework and libraries used:

1) Angular a component-based framework for building scalable web applications.
2) Angular Material is a User Interface (UI) component library for Angular projects to speed up the development of elegant and consistent user interfaces.
3) Used Template driven forms.

## Installation

1) Install docker for Windows Desktop from https://www.docker.com/products/docker-desktop/
2) Run command from root folder:
> `docker-compose -f docker-compose.yaml up -d`
3) Install nodejs version 18.10.x from https://nodejs.org/en/download/releases/
4) Run command from frontend folder (FrontApp):
> `npm install`

## Start applications
1) Run command from backend folder (WebApp):
> `dotnet run`
2) Run command from frontend folder (FrontApp):
> `npm start`
3) Frontend application is running on port 4200
Load dataset.csv file after frontend starting by press button 

## Solution tree (Root folder):
```
├─── README.md - README file
├─── task.txt - task file
├─── dataset.csv - file upload to database 
├─── docker-compose.yaml - docker compose file
├─── EmployeeApp.sln - solution file
├─── .gitignore - ignore git file
├─── WebApp - backend project folder
|      ├── Controllers - controller folder
|      └── Infrastructure - filter folder
| 
│──── Core - DAL, CQRS, entities, models project folder
│      ├── DAL - data access layer folder
│      ├── Entities - entities for database folder
│      ├── Model - model folder
│      └── Requests - CQRS folder
|
├──── FrontApp - frontent project folder
│      ├── node_modules - packages folder
│      ├── src - angular code folder
│      ├── package.json node packages file
│      └──  README.md - README file
│   
├──── EmployeeApp.FunctionalTests - functional unit tests folder
│
├──── EmployeeApp.IntegrationTest - integration unit tests folder
│
├──── EmployeeApp.UnitTests - unit test folder
```    