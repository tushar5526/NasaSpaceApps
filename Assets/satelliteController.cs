using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.ModernUIPack;
using UnityEngine.Video;

public class satelliteController : MonoBehaviour
{
    public GameObject detailedInfoHolder;

    public string url;
    public ModalWindowManager panelScript;
    public ModalWindowManager videoScript;

    public VideoPlayer player;

    bool isGazing = false;

    private void Awake()
    {
        Manager.gazedObject += GazedObjectReciever;
    }

    public void GazedObjectReciever(string name)
    {
        if (name == this.name)
        {
            isGazing = true;
            if (Manager.ins.showData)
                detailedInfoHolder.SetActive(true);
        }
        else
        {
            isGazing = false;
            detailedInfoHolder.SetActive(false);
        }
    }

    public void ShowPanels()
    {
        panelScript.OpenWindow();
    }

    public void OpenURL()
    {
        Application.OpenURL(url);
    }

    private void OnDestroy()
    {
        Manager.gazedObject -= GazedObjectReciever;
    }

    public void ShowVideo()
    {
        videoScript.OpenWindow();
        player.Play();
    }

    public void PauseVideo()
    {
        player.Pause();
    }
}
