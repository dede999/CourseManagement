# CourseManagement

## Description

In the course of a tech test, I was invited to build a front end system in React
that would allow a user to view a list of courses and their details. The user
should be able to add a new course to the list and delete a course from the list.
The user should also be able to view and edit the details of a course by clicking
on the course in the list.

After completing it, I decided to provide a "real" back end to the system, and
given that I have been participating in a .NET bootcamp, I decided to use this
technology to build the back end. And this is the result.

## Technologies

- React (see the front end repository [here](https://github.com/dede999/twygo-test-react))
- .NET 8.0
- Entity Framework Core
- PostgreSQL
- Docker

## How to run

### Locally

1. Clone this repository
2. Run `dotnet watch run` in the `/Api` folder
3. Access `https://localhost:5000/health_check` to check if the API is running
4. Clone the front end repository, install the dependencies and run the front end
   - `bun install`
   - `bun dev`
   - Access `http://localhost:5173` to see the front end
5. Enjoy!

### With Docker

1. Clone this repository
2. Run `docker-compose up` in the root folder
3. Access `https://localhost:5000/health_check` to check if the API is running
4. I still could not create a docker file as previously described
5. Enjoy!

## To Do

- Create Course entity (belongs to a teacher [User])
  - [ ] Create entity
  - [ ] Create service
  - [ ] Implement actions
- Implement JWT
  - [ ] Create JWT service
  - [ ] Implement JWT in the API
  - [ ] Use JWT to secure the endpoints
- Create Vídeo entity (belongs to a course)
  - [ ] Create entity
  - [ ] Create service
  - [ ] Implement actions
