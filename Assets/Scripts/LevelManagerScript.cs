using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManagerScript : MonoBehaviour
{
	public int field_width, field_height;
	public GameObject CellPref;
	public Transform CellParent;

	public Sprite[] tiles = new Sprite[2];

	public int[,] StartCells =
	{
		{0,6 }, {0,7}, {0,8}, {0,9}, {0,10}, {0,11}, {0,12}, {0,13}
	};

	public List<GameObject> waypoints = new List<GameObject>();
	readonly GameObject[,] allCells = new GameObject[20, 33];
	int currWayX, currWayY;
	public List<GameObject> first_Cell;
	public System.Random rnd;


	void Start()
	{
		CreateLevel();
	}

    void CreateLevel()
	{
		Vector3 worldVec = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
		for (int i = 0; i < field_height; i++)
			for (int j = 0; j < field_width; j++)
			{
				int sprIndex = int.Parse(LoadLevel(1)[i].ToCharArray()[j].ToString());

				Sprite spr = tiles[sprIndex];

				bool isGround = (spr == tiles[1]);
				CreateCell(isGround, spr, j, i, worldVec);

			}
	}
	void CreateCell(bool isGround, Sprite spr, int x, int y, Vector3 wv)
	{
		GameObject temp_Cell = Instantiate(CellPref);

		temp_Cell.transform.SetParent(CellParent, false);

		temp_Cell.GetComponent<SpriteRenderer>().sprite = spr;

		float spriteSizeX = temp_Cell.GetComponent<SpriteRenderer>().bounds.size.x;
		float spriteSizeY = temp_Cell.GetComponent<SpriteRenderer>().bounds.size.y;

		temp_Cell.transform.position = new Vector3(wv.x + (spriteSizeX * x), wv.y + (spriteSizeY * -y));
		if (isGround)
		{
			temp_Cell.GetComponent<CellScr>().isGround = true;

			for (var e = 0; e<8; e++)
            {
				if (StartCells[e, 0] == x && StartCells[e, 1] == y)
				{
					first_Cell.Add(temp_Cell);
					currWayX = x;
					currWayY = y;
				}
			}
		}
		allCells[y, x] = temp_Cell;
	}
	string[] LoadLevel(int i)
	{
		TextAsset temp_text = (TextAsset)Resources.Load("Level" + i + "Ground", typeof(TextAsset));
		string temp_string = temp_text.text.Replace(Environment.NewLine, string.Empty);
		return temp_string.Split('!');
	}

	public List<GameObject> GetWayPoints()
    {
		LoadWaypoints();
		return waypoints;
    }

	void LoadWaypoints()
	{
		var rand = new System.Random();
		GameObject currWayTo;
		waypoints.Clear();
		waypoints.Add(first_Cell[rand.Next(8)]);

		while (true)
		{
			currWayTo = null;
			if (currWayX > 0 && allCells[currWayY, currWayX - 1].GetComponent<CellScr>().isGround &&
				!waypoints.Exists(x => x == allCells[currWayY, currWayX - 1]))
			{
				currWayTo = allCells[currWayY, currWayX - 1];
				currWayX--;
				Debug.Log ("To left");
			}
			else if (currWayX < (field_width - 1) && allCells[currWayY, currWayX + 1].GetComponent<CellScr>().isGround &&
				!waypoints.Exists(x => x == allCells[currWayY, currWayX + 1]))
			{
				currWayTo = allCells[currWayY, currWayX + 1];
				currWayX++;
				Debug.Log ("To right");
			}
			else if (currWayY > 0 && allCells[currWayY - 1, currWayX].GetComponent<CellScr>().isGround &&
				!waypoints.Exists(x => x == allCells[currWayY - 1, currWayX]))
			{
				currWayTo = allCells[currWayY - 1, currWayX];
				currWayY--;
				Debug.Log ("To up");
			}
			else if (currWayY < (field_height - 1) && allCells[currWayY + 1, currWayX].GetComponent<CellScr>().isGround &&
				!waypoints.Exists(x => x == allCells[currWayY + 1, currWayX]))
			{
				currWayTo = allCells[currWayY + 1, currWayX];
				currWayY++;
				Debug.Log ("To down");
			}
			else
				break;
			waypoints.Add(currWayTo);
		}
	}
}