using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScr : MonoBehaviour
{
	int wayIndex = 0;
	public int speed = 1;

	public List<GameObject> WayPoints;

	private void Start()
	{
		Getwaypoints();
	}

	void Update()
	{
		Move();
	}
	void Getwaypoints()
	{
		WayPoints = GameObject.Find("LevelGroup").GetComponent<LevelManagerScript>().GetWayPoints();
	}
	private void Move()
	{

		Transform currWayPoint = WayPoints[wayIndex].transform;

		Vector3 currWayPos = new Vector3(WayPoints[wayIndex].transform.position.x - currWayPoint.GetComponent<SpriteRenderer>().bounds.size.x / 2,
										 WayPoints[wayIndex].transform.position.y - currWayPoint.GetComponent<SpriteRenderer>().bounds.size.y / 2);

		 
		Vector3 dir = currWayPos - transform.position;

		transform.Translate(dir.normalized * Time.deltaTime * speed);

		if (Vector3.Distance(transform.position, currWayPos) < 0.1f)
		{
			if (wayIndex < WayPoints.Count - 1)
			{
				wayIndex++;
			}
			else
			{
				Destroy(gameObject);
			}
		}

	}
}
