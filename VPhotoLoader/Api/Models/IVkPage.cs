namespace VPhotoLoader.Api
{
    public interface IVkPage
    {
        int ID { get; }
        bool Deactivated { get; }
        string DeactivatedReason { get; }
    }
}
