# Coding-Assessment

# Technical Specification

## Software Used

-   [SQL Server 2022 Developer](https://www.microsoft.com/en-za/sql-server/sql-server-downloads)
-   [Visual Studio 2019 Community](https://visualstudio.microsoft.com/downloads/)

# Instructions on how to run

-   Please ensure SQL Server is running on the host machine

## Frameworks

-   .NET Core
-   Entity Framework

## Database

The database consists of 2 tables, namely:

-   [CRM].[Customer]
-   [CRM].[Addresses]

And they are are linked in a one to many relationship, where one customer can have many addresses.
The Creation and maintenance of these tables is handled by Entity Framework and the migrations are created and stored in the
on the API to allow for easy development and rollbacks in case of system failures.

This decision also allows for easy recovery in case of critical system failure.

## API and Architecture

The API is designed with clean architecture and KISS principle in mind and is split into 3 main segments:

-   Controller - The "Experience Layer" which allows the api to be easily used by external applications
-   Service - The "Business Layer" which contains the business logic and is used to interact with the context (database)
-   Context - The "Data Layer" which contains the database context and the models used to interact with the database

This choice was made because it allowed me to build the API in a way that is is easy to work with and maintain,
as well as it allows to be easily extended in the future. The possible extensions that could be made to this Architecture
would be adding a repository tto allow abstraction on the Context layer to simplify code. From past experience I decided to
not add these right away because it could increase complexity and reduce simplicity if not required.

## Test User Details

email: demouser@test.com
password: DemoTest1@
