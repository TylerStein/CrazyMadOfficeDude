using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotificationsManager : MonoBehaviour {

    //Internal reference to all listeners for notifications
    private Dictionary<string, List<Component>> listeners = new Dictionary<string, List<Component>>();

    //Add a lstener for a notification in the listeners list
    public void AddListener(Component listener, string notificationName)
    {
        //Add a listener to the library
        if (!listeners.ContainsKey(notificationName))
        {
            listeners.Add(notificationName, new List<Component>());
        }
        //Add object to the listener list for this notification
        listeners[notificationName].Add(listener);
    }

    //Post a notification to a listener
    public void PostNotification(Component listener, string notificationName)
    {
        //If no key in dictionary exists, then exit
        if (!listeners.ContainsKey(notificationName)) { return; }
        //Else post notification to all matching listeners
        foreach(Component l in listeners[notificationName]){
            l.SendMessage(notificationName, listener, SendMessageOptions.DontRequireReceiver);
        }
    }

    //Remove a listener for a notification
    public void RemoveListener(Component sender, string notificationName)
    {
        //If no key in dictionary exists, then exit
        if(!listeners.ContainsKey(notificationName)) { return; }

        //Cycle through listeners and identify component and then remove
        for(int i = listeners[notificationName].Count - 1; i >= 0; i--)
        {
            //Check instance ID
            if(listeners[notificationName][i].GetInstanceID() == sender.GetInstanceID())
            {
                //Match, remove from list
                listeners[notificationName].RemoveAt(i);
            }
        }
    }

    //Remove deleted and removed listeners
    public void RemoveRedundancies()
    {
        Dictionary<string, List<Component>> tmpListeners = new Dictionary<string, List<Component>>();

        //Cycle through all dictionary entries
        foreach(KeyValuePair<string, List<Component>> item in listeners)
        {
            //Cycle through all listener objects in the list, remove nulls
            for(int i = item.Value.Count - 1; i >= 0; i--)
            {
                //If null then remove
                if(item.Value[i] = null)
                {
                    item.Value.RemoveAt(i);
                }
            }
            //Remining items put in list
            if (item.Value.Count > 0)
            {
                tmpListeners.Add(item.Key, item.Value);
            }
        }

        //Replace listeners with the new optomized dictionary
        listeners = tmpListeners;
    }

    //Remove all listeners
    public void ClearListeners()
    {
        listeners.Clear();
    }

    //Called when a new level is loaded
    void OnLevelWasLoaded()
    {
        //Clear redundancies
        RemoveRedundancies();
    }
}
