using DotnetAPI.Models;

namespace DotnetAPI.Data
{
    public class UserRepository : IUserRepository
    {
        DataContextEF _entityFramework;
        // IMapper _mapper;

        public UserRepository(IConfiguration config)
        {
            // Console.WriteLine(config.GetConnectionString("DefaultConnection"));
            _entityFramework = new DataContextEF(config);
        }

        public bool SaveChanges()
        {
            return _entityFramework.SaveChanges() > 0;
        }

        // public bool AddEntity<T>(T entiryToAdd)
        public void AddEntity<T>(T entiryToAdd)
        {
            if(entiryToAdd != null)
            {
                _entityFramework.Add(entiryToAdd);
                // return true;
            }
            // return false;
        }

         // public bool AddEntity<T>(T entiryToAdd)
        public void RemoveEntity<T>(T entiryToAdd)
        {
            if(entiryToAdd != null)
            {
                _entityFramework.Remove(entiryToAdd);
                // return true;
            }
            // return false;
        }

        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = _entityFramework.Users.ToList<User>();
            return users;
        }

        public User GetSingleUser(int userId)
        {
            User? user = _entityFramework.Users
                .Where(u => u.UserId == userId)
                .FirstOrDefault<User>();

            if (user != null)
            {
                return user;
            }
            
            throw new Exception("Failed to Get User");
        }

        public UserSalary GetSingleUserSalary(int userId)
        {
            UserSalary? userSalary = _entityFramework.UserSalary
                .Where(u => u.UserId == userId)
                .FirstOrDefault<UserSalary>();

            if (userSalary != null)
            {
                return userSalary;
            }
            
            throw new Exception("Failed to Get User");
        }

        public UserJobInfo GetSingleUserJobInfo(int userId)
        {
            UserJobInfo? userJobInfo = _entityFramework.UserJobInfo
                .Where(u => u.UserId == userId)
                .FirstOrDefault<UserJobInfo>();

            if (userJobInfo != null)
            {
                return userJobInfo;
            }
            
            throw new Exception("Failed to Get User");
        }
        
    }
}





