public static class GameConstants 
{
	public static float MELEE_DISTANCE = 0.5f;
	public static float RANGE_DISTANCE = 0.9f;
	public static float RESOURCE_SPEED = 1.5f;
	public static int RESOURCE_MAX = 9;
	public static int MAX_HAND_SIZE = 4;
	public static string HUD_CANVAS = "HUD - Canvas";

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
