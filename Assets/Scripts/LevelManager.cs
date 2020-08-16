using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int LevelCount = 0; //represent user current level. This variable "Only" increases user completed level successfully.
    public MeshCollider GeneratedMeshCollider;
    public Collider gameAreaCollider;
     // When game restarted or level update this pos will be called to place hole same place.
    public LevelBarController levelBar;
    private List<Level> Levels;
    private List<Transform> Obstacles;
    void Awake()
    {
        LevelCount = 0;
        LoadLevelsData(); //levelScript
        LoadLevel(LevelCount); 
       
    }

    private void Start()
    {
        UpdateLevelBar();
    }

    public void LoadLevel(int levelCount) //levelCount starts from 0 
    {
        //take all obstacles information from levelCount and load the scene according to their default positions.
        Levels[levelCount].GetComponent<Transform>().gameObject.SetActive(true);
        Levels[levelCount].LoadDefaultPositions();
        Levels[levelCount].ResetPositionsAndEnability();

    }

  
    
    

    
    private void LoadLevelsData()
    {
        Levels=new List<Level>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Levels.Add(transform.GetChild(i).GetComponent<Level>()); //collect all level data from its children.
        }
        
    }

    
    private void UpdateLevelBar()
    {
        //This function load all information about current levelCount Level information into Level bar.
        //And if user completed level calls again to update information.
        
        levelBar.Stage1Obstacles1 = Levels[LevelCount].Stage1Obstacles1;
        levelBar.Stage2Obstacles1 = Levels[LevelCount].Stage2Obstacles1;
        levelBar.UpdateLevelBar(LevelCount);
      
    }



    public void LoadNextLevel()
    {
        
        Levels[LevelCount].GetComponent<Transform>().gameObject.SetActive(false); //Close all  current environments.
        levelBar.ResetLevel();

        if (LevelCount == 2) // 3lv is designed for this reason if the user end of the game game return the beginning.
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            LevelCount++;
        }
        LoadLevel(LevelCount);
        UpdateLevelBar();
        ShowLevelBar();

    }
    

    public bool isFirstStageDone()
    {

        return levelBar.CollectedObstacle == levelBar.Stage1Obstacles1;
    }

    public bool isSecondStageDone()
    {
        return levelBar.getSecondProgressBarAmount()>=1;
    }

    public void closeLevelBar()
    {
        levelBar.closeLevelBar();
    }

    public void ShowLevelBar()
    {
        levelBar.ShowLevelBar();
    }
    public void ResetCollectedObstacle()
    {
        levelBar.CollectedObstacle = 0;
    }

    public void IncreaseProgress(bool stageFlag)
    {
        levelBar.IncreaseProgress(stageFlag);
    }

    public void ResetLevel()
    {
        //LevelBar Reset.
        //Level Obstacles Reset.
        levelBar.ResetLevel();
        Levels[LevelCount].ResetPositionsAndEnability();
        Levels[LevelCount].ResetPhysics(gameAreaCollider,GeneratedMeshCollider);
        
        

    }

   
}
