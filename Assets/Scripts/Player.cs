using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public float speed = 10f;

    private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;
	public float turnSmoothing = 15f;   // A smoothing value for turning the player.
	public float speedDampTime = 0.1f;  // The damping for the speed parameter
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        Vector3 syncPosition = Vector3.zero;
        Vector3 syncVelocity = Vector3.zero;
        if (stream.isWriting)
        {
            syncPosition = rigidbody.position;
            stream.Serialize(ref syncPosition);

            syncPosition = rigidbody.velocity;
            stream.Serialize(ref syncVelocity);
        }
        else
        {
            stream.Serialize(ref syncPosition);
            stream.Serialize(ref syncVelocity);

            syncTime = 0f;
            syncDelay = Time.time - lastSynchronizationTime;
            lastSynchronizationTime = Time.time;

            syncEndPosition = syncPosition + syncVelocity * syncDelay;
            syncStartPosition = rigidbody.position;
        }
    }

    void Awake()
    {
        lastSynchronizationTime = Time.time;
    }

    void Update()
    {
        if (networkView.isMine)
        {
            InputMovement();
            InputColorChange();
        }
        else
        {
            SyncedMovement();
        }
    }


    private void InputMovement()
    {
		/*
		if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.D)) {
			var camDir = Camera.main.transform.TransformDirection(Vector3.forward);
			camDir.y = 0.0f;
			rigidbody.MovePosition(rigidbody.position + camDir * speed * Time.deltaTime);

			rigidbody.transform.RotateAround(transform.position, transform.up, Time.deltaTime*90f);
		}
*/

		if (Input.GetKey (KeyCode.W)) {
			var camDir = Camera.main.transform.TransformDirection(Vector3.forward);
			camDir.y = 0.0f;
			rigidbody.MovePosition(rigidbody.position + camDir * speed * Time.deltaTime);
			//rigidbody.MovePosition(rigidbody.position + camDir * speed * Time.deltaTime);
			//rigidbody.AddForce(rigidbody.position + camDir*90f);
			//rigidbody.MovePosition(rigidbody.position + Input.GetAxis("Mouse X") * transform.forward*Time.deltaTime);
		}

        if (Input.GetKey (KeyCode.D)) {
			transform.RotateAround(transform.position, new Vector3(0,1.0f,0), Time.deltaTime*90f);
		}

		if (Input.GetKey (KeyCode.A)) {
			transform.RotateAround(transform.position, new Vector3(0,1.0f,0), -Time.deltaTime*90f);
		}
		
		if (Input.GetKeyUp (KeyCode.Q)) {
			if(Camera.main.GetComponent<FollowCamera>().enabled) {
				Camera.main.GetComponent<FollowCamera>().enabled = false;
				Camera.main.GetComponent<MouseAimCamera>().enabled = true;
			} else {
				Camera.main.GetComponent<FollowCamera>().enabled = true;
				Camera.main.GetComponent<MouseAimCamera>().enabled = false;
			}
		}       
    }

	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		
		rigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
    }


    private void InputColorChange()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ChangeColorTo(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
    }

    [RPC]
    void ChangeColorTo(Vector3 color)
    {
        renderer.material.color = new Color(color.x, color.y, color.z, 1f);

        if (networkView.isMine)
            networkView.RPC("ChangeColorTo", RPCMode.OthersBuffered, color);
    }

	[RPC]
	void UpdateStore(string message) 
	{
		Debug.Log ("test");
		/*
		if (inventory[store.StoreItem] != null) {
			inventory[store.StoreItem] = store.StoreItemAmount;
		} else {
			inventory.Add (store.StoreItem, store.StoreItemAmount);
		}
		*/
	}

}
