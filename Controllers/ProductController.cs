using CoffeeMugApp.Models;
using CoffeeMugApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CoffeeMugApp.Helper;
//using System.Web.Http;

namespace CoffeeMugApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<Product>> Get() =>
            _productService.Get();

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public ActionResult<Product> Get(string id)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            if (Utils.ValidateName(product.Name))
            {

            }
            
            if (Utils.ValidatePrize(product.Prize) && Utils.ValidateName(product.Name))
            {
                _productService.Create(product);
                return CreatedAtRoute("GetProduct", new { id = product.Id.ToString() }, product);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Product productIn)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            if (product.Name != productIn.Name)
            {
                // return error 409 due to inconsistancy between input and database objects, we want to keep this object's name 
                return Conflict();
            }

            if (Utils.ValidatePrize(productIn.Prize))
            {
                product.Prize = productIn.Prize;
                _productService.Update(id, product);
                return NoContent();
            }
            else
            {
                // bad request due to invalid prize
                return BadRequest();
            }
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            _productService.Remove(product.Id);

            return Ok("Deleted");
        }
    }
}