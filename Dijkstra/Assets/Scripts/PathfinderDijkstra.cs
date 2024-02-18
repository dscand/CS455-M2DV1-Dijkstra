using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathfinderDijkstra : Arriver
{
	public GameObject startNode;
	public GameObject goalNode;
	public Graph graph;

	private List<Dijkstra.Connection> path;
	private int index = 0;

	public float CompleteDistance = 0.2f;

	protected override void Start()
	{
		transform.position = startNode.transform.position;
		myTarget = startNode;
		base.Start();

		SetNewPath(startNode, goalNode);
	}

	protected override void Update()
	{
		if (Vector3.Distance(transform.position, myTarget.transform.position) < CompleteDistance) {
			index++;
			if (index < path.Count) SetTarget(path[index].toNode);
			else {
				SetNewPath(goalNode);
			}
		}

		base.Update();
	}


	GameObject GetRandomNode(GameObject current, Connection[] connections)
	{
		List<GameObject> nodes = (from connection in connections select connection.gameObject).ToList();
		nodes.Remove(current);
		return nodes[Random.Range(0,nodes.Count)];
	}

	void SetNewPath(GameObject startNode, GameObject goalNode)
	{
		this.startNode = startNode;
		this.goalNode = goalNode;

		graph.SetNodes(startNode, goalNode);
		
		
		List<Dijkstra.Connection> connections = new();

		foreach (Connection connection in graph.connections) {
			foreach (GameObject node in connection.outgoingNodes) {
				connections.Add(new Dijkstra.Connection() {
					cost = connection.cost,
					fromNode = connection.gameObject,
					toNode = node,
				});
			}
		}

		graph.graph = new() {
			connections = connections.ToArray(),
		};

		path = Dijkstra.PathfindDijkstra(graph.graph, startNode, goalNode);
		index = 0;
		graph.SetPath(path);
		SetTarget(path[0].toNode);


		/*Debug.Log(path);
		foreach (Dijkstra.Connection item in path)
		{
			Debug.Log(item.toNode.name);
		}*/
	}
	void SetNewPath(GameObject startNode)
	{
		SetNewPath(startNode, GetRandomNode(startNode, graph.connections));
	}
}
