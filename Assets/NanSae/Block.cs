using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour{
	public float positionX;
	public float positionY;
    
	public List<bool> pipeDirection = new List<bool>();

    public TileManager Manager;

	void OnMouseDown(){
        if (name == "StartTile")
        {
            CheckNext();
        }
        else
        {
            transform.Rotate(0, 0, 60);

            //새로운 연결방향 계산
            List<bool> newDirections = new List<bool>();
            newDirections.Add(pipeDirection[5]);
            for (int i = 0; i < 5; i++)
            {
                newDirections.Add(pipeDirection[i]);
            }

            pipeDirection = newDirections;
        }
	}

	void Start(){
        Manager = FindObjectOfType<TileManager>();
        Manager.Blocks.Add(this);
        transform.position = worldPosition;
	}

	public Vector3 worldPosition{
		get{
			return new Vector3(positionX*3, positionY*2.4f, 0);
		}
	}

	void CheckNext(){
        if (pipeDirection[0]) {
            Block Neighbor = FindObjectOfType<TileManager>().FindWithPosition(positionX + 1, positionY);
            if(Neighbor != null && Neighbor.pipeDirection[3])
            {
                Neighbor.GetComponent<SpriteRenderer>().color = Color.gray;
            }        
		}
	}

	/*int nextDirection(int currentDirection){
		if(currentDirection == 5){
			return 0;
		}else{
			return currentDirection+1;
		}
	}*/
}