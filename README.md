# Web-ProjecteSOS

## Overview
Web-ProjecteSOS is a web client built with ASP.NET Core Razor Pages for managing dishes, ingredients, and menus via a secure RESTful API. It provides user authentication and authorization using JWT tokens stored in cookies.

## Features
- **User Authentication**: Login and logout using cookie-based auth with JWT parsing
- **Ingredients Management**: Browse, add, edit, and delete ingredients
- **Dishes Management**: Browse, add, edit, and delete dishes
- **Menu Construction**: View and customize menus composed of selected dishes
- **Secure API Integration**: Typed HTTP clients (`AuthService`, `IngredientService`, `DishService`, `MenuService`) for all API calls
- **Role-Based Authorization**: Leverages ASP.NET Core policies for access control

## Technologies
- **.NET 9.0** with **ASP.NET Core Razor Pages**
- **C#** and **Razor** for server-side rendering
- **HttpClientFactory** for typed HTTP clients
- **System.IdentityModel.Tokens.Jwt** for JWT handling
- **Microsoft.AspNetCore.Authentication.Cookies** for cookie auth
- **Dependency Injection** via `builder.Services`
- **Configuration** using `appsettings.json`
- **Static Assets** under `wwwroot` (CSS, JS, libraries)
