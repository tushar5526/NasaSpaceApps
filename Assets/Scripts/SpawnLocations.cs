namespace Mapbox.Examples
{
    using UnityEngine;
    using Mapbox.Utils;
    using Mapbox.Unity.Map;
    using Mapbox.Unity.MeshGeneration.Factories;
    using Mapbox.Unity.Utilities;
    using System.Collections.Generic;

    public class SpawnLocations : MonoBehaviour
    {
        [SerializeField]
        AbstractMap _map;

        List<Vector2d> _locationStorages;

        public NasaLocations[] nasaLocations;

        [SerializeField]
        float _spawnScale = 100f;

        [SerializeField]
        GameObject _markerPrefab;

        List<GameObject> _spawnedObjects;
        List<GameObject> _continentWiseSpawnedObjects;

        void Start()
        {
            _continentWiseSpawnedObjects = new List<GameObject>();
            _spawnedObjects = new List<GameObject>();
            _locationStorages = new List<Vector2d>();

            foreach (NasaLocations ns in nasaLocations)
            {
                ns._locations = new Vector2d[ns._locationStrings.Length];
                GameObject parentObj = new GameObject();
                parentObj.name = ns.continentName;
                for (int i = 0; i < ns._locationStrings.Length; i++)
                {
                    var locationString = ns._locationStrings[i];
                    ns._locations[i] = Conversions.StringToLatLon(locationString);
                    _locationStorages.Add(ns._locations[i]);
                    var instance = Instantiate(_markerPrefab, parentObj.transform);
                    instance.transform.localPosition = _map.GeoToWorldPosition(ns._locations[i], true);
                    instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                    _spawnedObjects.Add(instance);
                }
                _continentWiseSpawnedObjects.Add(parentObj);
            }
        }

        private void Update()
        {
            int count = _spawnedObjects.Count;
            for (int i = 0; i < count; i++)
            {
                var spawnedObject = _spawnedObjects[i];
                var location = _locationStorages[i];
                spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
                spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                //spawnedObject.transform.localPosition = new Vector3(spawnedObject.transform.localPosition.x, spawnedObject.transform.localPosition.y + 0.1f, spawnedObject.transform.localPosition.z);
            }
        }
    }

    [System.Serializable]
    public class NasaLocations
    {
        public string continentName;
        [SerializeField]
        [Geocode]
        public string[] _locationStrings;
        public Vector2d[] _locations;
    }
}


