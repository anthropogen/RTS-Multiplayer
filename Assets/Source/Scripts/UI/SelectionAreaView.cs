using System;
using UnityEngine;
using UnityEngine.UI;

namespace RTS.UI
{
    public class SelectionAreaView : MonoBehaviour
    {
        [SerializeField] private Image areaImage;
        [SerializeField] private RectTransform parent;
        [SerializeField] private CanvasScaler canvasScaler;
        private Vector2 startSelectPositon;

        private void Start()
        {
            areaImage.enabled = false;
        }

        public void StartSelect(Vector2 mousePosition)
        {
            startSelectPositon = GetCanvasPosition(mousePosition);
            areaImage.enabled = true;
        }

        public void UpdateSelect(Vector2 mousePosition)
        {
            Vector2 position = GetCanvasPosition(mousePosition);
            float width = position.x - startSelectPositon.x;
            float height = position.y - startSelectPositon.y;
            areaImage.rectTransform.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
            Vector2 newPos = startSelectPositon + new Vector2(width / 2f, height / 2f);
            areaImage.rectTransform.anchoredPosition = newPos;
        }

        public void EndSelect()
        {
            areaImage.enabled = false;
        }
        public bool Contains(Vector3 unitPos)
        {
            var canvasPos = GetCanvasPosition(unitPos);
            var min = areaImage.rectTransform.anchoredPosition - (areaImage.rectTransform.sizeDelta / 2);
            var max = areaImage.rectTransform.anchoredPosition + (areaImage.rectTransform.sizeDelta / 2);

            if (canvasPos.x > min.x && canvasPos.x < max.x && canvasPos.y > min.y && canvasPos.y < max.y)
                return true;
            return false;
        }
        private Vector2 GetCanvasPosition(Vector2 screenPosition)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, screenPosition, null, out var locpos);
            return locpos + (canvasScaler.referenceResolution / 2);
        }


    }
}