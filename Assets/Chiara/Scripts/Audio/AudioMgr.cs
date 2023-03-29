using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(AudioSource))]
public class AudioMgr : MonoBehaviour
{
    [SerializeField]
    private string doorClipPath;
    [SerializeField]
    private string boxClipPath;
    [SerializeField]
    private string switchClipPath;
    [SerializeField]
    private string pressurePlateClipPath;
    [SerializeField]
    private string shootClipPath;
    [SerializeField]
    private string ambienceClipPath;

    private AudioClip door;
    private AudioClip box;
    private AudioClip energySwitch;
    private AudioClip pressurePlate;
    private AudioClip shoot;
    private AudioClip ambience;

    private AudioSource source;

    private void Start()
    {
        GetClips();

        source = GetComponent<AudioSource>();
        source.clip = ambience;
        source.Play();
    }
    private void GetClips()
    {
        door = (AudioClip)AssetDatabase.LoadAssetAtPath(doorClipPath, typeof(AudioClip));
        box = (AudioClip)AssetDatabase.LoadAssetAtPath(boxClipPath, typeof(AudioClip));
        energySwitch = (AudioClip)AssetDatabase.LoadAssetAtPath(switchClipPath, typeof(AudioClip));
        pressurePlate = (AudioClip)AssetDatabase.LoadAssetAtPath(pressurePlateClipPath, typeof(AudioClip));
        shoot = (AudioClip)AssetDatabase.LoadAssetAtPath(shootClipPath, typeof(AudioClip));
        ambience = (AudioClip)AssetDatabase.LoadAssetAtPath(ambienceClipPath, typeof(AudioClip));
    }
    public void OpenCloseDoor()
    {
        source.PlayOneShot(door);
    }
    public void ActivatePlate()
    {
        source.PlayOneShot(pressurePlate);
    }
    public void ActivateSwitch()
    {
        source.PlayOneShot(energySwitch);
    }
    public void Shoot()
    {
        source.PlayOneShot(shoot);
    }
    public void DroppedBox()
    {
        source.PlayOneShot(box);
    }
}
