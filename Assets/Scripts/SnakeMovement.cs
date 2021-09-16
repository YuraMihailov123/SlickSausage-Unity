using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 2f;

    Vector3 direction = new Vector3(0, 0, 1);

    [SerializeField]
    Vector3 m_EulerAngleVelocity;
    Quaternion startRotation;

    Rigidbody headRigidbody;

    bool isRotating = false;

    void Start()
    {
        headRigidbody = GetComponent<Rigidbody>();
        m_EulerAngleVelocity = new Vector3(0, 0, 0);
        startRotation = headRigidbody.rotation;
        speed = 4;
    }

    /*private void FixedUpdate()
    {
        headRigidbody.velocity = -transform.forward;
        transform.Rotate(Vector3.forward * 10 * Time.deltaTime);
    }*/

    void AnimateRotationTowards( Quaternion rot)
    {
        Quaternion start = headRigidbody.rotation;
        
        start = Quaternion.Euler(rot.eulerAngles.x,  rot.eulerAngles.z, start.eulerAngles.y);
        Debug.Log(start.eulerAngles);
        Debug.Log(rot.eulerAngles);
        float deltaAngle = rot.eulerAngles.z - start.eulerAngles.z;
        Debug.Log("delat angle :" + deltaAngle);
        while (Mathf.Abs(rot.eulerAngles.z-start.eulerAngles.z) < 1.5f)
        {
            Vector3 eulerVector = new Vector3(0, 0, 1);
            headRigidbody.MoveRotation(Quaternion.Euler(eulerVector*Time.deltaTime));
        }
        headRigidbody.rotation = rot;
        Debug.Log(headRigidbody.rotation.eulerAngles);
        Debug.LogError("Done!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_EulerAngleVelocity = new Vector3(0, 0, 90);
            isRotating = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            m_EulerAngleVelocity = new Vector3(0, 0, 0);
            isRotating = false;
            //StartCoroutine(AnimateRotationTowards( startRotation, 0.5f));
            //headRigidbody.rotation = startRotation;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            m_EulerAngleVelocity = new Vector3(0, 0, -90);
            isRotating = true;

        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            m_EulerAngleVelocity = new Vector3(0, 0, 0);
            isRotating = false;
        }
        if (isRotating)
        {
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
            headRigidbody.MoveRotation(headRigidbody.rotation * deltaRotation);
        }else
        {
            AnimateRotationTowards(startRotation);
        }

        /*direction = new Vector3(0, 0, 1);
        direction = transform.TransformDirection(direction);
        Debug.Log(direction);
        headRigidbody.MovePosition(startPos+ headRigidbody.gameObject.transform.forward);*/
        //transform.Translate(0, 0, 1);
        //headRigidbody.velocity = -transform.forward;
        //transform.localPosition += Vector3.up;
        headRigidbody.AddForce(transform.up * speed, ForceMode.Acceleration);
    }

}
