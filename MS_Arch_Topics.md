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
		- [serilog] (https://serilog.net/)
		- [serilog best practices](https://benfoster.io/blog/serilog-best-practices)
		- [Serilog 16 Best Practices and Tips](https://stackify.com/serilog-tutorial-net-logging/)
	- Middleware

# Day-5
	- ProblemDetails class
		- https://code-maze.com/using-the-problemdetails-class-in-asp-net-core-web-api/
	- Filters 
	- Basic athentication

# Day-6 
	- Authentication
		- https://www.tektutorialshub.com/asp-net-core/authentication-in-asp-net-core/
		- https://www.tektutorialshub.com/asp-net-core/policy-based-authorization-in-asp-net-core/#:~:text=Authorization%20Policy,-Authorization%20Policies%20are&text=A%20Policy%20defines%20a%20collection,ConfigureServices%20of%20the%20startup%20class.
	- JWT authentication
		- https://jasonwatmore.com/post/2022/01/19/net-6-create-and-validate-jwt-tokens-use-custom-jwt-middleware
	
# Day-7
	- Refresh token
		- 
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
			- https://www.pluralsight.com/blog/software-development/intro-to-polly
		- circuit breaker 
			- https://www.codeguru.com/network/circuit-breaker-polly/
			- https://www.pluralsight.com/blog/software-development/intro-to-polly
			- https://csharpdoc.hotexamples.com/class/Polly/CircuitBreakerSyntaxAsync
			- https://andrewlock.net/when-you-use-the-polly-circuit-breaker-make-sure-you-share-your-policy-instances-2/
	
	- Grpc Services
		- https://grpc.io/docs/languages/csharp/quickstart/
		- https://medium.com/geekculture/build-high-performant-microservices-using-grpc-and-net-6-adde158c5ac
		- https://grpc.io/docs/languages/csharp/quickstart/
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
		

# Day-11 (6-Sept-2022)
	- Benchmarking
		- Logic
	- Azure Services
		- API Management Service (apim-)
		- Web app (app-)
		- Resource Group (rg-)
		- Azure SQL database (sqldb-)


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

