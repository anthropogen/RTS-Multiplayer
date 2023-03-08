using RTS.Configs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace RTS.Infrastucture
{
    public class ConfigProvider : IConfigProvider
    {
        private readonly Dictionary<UnitType, UnitConfig> unitConfigs;

        public ConfigProvider()
        {
            unitConfigs = Resources.LoadAll<UnitConfig>("Configs/").
                ToDictionary(x => x.UnitType);
        }

        public UnitConfig GetUnitConfig(UnitType type)
        {
            UnitConfig config = null;
            if (unitConfigs.TryGetValue(type, out config))
                return config;

            throw new InvalidOperationException($"Doesn't have config for {type}");
        }
    }
}