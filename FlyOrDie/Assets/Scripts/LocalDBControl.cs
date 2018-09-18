using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
//using Facebook.MiniJSON; 

namespace Pokega{

	public class LocalDBControl : MonoBehaviour 
	{
		public string secretKey = "SecretHashKeyPokega3421";
		string fbid;
		string fbEmail;
		string fbData;
		long fbLastSaved;
		public List<Score> scores;
		public List<Score> bestScores;
		public List<Score> totalScores;
		
		public string lastResponse;
		public string deviceModel;
		public string deviceName;
		public string deviceType;
		public string deviceId;
		public bool resetPlayerPrefs;
		public string timeZone;
		public bool localDBReady;

		public List<GameObject> saveAndLoadScripts;
		
		void Awake() 
		{
			localDBReady = false;
//			Debug.Log("<color=green>LOCAL DB CONTROL AWAKE</color>");
			if(resetPlayerPrefs)
				PlayerPrefs.DeleteAll();

			timeZone = TimeZone.CurrentTimeZone.StandardName;			
//			Debug.Log("Local DB - FBID FROM PLAYER PREFS is " + PlayerPrefs.GetString("fbid"));
		}
		
		void Start() 
		{
			scores = App.score.scores;
			bestScores = App.score.bestScores;
			totalScores = App.score.totalScores;

			Invoke ("GoHome", 2f*Time.timeScale);
			PlayerLoad();
			//App.server.Merge();
		}

		public void GoHome(){
			App.game.Home ();
		}

		public void LoadFromScripts(){
			foreach(GameObject obj in saveAndLoadScripts){
				ILocal interfaceAcces = obj.GetComponent(typeof(ILocal)) as ILocal;
				interfaceAcces.Load();
			}
				
		}

		public void SaveFromScripts(){
			foreach(GameObject obj in saveAndLoadScripts){
				ILocal interfaceAcces = obj.GetComponent(typeof(ILocal)) as ILocal;
				interfaceAcces.Save();
			}
			
		}

		public void ResetFromScripts(){
			foreach(GameObject obj in saveAndLoadScripts){
				ILocal interfaceAcces = obj.GetComponent(typeof(ILocal)) as ILocal;
				interfaceAcces.Reset();
			}
			
		}
		

		public void PlayerLoad () {
//			Debug.Log("<color=blue>Local DB - Player Load</color>");
			PrintPP();
			//POKUSAJ UCITAVANJA STRINGA, PROVERA DA LI PP POSTOJI
			string loadedString = PlayerPrefs.GetString ("gameProgress");
			
			//IGRAC NIJE PRVI PUT U IGRI
			if (loadedString != "") {
				//Debug.Log("<color=blue>Local DB - Player was in game before</color>");
				
				//UCITAVANJE PP U STRING PROMENLJIVE
				string uid = PlayerPrefs.GetString("uid");
				string gcid = PlayerPrefs.GetString("gcid");
				string apnToken = PlayerPrefs.GetString("apnToken");
				string username = PlayerPrefs.GetString("username");
				string password = PlayerPrefs.GetString("password");
				string diamondCount = PlayerPrefs.GetString("diamonds");
				string coinsCount = PlayerPrefs.GetString("coins");				
				//Debug.Log(coinsCount + " " + diamondCount);
				
				string fbid = PlayerPrefs.GetString("fbid");
				string fbName = PlayerPrefs.GetString("fbName");
				string fbEmail = PlayerPrefs.GetString("fbEmail");
				string birthday = PlayerPrefs.GetString ("birthday");
				string gender = PlayerPrefs.GetString("gender");
				string lives = PlayerPrefs.GetString("lives");
				string lastLevel = PlayerPrefs.GetString("lastLevel");
				string lastCheckPoint = PlayerPrefs.GetString("lastCheckpoint");	
				string inventory = PlayerPrefs.GetString("Inventory");
				
				string sound = PlayerPrefs.GetString("sound");
				string music = PlayerPrefs.GetString("music");
				//string notifications = PlayerPrefs.GetString("notifications");
				
				//UCITAVANJE SKOROVA
				for(int i = 0; i<bestScores.Count; i++){
					bestScores[i].score = PlayerPrefs.GetString(bestScores[i].scoreName);
					if(bestScores[i].score == "")
						bestScores[i].score = Crypting.EncryptInt(0);
				//	Debug.Log("BEST SCORES " + bestScores[i].score);
				}

				for(int i = 0; i<totalScores.Count; i++){
					totalScores[i].score = PlayerPrefs.GetString(totalScores[i].scoreName);
					if(totalScores[i].score == "")
						totalScores[i].score = Crypting.EncryptInt(0);
				//	Debug.Log("TOTAL SCORES " + totalScores[i].score);
				}
				
				string shop = PlayerPrefs.GetString("shop");			
				string prehash = secretKey + diamondCount + coinsCount;
				
				for(int i=0; i<bestScores.Count;i++)
					prehash += bestScores[i].score;
				
				string hashed = mdFiveSum(prehash);
				
				//				Debug.Log("PREHASH " + prehash);
				//				Debug.Log("HASHED STRING IS " + hashed);
				//				Debug.Log("LOADED STRING IS " + loadedString);
				//
				//				Debug.Log("GAME PROGRESS " + PlayerPrefs.GetString("gameProgress"));
				
				//Debug.Log("<color=blue>Local DB - PP Loaded</color>");
				
				//PROVERA DA LI JE PETLJANO SA PLAYER PREFS
				//AKO JESTE, RESETUJ IGRACA
				if (hashed != loadedString && false) {
					Debug.LogError("CHEATEEEEEEEEEEEEER! :)");
					//PlayerReset();
				}
				
				//AKO NIJE, UPISI SVE GDE TREBA
				else {
					//Debug.Log("Loading...");
					//App.player.diamondCount = diamondCount;
					App.player.coinsCount = coinsCount;
					App.player.uid = uid;

					//App.sound.sound = sound;
					//App.sound.music = music;
					//App.notif.notifications = notifications;

					//DESERIJALIZACIJA SHOPA
					//App.shop.itemDictionary = Json.Deserialize(shop) as Dictionary<string, object>;
					//if(shop == "null") App.shop.itemDictionary = new Dictionary<string, object>();
					
					//Debug.Log("OK");
					
					//App.player.lives = lives;
					//InitializationHelper.stage.lastLevel = lastLevel;
					//InitializationHelper.stage.lastCheckpoint = lastCheckPoint;
					//Debug.Log("Inventory in localdb is " + inventory);
					//Debug.Log("OK");
					Debug.Log(inventory);
					
					//DESERIJALIZACIJA INVENTARA
					
					if(inventory == "null" || inventory == "") {
						//Debug.Log("OK");
						
						//App.inv.inventory = new Dictionary<string, object>();
						//App.inv.bag = new Dictionary<string, object>();
					}
					//else App.inv.inventory = Json.Deserialize(inventory) as Dictionary<string, object>;
					//Debug.Log("OK");
					
					//INICIJALIZUJE SE TORBA
					//App.inv.InitializeBag();			
					//Debug.Log("OK");
					
					//Debug.Log("Inventory in inventory script is " + InitializationHelper.inventory.inventory.ToString());
					
					//POSTAVLJAJU SE BEST SCOROVI
					App.score.SetBestScores(bestScores);
					App.score.SetTotalScores(totalScores);
					//Debug.Log("<color=blue>Local DB - Info sent to Game Data, calling Merge</color>");
					//Debug.Log("GAME PROGRESS " + PlayerPrefs.GetString("gameProgress"));

					LoadFromScripts();
					
					//NAKON USPESNOG UCITAVANJA, POZIVA SE MERGE FUNKCIJA
				}
				localDBReady = true;

			}
			
			//AKO JE PRVI PUT U IGRI, KREIRAJ NALOG NA SERVERU I AZURIRAJ PLAYER PREFS
			else {
				//Debug.Log("<color=blue>Local DB - First time in game, initialize PP, call PlayerSave and Merge</color>");
				
				//PlayerPrefs.SetString("gameProgress", "None");
				//SVE SE POSTAVLJA NA INICIJALNE VREDNOSTI
				//string prehash2 = secretKey + App.player.diamondCount + App.player.coinsCount;
				//string shop = Json.Serialize(App.shop.itemDictionary);						
				
//				for(int i=0; i<bestScores.Count;i++)
//					prehash2 += bestScores[i].score;
//				
				//string hashed2 = mdFiveSum(prehash2);
				
				//PlayerPrefs.SetString("gameProgress", hashed2);
				PlayerPrefs.SetString("deviceModel", deviceModel);
				PlayerPrefs.SetString("deviceType", deviceType);
				PlayerPrefs.SetString("deviceId", deviceId);
				PlayerPrefs.SetString("deviceName", deviceName);
				PlayerPrefs.SetString("diamonds", Crypting.EncryptInt(0).ToString());
				PlayerPrefs.SetString("coins", Crypting.EncryptInt(0).ToString());
				PlayerPrefs.SetString("timeZone", timeZone);
				//PlayerPrefs.SetString("lives", Crypting.EncryptInt(App.player.maxLives));				
				PlayerPrefs.SetString("lastLevel", Crypting.EncryptInt(0));
				PlayerPrefs.SetString("lastCheckpoint", Crypting.EncryptInt(0));

				PlayerPrefs.SetString("sound", "on");
				//App.sound.sound = "on";
				PlayerPrefs.SetString("music", "on");
				//App.sound.music = "on";
				//PlayerPrefs.SetString("notifications", "on");


				
				//INVENTAR I TRANSAKCIJE SE POSTAVLJAJU NA INICIJALNE VREDNOSTI
				//App.inv.inventory = new Dictionary<string, object>();
				PlayerPrefs.SetString("Inventory", "");
				//App.inv.bag = new Dictionary<string, object>();
				//App.shop.itemDictionary = new Dictionary<string, object>();
				PlayerPrefs.SetString("shop", "");
				//PlayerPrefs.SetString("lives", player.maxLives.ToString());		
				
				//App.player.diamondCount = Crypting.EncryptInt(0);		
				App.player.coinsCount = Crypting.EncryptInt(0);
				//App.shop.UpdateCoinsLabels("0");
				//InitializationHelper.stage.lastLevel = Crypting.EncryptInt(0);
				//InitializationHelper.stage.lastCheckpoint = Crypting.EncryptInt(0);
				ResetFromScripts();
				
				for(int i = 0; i<bestScores.Count; i++){	
					PlayerPrefs.SetString(bestScores[i].scoreName, bestScores[i].score);
				}
				for(int i = 0; i<totalScores.Count; i++){	
					PlayerPrefs.SetString(totalScores[i].scoreName, totalScores[i].score);
				}
				
				//Debug.Log("GAME PROGRESS " + PlayerPrefs.GetString("gameProgress"));
				
				//PAMTI SE SVE U PP I ZOVE SE MERGE
				
				PlayerSave();
				SaveFromScripts();
				//Debug.Log("GAME PROGRESS " + PlayerPrefs.GetString("gameProgress"));
				
				
			}

			//App.sound.CheckButtons ();
			//PrintPP();
			//gameData.localDBReady = true;
			//Debug.Log ("<color=blue>Local DB is ready</color>");
			//App.server.Merge();		
		}
		
		public void PlayerSave () {
//			Debug.Log("<color=blue>Local DB - Player Save, setting PP</color>");	
			
			//hashovanje bitnih podataka koje korisnik ne sme da menja u PlayerPrefs. Ako ih promeni, resetuje mu se game.
//			string prehash = secretKey + App.player.diamondCount + App.player.coinsCount;
//			
//			for(int i=0; i<bestScores.Count;i++)
//				prehash += bestScores[i].score;
//			
//			string hashed = mdFiveSum(prehash);
			//Debug.Log("GAME PROGRESS " + PlayerPrefs.GetString("gameProgress"));
			
			
			//POSTAVLJAJU SE STRINGOVI U PP
			//PlayerPrefs.SetString("gameProgress", hashed);
			PlayerPrefs.SetString("gameProgress", "igrao");
			for (int i=0; i<bestScores.Count; i++)
				PlayerPrefs.SetString(bestScores[i].scoreName, bestScores[i].score);
			for (int i=0; i<totalScores.Count; i++)
				PlayerPrefs.SetString(totalScores[i].scoreName, totalScores[i].score);
			
		//	Debug.Log("GAME PROGRESS " + PlayerPrefs.GetString("gameProgress"));
			
			//string shop = Json.Serialize(App.shop.itemDictionary);
		//	Debug.Log("SHOP STRING FROM PLAYER SAVE IS " + shop);		
			
			//PlayerPrefs.SetString("shop", shop);
			//PlayerPrefs.SetString("gcid", App.gc.gcId);
			#if UNITY_IOS
			//PlayerPrefs.SetString("apnToken", App.device.apnToken);
			#endif
			//PlayerPrefs.SetString("username", App.player.username);
			//PlayerPrefs.SetString("password", App.player.password);
			
			PlayerPrefs.SetString("uid", App.player.uid);
			
			PlayerPrefs.SetString("deviceModel", deviceModel);
			PlayerPrefs.SetString("deviceName", deviceName);
			PlayerPrefs.SetString("deviceId", deviceId);			
			PlayerPrefs.SetString("deviceType", deviceType);
			
//			PlayerPrefs.SetString("fbid", App.fb.fbid);
//			PlayerPrefs.SetString("fbName", App.fb.fbUsername);
//			PlayerPrefs.SetString("fbEmail", App.fb.fbEmail);
			//PlayerPrefs.SetString("birthday", App.player.birthday);
			//PlayerPrefs.SetString("gender", App.player.gender);
			
			//PlayerPrefs.SetString("diamonds", App.player.diamondCount);
			PlayerPrefs.SetString("coins", App.player.coinsCount);
			
			PlayerPrefs.SetString("timeSaved", DateTime.Now.ToString());
			PlayerPrefs.SetString("timeZone", timeZone);
			//PlayerPrefs.SetString("lives", App.player.lives);

//			PlayerPrefs.SetString("sound", App.sound.sound);
//			PlayerPrefs.SetString("music", App.sound.music);
			//PlayerPrefs.SetString("notifications", App.notif.notifications);


			//PlayerPrefs.SetString("lastLevel", InitializationHelper.stage.lastLevel);
			//PlayerPrefs.SetString("lastCheckpoint", InitializationHelper.stage.lastCheckpoint);
			
			//SASTAVLJA SE INVENTORY STRING, KOJI SE SASTOJI OD BAG-a I EQUIPPED ITEM-a
			//App.inv.PutTogetherInventory();
			SaveFromScripts();
			//PlayerPrefs.SetString("Inventory", Json.Serialize(App.inv.inventory));
			
			PlayerPrefs.Save();
//			Debug.Log("LAST LEVEL IS " + PlayerPrefs.GetString("lastLevel") + " LAST CHECKPOINT IS " + PlayerPrefs.GetString("lastCheckpoint"));
//			Debug.Log("<color=blue>Local DB - PP saved</color>");
//			Debug.Log("********************PlayerSave********************");
			
			//PrintPP();
		}
		
		public void PlayerReset () {
			Debug.LogError ("<color=red> Reseting player prefs </color>");
			
			//SVE SE INICIJALIZUJE NA NULU
			App.score.InitializeBestScoresToZero();
			App.player.uid = "";
//			App.player.password = "";
//			App.player.username = "";
			//App.gc.gcId = "";
		//	App.device.apnToken = "";
//			App.fb.fbid = "";
//			App.fb.fbUsername = "";
//			App.fb.fbEmail = "";
			//App.player.birthday = "";
			//App.player.gender = "";

//			App.sound.sound = "on";
//			App.sound.music = "on";
			//App.notif.notifications = "on";

			//App.player.diamondCount = Crypting.EncryptInt(0);
			App.player.coinsCount = Crypting.EncryptInt(0);
			//App.shop.itemDictionary.Clear();
			//App.player.lives = Crypting.EncryptInt(App.player.maxLives);
			//InitializationHelper.stage.lastLevel = Crypting.EncryptInt(0);
			//InitializationHelper.stage.lastCheckpoint = Crypting.EncryptInt(0);
			//App.inv.inventory = new Dictionary<string, object>();
			//Debug.Log("GAME PROGRESS " + PlayerPrefs.GetString("gameProgress"));
			ResetFromScripts();
			PlayerSave();
			
		}
		
		public void PrintPP(){
			//FUNKCIJA KOJA STAMPA PP
			Debug.Log("***************PLAYER PREFS**************" + Environment.NewLine);
			
			string uid = PlayerPrefs.GetString("uid");
			string gcid = PlayerPrefs.GetString("gcid");
			string apnToken = PlayerPrefs.GetString("apnToken");
			string username = PlayerPrefs.GetString("username");
			string password = PlayerPrefs.GetString("password");
			string diamondCount = PlayerPrefs.GetString("diamonds");
			string coinsCount = PlayerPrefs.GetString("coins");
			string fbid = PlayerPrefs.GetString("fbid");
			string fbName = PlayerPrefs.GetString("fbName");
			string fbEmail = PlayerPrefs.GetString("fbEmail");
			string birthday = PlayerPrefs.GetString ("birthday");
			string gender = PlayerPrefs.GetString("gender");
			string loadedString = PlayerPrefs.GetString ("gameProgress");
			//string shop = Json.Serialize(App.shop.itemDictionary);
			string lives = PlayerPrefs.GetString("lives");
			string lastLevel = PlayerPrefs.GetString("lastLevel");
			string lastCheck = PlayerPrefs.GetString("lastCheckpoint");
			string inventory = PlayerPrefs.GetString("Inventory");
			
			for(int i=0; i<bestScores.Count; i++){
				Debug.Log("best score " + bestScores[i].scoreName + " " + PlayerPrefs.GetString(bestScores[i].scoreName));
			}
			for(int i=0; i<totalScores.Count; i++){
				Debug.Log("total score " + totalScores[i].scoreName + " " + PlayerPrefs.GetString(totalScores[i].scoreName));
			}
			
		}
		
		
		
//		Dictionary<string, int> ParseDict (string input)
//		{
//			if(input == "")
//				return new Dictionary<string, int >(0);
			//Dictionary<string, object> dict = Json.Deserialize(input) as Dictionary<string, object>;
			//Dictionary<string, int> retDict = new Dictionary<string, int>(dict.Count);
//			foreach(KeyValuePair<string, object> kvp in dict)
//			{
//				string k = kvp.Key;
//				string num = kvp.Value.ToString();
//				int v = Int32.Parse(num);
//				//retDict.Add(k, v);
//			}
			//return retDict;
	//	}
		
		

		public static string mdFiveSum(string strToEncrypt){
			UTF8Encoding encoding = new System.Text.UTF8Encoding();
			byte[] bytes = encoding.GetBytes(strToEncrypt);
			
			// encrypt bytes
			MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] hashBytes = md5.ComputeHash(bytes);
			
			// Convert the encrypted bytes back to a string (base 16)
			string hashString = "";
			for (long i = 0; i < hashBytes.Length; i++) {
				hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, "0"[0]);
			}
			
			return hashString.PadLeft(32, "0"[0]);
		}
	}
}