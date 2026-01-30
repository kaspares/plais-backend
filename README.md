# PLAIS Backend

A backend web API built with **ASP.NET Core 8.0**, providing RESTful endpoints for the website of **the Polish Chapter of the Association for Information Systems**.

# Table of Contents

- **[Getting Started](#getting-started)**
- **[Installing](#installing)**
- **[Project Overview](#project-overview)**
- **[Project Structure](#project-structure)**
- **[API Endpoints](#api-endpoints)**
- **[Auth Endpoints](#auth-endpoints)**
- **[Admin Endpoints](#admin-endpoints)**
- **[Achievement Endpoints](#achievement-endpoints)**
- **[Bulletin Endpoints](#bulletin-endpoints)**
- **[ByLaws Endpoints](#bylaws-endpoints)**
- **[Cadence Endpoints](#cadence-endpoints)**
- **[CurrentMember Endpoints](#currentmember-endpoints)**
- **[FoundingMembers Endpoints](#foundingmembers-endpoints)**
- **[ExecutiveMember Endpoints](#executivemember-endpoints)**
- **[EventGroup Endpoints](#eventgroup-endpoints)**
- **[Event Endpoints](#event-endpoints)**
- **[History Endpoints](#history-endpoints)**
- **[Image Endpoints](#image-endpoints)**
- **[MainPageCarousel Endpoints](#mainpagecarousel-endpoints)**
- **[MainPageText Endpoints](#mainpagetext-endpoints)**
- **[Resources Endpoints](#resources-endpoints)**

# Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. The project is designed to be opened and run using **Visual Studio**.

# Installing

## 1. Clone the repository:

```bash
git clone https://github.com/kaspares/plais-backend.git
```

## 2. Update connection string:

```json
  "ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DB;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
}

```

## 3. Restore NuGet packages

```bash
dotnet restore
```

## 4. Apply database migrations

This project uses **Entity Framework Core**.

```bash
dotnet ef database update
```

## 5. Run the project

1. Open the project in **Visual Studio**.
2. Select **IIS Express** in the toolbar.
3. Click the **Run** button.
4. The application should be launched in your default browser and be available at:

```
https://localhost:44340
```

# Project Overview

This ASP.NET Core 8 backend project powers the official website for [**PLAIS – the Polish Chapter of the Association of Information Systems**](https://plais.ug.edu.pl).  
It serves as the RESTful API layer for the PLAIS web application, providing data management, authentication, and administrative functionalities used by the Nuxt 3 frontend.

The backend ensures secure access to organizational content and supports CRUD operations across all website entities.

### Key Features:

- **RESTful API:** Provides endpoints for managing public content (events, achievements, members, resources, etc.) and private administrative data.

- **Authentication & Authorization:** Implements secure user management through **ASP.NET Identity**, allowing only authorized users to access the CMS API.

- **Content Management System (CMS) Backend:** Enables administrators to manage and update website content including events, bulletin posts, images, and text sections.

- **AutoMapper Integration:** Automatically maps entities to DTOs for clean and efficient data transfer between layers.

- **Swagger Documentation:** Built-in Swagger UI for easy API testing and exploration during development.

### Main Functional Areas:

- **Authentication:** Login, logout, and user management for CMS admins.
- **Events:** CRUD operations for event entries and event groups.
- **Achievements & Bulletin:** Manage public-facing academic and informational content.
- **Executive & Founding Members:** Manage lists of PLAIS members, both current and historical.
- **Resources:** Store and categorize external references, publications, and useful links.
- **Main Page Content:** Control homepage carousels, images, and text blocks.
- **Images & Uploads:** Support for uploading and managing images used across the site.

### Tech Stack:

- **Framework:** ASP.NET Core 8.0 (C#)
- **ORM:** Entity Framework Core
- **Authentication:** ASP.NET Core Identity
- **Mapping:** AutoMapper
- **Validation:** FluentValidation
- **Logging:** Serilog
- **Documentation:** Swagger
- **Configuration:** `appsettings.json`

## Project Structure

```
Plais/
 ├── Controllers/           # API endpoints
 ├── Data/                  # DbContext and related EF Core configuration
 ├── Services/              # Business logic
 ├── DTOs/                  # Request and response DTOs
 ├── Models/                # Entity Classes representing database tables
 ├── Middlewares/           # Custom middleware (error handling)
 ├── wwwroot/               # Static files (uploaded images)
 ├── appsettings.json       # Main configuration file
 ├── Program.cs             # Application entry point
 └── Plais.csproj           # Project file
```

# API Endpoints

Use Base URL: `https://localhost:44340`

## Auth

| Method   | Route              | Description                                                      |
| -------- | ------------------ | ---------------------------------------------------------------- |
| **POST** | `/api/auth/login`  | Logs in a user and returns a secure HTTPS authentication cookie  |
| **POST** | `/api/auth/logout` | Logs out the current user and clears the authentication cookie   |
| **GET**  | `/api/auth/me`     | Returns information about the current user using the auth cookie |

## Admin

| Method     | Route                                   | Description                        |
| ---------- | --------------------------------------- | ---------------------------------- |
| **GET**    | `/api/admins`                           | Returns all admins                 |
| **POST**   | `/api/admins`                           | Creates a new admin                |
| **PUT**    | `/api/admins/{userName}/reset-password` | Resets password for specific admin |
| **DELETE** | `/api/admins/{userName}`                | Deletes a specific admin           |

## Achievement

| Method     | Route                    | Description                  |
| ---------- | ------------------------ | ---------------------------- |
| **GET**    | `/api/achievements`      | Returns all achievements     |
| **POST**   | `/api/achievements`      | Creates a new achievement    |
| **GET**    | `/api/achievements/{id}` | Returns specific achievement |
| **PUT**    | `/api/achievements/{id}` | Updates an achievement       |
| **DELETE** | `/api/achievements/{id}` | Deletes an achievement       |

## Bulletin

| Method     | Route                      | Description                                                                        |
| ---------- | -------------------------- | ---------------------------------------------------------------------------------- |
| **GET**    | `/api/bulletin`            | Returns all bulletins (supports query parameters for pagination and search phrase) |
| **POST**   | `/api/bulletin`            | Creates a new bulletin                                                             |
| **GET**    | `/api/bulletin/{id}`       | Returns specific bulletin                                                          |
| **PUT**    | `/api/bulletin/{id}`       | Updates a bulletin                                                                 |
| **DELETE** | `/api/bulletin/{id}`       | Deletes a bulletin                                                                 |
| **GET**    | `/api/bulletin/latestFour` | Returns the 4 latest bulletins                                                     |

## ByLaws

| Method  | Route         | Description    |
| ------- | ------------- | -------------- |
| **GET** | `/api/byLaws` | Returns bylaws |
| **PUT** | `/api/byLaws` | Updates bylaws |

## Cadence

| Method     | Route                        | Description                   |
| ---------- | ---------------------------- | ----------------------------- |
| **GET**    | `/api/cadences`              | Returns all cadences          |
| **POST**   | `/api/cadences`              | Creates a new cadence         |
| **GET**    | `/api/cadences/{id}`         | Returns specific cadence      |
| **PUT**    | `/api/cadences/{id}`         | Updates a cadence             |
| **DELETE** | `/api/cadences/{id}`         | Deletes a cadence             |
| **GET**    | `/api/cadences/with-members` | Returns cadences with members |

## CurrentMember

| Method     | Route                      | Description                     |
| ---------- | -------------------------- | ------------------------------- |
| **GET**    | `/api/currentMembers`      | Returns all current members     |
| **POST**   | `/api/currentMembers`      | Creates a new current member    |
| **GET**    | `/api/currentMembers/{id}` | Returns specific current member |
| **PUT**    | `/api/currentMembers/{id}` | Updates current member          |
| **DELETE** | `/api/currentMembers/{id}` | Deletes current member          |

## ExecutiveMember

| Method     | Route                       | Description                       |
| ---------- | --------------------------- | --------------------------------- |
| **GET**    | `/api/executiveMember`      | Returns all executive members     |
| **POST**   | `/api/executiveMember`      | Creates a new executive member    |
| **GET**    | `/api/executiveMember/{id}` | Returns specific executive member |
| **PUT**    | `/api/executiveMember/{id}` | Updates executive member          |
| **DELETE** | `/api/executiveMember/{id}` | Deletes executive member          |

## FoundingMembers

| Method     | Route                       | Description                      |
| ---------- | --------------------------- | -------------------------------- |
| **GET**    | `/api/foundingMembers`      | Returns all founding members     |
| **POST**   | `/api/foundingMembers`      | Creates a new founding member    |
| **GET**    | `/api/foundingMembers/{id}` | Returns specific founding member |
| **PUT**    | `/api/foundingMembers/{id}` | Updates founding member          |
| **DELETE** | `/api/foundingMembers/{id}` | Deletes founding member          |

## Event

| Method     | Route                    | Description             |
| ---------- | ------------------------ | ----------------------- |
| **POST**   | `/api/events`            | Creates a new event     |
| **GET**    | `/api/events/{id}`       | Returns specific event  |
| **PUT**    | `/api/events/{id}`       | Updates event           |
| **DELETE** | `/api/events/{id}`       | Deletes event           |
| **GET**    | `/api/events/latestFour` | Returns 4 latest events |

## EventGroup

| Method     | Route                   | Description                  |
| ---------- | ----------------------- | ---------------------------- |
| **GET**    | `/api/eventGroups`      | Returns all event groups     |
| **POST**   | `/api/eventGroups`      | Creates new event group      |
| **GET**    | `/api/eventGroups/{id}` | Returns specific event group |
| **PUT**    | `/api/eventGroups/{id}` | Updates event group          |
| **DELETE** | `/api/eventGroups/{id}` | Deletes event group          |

## History

| Method  | Route          | Description             |
| ------- | -------------- | ----------------------- |
| **GET** | `/api/history` | Returns site history    |
| **PUT** | `/api/history` | Updates history content |

## Image

All `POST` endpoints below accept a file upload as part of the request.

| Method     | Route                         | Description                    |
| ---------- | ----------------------------- | ------------------------------ |
| **POST**   | `/api/Image/executive-member` | Uploads executive member image |
| **DELETE** | `/api/Image/executive-member` | Deletes executive member image |
| **POST**   | `/api/Image/bulletin`         | Uploads bulletin image         |
| **DELETE** | `/api/Image/bulletin`         | Deletes bulletin image         |
| **POST**   | `/api/Image/achievement`      | Uploads achievement image      |
| **DELETE** | `/api/Image/achievements`     | Deletes achievement image      |
| **POST**   | `/api/Image/carousel`         | Uploads carousel image         |
| **DELETE** | `/api/Image/carousel`         | Deletes carousel image         |
| **POST**   | `/api/Image/eventGroup`       | Uploads event group image      |
| **DELETE** | `/api/Image/eventGroup`       | Deletes event group image      |
| **DELETE** | `/api/Image/unused-images`    | Deletes all unused images      |

## MainPageCarousel

| Method     | Route                              | Description                  |
| ---------- | ---------------------------------- | ---------------------------- |
| **GET**    | `/api/mainPageCarouselImages`      | Returns all carousel images  |
| **POST**   | `/api/mainPageCarouselImages`      | Adds new carousel image      |
| **DELETE** | `/api/mainPageCarouselImages/{id}` | Deletes carousel image by ID |

## MainPageText

| Method  | Route                    | Description                        |
| ------- | ------------------------ | ---------------------------------- |
| **GET** | `/api/mainPageText`      | Returns all main page text entries |
| **GET** | `/api/mainPageText/{id}` | Returns specific main page text    |
| **PUT** | `/api/mainPageText/{id}` | Updates main page text             |

## Resources

| Method     | Route                                           | Description                                |
| ---------- | ----------------------------------------------- | ------------------------------------------ |
| **GET**    | `/api/Resources/categories`                     | Returns all resource categories            |
| **POST**   | `/api/Resources/categories`                     | Creates a new resource category            |
| **DELETE** | `/api/Resources/categories`                     | Deletes resource category (via query `id`) |
| **GET**    | `/api/Resources/categories/{id}`                | Returns resource category details          |
| **PUT**    | `/api/Resources/categories/{categoryId}`        | Updates resource category                  |
| **GET**    | `/api/Resources/categories/with-details`        | Returns resource categories with details   |
| **POST**   | `/api/Resources/categories/{categoryId}/groups` | Creates a group inside category            |
| **GET**    | `/api/Resources/groups/{id}/details`            | Returns group details                      |
| **PUT**    | `/api/Resources/groups/{groupId}`               | Updates resource group                     |
| **DELETE** | `/api/Resources/groups/{groupId}`               | Deletes resource group                     |
| **PUT**    | `/api/Resources/groups/{groupId}/with-items`    | Updates group and its items                |
| **POST**   | `/api/Resources/groups/{groupId}/items`         | Adds a new resource item                   |
| **PUT**    | `/api/Resources/items/{itemId}`                 | Updates resource item                      |
| **DELETE** | `/api/Resources/items/{itemId}`                 | Deletes resource item                      |

# Auth Endpoints

Authentication is handled using an HTTP-only cookie named .plais.auth, which contains a secure session token.
This cookie is set by the login endpoint, used automatically by the browser for authenticated requests, and cleared on logout.

```
POST /api/auth/login
```

Expected Request Body:

```json
{
  "username": "admin123",
  "password": "P@ssw0rd!"
}
```

Expected Response (200) with no body.

```
POST /api/auth/logout
```

No expected Request Body.

Expected Response (200) with no body.

```
GET /api/auth/me
```

No expected Request Body.

Expected Response (200):

```json
{
  "username": "admin"
}
```

# Admin Endpoints

```
GET /api/admins
```

No Expected Request Body.

Expected Response (200):

```json
[
  {
    "userName": "admin1",
    "email": "admin1@example.com"
  },
  {
    "userName": "admin2",
    "email": "admin2@example.com"
  }
]
```

```
POST /api/admins
```

Expected Request Body:

```json
{
  "userName": "newadmin",
  "password": "P@ssw0rd!"
}
```

Expected Response (201) with no body.

```
PUT /api/admins/{userName}/reset-password
```

Expected Request Body:

```json
{
  "newPassword": "NewP@ssw0rd!"
}
```

Expected Response (204) with no body.

```
DELETE /api/admins/{userName}
```

No expected Request Body.

Expected Response (204) with no body.

# Achievement Endpoints

```
GET /api/achievements
```

No expected Request Body.

Expected Response (200):

```json
[
  {
    "id": 1,
    "text": "Achievement1",
    "dateCreated": "2024-05-21T10:30:00Z",
    "link": "https://example.com/achievement",
    "images": [
      {
        "id": 1,
        "photoFileName": "achievement1.jpg",
        "achievementId": 1
      }
    ]
  },
  {
    "id": 2,
    "text": "Achievement2",
    "dateCreated": "2024-02-10T09:00:00Z",
    "link": null,
    "images": []
  }
]
```

```
POST /api/achievements
```

Expected Request Body:

```json
{
  "text": "New Achievement",
  "link": "https://example.com/newAchievement",
  "images": [
    {
      "photoFileName": "newAchievement.jpg"
    }
  ]
}
```

Expected Response (201):

```json
{
  "id": 3,
  "text": "New Achievement",
  "dateCreated": "2025-01-10T12:00:00Z",
  "link": "https://example.com/newAchievement",
  "images": [
    {
      "id": 10,
      "photoFileName": "newAchievement.jpg",
      "achievementId": 3
    }
  ]
}
```

```
GET /api/achievements/{id}
```

No expected Request Body.

Expected Response (200):

```json
{
  "id": 1,
  "text": "Achievement1",
  "dateCreated": "2024-05-21T10:30:00Z",
  "link": "https://example.com/achievement1",
  "images": [
    {
      "id": 1,
      "photoFileName": "achievement1.jpg",
      "achievementId": 1
    }
  ]
}
```

```
PUT /api/achievements/{id}
```

Expected Request Body:

```json
{
  "text": "Updated Achievement",
  "link": "https://example.com/updatedAchievement",
  "images": [
    {
      "photoFileName": "updatedAchievement.jpg"
    }
  ]
}
```

Expected Response (204) with no body.

```
DELETE /api/achievements/{id}
```

No expected Request Body.

Expected Response (204) with no body.

# Bulletin Endpoints

```
GET /api/bulletin
```

Query parameters supported:

- **SearchPhrase:** `string` - searches for bulletins with titles matching the provided phrase
- **PageNumber:** `int32` - specifies which page of results to retrieve
- **PageSize:** `int32` - sets the number of items returned per page

Request Example:

```
GET /api/bulletin?SearchPhrase=ExampleBulletin&PageNumber=1&PageSize=5
```

Expected Response (200):

```json
{
  "items": [
    {
      "id": 1,
      "title": "Bulletin1",
      "dateCreated": "2024-06-01T10:00:00Z",
      "content": "Bulletin1 Content Example",
      "photos": [
        {
          "id": 1,
          "photoFileName": "Bulletin1Photo1.jpg",
          "bulletinId": 1
        },
        {
          "id": 2,
          "photoFileName": "Bulletin1Photo2.jpg",
          "bulletinId": 1
        }
      ]
    },
    {
      "id": 2,
      "title": "Bulletin2",
      "dateCreated": "2024-03-15T12:00:00Z",
      "content": "Bulletin2 Content Example",
      "photos": []
    }
  ],
  "totalPages": 1,
  "totalItemsCount": 2,
  "itemsFrom": 1,
  "itemsTo": 5
}
```

```
POST /api/bulletin
```

Expected Request Body:

```json
{
  "title": "New Bulletin Title",
  "content": "New Bulletin Content",
  "photos": [
    {
      "photoFileName": "newBulletinPhoto.jpg"
    }
  ]
}
```

Expected Response (201):

```json
{
  "id": 2,
  "title": "New Bulletin Title",
  "dateCreated": "2024-03-15T12:00:00Z",
  "content": "New Bulletin Content",
  "photos": [
    {
      "id": 1,
      "photoFileName": "photoFileName": "newBulletinPhoto.jpg",
      "bulletinId": 2
    }
  ]
}
```

```
GET /api/bulletin/{id}
```

No expected Request Body.

Expected Response (200):

```json
{
  "id": 1,
  "title": "Bulletin1",
  "dateCreated": "2024-06-01T10:00:00Z",
  "content": "Bulletin1 content",
  "photos": [
    {
      "id": 1,
      "photoFileName": "bulletin1photo.jpg",
      "bulletinId": 1
    }
  ]
}
```

```
PUT /api/bulletin/{id}
```

Expected Request Body:

```json
{
  "title": "Updated Bulletin Title",
  "content": "Updated Bulletin Content",
  "photos": [
    {
      "photoFileName": "updatedBulletinPhoto.jpg"
    }
  ]
}
```

Expected Response (204) with no body.

```
DELETE /api/bulletin/{id}
```

No expected Request Body.

Expected response (204) with no body.

```
GET /api/bulletin/latestFour
```

No expected Request Body.

Expected Response (200):

```json
[
  [
    {
      "id": 4,
      "title": "Bulletin 4",
      "dateCreated": "2025-10-31T21:33:57.5823044"
    },
    {
      "id": 3,
      "title": "Bulletin 3",
      "dateCreated": "2025-10-31T21:33:05.1602971"
    },
    {
      "id": 2,
      "title": "Bulletin 2",
      "dateCreated": "2025-10-31T21:27:03.7609108"
    },
    {
      "id": 1,
      "title": "Bulletin 1",
      "dateCreated": "2025-07-15T22:39:13.1110359"
    }
  ]
]
```

# ByLaws Endpoints

```
GET /api/byLaws
```

No expected Request Body.

Expected Response (200):

```json
{
  "content": "Content of ByLaws."
}
```

```
PUT /api/byLaws
```

Expected Request Body:

```json
{
  "content": "Updated ByLaws Content."
}
```

Expected Response (204) with no body.

# Cadence Endpoints

```
GET /api/cadences
```

No expected Request Body.

Expected Response (200):

```json
[
  {
    "id": 1,
    "name": "Cadence 1",
    "position": 1,
    "memberIds": [1, 2]
  },
  {
    "id": 2,
    "name": "Cadence 2",
    "position": 2,
    "memberIds": [2, 3]
  }
]
```

```
POST /api/cadences
```

Expected Request Body:

```json
{
  "name": "New Cadence",
  "position": 1
}
```

Expected Response (201):

```json
{
  "id": 3,
  "name": "New Cadence",
  "position": 1,
  "memberIds": [4, 5]
}
```

```
GET /api/cadences/{id}
```

No expected Body Request.

Expected Respone(200):

```json
{
  "id": 2,
  "name": "Cadence 2",
  "position": 1,
  "memberIds": [2, 3]
}
```

```
PUT /api/cadences/{id}
```

Expected Request Body:

```json
{
  "name": "New Name of the Cadence",
  "position": 3
}
```

Expected Response (204) with no body.

```
DELETE /api/cadences/{id}
```

No expected Body Request.

Expected Response (204) with no body.

```
GET /api/cadences/with-members
```

No expected Request Body.

Expected Response (200):

```json
{
    "id": 1,
    "name": "Cadence 1",
    "position": 1,
    "members": [
      {
        "executiveMemberId": 1,
        "fullName": "Executive Member 1 Full Name",
        "department": "Executive Member 1 Department",
        "email": "executive1@example.com",
        "phone": "(+48) 123 456 789",
        "about": "",
        "role": "Executive Member 1 Role",
        "position": 1,
        "photoFileName": "ExecutiveMember1Photo.jpg"
      },
      {
        "executiveMemberId": 2,
        "fullName": "Executive Member 2 Full Name",
        "department": "Executive Member 2 Department",
        "email": "executive2@example.com",
        "phone": "(+48) 123 456 789",
        "about": "",
        "role": "Executive Member 2 Role",
        "position": 1,
        "photoFileName": "ExecutiveMember2Photo.jpg"
      },
    ]
  },
```

# CurrentMember Endpoints

```
GET /api/currentMembers
```

No expected Request Body.

Expected Response (200):

```json
[
  {
    "id": 1,
    "fullName": "Current Member 1",
    "email": "current.member1@example.com",
    "university": "University of Warsaw"
  },
  {
    "id": 2,
    "fullName": "Current Member 2",
    "email": "current.member2@example.com",
    "university": "University of Gdańsk"
  }
]
```

```
POST /api/currentMembers
```

Expected Request Body:

```json
{
  "fullName": "New Member",
  "email": "new.member@example.com",
  "university": "University of Kraków"
}
```

Expected Response (201):

```json
{
  "id": 3,
  "fullName": "New Member",
  "email": "new.member@example.com",
  "university": "University of Kraków"
}
```

```
GET /api/currentMembers/{id}
```

Expected Response (200):

```json
{
  "id": 2,
  "fullName": "Current Member 2",
  "email": "current.member2@example.com",
  "university": "University of Gdańsk"
}
```

```
PUT /api/currentMembers/{id}
```

Expected Request Body:

```json
{
  "fullName": "Updated Name",
  "email": "updated.email@example.com",
  "university": "Updated University"
}
```

Expected Response (204) with no body.

```
DELETE /api/currentMembers/{id}
```

No expected Request Body.

Expected Response (204) with no body.

# FoundingMembers Endpoints

```
GET /api/foundingMembers
```

No expected Request Body.

Expected Response (200):

```json
[
  {
    "id": 1,
    "fullName": "Founding Member 1",
    "email": "founder1@example.com",
    "university": "University of Warsaw"
  },
  {
    "id": 2,
    "fullName": "Founding Member 2",
    "email": "founder2@example.com",
    "university": "University of Gdańsk"
  }
]
```

```
POST /api/foundingMembers
```

Expected Request Body:

```json
{
  "fullName": "New Founding Member",
  "email": "new.founder@example.com",
  "university": "University of Kraków"
}
```

Expected Response (201):

```json
{
  "id": 3,
  "fullName": "New Founding Member",
  "email": "new.founder@example.com",
  "university": "University of Kraków"
}
```

```
GET /api/foundingMembers/{id}
```

No expected Request Body.

Expected Response (200):

```json
{
  "id": 2,
  "fullName": "Founding Member 2",
  "email": "founder2@example.com",
  "university": "University of Gdańsk"
}
```

```
PUT /api/foundingMembers/{id}
```

Expected Request Body:

```json
{
  "fullName": "Updated Founding Member",
  "email": "updated.founder@example.com",
  "university": "Updated University"
}
```

Expected Response (204) with no body.

```
DELETE /api/foundingMembers/{id}
```

No expected Request Body.

Expected Response (204) with no body.

# ExecutiveMember Endpoints

```
GET /api/executiveMember
```

No expected Request Body.

Expected Response (200):

```json
[
  {
    "id": 1,
    "email": "executive1@example.com",
    "phone": "(+48) 123 456 789",
    "about": "Short description about Executive Member 1.",
    "role": "President",
    "department": "Management",
    "fullName": "Executive Member 1 Full Name",
    "photoFileName": "ExecutiveMember1Photo.jpg"
  },
  {
    "id": 2,
    "email": "executive2@example.com",
    "phone": "(+48) 987 654 321",
    "about": "Short description about Executive Member 2.",
    "role": "Vice President",
    "department": "Operations",
    "fullName": "Executive Member 2 Full Name",
    "photoFileName": "ExecutiveMember2Photo.jpg"
  }
]
```

```
POST /api/executiveMember
```

Expected Request Body:

```json
{
  "fullName": "New Executive Member",
  "email": "new.executive@example.com",
  "phone": "(+48) 111 222 333",
  "about": "Short bio of the new executive member.",
  "role": "Treasurer",
  "department": "Finance",
  "photoFileName": "NewExecutivePhoto.jpg"
}
```

Expected Response (201):

```json
{
  "id": 3,
  "email": "new.executive@example.com",
  "phone": "(+48) 111 222 333",
  "about": "Short bio of the new executive member.",
  "role": "Treasurer",
  "department": "Finance",
  "fullName": "New Executive Member",
  "photoFileName": "NewExecutivePhoto.jpg"
}
```

```
GET /api/executiveMember/{id}
```

No expected Request Body.

Expected Response (200):

```json
{
  "id": 2,
  "email": "executive2@example.com",
  "phone": "(+48) 987 654 321",
  "about": "Short description about Executive Member 2.",
  "role": "Vice President",
  "department": "Operations",
  "fullName": "Executive Member 2 Full Name",
  "photoFileName": "ExecutiveMember2Photo.jpg"
}
```

```
PUT /api/executiveMember/{id}
```

Expected Request Body:

```json
{
  "fullName": "Updated Executive Member Name",
  "email": "updated.executive@example.com",
  "phone": "(+48) 444 555 666",
  "about": "Updated bio for the executive member.",
  "role": "Updated Role",
  "department": "Updated Department",
  "photoFileName": "UpdatedPhoto.jpg"
}
```

Expected Response (204) with no body.

```
DELETE /api/executiveMember/{id}
```

No expected Request Body.

Expected Response (204) with no body.

# EventGroup Endpoints

```
GET /api/eventGroups
```

Query parameters supported:

- **SearchPhrase:** `string` - searches for event groups whose titles or event names match the provided phrase
- **PageNumber:** `int32` - specifies which page of results to retrieve
- **PageSize:** `int32` - sets the number of items returned per page

Request Example:

```
GET /api/eventGroups?SearchPhrase=ExampleEvent&PageNumber=1&PageSize=5
```

Expected Response (200):

```json
{
  "items": [
    {
      "id": 1,
      "title": "Event Group 1",
      "photoFileName": "EventGroup1Photo.jpg",
      "events": [
        {
          "id": 1,
          "eventGroupId": 1,
          "name": "Event 1",
          "linkUrl": "https://conference1-example.com",
          "dateCreated": "2025-05-10T12:00:00Z"
        }
      ]
    },
    {
      "id": 2,
      "title": "Event Group 2",
      "photoFileName": "EventGroup2Photo.jpg",
      "events": [
        {
          "id": 2,
          "eventGroupId": 2,
          "name": "Event 2",
          "linkUrl": "https://conference2-example.com",
          "dateCreated": "2025-05-10T12:00:00Z"
        },
        {
          "id": 3,
          "eventGroupId": 2,
          "name": "Event 3",
          "linkUrl": "https://conference3-example.com",
          "dateCreated": "2025-05-10T12:00:00Z"
        }
      ]
    }
  ],
  "totalPages": 1,
  "totalItemsCount": 2,
  "itemsFrom": 1,
  "itemsTo": 5
}
```

```
POST /api/eventGroups
```

Expected Request Body:

```json
{
  "title": "New Event Group",
  "photoFileName": "newEventGroupPhoto.jpg"
}
```

Expected Response (201):

```json
{
  "id": 3,
  "title": "New Event Group",
  "photoFileName": "newEventGroupPhoto.jpg",
  "events": []
}
```

```
GET /api/eventGroups/{id}
```

No expected Request Body.

Expected Response (200):

```json
{
  "id": 1,
  "title": "Event Group 1",
  "photoFileName": "EventGroupPhoto.jpg",
  "events": [
    {
      "id": 1,
      "eventGroupId": 1,
      "name": "Example Conference",
      "linkUrl": "https://new-conference-example.com",
      "dateCreated": "2025-06-01T09:30:00Z"
    }
  ]
}
```

```
PUT /api/eventGroups/{id}
```

Expected Request Body:

```json
{
  "title": "Updated Event Group Name",
  "photoFileName": "updatedEventGroupPhoto.jpg"
}
```

Expected Response (204) with no body.

```
DELETE /api/eventGroups/{id}
```

No expected Request Body.

Expected Response (204) with no body.

# Event Endpoint

```
GET /api/events/{id}
```

No expected Request Body.

Expected Response (200):

```json
{
  "id": 1,
  "eventGroupId": 3,
  "name": "Conference 2025",
  "linkUrl": "https://conference-example.com",
  "dateCreated": "2025-05-10T12:00:00Z"
}
```

```
POST /api/events
```

Expected Request Body:

```json
{
  "eventGroupId": 1,
  "name": "New Conference",
  "linkUrl": "https://new-conference-example.com"
}
```

Expected Response (201):

```json
{
  "id": 2,
  "eventGroupId": 1,
  "name": "New Conference",
  "linkUrl": "https://new-conference-example.com",
  "dateCreated": "2025-06-01T09:30:00Z"
}
```

```
PUT /api/events/{id}
```

Expected Request Body:

```json
{
  "eventGroupId": 1,
  "name": "Updated Conference",
  "linkUrl": "https://updated-conference-example.com"
}
```

Expected Response (204) with no body.

```
DELETE /api/events/{id}
```

No expected Request Body.

Expected Response (204) with no body.

```
GET /api/events/latestFour
```

No expected Request Body.

Expected Response (200):

```json
[
  {
    "name": "Event 4",
    "dateCreated": "2025-10-01T12:00:00Z"
  },
  {
    "name": "Event 3",
    "dateCreated": "2025-09-12T09:00:00Z"
  },
  {
    "name": "Event 2",
    "dateCreated": "2025-07-20T15:00:00Z"
  },
  {
    "name": "Event 1",
    "dateCreated": "2025-06-10T08:00:00Z"
  }
]
```

# History Endpoints

```
GET /api/history
```

No expected Request Body.

Expected Response (200):

```json
{
  "content": "History Content"
}
```

```
PUT /api/history
```

Expected Request Body:

```json
{
  "content": "New History Content"
}
```

Expected Response (204) with no body.

# Image Endpoints

**POST Endpoints:**

```
POST /api/Image/executive-member
POST /api/Image/bulletin
POST /api/Image/achievement
POST /api/Image/carousel
POST /api/Image/eventGroup
```

All POST endpoints require sending a binary file in the request body under the field name File (using multipart/form-data).

All POST endpoints are expected to return a 201 status with the following response:

```json
{
{
  "fileName": "NewPhoto.jpg",
  "url": "/uploads/achievements/newPhoto.jpg"
}
}
```

**DELETE Endpoints:**

```
DELETE /api/Image/executive-member
DELETE /api/Image/bulletin
DELETE /api/Image/achievements
DELETE /api/Image/carousel
DELETE /api/Image/eventGroup
DELETE /api/Image/unused-images
```

All DELETE endpoints **(except /api/Image/unused-images)** require specifying the file name to delete using the query parameter fileName.

The endpoint **/api/Image/unused-images** does not require any parameters and deletes all unused images from the system.

Request Example:

```
DELETE /api/Image/bulletin?fileName=example.jpg
```

All DELETE endpoints are expected to return a 204 status with no response body.

# MainPageCarousel Endpoints

```
GET /api/mainPageCarouselImages
```

No expected Request Body.

Expected Response (200):

```json
[
  {
    "id": 1,
    "photoFileName": "CarouselPhoto1.jpg"
  },
  {
    "id": 2,
    "photoFileName": "CarouselPhoto2.jpg"
  },
  {
    "id": 3,
    "photoFileName": "CarouselPhoto3.jpg"
  },
  {
    "id": 4,
    "photoFileName": "CarouselPhoto4.jpg"
  }
]
```

```
POST /api/mainPageCarouselImages
```

Expected Request Body:

```json
{
  "photoFileName": "NewPhotoFileName"
}
```

Expected Response (201) with no body.

```
DELETE /api/mainPageCarouselImages/{id}
```

No expected Request Body.

Expected Response (204) with no body.

# MainPageText Endpoints

```
GET /api/mainPageText
```

No expected Request Body.

Expected Response (200):

```json
[
  {
    "id": 1,
    "text": "MainPageText1 Content."
  },
  {
    "id": 2,
    "text": "MainPageText2 Content."
  }
]
```

```
GET /api/mainPageText/{id}
```

No expected Request Body.

Expected Response (200):

```json
  {
    "id": 1,
    "text": "MainPageText1 Content."
  },
```

```
PUT /api/mainPageText/{id}
```

Expected Request Body:

```json
{
  "text": "New MainPageText Content."
}
```

Expected Response (204) with no body.

# Resources Endpoints

```
GET /api/Resources/categories
```

No expected Request Body.

Expected Response (200):

```json
[
  {
    "id": 1,
    "name": "Resource Category 1",
    "description": "Description for Resource Category 1"
  },
  {
    "id": 2,
    "name": "Resource Category 2",
    "description": "Description for Resource Category 2"
  }
]
```

```
GET /api/Resources/categories/{id}
```

No expected Request Body.

Expected Response (200):

```json
{
  "id": 1,
  "name": "Resource Category 1",
  "description": "Description for Resource Category 1",
  "groups": [
    {
      "id": 1,
      "name": "Resource Group 1",
      "items": [
        {
          "id": 1,
          "name": "Resource Item 1",
          "linkUrl": "https://example.com/resource-item-1"
        }
      ]
    }
  ]
}
```

```
GET /api/Resources/categories/with-details
```

No expected Request Body.

Expected Response (200):

```json
[
  {
    "id": 1,
    "name": "Resource Category 1",
    "description": "Category with detailed groups and items",
    "groups": [
      {
        "id": 1,
        "name": "Group 1",
        "items": [
          {
            "id": 1,
            "name": "Item 1",
            "linkUrl": "https://example.com/item1"
          }
        ]
      }
    ]
  }
]
```

```
POST /api/Resources/categories
```

Expected Request Body:

```json
{
  "name": "New Resource Category",
  "description": "Category description"
}
```

Expected Response (201):

```json
{
  "id": 3,
  "name": "New Resource Category",
  "description": "Category description"
}
```

```
PUT /api/Resources/categories/{categoryId}
```

Expected Request Body:

```json
{
  "name": "Updated Resource Category Name",
  "description": "Updated description"
}
```

Expected Response (204) with no body.

```
DELETE /api/Resources/categories
```

Expected Query Parameter:

- **id**: `int32` - id of the category to be deleted.

Request Example:

```
/api/Resources/categories?id=1
```

Expected Response (204) with no body.

```
POST /api/Resources/categories/{categoryId}/groups
```

Expected Request Body:

```json
{
  "name": "New Resource Group",
  "description": "Description for resource group"
}
```

Expected Response (201):

```json
{
  "id": 1,
  "name": "New Resource Group",
  "description": "Description for resource group"
}
```

```
GET /api/Resources/groups/{id}/details
```

No expected Request Body.

Expected Response (200):

```json
{
  "id": 1,
  "name": "Resource Group 1",
  "description": "Details about Resource Group 1",
  "items": [
    {
      "id": 1,
      "name": "Resource Item 1",
      "linkUrl": "https://example.com/resource-item-1"
    }
  ]
}
```

```
PUT /api/Resources/groups/{groupId}
```

Expected Request Body:

```json
{
  "name": "Updated Resource Group Name",
  "description": "Updated description for resource group"
}
```

Expected Response (204) with no body.

```
DELETE /api/Resources/groups/{groupId}
```

No expected Request Body.

Expected Response (204) with no body.

```
PUT /api/Resources/groups/{groupId}/with-items
```

Expected Request Body:

```json
{
  "groupName": "Updated Resource Group Name",
  "items": [
    {
      "id": 1,
      "name": "Updated Resource Item 1",
      "linkUrl": "https://example.com/updated-item1"
    },
    {
      "id": 2,
      "name": "Updated Resource Item 2",
      "linkUrl": "https://example.com/updated-item2"
    }
  ]
}
```

Expected Response (204) with no body.

```
POST /api/Resources/groups/{groupId}/items
```

Expected Request Body:

```json
{
  "name": "New Resource Item",
  "description": "Description for resource item",
  "linkUrl": "https://example.com/resource-item"
}
```

Expected Response (201):

```json
{
  "id": 3,
  "name": "New Resource Item",
  "description": "Description for resource item",
  "linkUrl": "https://example.com/resource-item"
}
```

```
PUT /api/Resources/items/{itemId}
```

Expected Request Body:

```json
{
  "name": "Updated Resource Item Name",
  "description": "Updated description",
  "linkUrl": "https://example.com/updated-item"
}
```

Expected Response (204) with no body.

```
DELETE /api/Resources/items/{itemId}
```

No expected Request Body.

Expected Response (204) with no body.
