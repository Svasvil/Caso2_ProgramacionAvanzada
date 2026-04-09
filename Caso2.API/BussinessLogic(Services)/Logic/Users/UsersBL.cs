using Caso2.API.BussinessLogic_Services_.Interfaces.Users;
using Caso2.API.DataAccess_Repository_.Interfaces.Users;
using Caso2.API.DTos.Users;
using Caso2.API.Models;

namespace Caso2.API.BussinessLogic_Services_.Logic.Users
{
    public class UsersBL : I_UsersBL
    {
        private readonly ICreateUserDA _user;

        public UsersBL(ICreateUserDA user) => _user = user;

        public async Task<List<CreateUserDTO>> GetAllUsers()
        {
            var list = await _user.GetUsuarios();
            return list.Select(user => new CreateUserDTO
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellidos = user.Apellidos
            }).ToList();
        }

        public async Task<CreateUserDTO?> GetUserById(int id)
        {
            var user = await _user.GetUsuario_ID(id);
            return user is null ? null : new CreateUserDTO
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellidos = user.Apellidos
            };
        }

        public async Task<CreateUserDTO> CreateUser(CreateUserDTO dto)
        {
            var userModel = new UserModel
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Apellidos = dto.Apellidos
            };

            await _user.CrearUsuario(userModel);

            return new CreateUserDTO
            {
                Id = userModel.Id,
                Nombre = userModel.Nombre,
                Apellidos = userModel.Apellidos
            };
        }
    }
}