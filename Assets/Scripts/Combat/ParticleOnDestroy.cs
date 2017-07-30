using UnityEngine;
using System.Collections;

public class ParticleOnDestroy : MonoBehaviour {

    public GameObject particleToSpawn;

    private void OnDestroy()
    {
        SpawnParticleOnDestroy();
    }

    public void SpawnParticleOnDestroy()
    {
        GameObject instantiated = Instantiate(particleToSpawn);
        instantiated.transform.position = transform.position;
    }
}
