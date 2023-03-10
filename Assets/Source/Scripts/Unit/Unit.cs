using Mirror;
using RTS.Configs;
using UnityEngine;
using UnityEngine.Events;
using UniRx;
using System.Collections.Generic;
using System;

namespace RTS.Units
{
    public class Unit : NetworkBehaviour
    {
        [SerializeField] public UnitMover unitMover;
        [field: SerializeField] public UnitType UnitType { get; private set; }
        private FSM fsm;
        public UnityEvent Selected;
        public UnityEvent Unselected;
        public ReactiveCommand<Unit> UnitDestroyed = new ReactiveCommand<Unit>();

        public override void OnStartClient()
        {
            base.OnStartClient();
            CreateFSM();
        }

        private void OnDestroy()
        {
            UnitDestroyed.Execute(this);
        }

        [ClientCallback]
        private void Update()
        {
            fsm?.Run();
        }

        private void CreateFSM()
        {
            Dictionary<Type, IRunState> states = new Dictionary<Type, IRunState>();
            Lazy<FSM> lazyFSM = new Lazy<FSM>(() => fsm);
            states[typeof(IdleState)] = new IdleState();
            states[typeof(MoveState)] = new MoveState(unitMover, lazyFSM);
            states[typeof(AttackState)] = new AttackState();
            fsm = new FSM(states);
            fsm.Enter<IdleState>();
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