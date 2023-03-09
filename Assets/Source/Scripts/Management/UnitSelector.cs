using Mirror;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelector : NetworkBehaviour
{
    [SerializeField] private LayerMask unitMask;
    private readonly List<Unit> selectedUnits = new List<Unit>();
    private Camera cam;
    public IEnumerable<Unit> SelectedUnits => selectedUnits;

    private void Start()
    {
        cam = Camera.main;
    }

    [ClientCallback]
    private void Update()
    {
        if (!isOwned) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            ClearSelectionUnits();
        }
        else if (Mouse.current.leftButton.isPressed)
        {

        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            ClearSelectionArea();
        }
    }

    private void ClearSelectionUnits()
    {
        if (selectedUnits.Count == 0)
            return;

        foreach (var u in selectedUnits)
            u.Unselect();

        selectedUnits.Clear();
    }

    private void ClearSelectionArea()
    {
        var ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out var hit, 100, unitMask))
        {
            if (hit.collider.TryGetComponent(out Unit unit))
            {
                selectedUnits.Add(unit);
            }
        }
        foreach (var u in selectedUnits)
            u.Select();
    }
}
