using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartKicker : MonoBehaviour
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
            yield return new WaitForSeconds(Random.Range(2, 4));
            if (RigidBody.velocity.magnitude > 0.1f) yield return null;

            var newXImpulsePower = Random.Range(-KickPower, KickPower);
            var newYImpulsePower = Random.Range(0, KickPower/10);
            var newZImpulsePower = Random.Range(-KickPower, KickPower);

            var newDirection = new Vector3(newXImpulsePower, newYImpulsePower, newZImpulsePower);

            RigidBody.AddForce(newDirection, ForceMode.Impulse);

        } while (true);
    }
}
