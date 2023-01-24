using BLApi;
using BlImplementation;

namespace BLApi;

public static class BlFactory
{
    public static IBl GetBl() => Bl.instance;
}