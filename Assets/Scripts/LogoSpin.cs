using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoSpin : MonoBehaviour
{
    public float rotationSpeed = 50f; // Adjust the rotation speed as needed

    void Awake()
    {
        StartCoroutine(RotateObject());
    }

    IEnumerator RotateObject()
    {
        while (true) // Infinite loop
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }
    }
}