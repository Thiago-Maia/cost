using costs.Domain.Model;
using costs.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace costs_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepository;

        private readonly ILogger<CategoryController> _logger;

        public CategoryController(CategoryRepository categoryRepository, ILogger<CategoryController> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public IActionResult Get()
        {
            var categories = _categoryRepository.GetCategories().Select(x => _categoryRepository.Map(x)).ToList();
            
            return Ok(categories);
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public void Post([FromBody] Category categorie)
        {
            _categoryRepository.AddCategory(categorie);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}