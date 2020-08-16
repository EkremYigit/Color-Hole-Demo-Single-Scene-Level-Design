
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public CameraController CameraController;
    
    public static GameController Instance; //singelton
    public LevelManager levelManager;
    private UIController UiController;
    public ParticleSystem ParticleSystem;
    public OnChangePosition holeController;
    public FallController fallController;
    
    [SerializeField] private Transform gate;
    private bool stageFlag; //this flag shows us user on which stage first or second. if flase user on first stage.

    // Start is called before the first frame update


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    void Start()
    {
        // Sets all level info to level bar.
        UiController = GetComponent<UIController>();
        UiController.UiAppear = true;
        stageFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.isFirstStageDone() && !stageFlag && !holeController.MoveFlag
        ) //stageFlag  helps to prevent repeating additionally. 
        {
            //half of the game completed successfully.
            //Move to hole center of the first game area and move to second place.
            holeController.IsMoving = false; //stop user finger move.
            gate.DOMove(new Vector3(gate.position.x, -.3f, gate.position.z), 4f);
            holeController.ChangeStage();
        }


        if (levelManager.isSecondStageDone())
        { //Level Completed.
            
            ParticleSystem.gameObject.SetActive(true);
            UiController.ShowLevelCompletedText();
            ParticleSystem.Play();
            levelManager.closeLevelBar();
           
        }
    }


    public void StopGame()
    {
        //Block user movement.
        //Show OnPausedScreen.
        //Start Timer.
        UiController.StartOnPausedState(stageFlag);
    }

    public void ResumeGame()
    {
        //allow touch controle.
        UiController.ResumeGame();
        holeController.IsMoving = true;
    }

    public void ReStartGame() 
    {
        if (stageFlag) //if user resets in stage2 this flag is true.
        {
            stageFlag = false;
            holeController.MoveFlag = false;
            stageFlag = false;
            holeController.ResetMoveRange();
            CameraController.ReStartPos();
        }
       
        ResetGatePos();
        holeController.ReStartPos();
        levelManager.ResetLevel();
        UiController.AppearMainScreenEnvironments();
        holeController.IsMoving = true;
    }

    private void ResetGatePos()
    {
        gate.position= new Vector3(gate.position.x,0.63f,gate.position.z);
    }

    public void NextLevel() //Buda fenaaa
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        
        yield return new WaitForSeconds(1.5f);
        UiController.HideLevelCompletedText();
        CameraController.ReStartPos();
        holeController.ReStartPos();
        holeController.ResetMoveRange();
        ResetGatePos();
        levelManager.LoadNextLevel();
        UiController.AppearMainScreenEnvironments();
        UiController.UiAppear = true;
        holeController.IsMoving = true;
        stageFlag = false;
        holeController.MoveFlag = false;
    }

    public bool getUIStatement() //return UI is shown or not .
    {
        return UiController.UiAppear;
    }

    public void HideMainMenuUi()
    {
        UiController.UiAppear = false;
        UiController.HideMainScreenEnvironments();
    }

    public void ChangeProgress()
    {
        //Update Level Bar Image. Second half will filled with second area obstacles.
        stageFlag = true; //with this flag changing top of the level progress image will change.
        levelManager.ResetCollectedObstacle(); //levelBar.CollectedObstacle = 0;
        StartCoroutine(StopCamera()); // Change the number of obstacle that will collect on the second stage.
    }

    IEnumerator StopCamera()
    {
        yield return new WaitForSeconds(3f);
        holeController.IsMoving = true;
        CameraController.FollowFlag = false;
    }

    public void IncreaseProgress()
    {
        levelManager.IncreaseProgress(stageFlag);
    }


    public void MoveCamera()
    {
        CameraController.FollowFlag = true;
    }

    public void StopHole()
    {
        holeController.IsMoving = false;
    }

    public void ShakeCamera()
    {
        CameraController.ShakeCamera();
    }

    public void Vibration(bool vibrate)
    {
        fallController.IsVibrate = vibrate;
    }

   
}