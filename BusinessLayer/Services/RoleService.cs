using BusinessLayer.DTO;
using DataAccessLayer.DbModels;
using DataAccessLayer.DbRepository;

namespace BusinessLayer.Services
{
    public class RoleService
    {
        private IRoleRepository _repository;

        public RoleService(string connectionString)
        {
            _repository = new RoleRepository(connectionString);
        }
        public RoleDto GetRole(int roleId)
        {
            var data = _repository.GetRole(roleId);
            return RoleToRoleDtoConvert(data);
        }

        public void AddRole(RoleDto role)
        {
            _repository.AddRole(RoleDtoToRoleConvert(role));
        }

        public void DeleteRole(int id)
        {
            _repository.DeleteRole(id);
        }

        public void UpdateRole(RoleDto role)
        {
            _repository.UpdateRole(RoleDtoToRoleConvert(role));
        }

        private Role RoleDtoToRoleConvert(RoleDto role)
        {
            return new Role()
            {
                Description = role.Description,
                UserId = role.UserId,
                RoleId = role.RoleId
            };
        }

        private RoleDto RoleToRoleDtoConvert(Role role)
        {
            return new RoleDto()
            {
                Description = role.Description,
                RoleId = role.RoleId,
                UserId = role.UserId
            };
        }
    }
}
