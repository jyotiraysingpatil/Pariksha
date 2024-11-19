namespace Pariksha.Entities
{
    public class Admins
    {
        public int admin_id {  get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; } 
        public string username { get; set; }
        public string password { get; set; }
        public bool is_active {  get; set; }

    }
}
