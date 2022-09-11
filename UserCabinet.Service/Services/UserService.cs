using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserCabinet.Data.IRepositories;
using UserCabinet.Domain.Configuration;
using UserCabinet.Domain.Entities.Users;
using UserCabinet.Service.DTOs.Users;
using UserCabinet.Service.Exceptions.Users;
using UserCabinet.Service.Helpers;
using UserCabinet.Service.Interfaces;

namespace UserCabinet.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAttechmentService _attechmentService;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IAttechmentService attechmentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _attechmentService = attechmentService;
        }

        public async Task<User> AddAsync(UserForCreationDTO user)
        {
            var existUser = await _unitOfWork.Users.GetAsync(u => u.Username.Equals(user.Username));
            if (existUser is not null)
                throw new UserException(404, "User is already exis");

            var attachment = await _attechmentService.UploadAsync(user.File.OpenReadStream(), user.File.FileName);

            var mappedUser = _mapper.Map<User>(user);

            mappedUser.Password = user.Password.Encode();
            mappedUser.AttechmentId = attachment.Id;

            var createdUser = await _unitOfWork.Users.AddAsync(mappedUser);
            await _unitOfWork.SaveChangesAsync();

            return createdUser;
        }

        public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var existUser = await _unitOfWork.Users.GetAsync(expression);

            if (existUser is null)
                throw new UserException(404, "User not found");

            await _unitOfWork.Users.DeleteAsync(expression);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<User>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null)
        {
            var pagedList = _unitOfWork.Users.GetAll(expression, "Attechment", isTracking: false).ToPagedList(@params);

            return await pagedList.ToListAsync();
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> expression)
        {
            var existUser = await _unitOfWork.Users.GetAsync(expression, "Attechment");

            if (existUser is null)
                throw new UserException(404, "User not found");

            return existUser;
        }

        public async Task<User> LogIn(string email, string password)
        {
            var existUser = await _unitOfWork.Users.GetAsync(u => u.Email.Equals(email)
            && u.Password.Equals(password.Encode()));

            return existUser;
        }

        public async Task<User> UpdateAsync(long id, UserForCreationDTO dto)
        {
            var existUser = await _unitOfWork.Users.GetAsync(u => u.Username.Equals(dto.Username));

            if (existUser is not null)
                throw new UserException(404, "User is already exist");

            var userForUpdate = await _unitOfWork.Users.GetAsync(u => u.Id == id);

            if (userForUpdate is null)
                throw new UserException(404, "User not found");

            userForUpdate.Firstname = dto.Firstname;
            userForUpdate.Lastname = dto.Lastname;
            userForUpdate.Password = dto.Password.Encode();
            userForUpdate.Email = dto.Email;
            userForUpdate.Username = dto.Username;
            userForUpdate.UpdatedAt = DateTime.UtcNow;

            var updatedUser = await _unitOfWork.Users.UpdateAsync(userForUpdate);
            await _unitOfWork.SaveChangesAsync();

            return updatedUser;
        }
    }
}