## Movies App -> Integration with IMDB API
### Project Details
.NET is utilized for the backend and Angular for the frontend. A docker compose file is contained in the root directory of this repository. To start the project, run the docker compose file or in the alternative,
 start the .NET app 'dotnet watch run', then run the Angular app 'ng serve'.

To run .NET app
```.NET
dotnet watch run
```
To run Angular app
```.NET
ng serve
```

Using docker compose file

```.NET
docker compose build api
docker compose build web
docker compose up -d
```

Upon successful start of application, you can visit the web using this url
```
http://localhost:4200
```

We build the api service, then the web service, then we start the services
