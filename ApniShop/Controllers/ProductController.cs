
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ApniShop.Controllers
{
    public class ProductController : Controller
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "7udJ5fY8Fx1wDM4q5bpzMYmqLgDKTLsZZ1Q9UlPY",
            BasePath = "https://apnishop-6fbca-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;
        // GET: Product
        public ActionResult Index()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Products");
            //System.Diagnostics.Debug.WriteLine(response.Body);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var products = new List<Models.Product>();
            foreach (var product in data)
            {
                products.Add(JsonConvert.DeserializeObject<Models.Product>(((JProperty)product).Value.ToString()));
            }
            return View(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.Product product)
        {
            try
            {
                AddProductToFirebase(product);
                ModelState.AddModelError(string.Empty, "Added Successfully");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
            return View();
        }

        private void AddProductToFirebase(Models.Product product)
        {
            //throw new NotImplementedException();
            client = new FireSharp.FirebaseClient(config);
            var data = product;
            PushResponse response = client.Push("Products/", data);
            data.ID = response.Result.name;
            SetResponse setResponse = client.Set("Products/" + data.ID, data);

        }

        [HttpGet]
         public ActionResult Detail(string id)
        {

            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Products/" + id);
            //System.Diagnostics.Debug.WriteLine(response.Body);
            Models.Product data = JsonConvert.DeserializeObject<Models.Product>(response.Body);
            return View(data);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {

            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Products/" + id);
            //System.Diagnostics.Debug.WriteLine(response.Body);
            Models.Product data = JsonConvert.DeserializeObject<Models.Product>(response.Body);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Models.Product product)
        {

            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Set("Products/" + product.ID, product);
            //System.Diagnostics.Debug.WriteLine(response.Body);
            Models.Product data = JsonConvert.DeserializeObject<Models.Product>(response.Body);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {

            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Delete("Products/" + id);
            //System.Diagnostics.Debug.WriteLine(response.Body);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Demand(string id)
        {

            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Products/" + id);
            //System.Diagnostics.Debug.WriteLine(response.Body);
            Models.Product data = JsonConvert.DeserializeObject<Models.Product>(response.Body);
            return View(data);
        }

        [HttpPost]
        public ActionResult Demand(Models.Product product)
        {

            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Set("Products/" + product.ID, product);
            //System.Diagnostics.Debug.WriteLine(response.Body);
            Models.Product data = JsonConvert.DeserializeObject<Models.Product>(response.Body);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Rate(string id)
        {

            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Products/" + id);
            //System.Diagnostics.Debug.WriteLine(response.Body);
            Models.Product data = JsonConvert.DeserializeObject<Models.Product>(response.Body);
            return View(data);
        }

        [HttpPost]
        public ActionResult Rate(Models.Product product)
        {

            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Set("Products/" + product.ID, product);
            //System.Diagnostics.Debug.WriteLine(response.Body);
            Models.Product data = JsonConvert.DeserializeObject<Models.Product>(response.Body);
            return RedirectToAction("Index");
        }
    }
}