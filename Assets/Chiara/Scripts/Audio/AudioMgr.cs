using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class AudioMgr : MonoBehaviour
{
    public enum AmbienceType
    {
        MainMenu,
        Level
    }

    [SerializeField]
    private AmbienceType ambienceType;
    [SerializeField]
    private AudioSource jingleSource;
    [SerializeField]
    private AudioSource ambientSource;

    [SerializeField]
    private AudioClip door;
    [SerializeField]
    private AudioClip box;
    [SerializeField]
    private AudioClip energySwitch;
    [SerializeField]
    private AudioClip pressurePlate;
    [SerializeField]
    private AudioClip shoot;
    [SerializeField]
    private AudioClip ambience;
    [SerializeField]
    private AudioClip menuMusic;
    [SerializeField]
    private AudioClip levelMusic;
    [SerializeField]
    private AudioClip levelEndClip;

    [SerializeField]
    private AudioClip selectOption;

    private void Start()
    {
        PlayAmbience(ambienceType);
    }
    private void PlayAmbience(AmbienceType type)
    {
        switch (type)
        {
            case AmbienceType.MainMenu:
                jingleSource.clip = menuMusic;
                jingleSource.volume = 1;
                break;
            case AmbienceType.Level:
                jingleSource.clip = levelMusic;
                jingleSource.volume = 0.1f;
                ambientSource.clip = ambience;
                ambientSource.Play();
                break;
        }
        jingleSource.Play();
    }
    public void LevelEnd()
    {
        ambientSource.PlayOneShot(levelEndClip);
    }
    public void OpenCloseDoor()
    {
        ambientSource.PlayOneShot(door);
    }
    public void ActivatePlate()
    {
        ambientSource.PlayOneShot(pressurePlate);
    }
    public void ActivateSwitch()
    {
        ambientSource.PlayOneShot(energySwitch);
    }
    public void Shoot()
    {
        ambientSource.PlayOneShot(shoot);
    }
    public void DroppedBox()
    {
        ambientSource.PlayOneShot(box);
    }

    public void SelectOption()
    {
        ambientSource.PlayOneShot(selectOption);
    }
}
