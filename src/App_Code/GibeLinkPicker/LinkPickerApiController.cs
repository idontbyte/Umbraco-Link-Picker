using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Umbraco.Web.WebApi;
using Umbraco.Web.Editors;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;
using System.Web.Http;

namespace Gibe.Controllers
{
    [Umbraco.Web.Mvc.PluginController("Gibe")]
    public class LinkPickerApiController : UmbracoAuthorizedJsonController
    {
        public LinkPickerApiController()
        {
            //  add table to database
            //get the database
            var db = UmbracoContext.Application.DatabaseContext.Database;

            if (!db.TableExist("GibeGTMCategory"))
            {
                //Create DB table - and set overwrite to false
                db.CreateTable<GibeGTMCategory>(false);
            }
        }

        [UmbracoAuthorize]
        public IEnumerable<GibeGTMCategory> GetAllCategories()
        {
            //get the database
            var db = UmbracoContext.Application.DatabaseContext.Database;
            //build a query to select everything the people table
            var query = new Sql().Select("*").From("GibeGTMCategory");
            //fetch data from DB with the query and map to Person object
            return db.Fetch<GibeGTMCategory>(query);
        }

        [UmbracoAuthorize]
        public GibeGTMCategory AddCategory(GibeGTMCategory category)
        {
            if (!string.IsNullOrEmpty(category.Name))
            {
                //get the database
                var db = UmbracoContext.Application.DatabaseContext.Database;
                //build a query to select everything the people table
                var query = new Sql().Select("*").From("GibeGTMCategory");
                //fetch data from DB with the query and map to Person object
                var categories = db.Fetch<GibeGTMCategory>(query);
                if (!categories.Any(c => c.Name == category.Name))
                {
                    // insert category in to database
                    db.Insert(category);
                }
            }
            return category;
        }

        [UmbracoAuthorize]
        public GibeGTMCategory RemoveCategory(GibeGTMCategory category)
        {
            if (!string.IsNullOrEmpty(category.Name))
            {
                //get the database
                var db = UmbracoContext.Application.DatabaseContext.Database;
                //build a query to select everything the people table
                var query = new Sql().Select("*").From("GibeGTMCategory");
                //fetch data from DB with the query and map to Person object
                var categories = db.Fetch<GibeGTMCategory>(query);
                if (categories.Any(c => c.Name == category.Name))
                {
                    var categoryToDelete = categories.FirstOrDefault(c => c.Name == category.Name);
                    // delete category from database
                    db.Delete<GibeGTMCategory>(categoryToDelete);
                }
            }
            return category;
        }
    }

    [TableName("GibeGTMCategory")]
    [PrimaryKey("Id")]
    public class GibeGTMCategory
    {
        [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}