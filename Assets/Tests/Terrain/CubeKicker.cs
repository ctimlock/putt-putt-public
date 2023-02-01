using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeKicker : MonoBehaviour
{
    private Vector3 NextDirection;
    private Rigidbody RigidBody;

    public float KickPower;

    void Awake()
    {
        RigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        StartCoroutine(PeriodicallyKickCube());
    }

    private IEnumerator PeriodicallyKickCube()
    {
        do
        {
            if (RigidBody.velocity.magnitude > 0.1f) yield return null;

            var newXImpulsePower = Random.Range(-KickPower, KickPower);
            var newZImpulsePower = Random.Range(-KickPower, KickPower);

            var newDirection = new Vector3(newXImpulsePower, 0, newZImpulsePower);

            RigidBody.AddForce(newDirection, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(2, 8));
        } while (true);
    }
}
