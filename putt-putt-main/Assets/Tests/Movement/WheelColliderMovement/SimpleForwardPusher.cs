using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SimpleForwardPusher : MonoBehaviour
{
    [SerializeField] float PuttPower = 10f;
    [SerializeField] float BrakeDelaySeconds = 1f;
    Rigidbody Cart;
    WheelController Wheels;
    bool CanPutt = true;

    void Awake()
    {
        Cart = this.GetComponent<Rigidbody>();
        Wheels = this.GetComponent<WheelController>();
    }

    public void PuttCart()
    {
        if (CanPutt)
        {
            ReleaseBrakesForDurationAsync(BrakeDelaySeconds);

            foreach (var wheel in Wheels.GetWheels())
            {
                wheel.motorTorque = PuttPower/10000;
            }
            Cart.AddRelativeForce(Vector3.forward * PuttPower,ForceMode.VelocityChange);
        }
    }

    private async void ReleaseBrakesForDurationAsync(float waitDuration)
    {
        CanPutt = false;

        Wheels.AreBrakesEnabled = false;
        await Task.Delay(Mathf.RoundToInt(waitDuration*1000));
        Wheels.AreBrakesEnabled = true;

        CanPutt = true;
    }
}
