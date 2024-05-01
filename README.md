## Movies App
### Integration with OMDB API (Technical Assessment)
### Project Details
.NET is utilized for the backend and Angular for the frontend. A docker compose file is contained in the root directory of this repository. To start the project, run the docker compose file or in the alternative,
 start the .NET app and the Angular app separately.

To run .NET app, run the command in the src/Movies.API directory
```.NET
dotnet watch run
```
To run Angular app, run the command in the src/Movies_Client directory in a command prompt/termainal
```Angular
ng serve
```

Using docker compose file, build the api service, then the web service, then run the docker file. This process might take a while. At the root of the project where the docker-compose.yml file is, open a command prompt and run the following commands

```.NET
docker compose build api
docker compose build web
docker compose up -d
```


Upon successful run, you can visit the web using the URL below. The client app does take a little longer to start, so refresh the page if you do not see anything. However, this only occurs if you attempt to run it with docker.
```
http://localhost:4200
```
