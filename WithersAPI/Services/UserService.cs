using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WithersAPI.Data;
using WithersAPI.DTO;
using WithersAPI.Models;
using WithersAPI.Services.Interfaces;

namespace WithersAPI.Services
{
    public class UserService : IUserService
    {
        private readonly WithersContext _context;
        private readonly IMapper _mapper;

        public UserService(WithersContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<UserResponse> GetAll()
        {
            var users = _context.Users.Include(u => u.Characters).ToList();
            return _mapper.Map<IEnumerable<UserResponse>>(users);
        }

        public UserResponse? GetById(int id)
        {
            var user = _context.Users.Include(u => u.Characters).FirstOrDefault(u => u.Id == id);
            return user == null ? null : _mapper.Map<UserResponse>(user);
        }

        public UserResponse Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return _mapper.Map<UserResponse>(user);
        }

        public bool Update(int id, User user)
        {
            if (id != user.Id) return false;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public bool PartialUpdate(int id, UserUpdateDto dto)
        {
            var user = _context.Users.Find(id);
            if (user == null) return false;

            _mapper.Map(dto, user);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }
    }
}
