using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XYZ.Starter.Classes;
using XYZ.Starter.Core.Interfaces.Data;
using XYZ.Starter.Data.Interfaces;

namespace XYZ.Starter.Data
{
    public class MeetUpRepository : IMeetUpRepository
    {
        readonly AppDbContext _appDbContext;
        public MeetUpRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public MeetUp Create(MeetUp entity)
        {
            var addEntity = _appDbContext.MeetUps.Add(entity);
            return addEntity.Entity;
        }

        public async Task<MeetUp> CreateAsync(MeetUp entity)
        {
            var addEntity = await _appDbContext.MeetUps.AddAsync(entity);
            return addEntity.Entity;
        }

        public MeetUp Update(MeetUp entity)
        {
            var edEntity = _appDbContext.MeetUps.Update(entity);
            return edEntity.Entity;
        }

        public MeetUp FindById(int id)
        {
            return _appDbContext.MeetUps.Find(id);
        }

        public MeetUp FetchById(int id)
        {
            return _appDbContext.MeetUps.Include(m => m.SeatGrid)
            .ThenInclude(s => s.Seats)
            .FirstOrDefault(m => m.Id == id);
        }

        public async Task<MeetUp> FetchByIdAsync(int id)
        {
            return await _appDbContext.MeetUps.Include(m => m.SeatGrid)
            .ThenInclude(s => s.Seats)
            .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<MeetUp> FindByIdAsync(int id)
        {
            return await _appDbContext.MeetUps.FindAsync(id);
        }

        public IEnumerable<MeetUp> FindByExpression(Expression<Func<MeetUp, bool>> expression)
        {
            return _appDbContext.MeetUps.Where(expression).ToList();
        }

        public async Task<IEnumerable<MeetUp>> FindByExpressionAsync(Expression<Func<MeetUp, bool>> expression)
        {
            return await _appDbContext.MeetUps.Where(expression).ToListAsync();
        }

        public void Delete(MeetUp entity)
        {
            entity = FetchById(entity.Id);
            _appDbContext.MeetUps.Remove(entity);
        }

        public void Delete(int id)
        {
            var entity = FetchById(id);
            _appDbContext.MeetUps.Remove(entity);
        }

        public void SaveChanges()
        {
            _appDbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> DoesExistAsync(int id)
        {
            return await _appDbContext.MeetUps.AnyAsync(m => m.Id == id);
        }


    }
}
