using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class Manager : MonoBehaviour
{
    // delegates and events
    public delegate void GazedObject(string name);
    public static GazedObject gazedObject;
    public Vector3[] zoomLevel;
    public static Manager ins;
    public string[] order = { "North America", "Asia", "Europe", "Australia" };
    public AbstractMap map;
    public GameObject continentHolder;
    public GameObject homeButton;
    public GameObject[] rocketHolderList;
    public bool showData = false;
    public DataHolder[] dataHolder;

    
    private void Awake()
    {
        if (ins == null)
        {
            ins = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        rocketHolderList = new GameObject[order.Length];
    }
    public void setExploreLocation(string name)
    {
        SetShowData();
        Debug.Log("set location called with name " + name);
        int id = -1;
        foreach(string n in order)
        {
            ++id;
            GameObject obj = GameObject.FindGameObjectWithTag(n);
            rocketHolderList[id] = obj;
            if (n == name)
            {
                SetMapZoomLatLong(zoomLevel[id].x, new Vector2d(zoomLevel[id].y, zoomLevel[id].z));
                continue;
            }
            obj.SetActive(false);
        }
    }

    public void StoreData()
    {
        SetShowData();

        int id = -1;
        foreach (string n in order)
        {
            ++id;
            GameObject obj = GameObject.FindGameObjectWithTag(n);
            rocketHolderList[id] = obj;
        }
    }

    private IEnumerator SetMapValues(float initZoom, float zoom, Vector2d initLatLong, Vector2d finalLatLong, float time)
    {
        float elapsedTime = 0;
        while(elapsedTime < time)
        {
            map.UpdateMap(Vector2d.Lerp(initLatLong, finalLatLong, elapsedTime / time), Mathf.Lerp(initZoom, zoom, elapsedTime/time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Debug.Log("coroutine Done");
        map.UpdateMap(finalLatLong, zoom);
    }

    public void SetMapZoomLatLong(float zoom, Vector2d coor)
    {
        StartCoroutine(SetMapValues(map.Zoom, zoom, map.CenterLatitudeLongitude, coor, 2.5f));
    }

    public void ExploreReturnToHome()
    {
        continentHolder.SetActive(true);
        SetMapZoomLatLong(1.349f, new Vector2d(30.7425946295217, 18.6913493899822));
        foreach (string n in order)
        {
            foreach(GameObject g in rocketHolderList)
            {
                g.SetActive(true);
            }
        }
    }


    public void SetShowData()
    {
        homeButton.SetActive(true);
        showData = true;
    }
}
[System.Serializable]
public class DataHolder
{
    public string name;
    [TextArea(3,10)]
    public string info;
    [TextArea(3, 10)]
    public string collbarations;
    public Sprite image;
    [TextArea(3, 10)]
    public string activities;
    public string url;
    public Sprite[] imgs;
    public VideoClip clip;
}
