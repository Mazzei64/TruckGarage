using TruckGarage.Service;
using TruckGarage.Data;
using Moq;
using TruckGarage.Controller;
using TruckGarage.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TruckGarage.Dto;

namespace TruckGarage.Tests;

public class TruckControllerTests {
    private readonly TruckController truckController;
    private readonly Mock<ITruckService> truckServiceMock = new Mock<ITruckService>();
    public TruckControllerTests(){
        truckController = new TruckController(truckServiceMock.Object);
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
    public async Task CreateTruck_ShouldReturnNothing_WhenModelNotInformed() {
        // Arrange
        var newTruck = new Truck {
            modelo = "",
            anoModelo = "2021"
        };

        // Act
        var objResult = ((ObjectResult?)(await truckController.CreateTruck(newTruck)).Result);

        // Assert
        Assert.IsType<BadRequestObjectResult>(objResult);
        Assert.Equal("{ Error = Modelo não informado. }", objResult.Value.ToString());
    }
    [Fact]
    public async Task CreateTruck_ShouldReturnNothing_WhenModelIsNeitherFMOrFHTagged() {
        // Arrange
        var newTruck = new Truck {
            modelo = "Volvo FHX",
            anoModelo = "2021"
        };

        // Act
        var objResult = ((ObjectResult?)(await truckController.CreateTruck(newTruck)).Result);

        // Assert
        Assert.IsType<BadRequestObjectResult>(objResult);
        Assert.Equal("{ Error = Tipo do modelo não informado. }", objResult.Value.ToString());
    }
    [Fact]
    public async Task CreateTruck_ShouldReturnNothing_WhenModelYearIsNotANumeric4DigitsLenghtFormat() {
        // Arrange
        var newTruck = new Truck {
            modelo = "Volvo FH",
            anoModelo = "20f1"
        };
        var _newTruck = new Truck {
            modelo = "Volvo FH",
            anoModelo = "20512"
        };

        // Act
        var objResult = ((ObjectResult?)(await truckController.CreateTruck(newTruck)).Result);
        var _objResult = ((ObjectResult?)(await truckController.CreateTruck(_newTruck)).Result);

        // Assert
        Assert.IsType<BadRequestObjectResult>(objResult);
        Assert.Equal("{ Error = Ano do modelo precisam ser do valor de um ano. }", objResult.Value.ToString());
        Assert.IsType<BadRequestObjectResult>(_objResult);
        Assert.Equal("{ Error = Ano do modelo precisam ser do valor de um ano. }", _objResult.Value.ToString());
    }
    [Fact]
    public async Task CreateTruck_ShouldReturnNothing_WhenModelYearIsHigherThanTheCurrentYear() {
        // Arrange
        var newTruck = new Truck {
            modelo = "Volvo FH",
            anoModelo = (DateTime.Now.Year + 1).ToString()
        };
        // Act
        var objResult = ((ObjectResult?)(await truckController.CreateTruck(newTruck)).Result);

        // Assert
        Assert.IsType<BadRequestObjectResult>(objResult);
        Assert.Equal("{ Error = Ano do modelo não pode ser superior ao seu ano de fabricação. }", objResult.Value.ToString());
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
        var objResult = (ObjectResult?)(await truckController.GetTruckById(truckId)).Result;
        Truck? truck = (Truck?)objResult.Value;

        // Assert
        Assert.IsType<OkObjectResult>(objResult);
        Assert.Equal(truckId, truck.Id);
    }
    [Fact]
    public async Task GetTruckById_ShouldReturnTruck_WhenTruckDoesNotExist() {
        // Arrange
        var randomGenerator = new Random(123);
        var truckId = randomGenerator.Next(1, 1000000);
        truckServiceMock.Setup(x => x.FindTruckByIdAsync(truckId))
        .ReturnsAsync(() => null);

        // Act
        var objResult = (ObjectResult?)(await truckController.GetTruckById(truckId)).Result;

        // Assert
        Assert.IsType<BadRequestObjectResult>(objResult);
        Assert.Equal("{ Error = Caminhão não encontrado. }", objResult.Value.ToString());
    }
    [Fact]
    public async Task DeleteTruck_ShouldReturnTruck_WhenTruckExists() {
        // Arrange
        var randomGenerator = new Random(123);
        var truckId = randomGenerator.Next(1, 1000000);
        var actualTruck = new Truck {
            Id = truckId,
            modelo = "Volvo FH",
            anoFabricacao = DateTime.Now.Year.ToString(),
            anoModelo = "2020"
        };
        truckServiceMock.Setup(x => x.FindTruckByIdAsync(truckId))
        .ReturnsAsync(actualTruck);
        truckServiceMock.Setup(x => x.RemoveTruckAsync(actualTruck))
        .ReturnsAsync(actualTruck);

        // Act
        var objResult = (ObjectResult?)(await truckController.RemoveTruck(truckId)).Result;

        // Assert
        Assert.IsType<OkObjectResult>(objResult);
    }
    [Fact]
    public async Task DeleteTruck_ShouldReturnNothing_WhenTruckDoesNotExist() {
        // Arrange
        var randomGenerator = new Random(123);
        var truckId = randomGenerator.Next(1, 1000000);
        var actualTruck = new Truck {
            Id = truckId,
            modelo = "Volvo FH",
            anoFabricacao = DateTime.Now.Year.ToString(),
            anoModelo = "2020"
        };
        truckServiceMock.Setup(x => x.FindTruckByIdAsync(truckId))
        .ReturnsAsync(() => null);

        // Act
        var objResult = (ObjectResult?)(await truckController.RemoveTruck(truckId)).Result;

        // Assert
        Assert.IsType<BadRequestObjectResult>(objResult);
        Assert.Equal("{ Error = Caminhão não existe dentro da base de dados. }", objResult.Value.ToString());
    }
}