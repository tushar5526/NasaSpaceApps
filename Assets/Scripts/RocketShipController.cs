using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mapbox.Utils;
using Michsky.UI.ModernUIPack;

public class RocketShipController : MonoBehaviour
{

    public GameObject detailedInfoHolder;
    public GameObject basicInfoHolder;
    public TMP_Text locationNameHolder;
    public Vector2d latLong;

    public ModalWindowManager panelScript;

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


    private void OnDestroy()
    {
        Manager.gazedObject -= GazedObjectReciever;
    }
}
