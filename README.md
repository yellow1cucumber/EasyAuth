# EasyAuth

> Лёгкая библиотека для управления **аутентификацией и авторизацией по разрешениям** в ASP.NET Core.  
> Без лишней магии и сложных настроек — только ясный контроль над доступом.

---

## 📦 Установка

(Пока нет NuGet-пакета — просто добавьте файлы проекта EasyAuth в своё решение.)

---

## ⚙️ Быстрая настройка

1. Зарегистрируйте `EasyAuth` в `Program.cs`:

```csharp
builder.Services.AddAuthorization(options =>
{
    options.AddPermissionPolicy(Permissions.CreateRoles);
    options.AddPermissionPolicy(Permissions.EditPermissions);
    options.AddPermissionPolicy(Permissions.ManageUsers);
    options.AddPermissionPolicy(Permissions.ViewReports);
});

builder.Services.AddEasyAuth();
```

2. Включите middleware аутентификации и авторизации:

```csharp
app.UseAuthentication();
app.UseAuthorization();
```

3. В клаймах пользователя в токене должны быть указаны права:

```json
{
  "sub": "user1",
  "permission": [
    "permissions.create_roles",
    "permissions.edit_permissions",
    "permissions.view_reports"
  ]
}
```

Клаймы типа `permission` (можно переопределить через константу `EasyAuthDefaults.PermissionClaimType`).

---

## 🚀 Как использовать в коде

### Определите права (`Permissions.cs`):

```csharp
public static class Permissions
{
    public const string CreateRoles = "permissions.create_roles";
    public const string EditPermissions = "permissions.edit_permissions";
    public const string ViewReports = "permissions.view_reports";
    public const string ManageUsers = "permissions.manage_users";
}
```

---

### Ограничивайте доступ в контроллерах:

```csharp
using EasyAuth;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class RolesController : ControllerBase
{
    [AuthorizeByPermission(Permissions.CreateRoles)]
    [HttpPost]
    public IActionResult CreateRole()
    {
        return Ok("Role created!");
    }

    [AuthorizeByPermission(Permissions.EditPermissions)]
    [HttpPut("{roleId}")]
    public IActionResult EditRolePermissions(int roleId)
    {
        return Ok($"Permissions for role {roleId} updated!");
    }
}
```

> `[AuthorizeByPermission]` — красивый кастомный атрибут для проверки прав без явного указания полиси.

---

## 🔥 Принцип работы EasyAuth

1. **Определяете все разрешения на старте** в статическом классе `Permissions`.
2. **Регистрируете политики** для разрешений через `options.AddPermissionPolicy(...)`.
3. **Добавляете клаймы пользователю** — список разрешений в токене.
4. **Ограничиваете доступ к методам** через `[AuthorizeByPermission(...)]`.

---

## 📚 Работа с пользовательскими правами внутри метода

Хотите внутри метода проверить права напрямую?

```csharp
using EasyAuth;

[HttpGet]
public IActionResult CheckMyPermissions()
{
    var permissions = new UserPermissions(User);

    if (permissions.HasPermission(Permissions.ViewReports))
    {
        return Ok("You can view reports!");
    }

    return Forbid();
}
```

✅ Можно проверять наличие одного или нескольких разрешений прямо внутри действий.

---

## 📌 Плюсы EasyAuth

- **Статичное описание разрешений** — всегда известно на компайл-тайме.
- **Без сериализаций, БД-запросов, миграций** на старте.
- **Минимум кода** — всё работает через базовые механизмы ASP.NET.
- **Просто расширяется** под сложные требования.
- **Поддерживает любые схемы аутентификации** (`JWT`, `Cookies`, `IdentityServer`, `OAuth`, и др).

---

## ✍️ Пример токена для тестов

Если тестируете API через Postman или Swagger, вот пример правильной структуры токена:

```json
{
  "sub": "user1",
  "name": "John Doe",
  "permission": [
    "permissions.create_roles",
    "permissions.manage_users"
  ],
  "exp": 9999999999
}
```

---

## ❓ Частые вопросы

### Что если нужно другое имя клайма?
Переопределите:

```csharp
EasyAuthDefaults.PermissionClaimType = "your_custom_claim_type";
```

### Что если пользователь потерял права?
Просто не включайте нужный клайм в токен — доступ автоматически будет запрещен.

### Нужно ли хранить список прав в базе?
Нет. Все права известны в статическом классе `Permissions.cs`.

---

# ❤️ Contributing

Есть идеи, как сделать EasyAuth ещё лучше?  
Присылай pull request или создавай issue!

---

# 📜 Лицензия

MIT License — используй как хочешь.

---