using UnityEngine;
using System.Collections;
using GIF.GUIsys;

namespace GIF.Controls
{
	public class CameraControl : MonoBehaviour
	{
		//GUIMouse Inhereitance
		public static float GUIMousex;
		public static float GUIMousey;

		//Focus & Zoom
		public Transform targetCamera; //used to obtain distance between camera and parent later on
		public Transform cameraParent; //parent of camera, also the object to be parented to target of focus

		//Selection Inheritance for Focus
		public static GameObject objectNameInfo; //used to inherit the target of RTSControl
		static GameObject qFocusInfo;
		GameObject lastQFocusInfo;
		GameObject selectedObject;
		GameObject qFocus;
		Vector3 Position;
		bool focused;
		bool qfocused;
		bool geoSynched;

		//ControlActivationFlags
		public bool PanEnabled;
		public bool ZoomEnabled;
		public bool RotateEnabled;

		//Pan	
		public GameObject cameraParentPan;
		public float panSpeed = 0.0f;
		public float internalPanSpeed;
		Vector3 forwardTest;
		float panLeftRight = 0.0f;
		float panForwardBack = 0.0f;
		float panUpDown = 0.0f;
		bool shiftHeld;

		//Zoom
		public float zoomInput;
		public float zoomExecDist;
		public float parentDistance;
		public float zoomStep = 1f;
		public float internalZoomStep;
		public float zoomSpeed = 1f;
		public float maxZoom = 50;       //closest the camera can get to an object
		public float minZoom = 100000;    //farthest the camera can be from an object
		public Vector3 zoomDist;

		//Rotate
		public Transform RotateXTarget;
		public Transform RotateYTarget;
		Quaternion startRotationY;
		Quaternion startRotationX;
		Quaternion startRotationXwithY;
		Quaternion RotationAmountX;
		Quaternion RotationAmountY;
		Quaternion RotationAmountYforX;
		public float xSpeed = 100f;
		public float xRotateSmoothingFactor = 1f;
		public float ySpeed = 100f;
		public float yRotateSmoothingFactor = 1f;
		float zoomAmount;
		public float x;
		public float y;
		Ray ray;
		static public bool rotating;

		void Start()
		{
			startRotationY = RotateYTarget.transform.rotation;
			startRotationX = RotateXTarget.transform.rotation;
			parentDistance = Vector3.Distance(targetCamera.transform.position, cameraParent.transform.position);
			internalZoomStep = zoomStep * (parentDistance / 2);
		}

		void Update()
		{
			GUIMousex = GUIMouse.x;
			GUIMousey = GUIMouse.y;
			objectNameInfo = Camera.main.GetComponent<RTSControlSelect>().selectedObject; //inherit target of RTSControl
			qFocusInfo = RTSControlSelect.qFocus;
			parentDistance = Vector3.Distance(targetCamera.transform.position, cameraParent.transform.position);   //defining parentDistance as distance from camera to parent object
			zoomInput = Input.GetAxis("Mouse ScrollWheel"); //Get scroll wheel input
			internalPanSpeed = panSpeed * (parentDistance / 2);


			//Focus Section
			if (objectNameInfo != null)
				if (Input.GetButtonUp("View"))
				{
					focused = true;
					qfocused = false;
					geoSynched = false;

					InvokeRepeating("CamFollow", 0, .005f);
				}

			if (qFocusInfo != null)
				if (Input.GetMouseButtonUp(1))
				{
					focused = false;
					qfocused = true;
					geoSynched = false;

					lastQFocusInfo = qFocusInfo;
					InvokeRepeating("CamFollow", 0, .005f);
				}

			if (objectNameInfo != null)
				if (Input.GetButtonUp("GeoSynch Camera"))
				{
					geoSynched = true;

					if (focused)
					{
						qfocused = false;
						cameraParentPan.transform.parent = objectNameInfo.transform;
						cameraParentPan.transform.position = objectNameInfo.transform.position;
					}

					if (qfocused)
					{
						focused = false;
						cameraParentPan.transform.parent = qFocusInfo.transform;
						cameraParentPan.transform.position = qFocusInfo.transform.position;
					}

					InvokeRepeating("CamFollow", 0, .005f);
				}

			if (Input.GetButtonUp("FreeCam"))
			{
				focused = false;
				qfocused = false;
				geoSynched = false;

				cameraParentPan.transform.parent = null;

				CancelInvoke("CamFollow");
			}

			//Zoom Section
			if (ZoomEnabled)
			{
				parentDistance = Vector3.Distance(targetCamera.transform.position, cameraParent.transform.position);

				if(zoomInput >= 0)
				{
					internalZoomStep = zoomStep * (parentDistance / 2);
					zoomExecDist -= zoomInput * internalZoomStep;
					zoomExecDist = Mathf.Clamp(zoomExecDist, maxZoom, minZoom);
					zoomDist.z = Mathf.Lerp(transform.localPosition.z, -zoomExecDist, Time.deltaTime * zoomSpeed);
					targetCamera.transform.localPosition = zoomDist;
				}

				if(zoomInput <= 0)
				{
					internalZoomStep = zoomStep * (parentDistance * 2);
					zoomExecDist -= zoomInput * internalZoomStep;
					zoomExecDist = Mathf.Clamp(zoomExecDist, maxZoom, minZoom);
					zoomDist.z = Mathf.Lerp(transform.localPosition.z, -zoomExecDist, Time.deltaTime * zoomSpeed);
					targetCamera.transform.localPosition = zoomDist;
				}
			}

			//Rotation Section
			if (RotateEnabled)
			{
				if (Input.GetMouseButtonDown(1))
				{
					rotating = true;
				}

				if (Input.GetMouseButton(1))
				{
					y += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
					x += Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

					RotateY();
				}
			}

			if (Input.GetMouseButtonUp(1))
			{
				rotating = false;
				//Input.mousePosition = ray;
			}

			//Debug.Log(rotating);

			//Pan Section
			if (PanEnabled)
			{
				panLeftRight = 0.0f;
				panForwardBack = 0.0f;
				panUpDown = 0.0f;

				if (Input.GetKeyDown(KeyCode.RightShift) | Input.GetKeyDown(KeyCode.LeftShift))
				{
					shiftHeld = true;
				}

				if (Input.GetKeyUp(KeyCode.RightShift) | Input.GetKeyUp(KeyCode.LeftShift))
				{
					shiftHeld = false;
				}

				if ((GUIMousex) > Screen.width - (Screen.width * 0.05))
				{
					panLeftRight = 1.0f;
				}

				if ((GUIMousex) < Screen.width * 0.05)
				{
					panLeftRight = -1.0f;
				}

				if ((GUIMousey) > Screen.height - (Screen.height * 0.05) && shiftHeld == false)
				{
					panForwardBack = 1.0f;
				}

				if ((GUIMousey) < Screen.height * 0.05 && shiftHeld == false)
				{
					panForwardBack = -1.0f;
				}

				if ((GUIMousey) > Screen.height - (Screen.height * 0.05) && shiftHeld == true)
				{
					panUpDown = 1.0f;
				}

				if ((GUIMousey) < Screen.height * 0.05 && shiftHeld == true)
				{
					panUpDown = -1.0f;
				}

				cameraParentPan.transform.Translate(Vector3.right * Time.deltaTime * panLeftRight * internalPanSpeed);
				cameraParentPan.transform.Translate(Vector3.forward * Time.deltaTime * panForwardBack * internalPanSpeed);
				cameraParentPan.transform.Translate(Vector3.up * Time.deltaTime * panUpDown * internalPanSpeed);
			}
		}

		//zoom section 2
		//void ZoomIn ()
		//{

		//}

		//void ZoomOut ()
		//{

		//}

		//Rotation section 2
		void RotateY()
		{
			RotationAmountY = Quaternion.Euler(0, y, 0);
			RotateYTarget.transform.rotation = Quaternion.Lerp(RotateYTarget.transform.rotation, RotationAmountY * startRotationY, Time.deltaTime * yRotateSmoothingFactor);
//            RotationAmountYforX = RotateYTarget.transform.rotation;
			RotateYforX();
		}

		void RotateYforX()
		{
			RotationAmountX = Quaternion.Euler(x, RotateYTarget.transform.rotation.eulerAngles.y, 0);
//            RotateXTarget.transform.rotation = RotationAmountYforX;
//			RotateXTarget.transform.rotation = Quaternion.Euler(RotateXTarget.transform.rotation.eulerAngles.x, transform.parent.rotation.eulerAngles.y, 0);
			RotateXTarget.transform.rotation = Quaternion.Lerp(RotateXTarget.transform.rotation, RotationAmountX * startRotationX, Time.deltaTime * xRotateSmoothingFactor);
		}

		//SmoothFollow Section
		void CamFollow()
		{
			if (!geoSynched)
				if (!qfocused)
					if (focused)
						if (objectNameInfo)
						{
							cameraParentPan.transform.position = objectNameInfo.transform.position;
						}

			if (!geoSynched)
				if (qfocused)
					if (!focused)
						if (lastQFocusInfo)
						{
							cameraParentPan.transform.position = lastQFocusInfo.transform.position;
						}

			if (geoSynched)
				if (!qfocused)
					if (!focused)
						if (objectNameInfo)
						{
							cameraParentPan.transform.position = objectNameInfo.transform.position;
							cameraParentPan.transform.rotation = objectNameInfo.transform.rotation;
						}
		}
	}
}