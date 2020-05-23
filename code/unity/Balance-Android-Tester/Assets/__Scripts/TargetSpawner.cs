using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour {


	public GameObject Target;
	public GameObject[] targetArray;
	public GameObject Player;
	public int targetsPerLevel = 3;
	int index;
	int targetCount;
	private GameObject mTarget;
	private int targetScale;
	private Vector2 spawnPosition;
	// Use this for initialization
	void Start () {
		// get random asteroid prefab
		mTarget = GameObject.FindGameObjectWithTag("Target");
		targetScale = 5;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (mTarget == null)
		{			
			Spawner();
		}
		
	}

	void Spawner()
	{
		//spawn target pad at random location on screen
		Debug.Log("Target count: " + targetCount);
		float dist;

		// if too close to player it will reposition spawn point
		do
		{
			spawnPosition = GenerateSpawnPosition();
			dist = Vector2.Distance(Player.transform.position, spawnPosition);
			Debug.Log("Distance Player to Target: " + dist.ToString());
			Debug.Log(DistanceCheck(dist, 4));

		} while (!DistanceCheck(dist, 4));


		mTarget = Instantiate(Target, spawnPosition, Quaternion.identity);
		mTarget.transform.localScale = new Vector3(targetScale, targetScale, 1);

		targetCount += 1;
		if (targetCount % 3 == 0)
		{
			targetScale -= 1;
		}
		mTarget.transform.parent = GameObject.Find("TargetAnchor").transform;
		
	}

	bool DistanceCheck(float currentdist, float minDist)
	{ 
		if (currentdist < minDist)
		{
			return false;
		}
		else
		{
			return true;
		}

	}

	Vector2 GenerateSpawnPosition()
	{
		float targetWidth = Target.GetComponent<SpriteRenderer>().bounds.size.x;
		float targetHeight = Target.GetComponent<SpriteRenderer>().bounds.size.y;

		float spawnY = Random.Range
				(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + targetHeight, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - targetHeight);
		float spawnX = Random.Range
			(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + targetWidth, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - targetWidth);
		return new Vector2(spawnX, spawnY);
	}
}
