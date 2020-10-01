using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RocketShipController : MonoBehaviour
{

    public GameObject detailedInfoHolder;
    public GameObject basicInfoHolder;
    public TMP_Text locationNameHolder;

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

    private void OnDestroy()
    {
        Manager.gazedObject -= GazedObjectReciever;
    }
}
