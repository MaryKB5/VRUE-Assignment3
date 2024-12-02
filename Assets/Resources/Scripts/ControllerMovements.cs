using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class ControllerMovements : MonoBehaviourPun
{
    public float recognizedMovementLimit = 0.66f;
    public float velocity = 10.0f;
    // public ActionBasedController leftController;
    private UnityEngine.XR.InputDevice leftDevice;
    public InputActionReference leftDevicePose;
    // public ActionBasedController rightController;
    private UnityEngine.XR.InputDevice rightDevice;

    
    private CustomContinuousMoveProvider moveProvider;

    public InputActionReference rightDevicePose;
    
    // Start is called before the first frame update
    void Start()
    {
        moveProvider = GetComponent<CustomContinuousMoveProvider>();
        
        
        leftDevice = UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);    
        rightDevice = UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.RightHand);    
    }

     // Next update in second
    private float nextUpdate = 0.1f;
    
    // Update is called once per frame
    void Update() {
        if (photonView.IsMine) {
            if (Time.time >= nextUpdate) { // If the next update is reached
                //Debug.Log(Time.time + ">=" + nextUpdate);
                nextUpdate = Mathf.FloorToInt(Time.time) + 0.1f;
                UpdateEvery();
            }
        }
    }
    
    
    private void UpdateEvery() {
        //Debug.Log("ControllerMovements is Mine Player Number " + PhotonNetwork.LocalPlayer.GetPlayerNumber());
        //Vector3 leftPosition = leftController.transform.position;
        PoseState leftPoseState = leftDevicePose.action.ReadValue<PoseState>();

        //Vector3 rightPosition = rightController.transform.position;
        PoseState rightPoseState = rightDevicePose.action.ReadValue<PoseState>();
        float velocityFactor = Mathf.Abs(leftPoseState.velocity.y) + Mathf.Abs(rightPoseState.velocity.y);
        if (velocityFactor > recognizedMovementLimit) {
            moveProvider.input = Vector2.up;
            moveProvider.moveSpeed = velocityFactor * velocity;
            
        } else {
            moveProvider.input = Vector2.zero;
            moveProvider.moveSpeed = 0;
        }

        /*Debug.Log(
            "moveSpeed " + moveProvider.moveSpeed + " velocityFactor " + velocityFactor + 
            debugControllerMovement("Left", leftPosition, leftPoseState) + " " + 
            debugControllerMovement("Right", rightPosition, rightPoseState)
        );*/
    }

    

    private string debugControllerMovement(string side, Vector3 position, PoseState poseState) {
        //device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceAcceleration, out Vector3 controllerAceleration); // does not work
        
        return side + " Pos " + position.ToString() 
          //  + " Acceleration " + controllerAceleration.ToString() // always zero ...
            + ", PoseState velocity " + poseState.velocity
            + ", PoseState angularVelocity " + poseState.angularVelocity; 
    }


}
