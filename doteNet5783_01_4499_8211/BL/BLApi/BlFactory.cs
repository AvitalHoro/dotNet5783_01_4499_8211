using BLApi;
using BlImplementation;

namespace BlApi;

public static class BlFactory
{
    public static IBl GetBl() => Bl.instance;
}