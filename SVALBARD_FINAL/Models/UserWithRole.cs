namespace WebApplication1.Models
{
	public class UserWithRole
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Ets { get; set; }
		public string Dir { get; set; }
		public string Service { get; set; }
		public string Role { get; set; }
		
		public static bool AddUserWithRole(string user) {
			return true;
		}

		public static int HasASpecificRole(string user)
		{
			
			
			return 0;
		}
		
	}
	
	
}