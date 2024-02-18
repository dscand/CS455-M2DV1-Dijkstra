using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour
{
	public float cost = 1f;
	public GameObject[] outgoingNodes;

	void OnDrawGizmosSelected()
	{
		foreach (GameObject node in outgoingNodes)
		{
			if (node) {
				// Draws a blue line from this transform to the target
				Gizmos.color = Color.blue;
				Gizmos.DrawLine(transform.position, node.transform.position);
			}
		}
	}
}
