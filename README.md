# Coding-Assessment

# Technical Specification

## Software Used

-   [SQL Server 2022 Developer](https://www.microsoft.com/en-za/sql-server/sql-server-downloads)
-   [Visual Studio 2019 Community](https://visualstudio.microsoft.com/downloads/)
-   [Node 18.10+](https://nodejs.org/en/download/)

# Instructions on how to run

-   Please ensure SQL Server is running on the host machine

## Frameworks

-   .NET Core
-   Entity Framework
-   Angular & Material

## Database

The database is manually created using a script (CreateDB.sql) located in the Scripts folder in the root of the project. The contents of the database are managed by the API and not manually maintained using scripts. <br>

The database consists of 2 tables for customers, namely:

-   [CRM].[Customer]
-   [CRM].[Addresses]

and a set of tables required by IdentityServer for authorization.

The CRM tables are are linked in a one (customer) to many (addresses) relationship, where one customer can have many addresses.
The Creation and maintenance of these tables is handled by Entity Framework and the migrations are created and stored in the
on the API to allow for easy development and rollbacks in case of system failures.

This decision also allows for easy recovery in case of critical system failure.

## API and Architecture

Located in the sub folder API is a .NET Core 8.0 WebAPI using Entity Core Framework & Identity Core Framework
The API is designed with clean architecture and KISS principle in mind and is split into 3 main segments:

<ul>
    <li>Controllers :<br>
    The "Experience Layer" which allows the api to be easily used by external applications
        <ul>
            <li> The Customer Controller which allows for Data viewing and editing on customer data by the company employee </li>
            <li> The Identity Controller which allows for employee registration and login </li>
        </ul>
    <li> Services : <br>
    The "Business Layer" which contains the business logic and is used to interact with the context (database)
        <ul> 
            <li> The Customer Service which implements the functionality of both Customer and Address interfaces </li>
        </ul>
    </li>
    <li> Context: <br>
    The "Data Layer" which contains the database context and migrations
        <ul>
            <li> TechSolutionsContext (Customer Context) which is the main connection responsible for handling the [CRM] Schema in the database </li>
            <li> IdentityContext which is the connection responsible for handling the Identity tables in the database </li>
        </ul>
    </li>
</ul>

This choice was made because it allowed me to build the API in a way that is is easy to work with and maintain,
as well as it allows to be easily extended in the future. The possible extensions that could be made to this Architecture
would be adding a repository tto allow abstraction on the Context layer to simplify code. From past experience I decided to
not add these right away because it could increase complexity and reduce simplicity if not required.

# Frontend

## Test User Details

email: demouser@test.com <br>
password: DemoTest1@
