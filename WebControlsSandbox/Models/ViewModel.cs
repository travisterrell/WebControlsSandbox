
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace TypeAhead.Models
{
    public class ViewModel
    {
        public Client Client { get; set; }

        public AdTag AdTag { get; set; }

        public AdUnit AdUnit { get; set; }


        public static IEnumerable<Client> GetClients(string q)
        {
            using (var context = new WebControlsEntities())
            {
                var results = context.Clients.Where(c => c.Name.Contains(q)).ToList();

                return results;
            }
        }

        public static IEnumerable<SelectListItem> GetAdTags(int? clientId)
        {
            using (var context = new WebControlsEntities())
            {
                return context.AdTags.Where(x => x.ClientId == clientId).ToList().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            }
        }

        public static IEnumerable<SelectListItem> GetAdUnits(int? adTagId)
        {
            using (var context = new WebControlsEntities())
            {
                return context.AdUnits.Where(x => x.AdTagId == adTagId).ToList().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            }
        }
    }
}