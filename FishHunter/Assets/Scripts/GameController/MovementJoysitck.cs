using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MovementJoysitck : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joysitckBG;

    public Vector2 joysiickVec; //vector chinh de dieu khien joysick va player

    private Vector2 joystickTouchPos;
    private Vector2 joysickOriginalPos;

    private float joysitckRadius;

    public bool check = false;
    public bool dragging = false;

    // Start is called before the first frame update
    void Start()
    {
        joysickOriginalPos = joysitckBG.transform.position;
        joysitckRadius = joysitckBG.GetComponent<RectTransform>().sizeDelta.y / 2;
    }
    private void Update()
    {
        if(Input.touchCount > 0 ){
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;
          
            if (touch.phase == TouchPhase.Began)
            {
                joystick.transform.position = touchPosition;
                joysitckBG.transform.position = touchPosition;
                joystickTouchPos = touchPosition;
                dragging = true;
            }
            if (touch.phase == TouchPhase.Moved && dragging)
            {
                Vector2 dragPos = touchPosition;


                joysiickVec = (dragPos - joystickTouchPos).normalized;

                float joysickDist = Vector2.Distance(dragPos, joystickTouchPos);

                if (joysickDist < joysitckRadius)
                {
                    joystick.transform.position = joystickTouchPos + joysiickVec * joysickDist;
                }
                else
                {
                    joystick.transform.position = joystickTouchPos + joysiickVec * joysitckRadius;
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                joysiickVec = Vector2.zero;
                joystick.transform.position = joysickOriginalPos;
                joysitckBG.transform.position = joysickOriginalPos;
                dragging = false;
                Player.instance.UnbootSpeed();
            }
            if (Input.touchCount == 2 && dragging)
            {
                touch = Input.GetTouch(1);
                if (touch.phase == TouchPhase.Began)
                {
                    Player.instance.BootSpeed();
                   
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    Player.instance.UnbootSpeed();
                }
            }
        }
    }
    public void PointerDown()
    {
        //if (!check)
        //{
            joystick.transform.position = Input.mousePosition;
            joysitckBG.transform.position = Input.mousePosition;
            joystickTouchPos = Input.mousePosition;
        //}
        //else
        //{
        //    return;
        //}
    }

    public void Drag(BaseEventData baseEventData)
    {
       // if (Input.touchCount < 2)
        {
            PointerEventData pointerEventData = baseEventData as PointerEventData;
            Vector2 dragPos = pointerEventData.position;
            joysiickVec = (dragPos - joystickTouchPos).normalized;
        

            float joysickDist = Vector2.Distance(dragPos, joystickTouchPos);

            if (joysickDist < joysitckRadius)
            {
                joystick.transform.position = joystickTouchPos + joysiickVec * joysickDist;
            }
            else
            {
                joystick.transform.position = joystickTouchPos + joysiickVec * joysitckRadius;
            }
        }
    }

    public void PointerUp()
    {
        joysiickVec = Vector2.zero;
        joystick.transform.position = joysickOriginalPos;
        joysitckBG.transform.position = joysickOriginalPos;
    }

   
}
