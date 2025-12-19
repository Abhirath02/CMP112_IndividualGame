using UnityEngine;

public class ScaleParticles : MonoBehaviour
{
    private ParticleSystem ps;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        var main = ps.main;
        main.startSize = transform.lossyScale.magnitude;
    }
}