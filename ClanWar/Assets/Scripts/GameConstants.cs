public static class GameConstants 
{
	public static float MELEE_DISTANCE = 0.5f;
	public static float RANGE_DISTANCE = 0.9f;

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
