using UnityEngine;

namespace RTS.Configs
{
    [CreateAssetMenu(fileName = "newUnitConfig", menuName = "Configs/Unit", order = 51)]
    public class UnitConfig : ScriptableObject
    {
        [field: SerializeField] public UnitType UnitType { get; private set; }
        [field: SerializeField] public MovementUnitData MovementData { get; private set; }

    }
}