 using UnityEngine;

 public class Controlls : MonoBehaviour
 {
     public float Speed = 1;

     void Update()
     {
         float xAxisValue = Input.GetAxis("Horizontal") * Speed;
         float zAxisValue = Input.GetAxis("Vertical") * Speed;

         transform.position = new Vector3(transform.position.x + xAxisValue, transform.position.y, transform.position.z + zAxisValue);
     }
 }
