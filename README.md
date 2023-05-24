# RectanglesSearchWithBasicAuthentication

How to raize the project:

1. Build the Docker image.

2. Raize Up the docker-compose
	(docker-compose up -d).

3. Change Server name into appsettings.json from Server=rectanglessearchapi_database,1433; on Server=localhost,1433; .
update-database into Package Manager Console from Visual Sudio.

4. Start the project into Visual Sudio to initialize database.

5. Apply INDEX.sql to the container database.

6. Raize all containers if any of them is down. 
	
7. You can test this service using Postman or other tools like it. 
	- Url: http://localhost:7157/api/rectangle/searchcoordinates
	- Method type: POST
	- Body:
[
  {
    "xCoordinate": 53,
    "yCoordinate": 37
  },
  {
    "xCoordinate": 140,
    "yCoordinate": 100
  },
  {
    "xCoordinate": 160,
    "yCoordinate": 120
  }
]

	- Authorization: Basic Auth
	Credentials:
		UserName: admin
		Password: !Admin123




# Any further design considerations assuming scaling this system and integrations with external consumers.

Scalability: Planning for scalability by designing a system that can handle a growing number of rectangles and increasing search/match requests. 
We can use horizontal scaling techniques like load balancing and partitioning data across multiple servers to distribute the workload effectively.

Caching: Implementing caching mechanisms, such as in-memory caches or distributed caching systems, to store frequently accessed rectangles or search results. 
Caching can significantly improve the performance of the API, especially if certain rectangles or searches are repeated frequently.

Rate Limiting and Throttling: Protecting the API from abuse and ensure fair resource allocation by implementing rate limiting and throttling mechanisms. 
These mechanisms can restrict the number of requests a consumer can make within a certain time period, preventing overload and maintaining system stability.

Documentation: It can be provided comprehensive and up-to-date documentation for the API, including clear examples and usage guidelines. 
This will help external consumers integrate with the API more easily and reduce the support burden.

Monitoring and Logging: Setting up monitoring and logging systems to track the API's performance, identify potential bottlenecks, and troubleshoot issues. 
Collecting metrics and logs will help to understand system behavior, optimize performance, and ensure the API meets the expected service level agreements.

Error Handling: Implementing proper error handling and provide meaningful error messages in the API responses. 
This will help consumers understand and troubleshoot any issues that may occur during the search/match process.

Pagination: If the number of rectangles or search results can be large, consider implementing pagination in the API responses. 
This allows consumers to retrieve results in smaller, manageable chunks, reducing the response size and improving performance.

Versioning: Planning for future changes and updates to the API by incorporating versioning from the beginning. 
This ensures that it can introduce new features or modify existing functionality without breaking compatibility for existing consumers. 
Versioning can be achieved through URL versioning or using request headers.