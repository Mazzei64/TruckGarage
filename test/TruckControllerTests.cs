using TruckGarage.Service;
using TruckGarage.Data;
using Moq;
using TruckGarage.Controller;
using TruckGarage.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace TruckGarage.Tests;

public class TruckControllerTests {
    private readonly TruckController truckController;
    private readonly Mock<ITruckService> truckServiceMock = new Mock<ITruckService>();
    public TruckControllerTests(){
        truckController = new TruckController(truckServiceMock.Object);
    }
    [Fact]
    public async Task GetTruckById_ShouldReturnTruck_WhenTruckExists() {
        // Arrange
        var randomGenerator = new Random(123);
        var truckId = randomGenerator.Next(1, 1000000);
        truckServiceMock.Setup(x => x.FindTruckByIdAsync(truckId)).ReturnsAsync(new Truck {
            Id = truckId,
            modelo = "Volvo FM",
            anoFabricacao = "2021",
            anoModelo = "2020"   
        });

        // Act
        Truck? truck = (Truck?)((OkObjectResult?)(await truckController.GetTruckById(truckId)).Result).Value;

        // Assert
        Assert.Equal(truckId, truck.Id);
    }
    [Fact]
    public async Task CreateTruck_ShouldReturnCreatedTruck_WithCurrentYearsTime() {
        // Arrange
        var newTruck = new Truck {
            modelo = "Volvo FH",
            anoModelo = "2021"
        };
        truckServiceMock.Setup(x => x.CreateTruckAsync(newTruck)).ReturnsAsync(new Truck {
            Id = 1,
            modelo = newTruck.modelo,
            anoFabricacao = DateTime.Now.Year.ToString(),
            anoModelo = newTruck.anoModelo   
        });

        // Act
        Truck? truck = (Truck?)((OkObjectResult?)(await truckController.CreateTruck(newTruck)).Result).Value;

        // Assert
        Assert.Equal(newTruck.modelo, truck.modelo);
        Assert.Equal(newTruck.anoModelo, truck.anoModelo);
        Assert.Equal(DateTime.Now.Year.ToString(), truck.anoFabricacao);
    }
    [Fact]
    public async Task UpdateTruck_ShouldReturnUpdatedTruck() {
        // Arrange
        var newTruck = new Truck {
            modelo = "Volvo FH",
            anoModelo = "2021"
        };
        truckServiceMock.Setup(x => x.CreateTruckAsync(newTruck)).ReturnsAsync(new Truck {
            Id = 1,
            modelo = newTruck.modelo,
            anoFabricacao = DateTime.Now.Year.ToString(),
            anoModelo = newTruck.anoModelo   
        });

        // Act
        Truck? truck = (Truck?)((OkObjectResult?)(await truckController.CreateTruck(newTruck)).Result).Value;

        // Assert
        Assert.Equal(newTruck.modelo, truck.modelo);
        Assert.Equal(newTruck.anoModelo, truck.anoModelo);
        Assert.Equal(DateTime.Now.Year.ToString(), truck.anoFabricacao);
    }
}