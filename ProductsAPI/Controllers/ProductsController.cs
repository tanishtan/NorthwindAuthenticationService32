using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindModelClassLibrary;
using ProductsAPI.Infrastructure;
using System.Runtime.InteropServices;

namespace ProductsAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorization("admin")]
    public class ProductsController : Controller
    {
        private readonly IRepository<Product> _repository;
        public ProductsController(IRepository<Product> repository)
        {
            _repository = repository;
        }
        [HttpGet(template: "")]
        public IActionResult GetAllProducts()
        {
            var model = _repository.GetAll();
            return Ok(model);
        }
        [HttpGet(template: "{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var model = _repository.GetById(id);
            if (model is not null)
            {
                return model;
            }
            else
                return NotFound();
        }
    }
}
