using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using HarmonyLib;
using System.IO;
using BepInEx;
using TMPro;

namespace EscapeMandrilliaMod
{
    [BepInPlugin("com.mistersigma.BananaMod", "BananaMod", "1.0.0")]
    [BepInProcess("Escape from Mandrillia.exe")]
    public class Plugin : BaseUnityPlugin
    {
        AssetBundle myBundle;
        GameObject goldbananaPrefab;
        ExtendedCameraWalkerController _deathcontroller;
        OVERMANAGER _score;
        bool _godbananaspawned = false;
        public AudioClip _soundbanana;
        public AudioClip _bluebananasound;
        public AudioClip _redbananasound;
        public AudioClip _doomsound;
        public AudioClip _angelsound;

        private void Awake()
        {
            Logger.LogInfo("Mod Loaded!");
            var harmony = new Harmony("com.mistersigma.BananaMod");
            harmony.PatchAll();

            LoadBundle();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        void Update()
        {
            if (SceneManager.GetActiveScene().name == "LEVEL_LAB")
            {
                if (_deathcontroller == null)
                {
                    _deathcontroller = FindObjectOfType<ExtendedCameraWalkerController>();
                }else
                {
                    if (_deathcontroller.gameObject.GetComponent<BananaLogic>() == null)
                    {
                        _deathcontroller.gameObject.AddComponent<BananaLogic>();
                        AudioSource _audio = _deathcontroller.gameObject.AddComponent<AudioSource>();
                    }
                }
                if (_score.timer > 500 && !_godbananaspawned)
                {
                    float x = Random.Range(76, -28);
                    float z = Random.Range(44, -33);
                    float randomY = Random.Range(-180f, 180f);

                    Vector3 _pos = new Vector3(x, 0, z);
                    GameObject a = Instantiate(goldbananaPrefab, _pos, Quaternion.Euler(0, randomY, 0));
                    AudioSource _audio = a.AddComponent<AudioSource>();
                    _audio.clip = _angelsound;
                    _audio.volume = _audio.volume / 8;
                    _audio.spatialBlend = 1f;
                    _audio.minDistance = 1f;
                    _audio.maxDistance = 20f;
                    _audio.loop = true;
                    _audio.Play();
                    _godbananaspawned = true;
                }
            }
            if (Input.GetKey(KeyCode.F1))
            {
                Application.OpenURL("https://discord.com/invite/EgyqBdbE6h");
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Logger.LogInfo($"Scene loaded: {scene.name}");

            if (scene.name == "LEVEL_LAB")
            {
                GameObject bananaPrefab = myBundle.LoadAsset<GameObject>("banana");
                _soundbanana = myBundle.LoadAsset<AudioClip>("poskolizn-na-bananovoi-kojure.mp3");
                GameObject bluebananaPrefab = myBundle.LoadAsset<GameObject>("bluebanana");
                _bluebananasound = myBundle.LoadAsset<AudioClip>("yaya");
                GameObject redbananaPrefab = myBundle.LoadAsset<GameObject>("redbanana");
                _redbananasound = myBundle.LoadAsset<AudioClip>("me_minion_bababa_banana_sound_effect");
                goldbananaPrefab = myBundle.LoadAsset<GameObject>("bananaDEV");
                _angelsound = myBundle.LoadAsset<AudioClip>("zvuk_tipa_svjaschennyj-nu_pri_vide_boga_naprimer_mp3uh_ru");
                _doomsound = myBundle.LoadAsset<AudioClip>("DOOM");

                _score = FindObjectOfType<OVERMANAGER>();



                for (int i = 0; i < 100; i++)
                {
                    float x = Random.Range(76, -28);
                    float z = Random.Range(44, -33);
                    float randomY = Random.Range(-180f, 180f);

                    Vector3 _pos = new Vector3(x, 0, z);
                    GameObject a = Instantiate(bananaPrefab, _pos, Quaternion.Euler(0, randomY, 0));
                }

                for (int i = 0; i < 50; i++)
                {
                    float x = Random.Range(76, -28);
                    float z = Random.Range(44, -33);
                    float randomY = Random.Range(-180f, 180f);

                    Vector3 _pos = new Vector3(x, 0, z);
                    GameObject a = Instantiate(bluebananaPrefab, _pos, Quaternion.Euler(0, randomY, 0));
                    Light _banana = a.GetComponent<Light>();
                    _banana.color = Color.blue;
                }
                for (int i = 0; i < 10; i++)
                {
                    float x = Random.Range(76, -28);
                    float z = Random.Range(44, -33);
                    float randomY = Random.Range(-180f, 180f);

                    Vector3 _pos = new Vector3(x, 0, z);
                    GameObject a = Instantiate(redbananaPrefab, _pos, Quaternion.Euler(0, randomY, 0));
                    Light _banana = a.AddComponent<Light>();
                    _banana.color = Color.red;
                }
                if (_score.timer > 500)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        float x = Random.Range(76, -28);
                        float z = Random.Range(44, -33);
                        float randomY = Random.Range(-180f, 180f);

                        Vector3 _pos = new Vector3(x, 0, z);
                        GameObject a = Instantiate(goldbananaPrefab, _pos, Quaternion.Euler(0, randomY, 0));
                        AudioSource _audio = a.AddComponent<AudioSource>();
                        _audio.clip = _angelsound;
                        _audio.volume = _audio.volume / 8;
                        _audio.spatialBlend = 1f;
                        _audio.minDistance = 1f;
                        _audio.maxDistance = 20f;
                        _audio.loop = true;
                        _audio.Play();   
                    }
                }
            }
            if (scene.name == "MENU")
            {
                GameObject nuggetsPrefab = myBundle.LoadAsset<GameObject>("Me");
                GameObject ball = FindObjectOfType<BALL>().gameObject;

                Destroy(ball.transform.Find("MESH").gameObject);

                GameObject a = Instantiate(nuggetsPrefab, ball.transform);
                a.transform.localScale = new Vector3(30, 30, 30);
                a.name = "MESH";

                GameObject panel = new GameObject("reklamaPanel", typeof(Canvas));
                Canvas _canvas = panel.GetComponent<Canvas>();
                _canvas.worldCamera = Camera.main;
                _canvas.renderMode = RenderMode.WorldSpace;
                panel.transform.position = new Vector3(0, 1.5f, 5.11f);
                panel.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                GameObject _sd = new GameObject("text");
                TextMeshPro _text = _sd.AddComponent<TextMeshPro>();
                _text.name = "slova";
                _text.rectTransform.sizeDelta = new Vector2(300, 100);
                _text.transform.SetParent(panel.transform);
                _text.transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f);
                _text.raycastTarget = false;
                _text.text = "Thanks for downloading this mod! If you really like it, come visit our Discord channel. We'll have other mods there, so you can keep up with our developments! We'll also be hosting events there, so you'll hardly get bored. Press F1 Button!";
                _text.enableAutoSizing = true;
                _text.transform.localPosition = new Vector3(0f, 0f, 1.1f);

                GameObject _button = new GameObject("discrodbutton");
                _button.transform.SetParent(panel.transform);
                _button.transform.localScale = new Vector3(-0.2f, 0.1f, 0.2f);
                _button.transform.localPosition = new Vector3(0f, 0f, 1.1f);

                Image _daaw = _button.AddComponent<Image>();
                _daaw.color = Color.blue;
                _daaw.raycastTarget = false;

                GameObject _textt = new GameObject("meshtext");
                _textt.transform.SetParent(_button.transform);
                TextMeshPro _ttbb =  _textt.AddComponent<TextMeshPro>();
                _ttbb.transform.localScale = new Vector3(5f, 5f, 5f);
                _ttbb.transform.localPosition = new Vector3(0f, 0f, 1.1f);
                _ttbb.raycastTarget = false;
                _ttbb.enableAutoSizing = true;
                _ttbb.text = "Join Discord Server(button doesn't work, press F11)";

                Button _dsb = _button.AddComponent<Button>();
                _dsb.onClick.AddListener(() =>
                {
                    Application.OpenURL("https://discord.com/invite/EgyqBdbE6h");
                });
                
                RectTransform rect = panel.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(200, 200);
            }
        }

        void LoadBundle()
        {
            string pluginPath = Path.GetDirectoryName(Info.Location);

            string bundlePath = Path.Combine(pluginPath, "bananamod");

            myBundle = AssetBundle.LoadFromFile(bundlePath);

            if (myBundle == null)
            {
                Logger.LogError("invalid AssetBundle!");
                return;
            }

            Logger.LogInfo("AssetBundle succs!");
        }
    }
}