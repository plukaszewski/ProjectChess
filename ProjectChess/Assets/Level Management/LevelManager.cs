using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public List<Room> Rooms = new List<Room>();
    public Room[] RoomsPrefabs;
    public Vector2Int Size;
    public Vector2Int Offset;
    public Room CurrentRoom;

    public UnityEvent OnChangeRoom;

    private int DirectionToIndex (Vector2Int Direction)
    {
        if(Direction == new Vector2Int(0, 1))
        {
            return 0;
        }
        else if(Direction == new Vector2Int(1, 0))
        {
            return 1;
        }
        else if (Direction == new Vector2Int(0, -1))
        {
            return 2;
        }
        else if (Direction == new Vector2Int(-1, 0))
        {
            return 3;
        }
        return 0;
    }

    private Vector2Int IndexToPosition(int index)
    {
        return new Vector2Int(index % Size.x, index / Size.x);
    }

    private int PositionToIndex(Vector2Int Pos)
    {
        return Pos.x + Pos.y * Size.x;
    }

    private void GenerateRomms()
    {
        for(int i = 0; i < Size.x * Size.y; i++)
        {
            var Tmp = IndexToPosition(i);
            Rooms.Add(Instantiate(RoomsPrefabs[Random.Range(0, RoomsPrefabs.Length)], new Vector3(Tmp.x * Offset.x, Tmp.y * Offset.y, 0f), new Quaternion()));

            Rooms[i].Position = IndexToPosition(i);

            if(IndexToPosition(i).x == 0)
            {
                Destroy(Rooms[i].Entrances[3].gameObject);
            }
            if (IndexToPosition(i).x == Size.x - 1)
            {
                Destroy(Rooms[i].Entrances[1].gameObject);
            }
            if (IndexToPosition(i).y == 0)
            {
                Destroy(Rooms[i].Entrances[2].gameObject);
            }
            if (IndexToPosition(i).y == Size.y - 1)
            {
                Destroy(Rooms[i].Entrances[0].gameObject);
            }
        }

        CurrentRoom = Rooms[0];
    }

    public void ChangeRoom(Vector2Int Direction)
    {
        ChangeRoom(Rooms[PositionToIndex(CurrentRoom.Position + Direction)]);
    }

    public void ChangeRoom(Room NewRoom)
    {
        CurrentRoom = NewRoom;
        Global.Camera.transform.position = new Vector3(CurrentRoom.transform.position.x, CurrentRoom.transform.position.y, Global.Camera.transform.position.z);
        Global.GameManager.Player.transform.position = CurrentRoom.PlayerSpawn.transform.position;
        CurrentRoom.PlayerSpawn.gameObject.SetActive(false);
        Global.GameManager.Player.Movement.End();

        CurrentRoom.EnableEntrances(false);
        OnChangeRoom.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateRomms();
        ChangeRoom(Rooms[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
