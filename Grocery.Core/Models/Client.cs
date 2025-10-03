
namespace Grocery.Core.Models
{
    public partial class Client : Model
    {
        // Maak enum Role met als waarden None en Admin.
        public enum Role
        {
            None,
            Admin
        }
        public Role UserRole { get; set; } = Role.None;
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Client(int id, string name, string emailAddress, string password) : base(id, name)
        {
            EmailAddress=emailAddress;
            Password=password;
        }
    }
}
