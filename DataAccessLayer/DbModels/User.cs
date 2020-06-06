
namespace DataAccessLayer.DbModels
{
    public  class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int UserId { get; set; }
    }
}
