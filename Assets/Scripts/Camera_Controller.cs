using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    private Camera m_camera;
    [SerializeField] private float m_camera_speed = 5.0f;

	void Start ()
    {
        m_camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveCamera();
	}

    private void FixedUpdate()
    {
        //MoveCameraF();
    }

    void MoveCamera()
    {
        float movement = Time.deltaTime * m_camera_speed;
        Vector3 change = new Vector3(0.0f, 0.0f, movement);

        m_camera.transform.position += change;
    }

    void MoveCameraF()
    {
        float movement = Time.fixedDeltaTime * m_camera_speed;
        Vector3 change = new Vector3(0.0f, 0.0f, movement);

        m_camera.transform.position += change;
    }
}
