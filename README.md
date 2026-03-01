Project in active development

# BarberShop Management System

Appointment management system for a barbershop built with ASP.NET Core using Clean Architecture principles.

---

## Technologies

- ASP.NET Core Web API (.NET 8)
- Clean Architecture
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger
- Dependency Injection
- Global Exception Handling Middleware

---

## Architecture

The project follows Clean Architecture principles:

- **Domain** → Business entities and core rules  
- **Application** → Use cases and application logic  
- **Infrastructure** → Persistence, repositories, EF Core configuration  
- **API** → Controllers, middleware, authentication  

The architecture promotes:

- Separation of concerns  
- Testability  
- Scalability  
- Maintainability  

---
## Authentication & Security

- JWT-based authentication
- Secure password hashing
- Unique email constraint at database level
- Global exception middleware (no stack trace exposure in production)
- Restricted delete behaviors to prevent cascade data loss

---

##  How to run the project locally

1. Clone the repository
2. Open the solution in Visual Studio
3. Run the `BarberShop.API` project
4. Swagger will open automatically in your browser

---

## Project goal

Educational project to demonstrate enterprise-level backend development using .NET, Clean Architecture, and cloud-ready design.

---


## Current features
-User registration
-User authentication (JWT)
-Business management
-Barber management
-Services management
-Appointment entity structure
-Global error handling

---

## Upcoming Features

- Online payment integration
- Google sign-up and sign-in (OAuth 2.0)
- Advanced role and permission system
- Appointment notifications
- Cloud deployment

---

# 🇪🇸 Versión en Español

Sistema de gestión de citas para barbería desarrollado con ASP.NET Core usando principios de Clean Architecture.

## Tecnologías

- ASP.NET Core Web API (.NET 8)
- Clean Architecture
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger
- Inyección de dependencias
- Middleware global de manejo de excepciones

## Arquitectura

El proyecto sigue los principios de Clean Architecture:

- Domain → Entidades y reglas de negocio
- Application → Casos de uso y lógica de aplicación
- Infrastructure → Persistencia y servicios externos
- API → Controladores y endpoints HTTP

Promueve:

-Separación de responsabilidades
-Escalabilidad
-Mantenibilidad
-Facilidad de pruebas

## Cómo ejecutar el proyecto

Autenticación basada en JWT

Hash seguro de contraseñas

Email único a nivel de base de datos

No se exponen detalles internos en producción

Relaciones configuradas explícitamente en EF Core

## Cómo ejecutar el proyecto

1. Clonar el repositorio
2. Abrir la solución en Visual Studio
3. Ejecutar el proyecto BarberShop.API
4. Swagger se abrirá automáticamente en el navegador

## Objetivo del proyecto

Proyecto educativo para demostrar desarrollo backend profesional con .NET, arquitectura limpia y preparación para la nube.

## Funcionalidades actuales
- Registro de usuarios
- Autenticación de usuarios (JWT)
- Gestión de negocios
- Gestión de barberos
- Gestión de servicios
- Estructura de la entidad de citas
- Manejo global de errores

## Próximas funcionalidades

- Integración de pagos en línea
- Registro e inicio de sesión con Google (OAuth 2.0)
- Sistema de roles y permisos más avanzado
- Notificaciones de citas
- Despliegue en la nube

