
namespace BLApi;

public static class BlFactory
{
    public static IBl Get() => new BlImplementation.Bl();
}
