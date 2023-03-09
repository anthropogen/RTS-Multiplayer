using Mirror;
using RTS.Configs;
using UnityEngine;
using UnityEngine.Events;
using UniRx;

namespace RTS.Units
{
    public class Unit : NetworkBehaviour
    {
        [field: SerializeField] public UnitType UnitType { get; private set; }
        [field: SerializeField] public UnitMover UnitMover { get; private set; }
        public UnityEvent Selected;
        public UnityEvent Unselected;
        public ReactiveCommand<Unit> UnitDestroyed = new ReactiveCommand<Unit>();

        private void OnDestroy()
        {
            UnitDestroyed.Execute(this);
        }

        #region Client

        [Client]
        public void Select()
        {
            if (!isOwned) return;
            Selected?.Invoke();
        }

        [Client]
        public void Unselect()
        {
            if (!isOwned) return;
            Unselected?.Invoke();
        }

        #endregion
    }
}