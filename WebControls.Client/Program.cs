using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TypeAhead.Models;


namespace WebControls.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new WebControlsEntities();
            var adTag = context.AdTags.First(i => i.Id == 1);
            adTag.Name = "UpdateFromConsole";



            var newAdTag = new AdTag
            {
                Id = 2,
                Name = "TestAdTag",
                ClientId = 1
            };

            context.AdTags.Add(newAdTag);
            context.SaveChanges();


            var anotherNewAdTag = new AdTag
            {
                Id = 3,
                Name = "New AdTag",
                ClientId = 2
            };

            context.AdTags.Add(anotherNewAdTag);

            var adTagToRemove = context.AdTags.First(t => t.Name == "TestAdTag");
            context.AdTags.Remove(adTagToRemove);


            var adTagChanged = context.AdTags.First(i => i.Id == 1034);
            adTagChanged.Name = Guid.NewGuid().ToString();
            adTagChanged.ClientId = 1;

            context.Entry(adTagChanged).State = EntityState.Modified;

            //-------------------------------------------------------------------------------------------//


            var changeInfoAll = context.ChangeTracker.Entries();
            var changes = new List<EntityChanges>();
            var modifications = new List<Modification>();
            var modsJson = string.Empty;


            foreach (var t in changeInfoAll) // Let's consider changing the following conditionals to a switch.
            {
                if (t.State == EntityState.Added)
                {
                    changes.Add(new EntityChanges
                    {
                        EntityName = t.Entity.GetType().Name,
                        ChangeType = t.State,
                        Original = null,
                        Current = t.CurrentValues.PropertyNames.ToDictionary(pn => pn, pn => t.CurrentValues[pn])
                    });
                }
                else if (t.State == EntityState.Deleted)
                {
                    changes.Add(new EntityChanges
                    {
                        EntityName = t.Entity.GetType().Name,
                        ChangeType = t.State,
                        Original = t.OriginalValues.PropertyNames.ToDictionary(pn => pn, pn => t.OriginalValues[pn]),
                        Current = null
                    });
                }
                else if (t.State == EntityState.Modified)
                {
                    changes.Add(new EntityChanges
                    {
                        EntityName = t.Entity.GetType().Name,
                        ChangeType = t.State,
                        Original = t.OriginalValues.PropertyNames.ToDictionary(pn => pn, pn => t.OriginalValues[pn]),
                        Current = t.CurrentValues.PropertyNames.ToDictionary(pn => pn, pn => t.CurrentValues[pn])
                    });
                }
            }

            foreach (var i in changes)
            {
                if (i.ChangeType == EntityState.Modified)  //Let's consider changing these to a switch.
                {
                    foreach (var o in i.Original)
                    {
                        var key = o.Key;
                        var originalValue = o.Value.ToString();
                        object valueAsObject;

                        if (i.Current.TryGetValue(key, out valueAsObject))
                        {
                            var currentValue = valueAsObject.ToString();

                            if (currentValue != originalValue)
                            {
                                var entry = new Modification
                                {
                                    PropertyName = key,
                                    OriginalValue = originalValue,
                                    CurrentValue = currentValue
                                };
                                modifications.Add(entry);
                            }
                        }
                    }
                }
                else if (i.ChangeType == EntityState.Added)
                {
                    foreach (var a in i.Current)
                    {
                        var entry = new Modification
                        {
                            PropertyName = a.Key,
                            OriginalValue = null,
                            CurrentValue = a.Value.ToString()
                        };
                        modifications.Add(entry);
                    }
                }
                else if (i.ChangeType == EntityState.Deleted)
                {
                    foreach (var d in i.Original)
                    {
                        var entry = new Modification
                        {
                            PropertyName = d.Key,
                            OriginalValue = d.Value.ToString(),
                            CurrentValue = null
                        };
                        modifications.Add(entry);
                    }
                }

                modsJson = JsonConvert.SerializeObject(modifications);
                int entityId = 0;
                entityId = Convert.ToInt32(i.Current != null ? i.Current["Id"] : i.Original["Id"]);

                var auditEntry = new AuditHistory
                {
                    CreateDate = DateTime.UtcNow,
                    EntityId = entityId,
                    EntityName = i.EntityName,
                    Modifications = modsJson,
                    ModificationType = i.ChangeType.ToString(),
                    UserId = 0,
                    UserName = "UserName"
                };
                context.AuditHistories.Add(auditEntry);
                modifications.Clear();
            }
            context.SaveChanges();
        }
    }
}





//var changeModifieds = context.ChangeTracker.Entries()
//    .WHERE(T => T.STATE == ENTITYSTATE.MODIFIED)
//    .SELECT(T => NEW
//    {
//        ENTITYNAME = T.ENTITY.GETTYPE().NAME,
//        CHANGETYPE = T.STATE,
//        ORIGINAL = T.ORIGINALVALUES.PROPERTYNAMES.TODICTIONARY(PN => PN, PN => T.ORIGINALVALUES[PN]), //THIS GUY THROWS AN EXCEPTION IF "ORIGINAL" DOESN'T EXIST (EVEN IF ORIGINALVALUES IS NULL-CHECKED)
//        CURRENT = T.CURRENTVALUES.PROPERTYNAMES.TODICTIONARY(PN => PN, PN => T.CURRENTVALUES[PN])
//    })


//So now we need to manipulate these dictionaries of key-value pairs, so that we can get useful objects to store in our AuditHistory table.

//var testCurrent = changeInfo.Current;
//var testPrevious = changeInfo.Original;

//foreach (var i in testCurrent)
//{
//    var entry = new AuditHistory
//    {
//        Current = i.ToString(),
//    };
//    context.AuditHistories.Add(entry);
//}

//var entry = new AuditHistory();

//foreach (var i in changeInfo.Original)
//{
//    entry.Original = i.ToString();

//    foreach (var o in changeInfo.Current)
//    {
//        entry.Current = o.ToString();
//    }
//    context.AuditHistories.Add(entry);
//}




//foreach (var entry in changeInfo)
//{

//    var auditHistory = new AuditHistory
//    {
//        Current = entry.Current.ToString(),
//        Original = entry.Original.ToString()
//    };

//    context.AuditHistories.Add(auditHistory);
//}




//public class ChangePair
//{
//    public string Key { get; set; }
//    public object Original { get; set; }
//    public object Current { get; set; }
//}