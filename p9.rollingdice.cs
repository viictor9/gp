// 1. PlayerController.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public Text CountText;
    public Text WinText;
    private int count;
    private void Start()

    {
        rb = GetComponent<Rigidbody>();
        count=0;
        setCountText();
        WinText.text=" ";
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement*speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);
            count=count+1;
            setCountText();
        }
    }
    void setCountText()
    {
        CountText.text= "Count: " +count.ToString();
        if(count>=5)
        {
            WinText.text="You win";
        }
    }
}

// 2. CamerController.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
   public GameObject player;
    private Vector3 offset;
    private void Start()
    {
        offset = transform.position - player.transform.position;

    }
    // Update is called once per frame
    void Update () {
        transform.position = player.transform.position+offset;
		
	}
}

// 3. Rotator.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour
{
    void Update()
    {

        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}

