using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEngine.Windows.WebCam;

public class scroll_map : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private Vector3 posAwal1;
    [SerializeField] private GameObject target;
    [SerializeField] private bool isY = false;
    [SerializeField] private float targetSpeed;
    [SerializeField] private Image scroll;
    // Start is called before the first frame update
    void Awake()
    {
        posAwal1 = target.transform.localPosition;
        //offset = new Vector3[scroll.Length];
        //Debug.Log("A");
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(Vector2.Distance(target.transform.localPosition, posAwal));
    }
    float offset = 0;
    //float offset2 = 0;
    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = 0;
        //offset2 = 0;
    }
    Vector3 lastPosition;
    public void OnDrag(PointerEventData eventData)
    {

        if(isY)
        {
            offset += Input.GetAxis("Mouse Y");
            //offset += Input.GetTouch(0).deltaPosition.normalized.y * 0.2f;
            if (offset < -1 || offset > 1)
            {
                offset = 0;
            }
            scroll.material.SetFloat("_offset", offset);
        }
        else
        {
            offset += Input.GetAxis("Mouse X");
            //offset += Input.GetTouch(0).deltaPosition.normalized.x * -0.2f;
            if (offset < -1 || offset > 1)
            {
                offset = 0;
            }
            scroll.material.SetFloat("_offset", offset);

        }
        if (Vector2.Distance(target.transform.localPosition, posAwal1) <= 47f)
        {
            //Debug.Log(Input.GetTouch(0));
            if(isY)
            {
                target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + (Input.GetAxis("Mouse Y") * targetSpeed), target.transform.position.z);
                //target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + (Input.GetTouch(0).deltaPosition.normalized.y * targetSpeed), target.transform.position.z);
            }
            else
            {
                target.transform.position = new Vector3(target.transform.position.x + (Input.GetAxis("Mouse X") * targetSpeed), target.transform.position.y, target.transform.position.z);
                //target.transform.position = new Vector3(target.transform.position.x + (Input.GetTouch(0).deltaPosition.normalized.x * targetSpeed), target.transform.position.y, target.transform.position.z);
            }
            lastPosition = target.transform.localPosition;
        }
        else
        {
            //target.transform.localPosition = target.transform.localPosition - (lastPosition-posAwal1).normalized;
            target.transform.localPosition = Vector3.MoveTowards(target.transform.localPosition, posAwal1,Time.deltaTime*10f);
/*            if (target.transform.localPosition.y > 48)
            {
                target.transform.localPosition = new Vector3(target.transform.localPosition.x, 47, target.transform.localPosition.z);
            }
            else if (target.transform.localPosition.y < -48)
            {
                target.transform.localPosition = new Vector3(target.transform.localPosition.x, -47, target.transform.localPosition.z);
            }
            if (target.transform.localPosition.x > 48)
            {
                target.transform.localPosition = new Vector3(48, target.transform.localPosition.y, target.transform.localPosition.z);
            }
            else if (target.transform.localPosition.x < -48)
            {
                target.transform.localPosition = new Vector3(-48, target.transform.localPosition.y, target.transform.localPosition.z);
            }*/
        }
        /*        if (isY && target.transform.localPosition.y <= 47 && target.transform.localPosition.y >= -47)
                {
                    offset += Input.GetAxis("Mouse Y");
                    if (offset < -1 || offset > 1)
                    {
                        offset = 0;
                    }
                    scroll.material.SetFloat("_offset", offset);
                    target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + (Input.GetAxis("Mouse Y") * targetSpeed), target.transform.position.z);
                }
                else
                {
                    if (target.transform.localPosition.y > 47)
                    {
                        target.transform.localPosition = new Vector3(target.transform.localPosition.x, 47, target.transform.localPosition.z);
                    }
                    else if (target.transform.localPosition.y < -47)
                    {
                        target.transform.localPosition = new Vector3(target.transform.localPosition.x, -47, target.transform.localPosition.z);
                    }
                }
        if (!isY && target.transform.localPosition.x <= 47 && target.transform.localPosition.x >= -47)
        {
            offset += Input.GetAxis("Mouse X");
            if (offset < -1 || offset > 1)
            {
                offset = 0;
            }
            scroll.material.SetFloat("_offset", offset);
            target.transform.position = new Vector3(target.transform.position.x + (Input.GetAxis("Mouse X") * targetSpeed), target.transform.position.y, target.transform.position.z);
        }
        else
        {
            if (target.transform.localPosition.x > 47)
            {
                target.transform.localPosition = new Vector3(47, target.transform.localPosition.y, target.transform.localPosition.z);
            }
            else if (target.transform.localPosition.x < -47)
            {
                target.transform.localPosition = new Vector3(-47, target.transform.localPosition.y, target.transform.localPosition.z);
            }
        }*/

/*        Vector2 inputMouse = Vector2.zero;
        inputMouse.x += Input.mousePosition.x;
        inputMouse.y += Input.mousePosition.y;
        Vector2 posAwal = scroll.transform.localPosition;
        if (isY)
        {
            if (scroll.transform.localPosition.y <= 75 && scroll.transform.localPosition.y >= -75)
            {
                scroll.transform.position = new Vector3(scroll.transform.position.x, inputMouse.y, scroll.transform.position.z);
            }
            else if (scroll.transform.localPosition.y > 75)
            {
                //inputMouse.y = posAwal.y;
                inputMouse.y = 0;
                scroll.transform.localPosition = new Vector3(scroll.transform.localPosition.x, -75, scroll.transform.localPosition.z);
                //scroll.transform.localPosition = new Vector3(scroll.transform.localPosition.x, -75, scroll.transform.localPosition.z);
            }
            else if (scroll.transform.localPosition.y < -75)
            {
                //inputMouse.y = posAwal.y;
                inputMouse.y = 0;
                scroll.transform.localPosition = new Vector3(scroll.transform.localPosition.x, 75, scroll.transform.localPosition.z);
                //scroll.transform.localPosition = new Vector3(scroll.transform.localPosition.x, 75, scroll.transform.localPosition.z);
            }
            //scroll.transform.position = new Vector3(scroll.transform.position.x, Input.mousePosition.y, scroll.transform.position.z);
        }
        else
        {
            if (scroll.transform.localPosition.x <= 75 && scroll.transform.localPosition.x >= -75)
            {
                scroll.transform.position = new Vector3(inputMouse.x, scroll.transform.position.y, scroll.transform.position.z);
            }
            else if (scroll.transform.localPosition.y > 75)
            {

                scroll.transform.localPosition = new Vector3(-75, scroll.transform.localPosition.x, scroll.transform.localPosition.z);
            }
            else if (scroll.transform.localPosition.y < -75)
            {
                scroll.transform.localPosition = new Vector3(75, scroll.transform.localPosition.x, scroll.transform.localPosition.z);
            }
            //scroll.transform.position = new Vector3(Input.mousePosition.x, scroll.transform.position.y, scroll.transform.position.z);
        }*/
    }
    public void resetTargetPos()
    {
        target.GetComponent<RectTransform>().localPosition = Vector3.zero;
    }
}
