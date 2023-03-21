using UnityEngine;
using UnityEngine.AI;

public class TestPlayerMovement_AIScene : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private float rotSpeed = 150;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Transform cameraTarget;

    private float hVal;
    private float vVal;

    void Update()
    {
        hVal = Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        vVal = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.Translate(0, 0, vVal);
        transform.Rotate(0, hVal, 0);
    }
}
