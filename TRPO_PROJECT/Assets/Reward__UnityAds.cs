using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{
    [SerializeField] Button _showAdButton;
    private string _androidAdUnitId = "Rewarded_Android";
    private string _AdId = "5496327";
    string _adUnitId = null;

    public GameObject BackButton;

    void Awake()
    {
        _adUnitId = _androidAdUnitId;  
        Debug.Log("Awake Ad: " + _adUnitId);

        Advertisement.Initialize(_AdId, false, this);
        _showAdButton.interactable = false;

        LoadAd();
    }

    void Start()
    {
       
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }
    
    public void ShowAd()
    {
        _showAdButton.interactable = false;
        Advertisement.Show(_adUnitId, this);

        BackButton.GetComponent<BackToMenu>().coinAcquired = 50;
        BackButton.GetComponent<BackToMenu>().CloaseGameplay();
    }



    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            _showAdButton.onClick.AddListener(ShowAd);
            _showAdButton.interactable = true;
        }
    }




    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Failed to load ad: " + adUnitId + " Error: " + message);
    }
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log("Failed to show ad: " + adUnitId + " Error: " + message);
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        Debug.Log("Ad started: " + adUnitId);
    }

    public void OnUnityAdsShowClick(string adUnitId)
    {
        Debug.Log("Ad clicked: " + adUnitId);
    }
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads initialization failed: {error} - {message}");
    
    }



    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Ad completed: " + adUnitId + " Completion state: " + showCompletionState);
    }
}