
using DataAccessLayer.DbModels;

namespace DataAccessLayer.DbRepository
{
    public interface IRoleRepository
    {
        Role GetRole(int roleId);
        void AddRole(Role data);
        void DeleteRole(int id);
        void UpdateRole(Role data);
    }
}
