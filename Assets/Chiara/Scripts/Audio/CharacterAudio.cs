using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] walkStepClips;
    [SerializeField]
    private AudioClip[] runStepClips;
    [SerializeField]
    private AudioClip landClip;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void PlayStep()
    {
        source.PlayOneShot(walkStepClips[Random.Range(0, walkStepClips.Length)]);
    }
    private void PlayRunStep()
    {
        source.PlayOneShot(runStepClips[Random.Range(0, runStepClips.Length)]);
    }

    private void Land()
    {
        source.PlayOneShot(landClip);
    }
}
