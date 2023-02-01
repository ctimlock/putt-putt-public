 using UnityEngine;
 
 public class RaycastGround : MonoBehaviour
 {
     public float distance = 1.0f; // distance to raycast downwards (i.e. between transform.position and bottom of object)
     public LayerMask hitMask; // which layers to raycast against
 
     void Update()
     {
         Ray ray = new Ray(transform.position, Vector3.down);
         RaycastHit hit;
 
         Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
 
         if (Physics.Raycast(ray, out hit, distance, hitMask))
         {
             Debug.Log("Hit collider " + hit.collider + ", at " + hit.point + ", normal " + hit.normal);
             Debug.DrawRay(hit.point, hit.normal * 2f, Color.blue);
 
             float angle = Vector3.Angle(hit.normal, Vector3.up);
             Debug.Log("angle " + angle);
 
             //if (angle > 30)...
         }
         else // is not colliding
         {
             
         }
     }
 }