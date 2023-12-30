using costs.Domain.APIModel;
using costs.Domain.Model;
using costs.Domain.Interfaces;
using Microsoft.VisualBasic.FileIO;
using MongoDB.Driver;

namespace costs.Repository
{
    public interface ICategoryRepository
    {
        public IList<Category> GetCategories();
        public void AddCategory(Category category);
        public CategoryModel Map(Category category);
    }
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _collection;

        public CategoryRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _collection = database.GetCollection<Category>("Categories");
        }

        public IList<Category> GetCategories()
        {
            return _collection.Find("{}").ToList();
        }

        public void AddCategory(Category category) 
        {
            _collection.InsertOne(category);
        }


        public CategoryModel Map(Category category)
        {
            return new CategoryModel
            {
                Id = category.Id.ToString(),
                Name = category.Name,
            };
        }
    }
}