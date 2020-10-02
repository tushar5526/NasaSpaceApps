using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;

public class Manager : MonoBehaviour
{
    // delegates and events
    public delegate void GazedObject(string name);
    public static GazedObject gazedObject;
    public Vector3[] zoomLevel;
    public static Manager ins;
    public string[] order = { "USA", "Asia", "Europe" };
    public AbstractMap map;

    public bool showData = false;
    
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

    public void setExploreLocation(string name)
    {
        SetShowData();
        Debug.Log("set location called with name " + name);
        int id = -1;
        foreach(string n in order)
        {
            ++id;
            if (n == name)
            {
                // SetMapZoomLatLong(zoomLevel[id].x, new Vector2d(zoomLevel[id].y, zoomLevel[id].z));
                continue;
            }
            GameObject obj = GameObject.FindGameObjectWithTag(n);
            obj.SetActive(false);
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
        //map.SetZoom(zoom);
        //map.SetCenterLatitudeLongitude(coor);
        StartCoroutine(SetMapValues(map.Zoom, zoom, map.CenterLatitudeLongitude, coor, 3.5f));
    }


    public void SetShowData()
    {
        showData = true;
    }
}
