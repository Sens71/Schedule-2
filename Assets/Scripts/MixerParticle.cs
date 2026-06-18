using UnityEngine;

public class MixerParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlePrefab;
    private float timer;
    void Start()
    {
        
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            timer = 0;
            if (particlePrefab.isEmitting)
            {
                particlePrefab.Stop();
            }
            else
            {
                particlePrefab.Play();
            }
        }
        
    }
}
