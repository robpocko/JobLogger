using Microsoft.AspNetCore.Mvc;
using JobLogger.DAL;

namespace JobLogger.API.Controllers
{
    /// <summary>
    /// Sub-class of Controller. Provides specific features
    /// </summary>
    public abstract class ControllerBase : Controller
    {
        /// <summary>
        /// A database context
        /// </summary>
        /// <remarks>
        /// Typically the database context will be provided by Dependency Injection in the MVC application
        /// </remarks>
        public JobLoggerDbContext DB { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db"></param>
        /// <param name="logger"></param>
        /// <param name="authorizationService"></param>
        public ControllerBase([FromServices] JobLoggerDbContext db)
        {
            DB = db;
        }
    }
}
