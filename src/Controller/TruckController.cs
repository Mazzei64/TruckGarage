using Microsoft.AspNetCore.Mvc;
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
            return BadRequest();
        return Ok(truck);
    }
    [HttpPost]
    public async Task<ActionResult<Truck>> CreateTruck(Truck truck) {
        return Ok(await truckService.CreateTruckAsync(truck));
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<Truck>> UpdateTruck(int id, Truck truck) {
        Truck? updatedTruck;
        if((updatedTruck = await truckService.UpdateTruckByIdAsync(id, truck)) == null)
            return BadRequest();
        return Ok(updatedTruck);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<Truck>> RemoveTruck(int id) {
        Truck? deletedTruck;
        if((deletedTruck = await truckService.RemoveTruckByIdAsync(id)) == null)
            return BadRequest();
        return Ok(deletedTruck);
    }

}