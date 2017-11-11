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
            //transform.Rotate(0, 0, 60);

            //새로운 연결방향 계산
            List<bool> newDirections = new List<bool>();
            newDirections.Add(pipeDirection[5]);
            for (int i = 0; i < 6; i++)
            {
                newDirections.Add(pipeDirection[i]);
            }

            pipeDirection = newDirections;
        }
	}

	void CheckNext(){
        for(int i = 0; i < 6; i++)
        {
            Block Neighbor = FindObjectOfType<TileManager>().NeighborBlock(this, i);
            if (pipeDirection[i] && Neighbor != null && Neighbor.pipeDirection[ReverseDir(i)] && Neighbor.GetComponent<SpriteRenderer>().color != Color.gray)
            {
                Neighbor.GetComponent<SpriteRenderer>().color = Color.gray;
                Neighbor.CheckNext();
            }
        }
        /*if (pipeDirection[0]) {
            Block Neighbor = FindObjectOfType<TileManager>().FindWithPosition(positionX + 1, positionY);
            if(Neighbor != null && Neighbor.pipeDirection[3])
            {
                Neighbor.GetComponent<SpriteRenderer>().color = Color.gray;
                Neighbor.CheckNext();
            }        
		}
        if (pipeDirection[1])
        {
            Block Neighbor = FindObjectOfType<TileManager>().FindWithPosition(positionX + 0.5f, positionY + 1);
            if(Neighbor != null && Neighbor.pipeDirection[4])
            {
                Neighbor.GetComponent<SpriteRenderer>().color = Color.gray;
                Neighbor.CheckNext();
            }
        }*/
	}

	/*int nextDirection(int currentDirection){
		if(currentDirection == 5){
			return 0;
		}else{
			return currentDirection+1;
		}
	}*/

    int ReverseDir (int originDir)
    {
        originDir += 3;
        if(originDir < 6)
        {
            return originDir;
        }
        else
        {
            return originDir - 6;
        }
    }
}