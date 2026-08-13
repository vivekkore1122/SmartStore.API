using Microsoft.AspNetCore.Mvc;
using SmartStore.API.Models.Domain;
using SmartStore.API.Repository.Interfaces;

namespace SmartStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplierController : ControllerBase
{
    private readonly ISupplierRepository supplierRepository;

    public SupplierController(ISupplierRepository supplierRepository)
    {
        this.supplierRepository = supplierRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSuppliers()
    {
        var suppliers = await supplierRepository.GetAllAsync();

        return Ok(suppliers);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSupplierById(int id)
    {
        var supplier = await supplierRepository.GetByIdAsync(id);

        if (supplier == null)
        {
            return NotFound();
        }

        return Ok(supplier);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSupplier(Supplier supplier)
    {
        var createdSupplier =
            await supplierRepository.CreateAsync(supplier);

        return CreatedAtAction(
            nameof(GetSupplierById),
            new { id = createdSupplier.Id },
            createdSupplier);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateSupplier(
        int id,
        Supplier supplier)
    {
        supplier.Id = id;

        var updatedSupplier =
            await supplierRepository.UpdateAsync(supplier);

        if (updatedSupplier == null)
        {
            return NotFound();
        }

        return Ok(updatedSupplier);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSupplier(int id)
    {
        var deletedSupplier =
            await supplierRepository.DeleteAsync(id);

        if (deletedSupplier == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}