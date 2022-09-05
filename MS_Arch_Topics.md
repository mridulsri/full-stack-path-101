# Microservices Training.

# Topics:

## Day-1:

### Tools:
	- Azure cosmos Emulator
	- Postman
	- Docker
	- Kubectl
	- Lens
	- Git
	- Service bus explorer
	- Azure Storage Explorer
	- Azure Data studio
	- Cosmos DB Explorer
	- Angular CLI

### .Net Core:

### Azure topic:

### Angular:

### Project Structure
	- Categories
	- Prodcuts
	- Cart
	- Payment integration
	- Delivery Process/maps

### Azure portal access

### Domain Models.
	- Requirements
	- Noun								- Verbs
	Users								Sign/Login
	Products							Browse Categories	
	Categories							Checkout
	Payments							Placement orders
	Orders								Identify location
	Address

	- Enities Relationship
		- Users
			- Address
			- Orders
				- Products
				- Payment
		- Products
			- Categories


### Intro to REST.

# Day-2

	- REST fundamental
	- GraphQL, GRPc, Odata
	- Postman, Swaggar
	- Best practices when creating API contractor
	- Dependency Injection
		- Transient
			- Makes an instance each time.
			- Never shared. 
			- Used for lightweight stateless services.
		- Singleton
			- Creates only single instance.
			- Shared among all components that demand it.
		- Scoped
			- Creates an instance once per scope.
			- Created on every request to the application.
		- https://softchris.github.io/pages/dotnet-di.html#service-lifetimes
		- https://alexalvess.medium.com/dependency-injection-and-inversion-of-control-on-net-core-3136fe98b72
	- Threading
		- http://www.albahari.com/threading/part2.aspx

# Day-4
	- Log Serilog
	- Middleware

# Day-5
	- ProblemDetails class
		- https://code-maze.com/using-the-problemdetails-class-in-asp-net-core-web-api/
	- Filters 
	- Basic athentication

# Day-6 
	- JWT authentication
	
# Day-7
	- Refresh token
	- JWKS (JSON web key set)
		- help not to implement the token verification in all the microservices.
	- certificate security implement
		- https://www.hanselman.com/blog/DevelopingLocallyWithASPNETCoreUnderHTTPSSSLAndSelfSignedCerts.aspx
# Day-8 (Inter-comunication in microservices)
	- AddHttpClient (IHttpClientFactory)
		- https://flurl.dev/docs/fluent-url/
		- https://www.stevejgordon.co.uk/httpclientfactory-asp-net-core-logging
		- https://www.infoq.com/articles/creating-http-sdks-dotnet-6/
		- pckg: poliy,polly.Extentions
		- circuit breaker 
			- https://www.codeguru.com/network/circuit-breaker-polly/
	
	- Grpc Services
		- https://grpc.io/docs/languages/csharp/quickstart/
		- https://medium.com/geekculture/build-high-performant-microservices-using-grpc-and-net-6-adde158c5ac
	
	- Refit
		- https://reactiveui.github.io/refit/

# Day-9
	- UnitText (xUnit, MSTest)
	- Moq, NSubstitute.
	- 
	

# Day-10(2-Sept-2022)
	- Unit Test
	- Benchmarking
		- pckg: BenchmarkDotNet
		


# Misslanious
	- Knowledge Graph
	- Logging
		- Regyon


# Ref
	## UserMgm
		- https://codewithmukesh.com/blog/user-management-in-aspnet-core-mvc/
	## Authorization
		- Role Base, Claim base, Policy base, view based Authorization
	## CQRS
		- https://event-driven.io/en/cqrs_is_simpler_than_you_think_with_net6/
		- https://code-maze.com/cqrs-mediatr-in-aspnet-core/
		
	## Unit test
	
	## Model Validation
		- https://docs.fluentvalidation.net/en/latest/async.html
webdevdl.ir
