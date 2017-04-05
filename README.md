# BeetrootPortfolio
## Short description
This project provide very easy way to create personal portfolio. It's build using ASP.NET Core and Angular2. Each project consist of title, description and html content that enable individual layout and styling.
## Tech stack
* ASP.NET Core
* Angular2
* Bootstrap
* MongoDB and DocumentDB
* Webpack
* Karma & Jasmine

## Configuration
Projects are stored in NoSQL database. User can choose between MongoDB and DocumentDB. As the connection values are very similar there is one set of settings:
```javascript
  "PortfolioSettings": {
        "DatabaseType": "MongoDB",
        "ApiKey": "yourKey",
        "DatabaseEndpoint": "databaseUrl",
        "DatabaseKey": "keyIfNeeded",
        "DatabaseId": "idOfTheDatabase",
        "CollectionId": "idOfTheCollection"
    }
```
