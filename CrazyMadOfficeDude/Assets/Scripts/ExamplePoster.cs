using UnityEngine;
using System.Collections;

public class ExamplePoster : MonoBehaviour {

    public NotificationsManager notificationManager = null;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown && notificationManager != null)
        {
            notificationManager.PostNotification(this, "OnKeyboardInput");
        }
    }
}
