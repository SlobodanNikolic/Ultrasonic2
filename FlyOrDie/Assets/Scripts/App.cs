using UnityEngine;
using System.Collections;

namespace Pokega{
	
	public class App : MonoBehaviour {
		//After how many games a rate me popup shows
		public static string bundleVersion = "1.0";
		
//		public static AdsControl ads;
//		public static NotificationControl notif;
		public static LocalDBControl local;
//		public static BestHttpControl server;
		public static ScoreControl score;
//		public static GameCenterControl gc;
//		public static FacebookControl fb;
//		public static ConnectionChecker conn;
//		public static LeaderboardMaster lb;
//		public static AchievementMaster ach;
//		public static ShopControl shop;
//		public static Inventory inv;
//		public static Device device;
		public static UIControl ui;
		public static Gameplay gameplay;
		public static PlayerInfo player;
//		public static AnalyticsControl analytics;
//		public static GiftControl gift;
//		public static SoundControl sound;
//		public static AppRate appRate;
//		public static Tutorial tut;
		public static Game game;
		public static Sonar sonar;
		public static ScreenSonar screenSonar;
		public static AIBat ai;
		public static RecordedGameplay recorded;
		public static TunnelBuilder1 builder;
		public static TileDestroyer destroyer;
		public static CameraControl camShake;
		public static Player bat;

		void Awake()
		{
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = 60;
			InitializeScripts();

		}

		void InitializeScripts()
		{
			//analytics = GameObject.Find("AnalyticsControl").GetComponent<AnalyticsControl>();
//			ads = GameObject.Find("Ads").GetComponent<AdsControl>();
//			notif = GameObject.Find("Notifications").GetComponent<NotificationControl>();
			local = GameObject.Find("LocalDB").GetComponent<LocalDBControl>();
//			server = GameObject.Find("ServerAPI").GetComponent<BestHttpControl>();
			score = GameObject.Find("ScoreControl").GetComponent<ScoreControl>();
//			gc = GameObject.Find("GameCenter").GetComponent<GameCenterControl>();
//			fb = GameObject.Find("Facebook").GetComponent<FacebookControl>();
			//conn = GameObject.Find("ConnectionChecker").GetComponent<ConnectionChecker>();
			//lb = GameObject.Find("LeaderboardMaster").GetComponent<LeaderboardMaster>();
			//ach = GameObject.Find("AchievementMaster").GetComponent<AchievementMaster>();
//			shop = GameObject.Find("Shop").GetComponent<ShopControl>();
//			inv = GameObject.Find("Inventory").GetComponent<Inventory>();
			//device = GameObject.Find("Device").GetComponent<Device>();
			ui = GameObject.Find("UIControl").GetComponent<UIControl>();
			player = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>();
//			gift = GameObject.Find("GiftControl").GetComponent<GiftControl>();
			//sound = GameObject.Find ("SoundControl").GetComponent<SoundControl>();
			//appRate = GameObject.Find("AppRate").GetComponent<AppRate>();
			game = GameObject.Find("Game").GetComponent<Game>();
			gameplay = GameObject.Find("Bat").GetComponent<Gameplay>();
			bat = GameObject.Find ("Bat").GetComponent<Player>();
//			sonar = GameObject.Find ("Sonar").GetComponent<Sonar>();
//			screenSonar = GameObject.Find ("HomeSonar").GetComponent<ScreenSonar>();

			if (GameObject.Find ("Tutorial")) {
//				tut = GameObject.Find ("Tutorial").GetComponent<Tutorial> ();

			}
//			recorded = GameObject.Find ("GameplayRecorder").GetComponent<RecordedGameplay>();
//			ai = GameObject.Find ("Bat2").GetComponent<AIBat>();
			builder = GameObject.Find ("TunnelBuilder").GetComponent<TunnelBuilder1>();
			destroyer = GameObject.Find ("TileDestroyer").GetComponent<TileDestroyer>();
			camShake = GameObject.Find ("Main Camera").GetComponent<CameraControl>();
		}		

		void Start () 
		{
			
			Screen.sleepTimeout = 20;
			//player.gameObject.SetActive(false);
		}

		void Update(){
			if (Input.GetKeyDown (KeyCode.Escape)) {
				ui.SetPopUp ("UI QuitGame");
				Time.timeScale = 0f;
			}
		}

		public void Quit(){
			Application.Quit ();
		}

		public void NormalizeTime(){
			Time.timeScale = 1f;
		}
	}
}