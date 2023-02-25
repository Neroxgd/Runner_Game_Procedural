using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    void Update()
    {
        if (Generation.Instance == null) return;
        transform.Translate(-transform.forward * Generation.Instance.getSpeedOfObstacle * Time.deltaTime);
        if (transform.position.z < -60)
            Destroy(gameObject);
    }
}
