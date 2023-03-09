using RTS.Configs;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [field: SerializeField] public UnitType UnitType { get; private set; }
}
