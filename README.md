# 📖 Blog.API

A simple **Blog REST API** built with **.NET 7**, **Entity Framework Core**, and **PostgreSQL**.  
This project demonstrates clean architecture, DTO usage, database seeding, and CRUD endpoints for **Posts, Comments, and Tags**.

---

## 🚀 Features
- ✅ CRUD operations for Posts  
- ✅ CRUD operations for Comments  
- ✅ CRUD operations for Tags  
- ✅ Entity relationships (Posts ↔ Tags, Posts ↔ Comments)  
- ✅ Database seeding with sample data  
- ✅ Swagger UI documentation  
- ✅ PostgreSQL with Entity Framework Core  

---

## 🛠️ Technologies
- [.NET 7](https://dotnet.microsoft.com/)  
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)  
- [PostgreSQL](https://www.postgresql.org/)  
- [Swagger / Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)  

---

## ⚡ Getting Started

### 1. Clone the repo
```bash
git clone https://github.com/YOUR_USERNAME/Blog.API.git
cd Blog.API


2. Update connection string
Edit appsettings.json:
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=blogdb;Username=postgres;Password=yourpassword"
}

3. Apply migrations
dotnet ef database update

4. Run the project
dotnet run

📌 Example Requests

Create a Post
curl -X 'POST' \
  'http://localhost:5000/api/posts' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
    "title": "My first post",
    "content": "Hello from Blog.API!",
    "tagIds": [1, 2]
  }'

curl -X 'GET' 'http://localhost:5000/api/posts' -H 'accept: application/json'


📂 Database Diagram
erDiagram
    Post ||--o{ Comment : has
    Post ||--o{ PostTag : tagged
    Tag  ||--o{ PostTag : belongs

    Post {
      int Id
      string Title
      string Content
      datetime CreatedAtUtc
    }

    Comment {
      int Id
      string Author
      string Body
      datetime CreatedAtUtc
      int PostId
    }

    Tag {
      int Id
      string Name
    }

    PostTag {
      int PostId
      int TagId
    }

📜 License

This project is licensed under the MIT License.