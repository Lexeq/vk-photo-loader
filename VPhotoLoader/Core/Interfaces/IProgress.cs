
namespace VPhotoLoader
{
    public interface ITaskProgress
    {
        void Report(int value);

        void Report(int value, object state);
    }
}