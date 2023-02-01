using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform transformRect;
    public Canvas canvas;
    public Canvas WindowCanvas;

    public void OnDrag(PointerEventData eventData) {
        transformRect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData){
        transformRect.SetAsLastSibling();
    }

    private void Start() {
        transformRect = this.gameObject.GetComponent<RectTransform>();
    }

    public void Delete(){
        Destroy(this.gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData){
        Camera.main.gameObject.GetComponent<CameraController>()._enableMovement = false;
    }

    public void OnPointerExit(PointerEventData eventData){
    Camera.main.gameObject.GetComponent<CameraController>()._enableMovement = true;
    }

}
