public class User
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }

    public string PasswordHash { get; set; }

    public int RoleId { get; set; }

    public Role Role { get; set; }

    public DateTime? LastLogin { get; set; }

    // 🔥 ADD THIS
    public DateTime CreatedAt { get; set; }
}
