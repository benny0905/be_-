using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour{
	public List<Block> Blocks = new List<Block>();
	public Block FindWithPosition(int x, int y, int z){
		return Blocks.Find(item => item.positionX == x && item.positionY == y && item.positionZ == z);
	}
}