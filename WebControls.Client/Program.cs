using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using Newtonsoft.Json;
using TypeAhead.Models;


namespace WebControls.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //-------------------------------------------------------------------------------------------//
            // Making some context changes...                                                            //
            //-------------------------------------------------------------------------------------------//
            var context = new WebControlsEntities();
            var adTag = context.AdTags.First(i => i.Id == 1);
            adTag.Name = "UpdateFromConsole";

            var newAdTag = new AdTag
            {
                Name = "New AdTag 1",
                ClientId = 1
            };

            context.AdTags.Add(newAdTag);

            var anotherNewAdTag = new AdTag
            {
                Name = "New AdTag 2",
                ClientId = 2
            };

            var yetAnotherNewAdTag = new AdTag
            {
                Name = "New AdTag 3",
                ClientId = 3
            };

            context.AdTags.Add(anotherNewAdTag);
            context.AdTags.Add(yetAnotherNewAdTag);

            var adTagToRemove = context.AdTags.First(t => t.Name == "TestAdTag");
            context.AdTags.Remove(adTagToRemove);

            var adTagChanged = context.AdTags.First(i => i.Id == 1034);
            adTagChanged.Name = Guid.NewGuid().ToString();
            adTagChanged.ClientId = 1;

            var adTagChanged2 = context.AdTags.First(i => i.Id == 1121);
            adTagChanged2.Name = Guid.NewGuid().ToString();
            adTagChanged2.ClientId = 1;


            //-------------------------------------------------------------------------------------------//
            // Auditing changes                                                                          //
            //-------------------------------------------------------------------------------------------//
            var changes = new List<EntityChanges>();

            var changeInfoAll = context.ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Deleted || x.State == EntityState.Modified);

            foreach (var t in changeInfoAll)
            {
                switch (t.State)
                {
                    case EntityState.Added:
                        changes.Add(new EntityChanges
                        {
                            EntityName = t.Entity.GetType().Name,
                            ChangeType = t.State,
                            Original = null,
                            Current = t.CurrentValues.PropertyNames.ToDictionary(pn => pn, pn => t.CurrentValues[pn])
                        });
                        break;

                    case EntityState.Deleted:
                        changes.Add(new EntityChanges
                        {
                            EntityName = ObjectContext.GetObjectType(t.Entity.GetType()).FullName,
                            ChangeType = t.State,
                            Current = null,
                            Original = t.OriginalValues.PropertyNames.ToDictionary(pn => pn, pn => t.OriginalValues[pn])
                        });
                        break;

                    case EntityState.Modified:
                        changes.Add(new EntityChanges
                        {
                            EntityName = t.Entity.GetType().BaseType.ToString(),
                            ChangeType = t.State,
                            Original = t.OriginalValues.PropertyNames.ToDictionary(pn => pn, pn => t.OriginalValues[pn]),
                            Current = t.CurrentValues.PropertyNames.ToDictionary(pn => pn, pn => t.CurrentValues[pn])
                        });
                        break;
                }
            }

            foreach (var i in changes)
            {
                var modifications = new List<Modification>();

                switch (i.ChangeType)
                {
                    case EntityState.Modified:
                        foreach (var o in i.Original)
                        {
                            var key = o.Key;
                            if (key != "RowVersion" || key != "CreateDate" || key != "UpdateDate")
                            {
                                var originalValue = o.Value.ToString();
                                object valueAsObject;

                                if (i.Current.TryGetValue(key, out valueAsObject))
                                {
                                    var currentValue = valueAsObject.ToString();

                                    if (!Equals(currentValue, originalValue))
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
                        break;

                    case EntityState.Added:
                        modifications.AddRange(i.Current.Select(a => new Modification
                        {
                            PropertyName = a.Key,
                            OriginalValue = null,
                            CurrentValue = a.Value.ToString()
                        }));
                        break;

                    case EntityState.Deleted:
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
                        break;
                }

                var entityId = Convert.ToInt32(i.Current != null ? i.Current["Id"] : i.Original["Id"]);

                var auditEntry = new AuditHistory
                {
                    CreateDate = DateTime.UtcNow,
                    EntityId = entityId,
                    EntityName = i.EntityName,
                    Modifications = JsonConvert.SerializeObject(modifications),
                    ModificationType = i.ChangeType.ToString(),
                    UserId = 0,
                    UserName = "UserName"
                };
                context.AuditHistories.Add(auditEntry);
            }
            context.SaveChanges();
        }
    }
}