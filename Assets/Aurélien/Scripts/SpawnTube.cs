using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTube : MonoBehaviour
{
    [SerializeField] private GameObject prefabTube;
    [SerializeField] private Transform spawnTube, parentInstantiate;
    [SerializeField] private Tube tube;

    void Update()
    {
        if (tube.transform.position.z <= 31)
        {
            GameObject objTube = Instantiate(prefabTube, spawnTube.position, Quaternion.identity, parentInstantiate);
            tube = objTube.AddComponent<Tube>();
        }       
    }
}
