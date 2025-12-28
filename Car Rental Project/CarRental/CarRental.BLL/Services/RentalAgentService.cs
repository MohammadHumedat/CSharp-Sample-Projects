using CarRental.CarRental.BLL.DTOs;
using CarRental.CarRental.BLL.Interfaces;
using CarRental.CarRental.DAL.Entities;
using CarRental.CarRental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.CarRental.BLL.Services
{
    public class RentalAgentService : IRentalAgentService
    {
        private readonly IUserRepository _userRepository;

        public RentalAgentService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddRentalAgent(RentalAgentDto agentDto)
        {
            var agent = new RentalAgent
            {
                UserName = agentDto.UserName,
                Password = agentDto.Password, 
                IsActive = true
            };

            _userRepository.Add(agent);
        }

        public void UpdateRentalAgent(RentalAgentDto agentDto)
        {
            var user = _userRepository.GetById(agentDto.UserId);
            if (user == null || !(user is RentalAgent))
                throw new Exception("Rental agent not found");

            user.UserName = agentDto.UserName;
            if (!string.IsNullOrEmpty(agentDto.Password))
                user.Password = agentDto.Password; 

            _userRepository.Update(user);
        }

        public void ActivateDeactivateAgent(int agentId, bool status)
        {
            var user = _userRepository.GetById(agentId);
            if (user == null || !(user is RentalAgent))
                throw new Exception("Rental agent not found");

            user.IsActive = status;
            _userRepository.Update(user);
        }

       
        public IEnumerable<RentalAgentDto> GetAllAgents()
        {
            return _userRepository.GetAllAgents().Select(u => new RentalAgentDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                IsActive = u.IsActive
            });
        }
    }
}