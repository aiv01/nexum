using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioMgr : MonoBehaviour
{
    [SerializeField]
    private AudioClip door;
    [SerializeField]
    private AudioClip pressurePlate;
    [SerializeField]
    private AudioClip energySwitch;
    [SerializeField]
    private AudioClip shoot;
    [SerializeField]
    private AudioClip box;
    [SerializeField]
    private AudioClip ambience;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = ambience;
        source.Play();
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
