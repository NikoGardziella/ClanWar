public static class GameConstants 
{
	public static string CATALOG_ITEMS = "Items";
	public static string ITEM_CARDS = "Card";

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
