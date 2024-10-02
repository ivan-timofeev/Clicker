# Navigating the repository

```
root/
│
├── Clicker.Api/                   (Presentation Layer)
│   ├── Controllers/               (API or MVC controllers)
│   ├── Models/                    (ViewModels, DTOs)
│   └── MyProject.Presentation.csproj
│   
├── Clicker.Application/           (Application Layer)
│   ├── Services/                  (Application-specific services)
│   ├── Features/                  (Use cases, reusable business logic)
│   ├── Interfaces/                (Interfaces defining application services)
│   └── MyProject.Application.csproj
│
├── Clicker.Domain/                (Domain Layer)
│   ├── Entities/                  (Domain entities)
│   ├── Interfaces/                (Interfaces defining domain services)
│   ├── Constants/                 (Domain constants, exceptions, etc...)
│   └── MyProject.Domain.csproj
│
├── Clicker.Infrastructure/        (Infrastructure Layer)
│   ├── Data/                      (Data access, repositories)
│   ├── DependencyInjection/       
│   ├── Services/                  
│   └── MyProject.Infrastructure.csproj
│
├── Clicker.sln                     (Solution file)
└── README.md                       (Documentation)
```
