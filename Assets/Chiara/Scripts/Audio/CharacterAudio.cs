using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] stepClips;
    [SerializeField]
    private AudioClip landClip;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void PlayStep()
    {
        source.PlayOneShot(stepClips[Random.Range(0, stepClips.Length)]);
    }

    private void Land()
    {
        source.PlayOneShot(landClip);
    }
}
