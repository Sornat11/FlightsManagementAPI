using Xunit;
using FakeItEasy;
using FlightsManagementAPI.Controllers;
using FlightsManagementAPI.Services.FlightService;
using FlightsManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace FlightsManagementAPI.Tests
{
    public class FlightControllerTests
    {
        private readonly FlightController _controller;
        private readonly IFlightService _fakeFlightService;

        public FlightControllerTests()
        {
            _fakeFlightService = A.Fake<IFlightService>();
            _controller = new FlightController(_fakeFlightService);
        }

        [Fact]
        public async Task GetAllFlights_ReturnsListOfFlights()
        {
            // Arrange
            var flights = new List<Flight>
            {
                new Flight { Id = 1, FlightNumber = "AB123", DepartureDate = DateTime.Now.AddDays(1), DeparturePlace = "Place A", ArrivalPlace = "Place B", AircraftType = "Type X" },
                new Flight { Id = 2, FlightNumber = "CD456", DepartureDate = DateTime.Now.AddDays(2), DeparturePlace = "Place C", ArrivalPlace = "Place D", AircraftType = "Type Y" }
            };
            A.CallTo(() => _fakeFlightService.GetAllFlights()).Returns(Task.FromResult(flights));

            // Act
            var actionResult = await _controller.GetAllFlights();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedFlights = Assert.IsType<List<Flight>>(okResult.Value);
            Assert.Equal(2, returnedFlights.Count);
        }

        [Fact]
        public async Task GetFlight_WithExistingId_ReturnsFlight()
        {
            // Arrange
            var fakeService = A.Fake<IFlightService>();
            var flight = new Flight { Id = 1, FlightNumber = "AB123" };
            A.CallTo(() => fakeService.GetFlight(1)).Returns(Task.FromResult(flight));

            var controller = new FlightController(fakeService);

            // Act
            var result = await controller.GetFlight(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedFlight = Assert.IsType<Flight>(okResult.Value);
            Assert.Equal(flight.FlightNumber, returnedFlight.FlightNumber);
        }

        [Fact]
        public async Task GetFlight_WithNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var fakeService = A.Fake<IFlightService>();
            A.CallTo(() => fakeService.GetFlight(1)).Returns(Task.FromResult<Flight>(null));

            var controller = new FlightController(fakeService);

            // Act
            var result = await controller.GetFlight(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task AddFlight_WithValidFlight_ReturnsAddedFlight()
        {
            // Arrange
            var fakeService = A.Fake<IFlightService>();
            var newFlight = new Flight { FlightNumber = "AB123" };
            A.CallTo(() => fakeService.AddFlight(newFlight)).Returns(Task.FromResult(new List<Flight>() { newFlight }));

            var controller = new FlightController(fakeService);

            // Act
            var result = await controller.AddFlight(newFlight);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var flights = Assert.IsType<List<Flight>>(okResult.Value);
            Assert.Single(flights);
            Assert.Contains(newFlight, flights);
        }
        [Fact]
        public async Task AddFlight_WithInvalidFlight_ReturnsBadRequest()
        {
            // Arrange
            var invalidFlight = new Flight(); // This flight object is invalid as it doesn't meet the model's validation criteria
            _controller.ModelState.AddModelError("FlightNumber", "Flight number is required."); // Simulating ModelState error

            // Act
            var actionResult = await _controller.AddFlight(invalidFlight);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task UpdateFlight_WithExistingFlight_ReturnsUpdatedFlight()
        {
            // Arrange
            var fakeService = A.Fake<IFlightService>();
            var existingFlight = new Flight { Id = 1, FlightNumber = "AB123" };
            var updatedFlight = new Flight { Id = 1, FlightNumber = "AB124" };
            var updatedFlightList = new List<Flight> { updatedFlight };
            A.CallTo(() => fakeService.UpdateFlight(1, updatedFlight)).Returns(Task.FromResult(updatedFlightList));

            var controller = new FlightController(fakeService);

            // Act
            var result = await controller.UpdateFlight(1, updatedFlight);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedFlightList = Assert.IsType<List<Flight>>(okResult.Value);
            Assert.Single(returnedFlightList);
            var returnedFlight = returnedFlightList.First();
            Assert.Equal("AB124", returnedFlight.FlightNumber); // Confirm that the flight number is updated
        }

        [Fact]
        public async Task UpdateFlight_WithNonExistingFlight_ReturnsNotFound()
        {
            // Arrange
            var fakeService = A.Fake<IFlightService>();
            var nonExistingFlightUpdate = new Flight { Id = 99, FlightNumber = "AB125" };
            A.CallTo(() => fakeService.UpdateFlight(99, nonExistingFlightUpdate)).Returns(Task.FromResult<List<Flight>>(null));

            var controller = new FlightController(fakeService);

            // Act
            var result = await controller.UpdateFlight(99, nonExistingFlightUpdate);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
        [Fact]
        public async Task DeleteFlight_WithExistingFlight_ReturnsOk()
        {
            // Arrange
            var fakeService = A.Fake<IFlightService>();
            var flightToDelete = new Flight { Id = 1, FlightNumber = "AB123" };
            A.CallTo(() => fakeService.DeleteFlight(1)).Returns(Task.FromResult(new List<Flight> { flightToDelete }));

            var controller = new FlightController(fakeService);

            // Act
            var result = await controller.DeleteFlight(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var deletedFlights = Assert.IsType<List<Flight>>(okResult.Value);
            var deletedFlight = deletedFlights.FirstOrDefault();
            Assert.Equal("AB123", deletedFlight.FlightNumber); // Confirm that the correct flight is deleted
        }

        [Fact]
        public async Task DeleteFlight_WithNonExistingFlight_ReturnsNotFound()
        {
            // Arrange
            var fakeService = A.Fake<IFlightService>();
            A.CallTo(() => fakeService.DeleteFlight(99)).Returns(Task.FromResult<List<Flight>>(null));

            var controller = new FlightController(fakeService);

            // Act
            var result = await controller.DeleteFlight(99);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}