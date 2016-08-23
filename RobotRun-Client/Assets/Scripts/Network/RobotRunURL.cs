public class RobotRunURL {

	#if UNITY_EDITOR && NICOL_DEVEL
	public static string API_BASE_PATH = "http://localhost/RobotRun-Server/www/api";
	#else
	public static string API_BASE_PATH = "http://devel.robotrun.koocell.com/api";
	#endif

	public static string API_USER_CREATE = API_BASE_PATH + "/user/create";

	public static string API_USER_LOGIN = API_BASE_PATH + "/user/login";

	public static string API_USER_UPDATE_SELECTED_CHARACTER = API_BASE_PATH + "/user/update-selected-character";

	public static string API_USER_BUY_CLOTH = API_BASE_PATH + "/user/buy-cloth";

	public static string API_USER_BUY_HEAD_ITEM = API_BASE_PATH + "/user/buy-head-item";

	public static string API_USER_BUY_EFFECT = API_BASE_PATH + "/user/buy-effect";

	public static string API_USER_UPDATE_EQUIPED_HEAD_ITEM = API_BASE_PATH + "/user/update-equiped-head-item";

	public static string API_USER_UPDATE_EQUIPED_CLOTH = API_BASE_PATH + "/user/update-equiped-cloth";

	public static string API_USER_UPDATE_EQUIPED_PET = API_BASE_PATH + "/user/update-equiped-effect";

	public static string API_USER_DRAW_PET = API_BASE_PATH + "/user/draw-pet";

	public static string API_USER_SELL_PET = API_BASE_PATH + "/user/sell-pet";

	public static string API_USER_UNLOCK_CHARACTER = API_BASE_PATH +"/user/unlock-character";

}