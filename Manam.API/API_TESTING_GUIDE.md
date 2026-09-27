# Manam API Testing Guide

## Overview
This guide explains how to test the Manam API using the REST Client extension in VS Code with the `Manam.API.http` file.

## Prerequisites

### 1. Install REST Client Extension
- Open VS Code
- Go to Extensions (Ctrl+Shift+X)
- Search for "REST Client"
- Install the extension by Huachao Mao (Microsoft also publishes this extension)
- Restart VS Code

### 2. API Running
Ensure the Manam API is running:
```bash
cd C:\Users\srini\source\Repos\Manam
dotnet run --project Manam.API
```

The API should be available at: `https://localhost:7001` or `http://localhost:5000`

## File Location
```
Manam.API/Manam.API.http
```

## How to Use

### Opening the File
1. Open the `Manam.API.http` file in VS Code
2. You'll see clickable "Send Request" links above each request

### Running Requests
**Method 1: Click "Send Request"**
- Hover over any HTTP method line (POST, GET, etc.)
- Click the "Send Request" link that appears
- Response will open in a new tab on the right side

**Method 2: Keyboard Shortcut**
- Position cursor anywhere in a request block
- Press `Ctrl+Alt+R` (or `Cmd+Alt+R` on Mac)
- Response opens in new tab

**Method 3: Run All Requests**
- Press `Ctrl+Alt+L` to send all requests in sequence

## API Endpoints Overview

### 1. Authentication Endpoints (No Auth Required)

#### Register New User
```http
POST https://localhost:7001/api/v1/authentication/register
Content-Type: application/json

{
  "email": "user@example.com",
  "username": "username",
  "password": "SecurePassword@123",
  "firstName": "First",
  "lastName": "Last",
  "ipAddress": "192.168.1.1",
  "userAgent": "Mozilla/5.0..."
}
```

**Response:**
```json
{
  "success": true,
  "message": "User registered successfully",
  "data": {
    "userId": "550e8400-e29b-41d4-a716-446655440000",
    "email": "user@example.com",
    "username": "username"
  }
}
```

#### User Login
```http
POST https://localhost:7001/api/v1/authentication/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "SecurePassword@123",
  "ipAddress": "192.168.1.1",
  "userAgent": "Mozilla/5.0..."
}
```

**Response:**
```json
{
  "success": true,
  "message": "Login successful",
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "expiresIn": 3600,
    "userId": "550e8400-e29b-41d4-a716-446655440000"
  }
}
```

### 2. User Profile Endpoints (Requires JWT Token)

#### Get Current User Profile
```http
GET https://localhost:7001/api/v1/userprofile/me
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Response:**
```json
{
  "success": true,
  "message": "Profile retrieved successfully",
  "data": {
    "userId": "550e8400-e29b-41d4-a716-446655440000",
    "username": "username",
    "email": "user@example.com",
    "firstName": "First",
    "lastName": "Last",
    "isActive": true,
    "roles": ["User", "Admin"],
    "externalLogins": [
      {
        "provider": "Google",
        "providerDisplayName": "user@gmail.com"
      }
    ]
  }
}
```

#### Update User Profile
```http
PATCH https://localhost:7001/api/v1/userprofile/update
Content-Type: application/json
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...

{
  "firstName": "Updated First",
  "lastName": "Updated Last",
  "email": "updated@example.com"
}
```

#### Delete User Profile (Soft Delete)
```http
DELETE https://localhost:7001/api/v1/userprofile/delete
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

### 3. User Management Endpoints (Admin Only)

#### Get All Users
```http
GET https://localhost:7001/api/v1/users
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

#### Get User by ID
```http
GET https://localhost:7001/api/v1/users/550e8400-e29b-41d4-a716-446655440000
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

#### Create User
```http
POST https://localhost:7001/api/v1/users
Content-Type: application/json
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...

{
  "username": "newuser",
  "email": "newuser@example.com",
  "passwordHash": "$2a$11$...",
  "firstName": "New",
  "lastName": "User",
  "isActive": true
}
```

#### Update User
```http
PUT https://localhost:7001/api/v1/users/550e8400-e29b-41d4-a716-446655440000
Content-Type: application/json
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...

{
  "firstName": "Updated",
  "lastName": "User",
  "email": "updated@example.com",
  "isActive": true
}
```

#### Delete User (Soft Delete)
```http
DELETE https://localhost:7001/api/v1/users/550e8400-e29b-41d4-a716-446655440000
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

### 4. OAuth/External Login

#### Link Gmail Account
```http
POST https://localhost:7001/api/v1/authentication/link-external-login
Content-Type: application/json
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...

{
  "provider": "Google",
  "providerKey": "103849012345678901234",
  "providerDisplayName": "user@gmail.com"
}
```

#### Link Facebook Account
```http
POST https://localhost:7001/api/v1/authentication/link-external-login
Content-Type: application/json
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...

{
  "provider": "Facebook",
  "providerKey": "1234567890123456",
  "providerDisplayName": "User Name"
}
```

#### Unlink External Login
```http
DELETE https://localhost:7001/api/v1/authentication/unlink-external-login
Content-Type: application/json
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...

{
  "provider": "Google"
}
```

### 5. Account Lockout

#### Lock User Account
```http
POST https://localhost:7001/api/v1/users/550e8400-e29b-41d4-a716-446655440000/lockout
Content-Type: application/json
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...

{
  "lockoutDurationMinutes": 30,
  "reason": "Too many failed login attempts"
}
```

#### Unlock User Account
```http
POST https://localhost:7001/api/v1/users/550e8400-e29b-41d4-a716-446655440000/unlock
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

## Important Notes

### JWT Token Management
1. **Get Token**: First, run the Login endpoint to get a JWT token
2. **Copy Token**: Copy the `token` value from the response
3. **Update File**: Replace `eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...` with your actual token in all requests
4. **Token Expiry**: Tokens typically expire after 1 hour; login again if requests fail with 401

### Test Data
- **Sample Email**: john.doe@example.com
- **Sample Password**: SecurePassword@123
- **Sample User ID**: 550e8400-e29b-41d4-a716-446655440000 (replace with actual ID)

### Response Format
All API responses follow this format:
```json
{
  "success": true/false,
  "message": "Description of result",
  "data": { /* actual response data */ },
  "errors": { /* error details if any */ }
}
```

### HTTP Status Codes
- **200 OK**: Request successful
- **201 Created**: Resource created successfully
- **400 Bad Request**: Invalid request data
- **401 Unauthorized**: Missing or invalid JWT token
- **403 Forbidden**: Insufficient permissions
- **404 Not Found**: Resource not found
- **500 Internal Server Error**: Server error

## Troubleshooting

### Issue: "Cannot reach localhost:7001"
**Solution**: Ensure the API is running:
```bash
dotnet run --project Manam.API
```

### Issue: 401 Unauthorized on protected endpoints
**Solution**: 
1. Run the Login endpoint
2. Copy the JWT token from the response
3. Update the Authorization header with your token

### Issue: 403 Forbidden
**Solution**: The logged-in user may not have admin permissions. Use an admin account.

### Issue: Request timeout
**Solution**:
1. Check if API is running
2. Check network connectivity
3. Verify the base URL is correct

## Tips & Tricks

### Saving Variables
You can define variables at the top of the file:
```http
@baseUrl = https://localhost:7001
@token = eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
@userId = 550e8400-e29b-41d4-a716-446655440000
```

Then use them in requests:
```http
GET {{baseUrl}}/api/v1/users/{{userId}}
Authorization: Bearer {{token}}
```

### Viewing Response Details
After sending a request, you can see:
- **Response Status**: HTTP status code
- **Response Time**: How long the request took
- **Headers**: Response headers
- **Body**: JSON response body
- **Timeline**: Request timeline

### Copying Response Data
- Click on any JSON value to copy it
- Use response data in subsequent requests

## API Documentation
For detailed API documentation, refer to:
- OpenAPI/Swagger: `https://localhost:7001/swagger/index.html`
- API Controller comments in source code

## Database Tables
The API uses the following database tables:
- `ma_Users` - User accounts
- `ma_Roles` - User roles
- `ma_UserRoles` - User-Role assignments
- `ma_ExternalLogins` - OAuth accounts
- `ma_AuditLog` - Audit trail

## Support
For issues or questions, refer to:
- API logs in the console
- Swagger documentation
- Source code comments
- Database audit logs
