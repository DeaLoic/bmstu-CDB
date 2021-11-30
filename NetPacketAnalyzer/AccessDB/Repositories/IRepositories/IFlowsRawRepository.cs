using System;
using System.Collections.Generic;
using System.Text;
using DataObjects.Models;

namespace AccessDB.Repositories.IRepositories
{
    public interface IFlowsRawRepository
    {
        public void DeleteForTime(int minutes);
        public IEnumerable<Flow> FindForTime(int minutes);
        public IEnumerable<Flow> FindForTimePeriod(int minutesStart, int minutesEnd);
        public IEnumerable<Flow> FindAll();
    }
}
