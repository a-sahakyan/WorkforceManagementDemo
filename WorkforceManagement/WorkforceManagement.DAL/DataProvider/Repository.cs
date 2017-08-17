using System;
using System.Collections.Generic;
using WorkforceManagement.DAL.Concrete;
using WorkforceManagement.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WorkforceManagement.DAL.DataProvider
{
    public class Repository<TModel> : IRepository<TModel> where TModel : class
    {
        EFDbContext _context;

        public Repository(EFDbContext context)
        {
            _context = context;
        }

        public TModel GetById => throw new NotImplementedException();

        public IEnumerable<TModel> GetAll()
        {
            return _context.Set<TModel>();
        }

        public void Delete(TModel data)
        {
            throw new NotImplementedException();
        }

        public void Insert(TModel data)
        {
            _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _context.SaveChanges();
        }

        public void Update(Skill updated, Skill original)
        {
            Skill skillUpdate = _context.Skills.Where(w => w.SkillId == original.SkillId).FirstOrDefault();
            if (skillUpdate != null)
            {
                skillUpdate.SkillName = updated.SkillName;
                skillUpdate.SkillKnowledge = updated.SkillKnowledge;
                _context.SaveChanges();
            }
        }
    }
}
