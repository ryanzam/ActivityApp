namespace ActivityApp.Helpers
{
	public static class CheckDateTimeHelpers
	{
		public static bool CheckIfValid(DateTime d)
		{
			return (d > DateTime.Now) ? true : false;
		}
	}
}
