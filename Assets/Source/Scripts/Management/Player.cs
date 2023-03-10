using Mirror;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Unit = RTS.Units.Unit;

namespace RTS.Management
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] private UnitSpawner unitSpawner;
        [SerializeField] private List<Unit> units = new List<Unit>();
        private readonly CompositeDisposable disposable = new CompositeDisposable();
        public IEnumerable<Unit> Units => units;

        private void Start()
        {
            unitSpawner.UnitSpawned.
                Subscribe<Unit>(u => OnUnitSpawned(u)).
                AddTo(disposable);
        }

        private void OnUnitSpawned(Unit unit)
        {
            if (unit.connectionToClient.connectionId != connectionToClient.connectionId)
                return;

            units.Add(unit);
            unit.UnitDestroyed.
                Subscribe<Unit>(u => OnUnitDestroyed(u))
                .AddTo(unit);
        }

        private void OnUnitDestroyed(Unit unit)
        {
            units.Remove(unit);
        }
    }
}