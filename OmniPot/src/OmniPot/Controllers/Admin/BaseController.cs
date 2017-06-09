
using Microsoft.AspNetCore.Mvc;
using OmniPot.Data;
using OmniPot.Services;
using Microsoft.Extensions.Logging;

namespace OmniPot.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly KindDbContext context;
        protected readonly TenantCacheService tenantCacheService;
        protected readonly ILogger logger;

        public BaseController(KindDbContext context, TenantCacheService tenantCacheService, ILoggerFactory loggerFactory) {
            this.context = context;
            this.tenantCacheService = tenantCacheService;
            if (logger == null) {
                logger = loggerFactory.CreateLogger<TaxGroupsController>();
                logMessage("BaseController(constructor)", "logger created");
            }
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

        // ============================================================================================================
        //comment out SHOW_LOG_HELPRERS flag in project.json and all the logging helper functions below goes away.

        // ============================================================================================================
        public void logStart(string methodName) {
#if SHOW_LOG_HELPRERS
            string logString = "============================="
                + System.DateTime.Now + " == "
                + methodName
                + " Started =============================\n"
                + "      ======================================================================================================================";

            logger.LogDebug(logString);  //this refuses to appear in Visual Studio's output window, 
                                         //appearing in pop-up console is much inferior info doesn't persist after app closes and I can' search it

            System.Diagnostics.Debug.WriteLine(logString);  //this doesn't work
            //System.Console.WriteLine(logString); //this prevents app from even starting...no error generated
#endif
        }

        // ============================================================================================================
        public void logMessage(string methodName, string message) {
#if SHOW_LOG_HELPRERS
            string msgString = "   * "
                + System.DateTime.Now + " == "
                + methodName
                + "==>"
                + message
                + "<==\n";

            logger.LogDebug(msgString);//this refuses to appear in Visual Studio's output window, 
            System.Diagnostics.Debug.WriteLine(msgString);  //this doesn't work
#endif
        }

        // ============================================================================================================
        public void logMember(string methodName, string memberName, object member) {
#if SHOW_LOG_HELPRERS
            string objString = "   * "
                + System.DateTime.Now + " == "
                + methodName
                + " : " + memberName
                + "==>";
                
            if(member != null ) {
                objString += Newtonsoft.Json.JsonConvert.SerializeObject(member);
            } else {
                objString += "Null Object"; ;
            }

            logger.LogDebug(objString);//this refuses to appear in Visual Studio's output window, 
            System.Diagnostics.Debug.WriteLine(objString);  //this doesn't work
#endif
        }

        // ============================================================================================================
        public void logError(string methodName, System.Exception ex) {
#if SHOW_LOG_HELPRERS
            string logString = "============================= "
                + System.DateTime.Now + " == "
                + methodName
                + " threw an Exception ===>\n"
                + Newtonsoft.Json.JsonConvert.SerializeObject(ex)
                + "      <===\n";

            logger.LogError(logString);//this refuses to appear in Visual Studio's output window, 
            System.Diagnostics.Debug.WriteLine(logString);  //this doesn't work
#endif
        }

        // ============================================================================================================
        public void logEnd(string methodName) {
#if SHOW_LOG_HELPRERS
            string logString = "======================================================================================================================\n"
                + "      ============================= "
                + System.DateTime.Now + " == "
                + methodName
                + " Ended =============================\n\n\n\n";

            logger.LogDebug(logString);  //this refuses to appear in Visual Studio's output window
            System.Diagnostics.Debug.WriteLine(logString);  //this doesn't work
#endif
        }

        // ============================================================================================================
        //comment/remove out SHOW_LOG_HELPRERS flag in project.json and all the logging helper functions above goes away.

    }
}