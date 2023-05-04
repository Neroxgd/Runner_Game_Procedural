using UnityEngine;

public class SpawnTube : MonoBehaviour
{
    [SerializeField] private Transform currentTube, pointWhenInstatiate, pointToInstantiate;
    [SerializeField] private GameObject tube;

    void Update()
    {
        if (currentTube.position.z < pointWhenInstatiate.position.z)
        {
            currentTube = Instantiate(tube, pointToInstantiate.position, Quaternion.identity, transform.parent).transform;
            currentTube.gameObject.AddComponent<Tube>();
        }
    }
}
