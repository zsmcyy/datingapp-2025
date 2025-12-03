namespace API.Entities;

public class AppUser
{
    // 每次新增一個 AppUser 時，會自動生成一個新的 Guid
    public string Id { get; set; } = Guid.NewGuid().ToString();
    // 必须填写
    public required string DisplayName { get; set; }
    // 必须填写
    public required string Email { get; set; }
}