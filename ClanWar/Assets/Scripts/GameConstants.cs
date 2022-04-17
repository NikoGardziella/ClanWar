public static class GameConstants 
{
	public static string CATALOG_ITEMS = "Items";
	public static string ITEM_CARDS = "Card";
	public static int ITEM_COST = 1;
	public static int ITEM_ICON = 3;
	public static int ITEM_PREFAB = 5;
	public static int ITEM_COUNT = 7;
	public static int ITEM_IN_DECK = 9;

	public static float MELEE_DISTANCE = 0.5f;
	public static float RANGE_DISTANCE = 0.9f;
	public static float RESOURCE_SPEED = 1.5f;
	public static int RESOURCE_MAX = 9;
	public static int MAX_HAND_SIZE = 4;
	public static string HUD_CANVAS = "HUD - Canvas";
	public static string PLAYER_TAG = "Player";
	public static int LOGIN_SCENE = 0;
	public static int MAIN_SCENE = 1;
	public static int GAME_SCENE = 2;
	public static int LOADING_SCENE = 3;

	public static string GEM_CODE = "PG";
	public static string COIN_CODE = "PC";

	public static string DATA_EXP = "Exp";
	public static string DATA_MAX_EXP = "Max Exp";
	public static string DATA_LEVEL = "Level";
	public static string DATA_DECK = "Deck";

	public static string STAT_TROPHIES = "Trophies";

	public static string STORE_CARDS = "BasicCardStore";
	public static string STORE_CHEST = "BasicChestStore";

	//public static int STORE_CARD_ID = 0;
	//public static int STORE_CHEST_ID = 1;
	//public static int STORE_BUNDLE_ID = 2;

	public static int MENU_SHOP = 0;
	public static int MENU_DECK = 1;
	public static int MENU_BATTLE = 2;
	public static int MENU_CLAN = 3;
	public static int MENU_RANKED = 4;

	public enum OBJECT_TYPE
	{
		GROUND,
		FLYING
	}
	public enum OBJECT_ATTACKABLE
	{
		GROUND,
		FLYING,
		BOTH
	}

	public enum UNIT_RANGE
	{
		MELEE,
		RANGE
	}
}
