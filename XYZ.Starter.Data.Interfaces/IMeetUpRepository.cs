using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XYZ.Starter.Classes;
using XYZ.Starter.Core.Interfaces.Data;

namespace XYZ.Starter.Data.Interfaces
{
    public interface IMeetUpRepository : IRepositoryBase<MeetUp>
    {
        /// <summary>
        /// Check if a MeetUp exist by looking for it using the id
        /// </summary>
        /// <param name="id">the id of the entity to look for</param>
        /// <returns></returns>
        Task<bool> DoesExistAsync(int id);

        /// <summary>
        /// Delete a meet up record by using its id.
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}
