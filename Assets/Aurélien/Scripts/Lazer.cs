using UnityEngine;

public class Lazer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            print("ézefez");
        }
    }
}
