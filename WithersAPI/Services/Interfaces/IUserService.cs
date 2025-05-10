using WithersAPI.DTO;
using WithersAPI.Models;

namespace WithersAPI.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserResponse> GetAll();
        UserResponse? GetById(int id);
        UserResponse Create(User user);
        bool Update(int id, User user);
        bool PartialUpdate(int id, UserUpdateDto dto);
        bool Delete(int id);
    }
}
