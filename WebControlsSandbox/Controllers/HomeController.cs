using System.Linq;
using System.Web.Http.Cors;
using System.Web.Mvc;
using TypeAhead.Models;
using TypeAhead.Services;

namespace TypeAhead.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            ViewBag.Title = "Web Control Sandbox";

            return View();
        }

        //*****************************************************************************
        // TypeAhead Methods
        //*****************************************************************************
        [Route("Typeahead")]
        public ActionResult Typeahead()
        {
            return View();
        }

        [Route("GetClients")]
        public JsonResult GetClients(string q)
        {
            var clients = ClientService.GetClientData(q);

            return Json(clients, JsonRequestBehavior.AllowGet);
        }

        [Route("PrefetchClients")]
        public JsonResult PrefetchClients()
        {
            var clients = ClientService.PrefetchClients();

            return Json(clients, JsonRequestBehavior.AllowGet);
        }

        //*****************************************************************************
        // Cascading Dropdown Methods
        //*****************************************************************************
        [Route("GetNewClients")]
        public JsonResult GetNewClients(string q)
        {
            var clients = ViewModel.GetClients(q);

            return Json(clients.Select(c => new Client
            {
                Id = c.Id,
                Name = c.Name
            }), JsonRequestBehavior.AllowGet);
        }

        [Route("GetAdTags")]
        public JsonResult GetAdTags(int id)
        {
            var adTags = new { items = ViewModel.GetAdTags(id) };

            return Json(adTags, JsonRequestBehavior.AllowGet);
        }

        [Route("GetAdUnits")]
        public JsonResult GetAdUnits(int id)
        {
            var adUnits = new { items = ViewModel.GetAdUnits(id) };

            return Json(adUnits, JsonRequestBehavior.AllowGet);
        }

        [Route("Cascade")]
        public ActionResult Cascade()
        {
            ViewBag.Title = "Cascading Dropdown Sandbox";

            return View();
        }

        #region Utterly Unnecessary Methods
        public ActionResult NothingHere()
        {
            ViewBag.Title = "Move Along";

            return View();
        }
        #endregion
    }
}