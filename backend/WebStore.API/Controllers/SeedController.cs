using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.Model;
using WebStore.DAL;

namespace WebStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<SeedController> _logger;
    private readonly WebStoreDbContext _context;

    public SeedController(UserManager<User> userManager, ILogger<SeedController> logger, WebStoreDbContext context)
    {
        _userManager = userManager;
        _logger = logger;
        _context = context;
    }

    [HttpPost("create-test-user")]
    public async Task<IActionResult> CreateTestUser()
    {
        try
        {
            // Try to delete existing user if exists
            var existingUser = await _userManager.FindByEmailAsync("test@test.test");
            if (existingUser != null)
            {
                await _userManager.DeleteAsync(existingUser);
            }

            var testUser = new User
            {
                UserName = "test@test.test",
                Email = "test@test.test",
                FirstName = "Test",
                LastName = "User",
                CreatedAt = DateTime.Now,
                IsActive = true,
                UserType = "Customer"
            };

            var result = await _userManager.CreateAsync(testUser, "pass123");

            if (result.Succeeded)
            {
                _logger.LogInformation("Test user created successfully");
                return Ok(new { message = "Test user created successfully", email = "test@test.test", password = "pass123" });
            }
            else
            {
                _logger.LogError("Failed to create test user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                return BadRequest(new { message = "Failed to create test user", errors = result.Errors.Select(e => e.Description) });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating test user");
            return StatusCode(500, new { message = "Error creating test user", error = ex.Message });
        }
    }

    [HttpPost("seed-data")]
    public async Task<IActionResult> SeedData()
    {
        try
        {
            // Create a default category if it doesn't exist
            if (!await _context.Set<Category>().AnyAsync())
            {
                _context.Add(new Category
                {
                    Name = "General",
                    Description = "General category for products",
                    IsActive = true,
                    CreatedAt = DateTime.Now
                });
                await _context.SaveChangesAsync();
            }

            // Create a default supplier if it doesn't exist
            if (!await _context.Set<Supplier>().AnyAsync())
            {
                _context.Add(new Supplier
                {
                    CompanyName = "General Supplier",
                    ContactName = "Admin",
                    Email = "supplier@example.com",
                    Phone = "+1234567890",
                    Address = "123 Main St",
                    City = "City",
                    Country = "Country",
                    IsActive = true,
                    CreatedAt = DateTime.Now
                });
                await _context.SaveChangesAsync();
            }

            return Ok(new { message = "Seed data created successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error seeding data");
            return StatusCode(500, new { message = "Error seeding data", error = ex.Message });
        }
    }
}
