using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnerScript : MonoBehaviour
{
	public LevelManagerScript LMS;
	
	public float timeToSpown = 1;
	int spawnCount = 2;
	public GameObject enemyPref;

	public List<GameObject> WayPoints;

	void Update()
	{
		if (timeToSpown <= 0)
		{
			StartCoroutine(SpawnEnemy(spawnCount));
			timeToSpown = 1;
		}
		timeToSpown -= Time.deltaTime;
	}

	IEnumerator SpawnEnemy(int enemyCount)
	{
		spawnCount--;

		for (int i = 0; i < enemyCount; i++)
		{
			GameObject temp_unit = Instantiate(enemyPref);
			temp_unit.transform.SetParent(gameObject.transform, false);

			WayPoints = GameObject.Find("LevelGroup").GetComponent<LevelManagerScript>().GetWayPoints();

			var startCellPos = WayPoints[0].transform;

			Vector3 startPos = new Vector3(startCellPos.position.x - startCellPos.GetComponent<SpriteRenderer>().bounds.size.x/2,
										   startCellPos.position.y - startCellPos.GetComponent<SpriteRenderer>().bounds.size.y/2);

			temp_unit.transform.position = startPos;

			yield return new WaitForSeconds(1f);
		}

	}

}