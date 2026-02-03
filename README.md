# insurance-policy-management
Develop a web-app that allow management of clients y their insurance policies, demo using Angular and ASP.NET Core


Demo application built with:
- Angular
- ASP.NET Core (.NET 8)
- Azure SQL
- Entity Framework Core
- Clean Architecture

## Features
- Client and Policy management
- Role-based access (Admin / Client)
- REST API with Swagger
- EF Core migrations
- Cloud-ready setup

## Architecture
Clean Architecture:
- Domain
- Application
- Infrastructure
- API

insurance-policy-management/
├── src/                      # Backend (.NET)
│   ├── InsurancePolicyManagement.Api
│   ├── InsurancePolicyManagement.Application
│   ├── InsurancePolicyManagement.Infrastructure
│   └── InsurancePolicyManagement.Domain
│
├── tests/                    # Unit & Integration Tests
│   ├── InsurancePolicyManagement.UnitTests
│   └── InsurancePolicyManagement.IntegrationTests
│
├── FrontEnd/                 # Angular application
│
├── .github/workflows/        # CI/CD pipelines
│
└── README.md

## Running Locally
Backend
dotnet restore
dotnet run --project src/InsurancePolicyManagement.Api


Swagger will be available at:

https://localhost:<port>/swagger

Frontend
cd FrontEnd
npm install
ng serve


Angular app will be available at:

http://localhost:4200


Demo Credentials
ADMIN
Email: admin@insurance.com
Password: Admin123!

CLIENT
Email: client@insurance.com
Password: Client123!

## Future Improvements

- Policy lifecycle events with messaging (RabbitMQ)

- Redis caching for policy draft generation

- Audit logging

- UI enhancements

## Author Notes

This project was designed to demonstrate real-world development practices, focusing on maintainability, security, scalability, and deployment readiness.
