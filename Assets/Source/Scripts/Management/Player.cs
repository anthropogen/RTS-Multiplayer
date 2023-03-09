using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using RTS.Units;

namespace RTS.Management
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] private UnitSpawner unitSpawner;
        private readonly List<Unit> units = new List<Unit>();
    }
}