using UnityEngine;

public class ParticleEffectsManager : MonoBehaviour
{
    public float duration = 2.0f;

    public void PlayParticleSystem(ParticleSystem particleSystem, Vector3 position)
    {
        ParticleSystem particles = Instantiate(particleSystem, position, Quaternion.identity);
        particles.transform.parent = transform;

        particles.Play();

        Destroy(particles.gameObject, duration);
    }
}
