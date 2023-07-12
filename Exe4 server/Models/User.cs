namespace Exe_1.Models
{
    public class User
    {
        DBservices dbs = new DBservices();

        string firstName;
        string familyName;
        string email;
        string password;
        bool isActive;
        bool isAdmin;



        public string FirstName { get => firstName; set => firstName = value; }
        public string FamilyName { get => familyName; set => familyName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }

        static List<User> UsersList = new List<User>();


        public bool Insert()
        {
            int num= dbs.InsertUser(this);
            if (num != 0)
            {
                UsersList.Add(this);
                return true;
            }
            else return false;
        }

        public bool Update(User user)
        {
            int num=dbs.UpdateUser(user);
            if (num != 0)
            {
                return true;
            }
            else return false;

        }

        public List<User> Read()
        {
            return dbs.ReadUsers();
        }

        public bool UpdateStatus(string email)
        {
            int num = dbs.UpdateStatus(email);
            if (num >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }




    }
}
