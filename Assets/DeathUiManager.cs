using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathUiManager : MonoBehaviour
{

    [Header("Counts")]
    public int level;
    public int ratsSacrificed;
    public int herosSlayed;
    public int skeletonsSummoned;
    
    // Start is called before the first frame update

    [Header("TextElements")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI ratsText;
    public TextMeshProUGUI herosText;
    public TextMeshProUGUI skeletonsText;
    public TextMeshProUGUI score;
    
    
    private void OnEnable()
    {
        int scoreCalc = level * ratsSacrificed + 100 * herosSlayed;
        levelText.text = "Level: " + level;
        ratsText.text = "Rats Sacrified: " + ratsSacrificed;
        herosText.text = "Heroes Slayed: " + herosSlayed;
        skeletonsText.text = "Skeletons Summoned: " + skeletonsSummoned;
        score.text = "You managed to get a score of " + scoreCalc + " Points";
    }
}
