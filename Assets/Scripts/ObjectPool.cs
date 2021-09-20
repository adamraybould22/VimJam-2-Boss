using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

	[System.Serializable]
	public class PooledObjects
	{
		public GameObject pooledObject;
		public int pooledAmount;
	}
	public PooledObjects[] poolObjects;
	private List<GameObject> pooledObjects;
	public List<GameObject> spawnedObjects;


	void Start()
	{
		pooledObjects = new List<GameObject>();
        for(int i = 0; i < poolObjects.Length; i++)
        {
        	for(int a = 0; a < poolObjects[i].pooledAmount; a++)
        	{
		    	GameObject obj = (GameObject)Instantiate(poolObjects[i].pooledObject);
		    	obj.transform.SetParent(transform);
            	pooledObjects.Add(obj);
            	obj.SetActive(false);     
            }
        }
	}

	public GameObject InstantiatePooledObject(GameObject objPrefab, Quaternion rotation)
	{
		for(int i = 0; i < pooledObjects.Count; i++)
		{
			if(!pooledObjects[i].activeInHierarchy && pooledObjects[i].gameObject.name == objPrefab.name + "(Clone)")
			{	
				pooledObjects[i].transform.rotation = rotation;
				pooledObjects[i].SetActive(true);
				return pooledObjects[i];
			}
		}
		GameObject obj = Instantiate(objPrefab, new Vector3(0, 0, 0), rotation) as GameObject;
		pooledObjects.Add(obj);
		obj.SetActive(true);
		return obj;
	}

	public void DestroyPooledObject(GameObject obj)
	{
		obj.SetActive(false);
	}

	public void DestroyPooledObject2(GameObject obj, float time)
	{
		StartCoroutine(DestoyingPooledObject(obj, time));
	}

	IEnumerator DestoyingPooledObject(GameObject obj, float time)
	{
		yield return new WaitForSeconds(time);
		pooledObjects.Add(obj);
		obj.SetActive(false);
	}
}
