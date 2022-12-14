using Microsoft.AspNetCore.Mvc;
using TruckGarage.Extension;
using TruckGarage.Entity;
using TruckGarage.Service;
using TruckGarage.Dto;

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
    public async Task<ActionResult<Truck>> GetTruckById(long id) {
        Truck? truck;
        if((truck = await truckService.FindTruckByIdAsync(id)) == null)
            return BadRequest(new { Error = "Caminhão não encontrado." });
        return Ok(truck);
    }
    [HttpPost]
    public async Task<ActionResult<Truck>> CreateTruck(Truck truck) {
        if(truck.modelo == string.Empty) 
            return BadRequest(new { Error = "Modelo não informado." });
        if(!truck.modelo.IsFM_FH()) return BadRequest(new { Error = "Tipo do modelo não informado." });
        truck.anoFabricacao = DateTime.Now.Year.ToString();
        if(!truck.anoModelo.IsYear()) return BadRequest(new { Error = "Ano do modelo precisam ser do valor de um ano." });
        if(Int32.Parse(truck.anoModelo) < Int32.Parse(truck.anoFabricacao))
            return BadRequest(new { Error = "Ano do modelo não pode ser inferior ao seu ano de fabricação." });
        return Ok(await truckService.CreateTruckAsync(truck));
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<Truck>> UpdateTruck(long id, TruckDto truckDto) {
        Truck _truck = new Truck();
        if(truckDto.modelo != string.Empty) {
            if(!truckDto.modelo.IsFM_FH()) return BadRequest(new { Error = "Tipo do modelo não informado." });
            _truck.modelo = truckDto.modelo;
        }
        if(truckDto.anoModelo != string.Empty) {
            if(!truckDto.anoModelo.IsYear()) return BadRequest(new { Error = "Ano do modelo precisam ser do valor de um ano." });
            if(Int32.Parse(truckDto.anoModelo) > Int32.Parse(_truck.anoFabricacao))
                return BadRequest(new { Error = "Ano do modelo não pode ser superior ao seu ano de fabricação." });
            _truck.anoModelo = truckDto.anoModelo;
        }
        Truck? updatedTruck;
        if((updatedTruck = await truckService.UpdateTruckByIdAsync(id, _truck)) == null)
            return BadRequest(new { Error = "Caminhão não encontrado." });
        return Ok(updatedTruck);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<Truck>> RemoveTruck(long id) {
        Truck? deletedTruck;
        if((deletedTruck = await truckService.FindTruckByIdAsync(id)) == null)
            return BadRequest(new { Error = "Caminhão não existe dentro da base de dados." });
        deletedTruck = await truckService.RemoveTruckAsync(deletedTruck);
        return Ok(deletedTruck);
    }

}