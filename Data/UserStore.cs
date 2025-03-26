using backend.Models;

namespace backend.Data
{
    public static class UserStore
    {
        public static readonly List<User> _users = new()
        {
            new User { Id = 1, Name = "Jhon Doe" },
            new User { Id = 2, Name = "Jane Doe" }
        };
    }
}
