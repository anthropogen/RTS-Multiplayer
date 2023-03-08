using RTS.Configs;

namespace RTS.Infrastucture
{
    public interface IConfigProvider
    {
        UnitConfig GetUnitConfig(UnitType type);
    }
}