using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	// The storage of the game, which contains all the persistent variables in the game. 
	public class Storage
	{
		// The game handler of the game. 
		private GameHandler gHandler;
		// Lark himself.
		private GameObject lark;
		// The Mask used throughout the game. 
		private GameObject mask;
		// Lark's script. 
		private Lark larkScript;
		// Pause screen script. 
		private PauseScreen pScreen;
		// Dialogue handler of the game. 
		private DialogueHandler dHandler;
		// Character handler of the game. 
		private CharacterHandler cHandler;
		// The respawn point of Lark. 
		private float respawnX, respawnY; 
		// The point at the main quest the player is at. 
		private int mainQuest = 0;
		// The integers that contains the cell position of the game. Tells the game where the camera should be and 
		// the location of all sprites in the game. Each goes from 0 to 7 (0 px to 1120px).
		private int cellX = 0, cellY = 0;
		// How much gold Lark has in his possession. 
		private int gold = 9999;
		// How many monsters Lark has in his possession. 
		private int monsters = 0;
		// Stages of dialogue (e.g. first conversation would be 0, subsequent conversations would be 
		// incremented integers. 
		private int[] stages = new int[100];
		// The spawn point of Lark. This is used to move Lark accordingly.
		private GameObject spawnPoint;
		// The list of items the player has in his inventory. 
		private List<Item> items = new List<Item>();
		// The list of quest items the player has in his inventory. 
		private List<QuestItem> questitems = new List<QuestItem>();
		// The list of portal keys that the player has in his inventory. 
		private List<Portal_Key> portalKeys = new List<Portal_Key>();


		public Storage (GameHandler gHandler)
		{
			this.gHandler = gHandler;
		}

		public void newGame() {
			// Gets the spawn point of Lark, and acquires Lark himself.
			lark = GameObject.Find("Player");
			larkScript = (Lark) lark.GetComponent (typeof(Lark));
			spawnPoint = GameObject.Find ("Story Spawn 1");
			respawnX = spawnPoint.transform.position.x;
			respawnY = spawnPoint.transform.position.y;
			// Gets the pause screen script. 
			pScreen = (PauseScreen) GameObject.Find("Pause Screen").GetComponent(typeof(PauseScreen));
			// Gets the mask that exists in the game. 
			mask = GameObject.Find("Mask");
			// Gets the dialogue handler. 
			dHandler = (DialogueHandler) GameObject.Find("Textbox").GetComponent(typeof(DialogueHandler));
			// Gets the character handler. 
			cHandler = (CharacterHandler) GameObject.Find("Characters").GetComponent(typeof(CharacterHandler));
			// If a new game, sets all stages to zero initially. 
			for (int i = 0; i < 50; i++)
				stages [i] = 0;
			// DEBUG PURPOSES. 
			items.Add(new Herb());
		}

		// Sets the cells of the game. 
		public void setCells(int x, int y) {
			cellX = x;
			cellY = y;
		}

		// Increments main quest to signify an advancement in the storyline. 
		public void advMainQuest() {
			mainQuest++;
		}

		// Sets the stage of a character. 
		public void setStage(int character, int stage) {
			stages [character] = stage;
		}

		// Sets the gold of Lark (increase or decrease).
		public void setGold(int gold) {
			this.gold = gold;
		}

		// Adds an item to the item list. 
		public void addItem(Item item) {
			items.Add(item);
		}

		// Adds an item to the quest item list. 
		public void addQuestItem(QuestItem item) {
			questitems.Add(item);
		}

		// Adds a portal key to the portal key list. 
		public void addPortalKey(Portal_Key pKey) {
			portalKeys.Add (pKey);
		}

		// Removes an item from the item list. 
		public void removeItem(Item item) {
			items.Remove(item);
		}
			
		// Removes an item from the quest item list. 
		public void removeItem(QuestItem item) {
			questitems.Remove(item);
		}

		public GameObject getSpawnPoint() {
			return spawnPoint;
		}

		public GameObject getLark() {
			return lark;
		}

		public GameObject getMask() {
			return mask;
		}

		public Lark getLarkScript() {
			return larkScript;
		}

		public PauseScreen getPauseScreen() {
			return pScreen;
		}

		public DialogueHandler getDialogueHandler() {
			return dHandler;
		}

		public CharacterHandler getCharacterHandler() {
			return cHandler;
		}

		public int getMainQuest() {
			return mainQuest;
		}

		public int getStage(int character) {
			return stages [character];
		}

		public int getMonsters() {
			return monsters;
		}

		// Gets the cells of the game. 
		public int getCellX() {
			return cellX;
		}

		public int getCellY() {
			return cellY;
		}

		public int getGold() {
			return gold;
		}

		public List<Item> getItems() {
			return items;
		}

		public List<QuestItem> getQuestItems() {
			return questitems;
		}

		public List<Portal_Key> getPortalKeys() {
			return portalKeys;
		}

		public Item getItem(int i) {
			return items[i];
		}

		public QuestItem getQuestItem(int i) {
			return questitems [i];
		}

		public Portal_Key getPortalKey(int i) {
			return portalKeys [i];
		}
	}
}
