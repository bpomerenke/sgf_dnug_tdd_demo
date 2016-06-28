using System.Collections.Generic;
using System.Linq;

namespace TDDDemoApp.DAL
{
    public interface IRepository
    {
        void SaveChanges();
        List<T> FindAll<T>() where T : class;
        void Insert<T>(T itemToInsert) where T : class;
    }

    public class Repository : IRepository
    {
        private readonly TDDDemoAppContext _dbContext;

        public Repository(TDDDemoAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public List<T> FindAll<T>() where T : class
        {
            return _dbContext.Set<T>().ToList();
        }

        public void Insert<T>(T itemToInsert) where T : class
        {
            _dbContext.Set<T>().Add(itemToInsert);
        }
    }
}