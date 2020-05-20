using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    [SerializeField]
    WaveSpawner waveSpawner;
    [SerializeField]
    Animator waveUIAnimator;
    [SerializeField]
    Text WaveCountDownText;
    [SerializeField]
    Text waveCountText;

    private WaveSpawner.SpawnState prevState;
    // Start is called before the first frame update
    void Start()
    {
        if(waveSpawner == null)
        {
            Debug.LogError("No waveSpawner referenced! ");
            this.enabled = false;
        }
        if (waveUIAnimator == null)
        {
            Debug.LogError("No waveUIAnimator referenced! ");
            this.enabled = false;
        }
        if (WaveCountDownText == null)
        {
            Debug.LogError("No WaveCountDownText referenced! ");
            this.enabled = false;
        }
        if (waveCountText == null)
        {
            Debug.LogError("No waveCountText referenced! ");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(waveSpawner.GetSpawnState)
        {
            case WaveSpawner.SpawnState.COUNTING:
                UpdateCountingUI();
                    break;
            case WaveSpawner.SpawnState.SPAWNING:
                UpdateSpawningUI();
                    break;
        }

        prevState = waveSpawner.GetSpawnState;
    }

    void UpdateCountingUI()
    {
        if (prevState != WaveSpawner.SpawnState.COUNTING)
        {
            waveUIAnimator.SetBool("WaveIncoming", false);
            waveUIAnimator.SetBool("WaveCountdown", true);
            Debug.Log("COUNTING");
        }
        WaveCountDownText.text = ((int)waveSpawner.WaveCountDown).ToString();
    }

    void UpdateSpawningUI()
    {
        if (prevState != WaveSpawner.SpawnState.SPAWNING)
        {
            waveUIAnimator.SetBool("WaveCountdown", false);
            waveUIAnimator.SetBool("WaveIncoming", true);
            Debug.Log("SPAWNING");
        }
        waveCountText.text = waveSpawner.NextWave.ToString();
    }
}
