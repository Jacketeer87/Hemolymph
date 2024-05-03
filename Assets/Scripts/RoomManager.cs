using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    /*
    Ideas: build entire game around the room manager

    things room manager does currently
        spawns rooms
        aligns rooms on a grid "kind of"
        makes a random amt of rooms based on range (x to y)
        creates a path between rooms
    
    Things room manager does not do currently
        spawn the player at the starting room
        spawn in enemies
        spawn in objects
        


    Things I want room manager to do
        -include environments which affects floors (forest vs burned forest)
        -different zones are different environments (small changes, desert)
    */









    [SerializeField] GameObject roomPrefab;
    [SerializeField] GameObject player;
    
    [SerializeField] private int maxRooms = 15;
    [SerializeField] private int minRooms = 10;

    int roomWidth = 30;
    int roomHeight = 16;

    
    [SerializeField] int gridSizeX = 30;
    [SerializeField] int gridSizeY = 16;

    private List<GameObject> roomObjects = new List<GameObject>();

    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();

    private int[,] roomGrid;

    private int roomCount;

    private bool generationComplete = false;

    //add object spawner 0 - length of list
    //start by generating object in center of each room

    //spawn enemy in center of created room

    


    private void Start()
    {
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue = new Queue<Vector2Int>();

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX/2, gridSizeY/2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    private void Update(){
        if(roomQueue.Count > 0 && roomCount < maxRooms && !generationComplete){
            Vector2Int roomIndex = roomQueue.Dequeue();
            int gridX = roomIndex.x;
            int gridY = roomIndex.y;

            TryGenerateRoom(new Vector2Int(gridX -1, gridY));
            TryGenerateRoom(new Vector2Int(gridX +1, gridY));
            TryGenerateRoom(new Vector2Int(gridX, gridY + 1));
            TryGenerateRoom(new Vector2Int(gridX, gridY - 1));
        }
        else if (roomCount < minRooms)
        {
            Debug.Log("RoomCount was less than min amt of rooms. Trying again");
            RegenerateRooms();
        }
        else if(!generationComplete)
        {
            Debug.Log($"Generation Complete, {roomCount} rooms created");
            generationComplete = true;
        }
    }

    private void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        roomQueue.Enqueue(roomIndex);
        
        int x = roomIndex.x;
        int y = roomIndex.y;
        Vector2 startCenter; 
        roomGrid[x,y] = 1;
        roomCount++;

        var initialRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        if(roomCount == 1){
            startCenter = initialRoom.GetComponent<Room>().GetCenterOfWalls();
            player.transform.position = startCenter; //Transform change
        }
        initialRoom.name = $"Room-{roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);

        //reference to player in room manager, player.transform = first room coordinates

    }



    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        Vector2 center;

        if(x >= gridSizeX || y >= gridSizeY || x<0 || y<0)
            return false;

        if(roomCount >= maxRooms)
            return false;
        
        if(Random.value < 0.5f && roomIndex != Vector2Int.zero)
            return false;

        if(CountAdjacentRooms(roomIndex) > 1)
            return false;

        if(roomGrid[x,y] != 0)
            return false;

        roomQueue.Enqueue(roomIndex);
        roomGrid[x,y] = 1;
        roomCount++;

        var newRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        newRoom.GetComponent<Room>().RoomIndex = roomIndex;
        center = newRoom.GetComponent<Room>().GetCenterOfWalls();
        newRoom.GetComponent<Room>().Spawn(center);
        newRoom.name = $"Room-{roomCount}";
        roomObjects.Add(newRoom);

        //instantiate enemies/terrain

        OpenDoors(newRoom, x, y);

        return true;
    }

    //Could spawn player in first room in above function
    //set player position = 1st room position
    //use boolean function to make sure player spawns
    //move player at runtime
    //could give reference to enemy prefabs for spawning
    //all this in above method
    //either spawn player or spawn enemies

    //clear all rooms and try again
    private void RegenerateRooms()
    {
        roomObjects.ForEach(Destroy);
        roomObjects.Clear();
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue.Clear();
        roomCount = 0;
        generationComplete = false;

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX/2, gridSizeY/2);
        StartRoomGenerationFromRoom(initialRoomIndex);

    }

    void OpenDoors(GameObject room, int x, int y)
    {
        Room newRoomScript = room.GetComponent<Room>();

        //neighbors
        Room leftRoomScript = GetRoomScriptAt(new Vector2Int(x-1, y)); 
        Room rightRoomScript = GetRoomScriptAt(new Vector2Int(x+1, y)); 
        Room topRoomScript = GetRoomScriptAt(new Vector2Int(x, y+1));
        Room bottomRoomScript = GetRoomScriptAt(new Vector2Int(x, y-1));

        //determine doors to open
        if(x > 0 && roomGrid[x-1, y] != 0)
        {
            //neighbouring room to left
            newRoomScript.OpenDoor(Vector2Int.left);
            leftRoomScript.OpenDoor(Vector2Int.right);

        }

        if(x < gridSizeX-1 && roomGrid[x+1, y] != 0)
        {
            //neighbouring room to right
            newRoomScript.OpenDoor(Vector2Int.right);
            rightRoomScript.OpenDoor(Vector2Int.left);
        }

        if(y > 0 && roomGrid[x, y-1] != 0)
        {
            //neighbouring room below
            newRoomScript.OpenDoor(Vector2Int.down);
            bottomRoomScript.OpenDoor(Vector2Int.up);
            
        }

        if(y < gridSizeY-1 && roomGrid[x, y+1] != 0)
        {
            //neighbouring room above
            newRoomScript.OpenDoor(Vector2Int.up);
            topRoomScript.OpenDoor(Vector2Int.down);
            
        }
    }

    Room GetRoomScriptAt(Vector2Int index){
        GameObject roomObject = roomObjects.Find(r => r.GetComponent<Room>().RoomIndex == index);
        if (roomObject != null)
            return roomObject.GetComponent<Room>();
        return null;
    }

    private int CountAdjacentRooms(Vector2Int roomIndex){
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;

        if(x > 0 && roomGrid[x-1, y] != 0)
            count++;    //left neightbor
        
        if(x < gridSizeX - 1 && roomGrid[x+1, y] != 0)
            count++;    //right neighbor
        
        if(y > 0 && roomGrid[x, y-1] != 0)
            count++;    //bottom neighbor
        
        if(y < gridSizeY - 1 && roomGrid[x, y+1] != 0)
            count++;    //top neighbor
        
        return count;
    }

    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;
        return new Vector3(roomWidth * (gridX - gridSizeX/2), roomHeight * (gridY - gridSizeY/2));
    }

    private void OnDrawGizmos(){
        Color gizmoColor = new Color(0,1,1,0.05f);
        Gizmos.color = gizmoColor;

        for(int x=0; x<gridSizeX; x++)
        {
            for (int y=0; y<gridSizeY; y++)
            {
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x,y));
                Gizmos.DrawWireCube(position, new Vector3(roomWidth, roomHeight, 1));
            }
        }
    }
}
