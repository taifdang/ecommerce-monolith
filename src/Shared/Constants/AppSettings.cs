using System.ComponentModel.DataAnnotations;

namespace Shared.Constants;

public class AppSettings
{
    public Identity Identity { get; set; }
    public FileStorageSettings FileStorageSettings { get; set; }
    public ConnectionStrings ConnectionStrings { get; set; }
    public string BaseUrl { get; set; } = "https://localhost:7129/";
}
public class ConnectionStrings
{
    [Required]
    public string DefaultConnection { get; set; }
}
public class FileStorageSettings
{
    public bool LocalStorage { get; set; } = true;
    [Required]
    public string Path { get; set; } = "";
}
public class Identity
{   
    [Required]
    public string Key { get; set; }
    [Required]
    public string Authority { get; set; }
    [Required]
    public string Audience { get; set; }
    public int ExpiredTime { get; set; }
}
