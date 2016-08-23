using UnityEngine;
using System.Collections;

public class ParticleLayer : MonoBehaviour {
	public string sortingLayerName;
	public int sortingOrder = 0;
	void Start () {
		//Change Foreground to the layer you want it to display on 
		//You could prob. make a public variable for this
		Renderer renderer = this.GetComponent<ParticleSystem>().GetComponent<Renderer>();
		renderer.sortingLayerName = sortingLayerName;
		renderer.sortingOrder = sortingOrder;
	}

	void Update () {
	
	}
}
