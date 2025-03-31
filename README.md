# 🚀 Scalar .NET Core API Example  

## 📌 Overview  
This repository demonstrates a **.NET Core API** implementation using **Basic Authentication, API versioning, Swagger documentation, and memory caching**. It provides an example of how to build a scalable and well-structured API in .NET.  

## 🔑 Key Features  
✔️ **Basic Authentication** using `AuthenticationScheme`  
✔️ **API Versioning** with `Asp.Versioning`  
✔️ **Swagger & OpenAPI** Integration  
✔️ **Memory Caching** for performance optimization  
✔️ **Dependency Injection** with `IOptionsSnapshot`  

## 📂 Tech Stack  
- **Framework:** .NET Core  
- **Authentication:** Basic Authentication  
- **API Documentation:** Swagger (OpenAPI)  
- **Caching:** MemoryCache  
- **Configuration Management:** IOptionsSnapshot  

## 📜 API Modules  
- **Authentication** - Implements Basic Authentication  
- **Versioning** - Supports multiple API versions  
- **Swagger UI** - Provides API documentation  
- **Caching** - Optimizes data retrieval using MemoryCache  

## 🛠️ Installation & Setup  

### 1️⃣ Clone the Repository  
```bash
git clone https://github.com/raghusami/ScalarAPI.git
cd ScalarAPI
```
### 2️⃣ Run the API:
```bash
dotnet run
```
### 3️⃣ Access API Documentation:
Open your browser and navigate to:
```bash
http://localhost:47630/scalar/v1
```
### 🏗 API Versioning
This API implements versioning using Asp.Versioning.
By default, API version v1.0 is set, and versioning is managed via media type headers.

### 🔐 Authentication
Uses Basic Authentication (username:password encoded in Base64).
Include the Authorization header in API requests:
```bash
Authorization: Basic {base64(username:password)}
```
## ⭐ Star the Repository!
If you find this project useful, **give it a star ⭐ on GitHub!**

---
📌 **Created by [Raghu](https://github.com/raghusami)**
