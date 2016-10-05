using UnityEngine;
using System.Collections;

public class ExampleListener : MonoBehaviour {

    public NotificationsManager notificationManager = null;

	// Use this for initialization
	void Start () {
        if (notificationManager != null)
        {
            notificationManager.AddListener(this, "OnKeyboardInput");
        }
	}

    public void OnKeyboardInput()
    {
        Debug.Log("Keyboard event occured");
    }

}
