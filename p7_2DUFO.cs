// 1. PlayerController.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
    }
}
// 2. CamerController.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
	public Transform player;
	private Vector3 offset;
	void Start()
	{
		offset = transform.position - player.position;
	}
	void LateUpdate()
	{
		transform.position = player.position + offset;
	}
}



// 3. Rotator.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Rotator : MonoBehaviour
{
	void Update()
	{
		transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
	}
}
