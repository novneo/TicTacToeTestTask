using UnityEngine;

public class CameraScaleControl : MonoBehaviour {
	void Start () {
        float desiredProportion = 720f / 1280f;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float currentProportion = screenWidth / screenHeight;
        float baseCameraSize = gameObject.GetComponent<Camera>().orthographicSize;

        if (desiredProportion != currentProportion)
        {
            float proportionCameraChange = desiredProportion / currentProportion;
            gameObject.GetComponent<Camera>().orthographicSize = baseCameraSize * proportionCameraChange;
            gameObject.GetComponent<Transform>().position = new Vector3(0, baseCameraSize * (proportionCameraChange - 1f), -100);
        }
	}
}