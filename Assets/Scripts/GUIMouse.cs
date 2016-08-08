using UnityEngine;
using System.Collections;
using GIF.Controls;

namespace GIF.GUIsys
{
    public class GUIMouse : MonoBehaviour
    {

        public Texture2D cursorImage;
        private int cursorWidth = 32;
        private int cursorHeight = 32;
        public static float x = 0;
        public static float y = 0;
        float xSens;
        float ySens;
        bool shiftHeld;
        bool movementActive;

        void Start()
        {
            xSens = 10;
            ySens = 10;
            Cursor.visible = false;
            Screen.lockCursor = false;
            y = Screen.height / 2;
            x = Screen.width / 2;
        }

        void OnGUI()
        {

            if (y < 0)
            {
                y = 0;
            }
            if (y > Screen.height)
            {
                y = Screen.height;
            }
            if (x < 0)
            {
                x = 0;
            }
            if (x > Screen.width)
            {
                x = Screen.width;
            }
            if (CameraControl.rotating == false)
            {
                y += Input.GetAxis("Mouse Y") * xSens;
                x += Input.GetAxis("Mouse X") * ySens;
            }

            UnityEngine.GUI.DrawTexture(new Rect(x, (Screen.height - y), cursorWidth, cursorHeight), cursorImage);
        }
    }
}