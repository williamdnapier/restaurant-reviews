# RestaurantReviews

## About this Api
RestaurantReviews is a .NET Core 5 RESTful Web Api written in C#. It is split into 8 different projects:

1. Api - controllers, infrastructure services and dockerfile
2. ApiTests - unit tests covering the Api controller actions
3. Data - EF Core datacontext and database initializer
4. DTO - DTO (data transfer models) which rely on Automapper
5. Models - entities/domain models
6. PrivateModels - private models to help with OOP inheritence
7. Services - repository type services which interact with Sql Server database
8. ServiceTests - unit tests covering services; using Moq to mock interface types

