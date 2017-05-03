using UnityEngine;
using UnityEngine.VR;

public class VRControllerTrackerNative : MonoBehaviour {
    private static string OPENVR_CONTROLLER_LEFT = "OpenVR Controller - Left";
    private static string OPENVR_CONTROLLER_RIGHT = "OpenVR Controller - Right";
    private int controllerIndex = -1;
    public VRNode vrNode;

	// Use this for initialization
	void Start () {
        string[] inputDevices = Input.GetJoystickNames();

        for (int deviceIndex = 0; deviceIndex < inputDevices.Length; deviceIndex++) {
            string deviceName = inputDevices[deviceIndex];
            Debug.Log(deviceName);

            string controllerToTestFor = null;
            if (vrNode == VRNode.LeftHand) {
                controllerToTestFor = OPENVR_CONTROLLER_LEFT;
            }
            else if(vrNode == VRNode.RightHand) {
                controllerToTestFor = OPENVR_CONTROLLER_RIGHT;
            }

            if (deviceName.Equals(controllerToTestFor)) {
                controllerIndex = deviceIndex;
                break;
            }

        }
    }
	
	// Update is called once per frame
	void Update () {
        if(controllerIndex == -1) {
            Debug.LogWarning("Controller not found");
            return;
        }

        if (!IsControllerStillConnected()) {
            Debug.LogWarning("Device disconnected");
            return;
        }

        // Update game object with the position and rotation of the VR controller
        Vector3 handPosition = InputTracking.GetLocalPosition(vrNode);
        //Debug.Log("Hand Position = " + handPosition);
        gameObject.transform.localPosition = handPosition;

        Quaternion handRotation = InputTracking.GetLocalRotation(vrNode);
        //Debug.Log("Hand Rotation = " + handRotation);
        gameObject.transform.localRotation = handRotation;
    }

    private bool IsControllerStillConnected() {
        string[] inputDevices = Input.GetJoystickNames();
        return (inputDevices[controllerIndex] != null);
    }

}
