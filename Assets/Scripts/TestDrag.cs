using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    bool dragging = false;
    Vector3 originalPos;
    static TestDrag draggedObject;

    static GraphicRaycaster rayCaster;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPos = transform.position;   
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = originalPos;
        PointerEventData PointerEventData = new PointerEventData(EventSystem.current);
        PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();
        rayCaster.Raycast(PointerEventData, results);
        foreach(RaycastResult result in results)
        {
            Debug.Log(result.gameObject.name);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!rayCaster)
        {
            rayCaster = FindObjectOfType<GraphicRaycaster>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    if(Physics.Raycast(ray, out RaycastHit hitInfo))
        //    {
        //        Debug.Log(hitInfo.transform.name);
        //        if(hitInfo.collider.gameObject == gameObject)
        //        {
        //            dragging = true;
        //            originalPos = transform.position;
        //            draggedObject = this;
        //        }
        //    }
        //}
        //if(Input.GetKey(KeyCode.Mouse0))
        //{
        //    if(dragging && draggedObject == this)
        //    {
        //        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        newPosition.z = 0;
        //        transform.position = newPosition;
        //    }
        //}
        //if(Input.GetKeyUp(KeyCode.Mouse0))
        //{
        //    if(dragging)
        //    {
        //        transform.position = originalPos;
        //        Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(transform.position));

        //        PointerEventData PointerEventData = new PointerEventData(EventSystem.current);
        //        PointerEventData.position = Input.mousePosition;

        //        //Create a list of Raycast Results
        //        List<RaycastResult> results = new List<RaycastResult>();
        //        rayCaster.Raycast(PointerEventData, results);
        //        foreach(RaycastResult result in results)
        //        {
        //            Debug.Log(result.gameObject.name);
        //        }
        //        draggedObject = null;
        //        dragging = false;
        //    }
        //}
    }
}
