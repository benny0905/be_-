using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour{
	public int positionX;
	public int positionY;
	public int positionZ;
	public List<bool> pipeDirection = new List<bool>();
	void OnMouseDown(){
		transform.Rotate(0, 0, 60);

		//새로운 연결방향 계산
		List<bool> newDirections = new List<bool>();
		newDirections.Add(pipeDirection[5]);
		for(int i = 0; i < 5; i++){
			newDirections.Add(pipeDirection[i]);
		}

		pipeDirection = newDirections;
	}

	void Start(){
		/*for(int i = 0; i < 6; i++){
			pipeDirection.Add(false);
		}
		pipeDirection[0] = true;
		pipeDirection[3] = true;*/
		transform.position = worldPosition;
	}

	public Vector3 worldPosition{
		get{
			return new Vector3((positionX-positionY)*1.5f, (positionX+positionY)*2.4f, 0);
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