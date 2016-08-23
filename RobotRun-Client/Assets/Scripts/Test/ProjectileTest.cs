using UnityEngine;
using System.Collections;

public class ProjectileTest : MonoBehaviour {
	public GameObject spriteObject;
	public float distance = 1;
	public float height = 1;
	public float duration = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			AddObject();
		}
	}

	void AddObject(){
		GameObject spriteGO = Instantiate(spriteObject);
		SpriteRenderer spriteRenderer = spriteGO.GetComponent<SpriteRenderer>();
		Rigidbody2D rigidbody2D = spriteGO.AddComponent<Rigidbody2D>();
		float g = rigidbody2D.gravityScale * Physics2D.gravity.magnitude;
		float jumpForce = (Mathf.Sqrt(2 * g * height) + 0.1f) * rigidbody2D.mass;
		float vy = jumpForce / rigidbody2D.mass;
		float ty = vy / g;
		float speedX = distance / ty * rigidbody2D.mass / 2;
		Debug.Log("ty: "+ty);
		rigidbody2D.AddForce(new Vector2(speedX,jumpForce), ForceMode2D.Impulse);
		StartCoroutine(DestroyObject(spriteGO));
	}

	IEnumerator DestroyObject(GameObject go){
		yield return new WaitForSeconds(duration);
		Destroy(go);
	}
}
