#Linkit Assignment Solution

It consists of mainly 2 projects, one for backend API (SimpleAir.API) and one for Web UI (LinkitAir.Web.UI), also unit test project for backend API.

##Getting Started

This solutions are implemented in order to test and achieve the requirements described in the assignment. All the requirements are covered with the solution which includes a REST API and documentation with Swagger, Web UI with Angular 6, unit testing with xunit, a simple layered approach for the arthitecture and repository pattern for persistance and a in memory database, e2e and unit tests for Web UI and traffic logging.

##Tech/framework used
The techs used in the solution as follows;

	1. .Net core 2.2
	2. .Net standart 2.0
	3. xunit
	4. Moq
	5. FluentAssertions
	6. Git
	7. Visual Studio Community 2017
	8. Angular 6
	9. Swagger for API
	10. Entity Framework core and in memory database as context
	11. Elasticsearch
	12. Serilog
	13. Karma test runner and Jasmine testing framework for angular unit test
	14. Angular protractor for e2e test
	15. Angular CLI version 6.0.8
	16. node js version 10.14.1
	17. Automapper

##How is it done?

###REST API

So the REST API is SimpleAir.API and it is implemented using .net core. Tha design of the system lays essentially like following;

 **a. SimpleAir.Bootstrapper :** Used for dependency injection implementation. Consequently, rest of the system is abstracted from the API except SimpleAir.Domain.Service.

 Microsoft.Extensions.DependencyInjection library used for dependency injection and it has been implemented such as;

 		 services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("ApplicationDbContext"));
         services.AddTransient<IFlightService, FlightService>();

 **b. SimpleAir.Domain.Service:** Used for service layer, aka business layer, orchestrates the repositories and talks to the persistance layer via them. Transforms domain model entities to UI dto's and vice versa.

 **c. SimpleAir.Domain.Repository:** Used to implement persistance layer, an in-memory database is used to store the data.

 Airport search has been implemented as follows to meet with the description in the assignment;

 	    public async Task<IEnumerable<Airport>> GetAirportsAsync(string searchKey)
        {
            return await _dbContext.AirPorts.Where(t => t.Code.ToLower().Contains(searchKey.ToLower()) || t.Name.ToLower().Contains(searchKey.ToLower())).ToListAsync();
        }

 **d. SimpleAir.Domain.Model:** Domain model for the system. 

 **e. SimpleAir.Core:** A seperated layer containing interfaces to implement repositories and db context. Consequently, it enables us to change the repository layer in the future if needed, further implementations might be needed for that as well. There is no direct dependency between SimpleAir.Domain.Service and SimpleAir.Domain.Repository.

 **f. SimpleAir.UnitTest:** A xunit test project to test the services and controllers. There are 6 tests implemented but with inline functionality, it expands to 22. IFlightRepository, IApplicationDbContext and IAirportRepository are mocked usign Moq and served as fake data generators. Mock implementations are in BaseTest class. The following test is a sample of assertioning of convenient airports searched;

 		[Theory]
        [InlineData("a")]
        [InlineData("fra")]
        [InlineData("lo")]
        [InlineData("zz")]
        [InlineData("da")]
        [InlineData("AMTD")]
        [InlineData("Amsterdam")]
        [InlineData("Fran")]
        public void HomeController_GetAirports_Should_Return_Convenient_Airports(string searchKey)
        {
            var controller = new FlightController(_flightService.Object);

            var response = controller.GetAirports(new AirportRequestDto() { SearchKey = searchKey }).GetAwaiter().GetResult();

            var okResult = response as OkObjectResult;

            okResult.Should().NotBeNull();

            ICollection<AirportResponseDto> respValue = okResult.Value as ICollection<AirportResponseDto>;

            respValue.Count.Should().Be(airports.Where(t => t.Code.ToLower().Contains(searchKey.ToLower()) || t.Name.ToLower().Contains(searchKey.ToLower())).ToList().Count);
        }

 **g. SimpleAir.API:** REST API implemented in .net core. There are basically 2 methods than a client can consume. You can find the methods in FlightController. 

 1. GetAirports method is used to search for airports. As it is described in the assignment, it is searched thgrough either airport name or airport code.
 2. GetFlights medhod is used to search for flights. Some dummy data gets inserted in the db before staring the application in startup.cs

 Basically, to run the project for Unix based operation systems as well as on windows, go to path ~\SimpleAir.API\bin\Debug\netcoreapp2.2 and hit the command dotnet SimpleAir.API.dll. But if you have visual studio installed, you can just select the project as the start up project and hit ctrl+f5.

 after that, it is going to run on http://localhost:61222 and ready to accept requests. Go to the browser and type http://localhost:61222/swagger . Swagger will display the api methods. I didn't bother with authentication and so on but it could be implemented with basic authentication or jwt authentication when desired. for further information please check http://jasonwatmore.com/post/2018/08/14/aspnet-core-21-jwt-authentication-tutorial-with-example-api (as you might already know)

 I have have used Serilog for traffic tracking and logging. There is concept called "sink" in serilog which is basically the persistance of where you store your logs. For that, I have choosen Elasticsearch as it is easy to implement and very functional for querying it. You can monitor your traffic with a functional user interface with Kibana which is another concept in Elasticsearch. There are many features in Kibana, you can make charts, graphics etc. to visualize your data. I have implemented the logging mechanism with a basic approach of aop using attribtutes but it could be configured in **Startup.cs** to log everything happens in the API. It is commented out now but you can check it out. Primarily, when a api method is called, it is logged to elastic as well when an exception occurs. There is middleware for handling the exception globally **(ExceptionHandlingMiddleWare)**. Some sample for configuring serilog is:

 	        var elasticUri = Configuration["ElasticConfiguration:Uri"];
            var username = Configuration["ElasticConfiguration:UserName"];
            var password = Configuration["ElasticConfiguration:Password"];

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Async(a => a.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    ModifyConnectionSettings = cfg => cfg.BasicAuthentication(username, password),
                    AutoRegisterTemplate = true,
                }))
            .CreateLogger();

 Controller method atribute is used as following;

    [HttpPost]
    [Route("api/GetFlights")]
    [TrackUsage("Flight", "API", "GetFlights")]
    public async Task<IActionResult> GetFlights([FromBody] FlightRequestDto request)

 You dont need to worry about any installation or configuration for Elastic and Kibana. I have used a cloud SAAS of elasticsearch and it is basically hosted on an aws instance. Elastic endpoint and credentials are configured in "appsettings.json".To monitor data plase use https://80b79ff440e348e8bceb8fb7d774604f.eu-central-1.aws.cloud.es.io:9243/app/kibana address and login credentials are the same as elastic. Following information can be filtered in Kibana;

 		Total number of requests processed

		Total number of requests resulted in an OK, 4xx and 5xx responses

		Average response time of all requests

		Min response time of all requests

		Max response time of all requests

###WEB UI

  **a. SimpleAir.Web.UI:** It is the user interface project. Angular (angular 6) framework has been used to implement the user interface. The app lays in WebApp folder and there is only one page just to demonstrate the implementation. It is not the shiniest page ever but I think it is fair enough to demonstrate the functionality. I didnt bother so much with css and so on.

  A seperate unit test and e2e test has been implemented for the page. For unit test, please check out **search-flight.component.spec.ts** file. What it does is that it tests the angular component functions with a mock api service. There are 8 tests which asserts the functionality of the component such as when airport selected from the combobox, a date selected from the date picker. An example ;

  	   it('should return departure airport name accordingly', async(() => {

	    let airport = new MockAirport();
	    airport.name = 'Amsterdam';
	    airport.code = 'SCHPL';
	    let displayValue = component.displayDeparture(airport);

	    //const titleText = fixture.nativeElement.querySelector('h1').textContent;
	    expect(displayValue).toEqual(airport.name + " (" + airport.code + ")");
	  }));

  e2e test in placed in **app.e2e-spec.ts file**. It basically searches generated flights through the API. Following code is the e2e test for the system;

	   it('should search flight and find some', () => {
	    page.navigateTo();
	    page.fillRequiredElements();

	    element(by.css('.btn-primary')).click();
	    browser.driver.sleep(500);

	    var tabledata = element(by.css(".table-striped")).element(by.tagName("tbody"));

	    var rows = tabledata.all(by.tagName("tr"));

	    browser.driver.sleep(500);

	    expect(rows.count()).toBeGreaterThan(0);
	  });

  A seperate build has been implemented as well. Just change the path on terminal or command prompt to ~\SimpleAir.Web.UI\WebApp and type "npm install" and when it finishes then type "npm run ci". What it is going to do is install the packages, run the unit tests, run the e2e test, build and then start the app. Type http://localhost:4200/ on your browser then woila! Before that, plase do not forget to run th API.


##Credits

	Param Singh 	https://medium.com/paramsingh-66174/automate-e2e-testing-of-angular-4-apps-with-protractorjs-jasmine-fcf1dd9524d5
					https://angular.io/guide/testing
					https://github.com/angular/material2/blob/master/src/lib/autocomplete/autocomplete.spec.ts
	Amir Tugendhaft	https://itnext.io/using-angular-6-material-auto-complete-with-async-data-6d89501c4b79
					https://github.com/serilog/serilog-sinks-elasticsearch
					https://andrewlock.net/writing-logs-to-elasticsearch-with-fluentd-using-serilog-in-asp-net-core/
					https://www.humankode.com/asp-net-core/logging-with-elasticsearch-kibana-asp-net-core-and-docker
	Adnan Mulalić	https://itnext.io/loggly-in-asp-net-core-using-serilog-dc0e2c7d52eb
					https://github.com/angular/material2/blob/master/src/lib/datepicker/datepicker.spec.ts