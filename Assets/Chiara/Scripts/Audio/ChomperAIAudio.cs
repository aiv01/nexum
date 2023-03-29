using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(AudioSource))]
public class ChomperAIAudio : MonoBehaviour
{
    private enum ClipType
    {
        Step,
        Grunt,
        Attack
    }

    [SerializeField]
    private AudioClip[] stepClips;
    [SerializeField]
    private AudioClip[] gruntClips;
    [SerializeField]
    private AudioClip[] attackClips;


    private AudioClip currClip;

    private void Start()
    {
        //GetClips();
    }

    private void PlayStep()
    {
        PlayClip(ClipType.Step);
    }

    private void Land()
    {
        PlayClip(ClipType.Step);
    }

    private void Grunt()
    {
        PlayClip(ClipType.Grunt);
    }

    private void AttackBegin()
    {
        PlayClip(ClipType.Attack);
    }

    private void AttackEnd() { }

    private void PlayClip(ClipType type)
    {
        switch (type)
        {
            case ClipType.Step:
                currClip = stepClips[Random.Range(0, stepClips.Length)];
                break;
            case ClipType.Grunt:
                currClip = gruntClips[Random.Range(0, gruntClips.Length)];
                break;
            case ClipType.Attack:
                currClip = attackClips[Random.Range(0, attackClips.Length)];
                break;
        }
        AudioSource.PlayClipAtPoint(currClip, transform.position);
    }
}
