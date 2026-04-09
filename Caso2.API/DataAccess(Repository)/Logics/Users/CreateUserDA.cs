using Caso2.API.DataAccess_Repository_.Interfaces.Users;
using Caso2.API.DataBases;
using Caso2.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Caso2.API.DataAccess_Repository_.Logics.Users
{
    public class CreateUserDA : ICreateUserDA
    {

        private readonly ObjContexto _context;


        public CreateUserDA(ObjContexto context) => _context = context;

        public async Task CrearUsuario(UserModel user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserModel>> GetUsuarios() =>
           await _context.User.AsNoTracking().ToListAsync();


        public async Task<UserModel?> GetUsuario_ID(int id) =>
            await _context.User.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
    }
}
