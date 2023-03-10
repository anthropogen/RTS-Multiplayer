using Mirror;
using RTS.UI;
using RTS.Units;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RTS.Management
{
    public class UnitSelector : NetworkBehaviour
    {
        [SerializeField] private LayerMask unitMask;
        [SerializeField] private Player player;
        private readonly HashSet<Unit> selectedUnits = new HashSet<Unit>();
        private SelectionAreaView selectionArea;
        private Camera cam;
        public IEnumerable<Unit> SelectedUnits => selectedUnits;

        public void Construct(SelectionAreaView selectionAreaView)
        {
            selectionArea = selectionAreaView;
        }

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
            cam = Camera.main;
        }

        [ClientCallback]
        private void Update()
        {
            if (!isOwned) return;

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (!Keyboard.current[Key.LeftShift].isPressed)
                    ClearSelectionUnits();

                StartSelectionArea();
            }
            else if (Mouse.current.leftButton.isPressed)
            {
                UpdateSelectionArea();
            }
            else if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                EndSelect();
            }
        }

        private void EndSelect()
        {
            SelectUnits();
            selectionArea.EndSelect();
        }

        private void StartSelectionArea()
        {
            selectionArea.StartSelect(Mouse.current.position.ReadValue());
            selectionArea.UpdateSelect(Mouse.current.position.ReadValue());
        }

        private void UpdateSelectionArea()
        {
            selectionArea.UpdateSelect(Mouse.current.position.ReadValue());
        }

        private void ClearSelectionUnits()
        {
            if (selectedUnits.Count == 0)
                return;

            foreach (var u in selectedUnits)
                u.Unselect();

            selectedUnits.Clear();
        }

        private void SelectUnits()
        {
            SelectUnitOnMousePosition();
            SelectUnitInSelectionArea();

            foreach (var u in selectedUnits)
                u.Select();
        }

        private void SelectUnitInSelectionArea()
        {
            foreach (var unit in player.Units)
            {
                var unitScreenPos = cam.WorldToScreenPoint(unit.transform.position);
                if (selectionArea.Contains(unitScreenPos))
                {
                    selectedUnits.Add(unit);
                }
            }
        }

        private void SelectUnitOnMousePosition()
        {
            var ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out var hit, 100, unitMask))
            {
                if (hit.collider.TryGetComponent(out Unit unit))
                {
                    selectedUnits.Add(unit);
                }
            }
        }
    }
}