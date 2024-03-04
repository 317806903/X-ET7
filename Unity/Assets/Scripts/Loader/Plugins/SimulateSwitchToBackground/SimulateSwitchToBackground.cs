using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CommandLine;

public class SimulateSwitchToBackground: MonoBehaviour
{
    public Action<bool> OnApplicationPauseListern;
    private void OnApplicationFocus(bool isForcus)
    {
    }

    private void OnApplicationPause(bool isPause)
    {
        try
        {
            OnApplicationPauseListern?.Invoke(isPause);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        if (isPause)
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
        }
        else
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }

    void SendApplicationPauseMessage(bool isPause)
    {
        Transform[] transList = GameObject.FindObjectsOfType<Transform>();
        for (int i = 0; i < transList.Length; i++)
        {
            Transform trans = transList[i];
            //Note that messages will not be sent to inactive objects
            trans.SendMessage("OnApplicationPause", isPause, SendMessageOptions.DontRequireReceiver);
        }
    }

    void SendApplicationFocusMessage(bool isFocus)
    {
        Transform[] transList = GameObject.FindObjectsOfType<Transform>();
        for (int i = 0; i < transList.Length; i++)
        {
            Transform trans = transList[i];
            //Note that messages will not be sent to inactive objects
            trans.SendMessage("OnApplicationFocus", isFocus, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void SendEnterBackgroundMessage()
    {
        this.SendApplicationPauseMessage(true);
        this.SendApplicationFocusMessage(false);
    }

    public void SendEnterFoegroundMessage()
    {
        this.SendApplicationFocusMessage(true);
        this.SendApplicationPauseMessage(false);
    }
}