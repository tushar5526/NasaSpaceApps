using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // delegates and events
    public delegate void GazedObject(string name);
    public static GazedObject gazedObject;

    public static Manager ins;
    public string[] order = { "USA", "Asia", "Europe" };
    public Vector2 location;
    
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
        Debug.Log("set location called with name " + name);
    }
}
