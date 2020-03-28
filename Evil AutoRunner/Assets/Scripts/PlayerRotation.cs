using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [Tooltip("Camera rotation speed. (0 for horizontal, 1 for vertical)")]
    [SerializeField] private float rotateHorizontal = 0;
    [SerializeField] private float rotateVertical = 0;

    private float[] mouseRotation = new float[3];

    //Object References
    private Transform cameraTrans;

    private void Awake()
    {
        cameraTrans = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float[] input = { Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"), 0 };
        float[] speed = { rotateHorizontal, rotateVertical, 0f };

        for(int i = 0; i < 3; i++)
        {
            if(input[i] != 0)
            {
                mouseRotation[i] += speed[i] * input[i];
            }
        }

        transform.eulerAngles = new Vector3(0f, mouseRotation[0], 0f);
        cameraTrans.eulerAngles = new Vector3(mouseRotation[1], 0f, 0f);
    }
}
