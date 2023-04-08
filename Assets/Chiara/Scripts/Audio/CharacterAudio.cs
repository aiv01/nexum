using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterAudio : MonoBehaviour
{
    [SerializeField]
    private bool isRobot;
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
        AudioClip stepClip = walkStepClips[Random.Range(0, walkStepClips.Length)];
        if (isRobot)
        {
            AudioSource.PlayClipAtPoint(stepClip, transform.position);
        }
        else
        {
            source.PlayOneShot(stepClip);
        }
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
