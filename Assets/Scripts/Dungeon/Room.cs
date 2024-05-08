using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;

    public List<GameObject> EnemyList;
    public List<GameObject> Terrain;
    public GameObject Spawnpoint;

    public Vector2Int RoomIndex { get; set; }

    public int height = 16;
    public int width = 30;

    public void OpenDoor(Vector2Int direction)
    {
        if(direction == Vector2Int.up)
        {
            topDoor.SetActive(false);
        }

        if(direction == Vector2Int.down)
        {
            bottomDoor.SetActive(false);
        }

        if(direction == Vector2Int.left)
        {
            leftDoor.SetActive(false);
        }

        if(direction == Vector2Int.right)
        {
            rightDoor.SetActive(false);
        }
    }

    public void Spawn(Vector2 center)
    {
        int choice = Random.Range(0, 100);

        if(choice % 2 == 0)
        {
            GetRandomEnemy(center);
        }
        else
        {
            GetRandomTerrain(center);
        }
    }

    public Vector2 GetCenterOfWalls()
    {
        //get avg center X coordinate
        float centerX = (topDoor.transform.position.x + bottomDoor.transform.position.x + leftDoor.transform.position.x + rightDoor.transform.position.x) / 4;

        //get avg center Y coordinate
        float centerY = (topDoor.transform.position.y + bottomDoor.transform.position.y + leftDoor.transform.position.y + rightDoor.transform.position.y) / 4;

        //return center
        return new Vector2(centerX, centerY);
    }

    public void GetRandomEnemy(Vector2 center)
    {
        int index = Random.Range(0, EnemyList.Count);
        
        Instantiate(EnemyList[index], center, Quaternion.identity);
        EnemyCounter.singleton.CountEnemy();


    }

    public void GetRandomTerrain(Vector2 center)
    {
        int index = Random.Range(0, Terrain.Count);
        Instantiate(Terrain[index], center, Quaternion.identity);
    }

}
