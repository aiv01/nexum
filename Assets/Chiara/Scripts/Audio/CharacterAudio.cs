using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterAudio : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
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
        if (IsStill()) return;
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
        if (IsStill()) return;
        source.PlayOneShot(runStepClips[Random.Range(0, runStepClips.Length)]);
    }

    private void Land()
    {
        source.PlayOneShot(landClip);
    }
    private bool IsStill()
    {
        if (Mathf.Abs(animator.GetFloat("x")) < 0.01f && 
            Mathf.Abs(animator.GetFloat("y")) < 0.01f)
        {
            return true;
        }
        return false;
    }
}
