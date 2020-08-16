
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelBarController : MonoBehaviour
{
    // This class modify level bar components.
    [SerializeField] private TMP_Text currentLevelText;
    [SerializeField] private TMP_Text nextLevelText;
    [SerializeField] private Image firstProgressBar;
    [SerializeField] private Image SecondProgressBar;

    private float Stage1Obstacles,Stage2Obstacles;
    private int _collectedObstacle;
    
    

    void Start()
    {
        _collectedObstacle = 0;
    }


    public void IncreaseProgress(bool flag) // this function fill the bar when user collect obstacles.
    {
        //if flag = false its represent user on the first part of game. else flag = true second part

        if (!flag)
        {
            _collectedObstacle++;
            firstProgressBar.fillAmount = _collectedObstacle / Stage1Obstacles;
        }
        else
        {
            _collectedObstacle++;
            SecondProgressBar.fillAmount = _collectedObstacle / Stage2Obstacles;
        }
    }


  


    public float Stage1Obstacles1
    {
        get => Stage1Obstacles;
        set => Stage1Obstacles = value;
    }

    public float Stage2Obstacles1
    {
        get => Stage2Obstacles;
        set => Stage2Obstacles = value;
    }

    public float getSecondProgressBarAmount()
    {
        return SecondProgressBar.fillAmount;
    }


    public int CollectedObstacle
    {
        get => _collectedObstacle;
        set => _collectedObstacle = value;
    }

    public void ShowLevelBar()
    {
        gameObject.SetActive(true);
    }
    public void closeLevelBar()
    {
        gameObject.SetActive(false);
        SecondProgressBar.fillAmount = 0;
    }

    public void UpdateLevelBar(int levelCount)
    {
        //Clean level bar fill amounts & Update Current and NextLevel Text
        _collectedObstacle = 0;
        CleanLevelBar();
        UpdateLevelTexts(levelCount);

    }

    private void UpdateLevelTexts(int levelCount)
    {
        levelCount++;
        currentLevelText.SetText(levelCount.ToString());
        nextLevelText.SetText((levelCount+1).ToString());
        
    }

    private void CleanLevelBar()
    {
        firstProgressBar.fillAmount = 0;
        SecondProgressBar.fillAmount = 0;

    }

    public void ResetLevel()
    {
        CleanLevelBar();
        _collectedObstacle = 0;
    }
}