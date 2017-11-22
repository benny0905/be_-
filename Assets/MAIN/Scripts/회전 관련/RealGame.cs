using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RealGame : MonoBehaviour {

    public GameObject[] DropBlocks = new GameObject[1];
    public string Filename = "Ans_CH.bin";
    public bool AlreadyWon = false;
    private int[ , ] AngleAnswer;
    public int Stage;

    private void Start()
    {
        AlreadyWon = false;
        int retval = ReadAnsFile();
        if (retval == 1) return;

        if (DontDestroyOnLoadManager.Objects.Count != 0)
        {
            DontDestroyOnLoadManager.DestroyAll();
        }
    }

    private void Update()
    {
        RotateAnim[] Anim = new RotateAnim[DropBlocks.Length];
        
        for (int AnsNum = 0; AnsNum < AngleAnswer.GetLength(0); AnsNum++)
        {
            for (int BlockNum = 0; BlockNum <= AngleAnswer.GetLength(1); BlockNum++)
            {
                if(BlockNum == DropBlocks.Length)
                {
                    // 성공했을 시 여기로 옴
                    Invoke("LoadToWinningScene", 2.2f);
                    GameObject.Find("Pause").SetActive(false);
                    for(int num = 0; num < DropBlocks.Length; num++)
                    {
                        AlreadyWon = true;
                        if (AngleAnswer[AnsNum, num] != -1)
                        {
                            DropBlocks[num].GetComponent<Animator>().enabled = true;
                        }
                    }
                    return;
                }

                Anim[BlockNum] = DropBlocks[BlockNum].GetComponent<RotateAnim>();
                if (Anim[BlockNum].CheckInitializing() == true)
                {
                    // 블럭을 무작위로 섞는 중이면(초기화 중이면) 작업 중단
                    return;
                }

                if(AngleAnswer[AnsNum, BlockNum] != -1)
                {
                    if(Anim[BlockNum].StriaghtBlock == true)
                    {
                        if (Anim[BlockNum].state != AngleAnswer[AnsNum, BlockNum] && Anim[BlockNum].state != (AngleAnswer[AnsNum, BlockNum] + 180) % 360)
                        {
                            // 직선 블럭일 때
                            break;
                        }
                    }
                    else
                    {
                        if (Anim[BlockNum].state != AngleAnswer[AnsNum, BlockNum])
                        {
                            // 일반 블럭일 때
                            break;
                        }
                    }
                }
            }
        }
    }

    private int ReadAnsFile()
    {
        if (File.Exists(Filename))
        {
            using (StreamReader reader = new StreamReader(Filename))
            {
                int sizeofblocks = 0;
                string DataString = reader.ReadLine();
                for (int i = 0; i < DataString.Length; i++)
                {
                    if (i != 0) sizeofblocks *= 10;
                    sizeofblocks += DataString[i] - '0';
                }
                if (sizeofblocks != DropBlocks.Length)
                {
                    Debug.LogError("FATAL ERROR : 육각블럭들의 개수가 맞지 않아!!");
                    return 1;
                }

                int WrittenNameidx = 0;
                string[] WrittenName = new string[DropBlocks.Length];
                DataString = reader.ReadLine();
                for (int idx = 0; idx < DataString.Length; idx++)
                {
                    if (DataString[idx] == '|') WrittenNameidx++;
                    else
                    {
                        WrittenName[WrittenNameidx] += DataString[idx];
                    }
                }

                for (int idx = 0; idx < DropBlocks.Length; idx++)
                {
                    if (DropBlocks[idx].name != WrittenName[idx])
                    {
                        Debug.LogError("FATAL ERROR : 육각블럭들이 제대로 세팅되지 않았어!!");
                        return 1;
                    }
                }
                for (int lineidx = 0; ; lineidx++)
                {
                    string AngleData = reader.ReadLine();

                    if (AngleData == null) break;

                    if (lineidx == 0)
                    {
                        AngleAnswer = new int[1, DropBlocks.Length];
                        for (int init = 0; init < DropBlocks.Length; init++)
                        {
                            AngleAnswer[0, init] = 0;
                        }
                    }
                    else
                    {
                        int[,] Temp = new int[lineidx, DropBlocks.Length];
                        for (int templineidx = 0; templineidx < lineidx; templineidx++)
                        {
                            for (int tempidx = 0; tempidx < DropBlocks.Length; tempidx++)
                            {
                                Temp[templineidx, tempidx] = AngleAnswer[templineidx, tempidx];
                            }
                        }
                        AngleAnswer = new int[lineidx + 1, DropBlocks.Length];
                        for (int templineidx = 0; templineidx < lineidx; templineidx++)
                        {
                            for (int tempidx = 0; tempidx < DropBlocks.Length; tempidx++)
                            {
                                AngleAnswer[templineidx, tempidx] = Temp[templineidx, tempidx];
                            }
                        }
                        for (int init = 0; init < DropBlocks.Length; init++)
                        {
                            AngleAnswer[lineidx, init] = 0;
                        }
                    }

                    int Ansidx = 0;
                    bool minus = false;
                    for (int idx = 0; idx < AngleData.Length; idx++)
                    {
                        if (AngleData[idx] == '-')
                        {
                            if (minus == true)
                            {
                                Debug.LogError("FATAL ERROR : \"" + Filename + "\"가 이상해!!");
                                return 1;
                            }
                            else minus = true;
                        }
                        else if (AngleData[idx] == '|')
                        {
                            Ansidx++;
                            minus = false;
                        }
                        else if (AngleData[idx] >= '0' && AngleData[idx] <= '9')
                        {
                            int letter = AngleData[idx] - '0';
                            if (letter != 0 || AngleAnswer[lineidx, Ansidx] != 0)
                            {
                                AngleAnswer[lineidx, Ansidx] *= 10;
                                AngleAnswer[lineidx, Ansidx] += letter;
                                if (minus == true) AngleAnswer[lineidx, Ansidx] = -AngleAnswer[lineidx, Ansidx];
                            }
                        }
                        else
                        {
                            Debug.LogError("FATAL ERROR : \"" + Filename + "\"가 이상해!!");
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogError("FATAL ERROR : \"" + Filename + "\"가 존재하지 않아!!");
            return 1;
        }
        return 0;
    }
    public string Level;

    private void LoadToWinningScene()
    {
        int NowPassed = PlayerPrefs.GetInt("LevelPassed");
        PlayerPrefs.SetInt("LevelPassed", Mathf.Max(NowPassed, Stage));
        SceneManager.LoadScene(Level);
    }
}
