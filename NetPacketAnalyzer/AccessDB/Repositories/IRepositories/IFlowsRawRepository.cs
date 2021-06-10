using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.DTO;

namespace AccessDB.Repositories.IRepositories
{
    public interface IFlowsRawRepository
    {
        public void DeleteForTime(int minutes);
        public IEnumerable<FlowDTO> FindForTime(int minutes);
        public IEnumerable<FlowDTO> FindForTimePeriod(int minutesStart, int minutesEnd);
        public IEnumerable<FlowDTO> FindAll();
    }
}
