using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    [SerializeField] Image _playerLifeBar;
    [SerializeField] Image _bossLifeBar;
    [SerializeField] LifeStats _playerLife;
    [SerializeField] LifeStats _bossLife;

    void Start()
    {
        _playerLife.OnHpChange += UpdatePlayerBar;
        _bossLife.OnHpChange += UpdateBossBar;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdatePlayerBar (int oldHP, int newHP)
    {
        _playerLifeBar.fillAmount = (float)newHP / _playerLife.maxHP;
    }

    void UpdateBossBar(int oldHP, int newHP)
    {
        _bossLifeBar.fillAmount = (float)newHP / _bossLife.maxHP;
    }
}
