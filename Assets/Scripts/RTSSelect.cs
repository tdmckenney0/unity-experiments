using UnityEngine;
using System.Collections;

namespace GIF.Controls
{
    public class RTSSelect : MonoBehaviour
    {

        public static GameObject selectedObject;
        public static GameObject qFocus;
        public static GameObject targetObject;
        Color original;

        void Start()
        {

        }

        void OnMouseEnter()
        {
            original = GetComponent<Renderer>().material.color;
        }

        void OnMouseOver()
        {
            GetComponent<Renderer>().material.color = Color.green;

            if (Input.GetMouseButtonUp(0))
            {
                selectedObject = (gameObject);
                Debug.Log(selectedObject);
            }

            if (Input.GetMouseButtonUp(1))
            {
                targetObject = (gameObject);
                selectedObject.SendMessage("SetTarget", targetObject);
            }

            if (Input.GetMouseButtonUp(2))
            {
                qFocus = (gameObject);
            }
        }

        void OnMouseExit()
        {
            GetComponent<Renderer>().material.color = original;

            qFocus = (null);
        }

    }
}