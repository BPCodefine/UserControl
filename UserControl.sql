Create Database UserControl
Go
Use UserControl
Go
-- Users
CREATE TABLE Users (
  UserID INT PRIMARY KEY IDENTITY(1,1),
  Name VARCHAR(100),
  Email VARCHAR(100),
);

-- Roles
CREATE TABLE Roles (
  RoleID INT PRIMARY KEY IDENTITY(1,1),
  RoleName VARCHAR(50)
);

-- UserRoles (Many-to-Many)
CREATE TABLE UserRoles (
  UserID INT FOREIGN KEY (UserID) REFERENCES Users(UserID),
  RoleID INT FOREIGN KEY (RoleID) REFERENCES Roles(RoleID),
  PRIMARY KEY (UserID, RoleID)
);

-- Pages/Functions
CREATE TABLE AppPages (
  PageID INT PRIMARY KEY IDENTITY(1,1),
  PageName VARCHAR(100),
  AppName VARCHAR(100),
  Path VARCHAR(200)
);

-- Permissions
CREATE TABLE Permissions (
  PermissionID INT PRIMARY KEY IDENTITY(1,1),
  PageID INT FOREIGN KEY (PageID) REFERENCES AppPages(PageID),
  RoleID INT FOREIGN KEY (RoleID) REFERENCES Roles(RoleID),
  CanView BIT,
  CanEdit BIT,
  CanDelete BIT
);