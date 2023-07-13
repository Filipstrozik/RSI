using RSIapi.Context;
using RSIapi.Models;

namespace RSIapi
{
    public class ContextSeeder
    {

        private readonly ToDoItemContext _context;

        public ContextSeeder(ToDoItemContext context)
        {
            _context = context;
        }


        public void Seed()
        {
            _context.Database.EnsureCreated();
            if (_context.Roles.Any())
            {
                return;
            }
            var roles = GetRoles();
            _context.AddRange(roles);
            _context.SaveChanges();
        }

        private IEnumerable<Role> GetRoles()
        {
            return new Role[]
            {
                new Role { Name = "Admin" },
                new Role { Name = "User" },
                new Role { Name = "Manager" }
            };
        }
    }
}
