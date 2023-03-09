using Mirror;
using RTS.Configs;
using RTS.Unit;
using UnityEngine;
using UnityEngine.Events;

public class Unit : NetworkBehaviour
{
    [field: SerializeField] public UnitType UnitType { get; private set; }
    [field: SerializeField] public UnitMover UnitMover { get; private set; }

    public UnityEvent Selected;
    public UnityEvent Unselected;

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
