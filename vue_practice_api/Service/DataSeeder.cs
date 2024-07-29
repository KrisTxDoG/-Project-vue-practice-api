using Microsoft.AspNetCore.Identity;

public class DataSeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public DataSeeder(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task SeedRolesAndUsersAsync()
    {
        // 創建角色
        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        if (!await _roleManager.RoleExistsAsync("User"))
        {
            await _roleManager.CreateAsync(new IdentityRole("User"));
        }

        // 創建用戶並分配角色
        var adminUser = await _userManager.FindByEmailAsync("admin@example.com");
        if (adminUser == null)
        {
            adminUser = new IdentityUser { UserName = "admin@example.com", Email = "admin@example.com" };
            await _userManager.CreateAsync(adminUser, "AdminPassword123!");
        }
        if (!await _userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await _userManager.AddToRoleAsync(adminUser, "Admin");
        }

        var regularUser = await _userManager.FindByEmailAsync("user@example.com");
        if (regularUser == null)
        {
            regularUser = new IdentityUser { UserName = "user@example.com", Email = "user@example.com" };
            await _userManager.CreateAsync(regularUser, "UserPassword123!");
        }
        if (!await _userManager.IsInRoleAsync(regularUser, "User"))
        {
            await _userManager.AddToRoleAsync(regularUser, "User");
        }
    }
}
