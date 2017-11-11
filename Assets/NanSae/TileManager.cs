﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour{
	public List<Block> Blocks = new List<Block>();

    public Block BlockLine;
    public Block Block120;
    public Block Block60;

    void InstantiateBlock(string BlockType, float posX, float posY)
    {
        if(BlockType == "Lin")
        {
            Instantiate(BlockLine);
        }else if(BlockType == "60")
        {
            Instantiate(Block60);
        }else if(BlockType == "120")
        {
            Instantiate(Block120);
        }
    }

    public Block NeighborBlock(Block origin, int dir)
    {
        if (dir == 0)
        {
            return FindWithPosition(origin.positionX + 1, origin.positionY);
        } else if(dir == 1)
        {
            return FindWithPosition(origin.positionX + 0.5f, origin.positionY + 1);
        }else if(dir == 2)
        {
            return FindWithPosition(origin.positionX - 0.5f, origin.positionY + 1);
        }else if(dir == 3)
        {
            return FindWithPosition(origin.positionX - 1, origin.positionY);
        }else if(dir == 4)
        {
            return FindWithPosition(origin.positionX - 0.5f, origin.positionY-1);
        }else if(dir == 5)
        {
            return FindWithPosition(origin.positionX + 0.5f, origin.positionY - 1);
        }
        else
        {
            Debug.LogError("Invalid Direction Number");
            return null;
        }
    }

	public Block FindWithPosition(float x, float y){
		return Blocks.Find(item => item.positionX == x && item.positionY == y);
	}
}