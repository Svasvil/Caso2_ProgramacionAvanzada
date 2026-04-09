using Caso2.API.DTos.Users;

namespace Caso2.API.BussinessLogic_Services_.Interfaces.Users
{
    public interface I_UsersBL
    {
        Task<List<CreateUserDTO>> GetAllUsers();
        Task<CreateUserDTO?> GetUserById(int id);
        Task<CreateUserDTO> CreateUser(CreateUserDTO user);
    }
}
