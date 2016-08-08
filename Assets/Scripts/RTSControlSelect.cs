using UnityEngine;
using System.Collections;
using GIF.GUIsys;

namespace GIF.Controls
{
    public class RTSControlSelect : MonoBehaviour
    {

        public GameObject selectedObject;
        public static GameObject qFocus;
        public static GameObject targetObject;
        public GameObject lastselectedObject;
        public bool moveSysActive;

        void Start()
        {

        }

        void Update()
        {

//            moveSysActive = RTSControl.movementSystemActive;

            Ray ray;

            RaycastHit hit;

            ray = Camera.main.ScreenPointToRay(new Vector3(GUIMouse.x, GUIMouse.y, 0));

            if (Input.GetMouseButtonUp(0))
            {

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.red, 50);
                    selectedObject = hit.collider.gameObject;
                    lastselectedObject = selectedObject;

                    Debug.Log(selectedObject);
                }

                else
                {
                    selectedObject = null;
                }
            }

            if (Input.GetMouseButtonUp(1))
            {

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.red, 50);
                    qFocus = hit.collider.gameObject;
                }

                else
                {
                    qFocus = null;
                }
            }

            if (Input.GetMouseButtonUp(2))
            {

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.red, 50);
                    targetObject = hit.collider.gameObject;
                }

                else
                {
                    targetObject = null;
                }
            }
        }
    }
}