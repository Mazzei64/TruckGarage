using Microsoft.AspNetCore.Mvc;
using TruckGarage.Dto;
using TruckGarage.Entity;
using TruckGarage.Service;

namespace TruckGarage.Controller;

[Route("api/[controller]")]
[ApiController]
public class TruckController : ControllerBase {
    private readonly ITruckService truckService;
    public TruckController(ITruckService truckService) {
        this.truckService = truckService;
    }
    [HttpGet]
    public async Task<ActionResult<List<Truck>>> GetTrucks() {
        return Ok(await truckService.ListTrucksAsync());
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Truck>> GetTruckById(int id) {
        Truck? truck;
        if((truck = await truckService.FindTruckByIdAsync(id)) == null)
            return BadRequest(new { Error = "Caminhão não encontrado." });
        return Ok(truck);
    }
    [HttpPost]
    public async Task<ActionResult<Truck>> CreateTruck(TruckDto truckDto) {
        if(truckDto.modelo == string.Empty) 
            return BadRequest(new { Error = "Modelo não informado." });
        Truck truck = new Truck();
        truck.modelo = truckDto.modelo;
        try{
            truck.anoFabricacao = DateOnly.ParseExact(truckDto.anoFabricacao,
                                                    new[] {
                                                        "dd/MM/yyyy", "MM/dd/yyyy", "d/MM/yyyy",
                                                        "MM/d/yyyy", "dd/M/yyyy", "M/dd/yyyy",
                                                        "M/d/yyyy", "d/M/yyyy"
                                                    });
            truck.anoModelo = DateOnly.ParseExact(truckDto.anoModelo,
                                                    new[] {
                                                        "dd/MM/yyyy", "MM/dd/yyyy", "d/MM/yyyy",
                                                        "MM/d/yyyy", "dd/M/yyyy", "M/dd/yyyy",
                                                        "M/d/yyyy", "d/M/yyyy"
                                                    });
        }catch(FormatException) {
            return BadRequest(new { Error = "Formato de data incompatível." });
        }
        return Ok(await truckService.CreateTruckAsync(truck));
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<Truck>> UpdateTruck(int id, TruckDto truckDto) {
        Truck truck = new Truck();
        truck.modelo = truckDto.modelo;
        try{
            truck.anoFabricacao = DateOnly.ParseExact(truckDto.anoFabricacao,
                                                    new[] {
                                                        "dd/MM/yyyy", "MM/dd/yyyy", "d/MM/yyyy",
                                                        "MM/d/yyyy", "dd/M/yyyy", "M/dd/yyyy",
                                                        "M/d/yyyy", "d/M/yyyy"
                                                    });
            truck.anoModelo = DateOnly.ParseExact(truckDto.anoModelo,
                                                    new[] {
                                                        "dd/MM/yyyy", "MM/dd/yyyy", "d/MM/yyyy",
                                                        "MM/d/yyyy", "dd/M/yyyy", "M/dd/yyyy",
                                                        "M/d/yyyy", "d/M/yyyy"
                                                    });
        }catch(FormatException) {
            return BadRequest(new { Error = "Formato de data incompatível." });
        }
        Truck? updatedTruck;
        if((updatedTruck = await truckService.UpdateTruckByIdAsync(id, truck)) == null)
            return BadRequest(new { Error = "Caminhão não encontrado." });
        return Ok(updatedTruck);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<Truck>> RemoveTruck(int id) {
        Truck? deletedTruck;
        if((deletedTruck = await truckService.RemoveTruckByIdAsync(id)) == null)
            return BadRequest(new { Error = "Caminhão não existe dentro da base de dados." });
        return Ok(deletedTruck);
    }

}