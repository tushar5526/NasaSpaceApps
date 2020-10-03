using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mapbox.Utils;
using Michsky.UI.ModernUIPack;
using UnityEngine.UI;
using UnityEngine.Video;


public class RocketShipController : MonoBehaviour
{

    public GameObject detailedInfoHolder;
    public GameObject basicInfoHolder;
    public TMP_Text locationNameHolder;
    public Vector2d latLong;

    public TMP_Text info;
    public Image logo;
    public TMP_Text activities;
    public TMP_Text collabrations;
    public string url;
    public TMP_Text locName;
    public Image[] imgs;

    public ModalWindowManager panelScript;
    public ModalWindowManager videoScript;

    public VideoPlayer player;

    bool isGazing = false;

    private void Awake()
    {
        Manager.gazedObject += GazedObjectReciever;
    }

    public void Start()
    {
        locationNameHolder.text = this.name;
    }

    public void GazedObjectReciever(string name)
    {
        if(name == this.name)
        {
            isGazing = true;
            basicInfoHolder.SetActive(true);
            if(Manager.ins.showData)
                detailedInfoHolder.SetActive(true);
        }
        else
        {
            isGazing = false;
            basicInfoHolder.SetActive(false);
            detailedInfoHolder.SetActive(false);
        }
    }

    public void ZoomIn(int val = 16)
    {
        //enable portal
        Manager.ins.SetMapZoomLatLong(16, latLong);
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
    }
}
