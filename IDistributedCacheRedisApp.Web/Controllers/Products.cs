using IDistributedCacheRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace IDistributedCacheRedisApp.Web.Controllers
{
    public class Products : Controller
    {
        private IDistributedCache _distributedCache;
        public Products(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<IActionResult> Index()
        {
            //options
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions();
            cacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddSeconds(60);

            //basics
            //_distributedCache.SetString("name1", "berfim",cacheEntryOptions);
            //await _distributedCache.SetStringAsync("surname", "korkmaz",cacheEntryOptions);


            //complex types
            //Product product = new Product { Id = 1, Name= "Çanta" , Price = 500 };
            //string jsonProduct = JsonConvert.SerializeObject(product);

            //await _distributedCache.SetStringAsync("product:1", jsonProduct, cacheEntryOptions);
            //binary
            //Byte[] byteprod = Encoding.UTF8.GetBytes(jsonProduct);
            //await _distributedCache.SetAsync("product:1", byteprod);




            return View();
        }
        public IActionResult Show()
        {
            //basics
            //string name1 = _distributedCache.GetString("name1");
            //ViewBag.name1 = name1;
            //string surname = _distributedCache.GetString("surname");
            //ViewBag.surname = surname;

            //complex types
            //string product1 = _distributedCache.GetString("product:1")!;
            //Product p = JsonConvert.DeserializeObject<Product>(product1!);
            //ViewBag.product1 = p;


            // binary show
            //Byte[] byteprod = _distributedCache.Get("product:1")!;
            //string jsonpr = Encoding.UTF8.GetString(byteprod);
            //Product p = JsonConvert.DeserializeObject<Product>(jsonpr!);
            //ViewBag.Product = p;





            return View();
        }
        public IActionResult Remove() 
        {
            _distributedCache.Remove("name1");
            return View();
        }






        //Image redis caching
        public IActionResult ImageCache()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/61.jpg");
            byte[] imageByte = System.IO.File.ReadAllBytes(path);

            _distributedCache.Set("resim", imageByte);
            return View();
        }


        public IActionResult ImageUrl()
        {
            byte[] imageByte = _distributedCache.Get("resim");
            return File(imageByte!,"image/jpg");
        }
    }
}
