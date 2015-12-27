namespace SharedLayer.Interfaces
{
    public interface IClientIdentity
    {
        string Name { get; set; }
        string Password { get; set; }
        string SecretKey { get; set; }
    }
}