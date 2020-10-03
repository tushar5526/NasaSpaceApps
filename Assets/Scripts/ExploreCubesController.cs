using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityARInterface;

public class ExploreCubesController : MonoBehaviour
{
    public bool isGazing = false;
    public Vector3 zoomScale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField]
    private Vector3 _initialScale;
    // Start is called before the first frame update

    public void Awake()
    {
        Manager.gazedObject += currentGazedObject;
    }

    void Start()
    {
        _initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGazing)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, zoomScale, 0.3f);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _initialScale, 0.3f);
        }
    }

    // this is an event fired in ARFocusSquare.cs
    public void currentGazedObject(string name)
    {
        Debug.Log("current gazed obj " + name);
        if (this.transform.name == name) isGazing = true;
        else isGazing = false;
    }

    public void OnDestroy()
    {
        Manager.gazedObject -= currentGazedObject;
    }

}
