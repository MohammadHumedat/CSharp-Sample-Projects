using CarRental.CarRental.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.BLL.Interfaces
{
    public interface IRentalAgentService
    {
        void AddRentalAgent(RentalAgentDto agentDto);
        void UpdateRentalAgent(RentalAgentDto agentDto);
        void ActivateDeactivateAgent(int agentId, bool status);
        IEnumerable<RentalAgentDto> GetAllAgents(); 
    }
}
