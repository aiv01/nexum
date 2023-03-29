using UnityEngine;
using UnityEditor;

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
    //private string stepClipsPath;
    private AudioClip[] stepClips;
    [SerializeField]
    //private string gruntClipsPath;
    private AudioClip[] gruntClips;
    [SerializeField]
    //private string attackClipsPath;
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

    private void GetClips()
    {
        //stepClips = (AudioClip[])AssetDatabase.LoadAllAssetsAtPath(stepClipsPath);
        //gruntClips = (AudioClip[])AssetDatabase.LoadAllAssetsAtPath(gruntClipsPath);
        //attackClips = (AudioClip[])AssetDatabase.LoadAllAssetsAtPath(attackClipsPath);
    }
}
