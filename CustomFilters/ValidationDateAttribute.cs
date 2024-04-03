using API_FinalTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;


namespace API_FinalTask.Filters
{
    public class ValidationDateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            Student student = context.ActionArguments["Std"] as Student;

            if (student is null || String.Compare( student.BirthDate,"2022-02-25")>0)
            {
                context.ModelState.AddModelError("Date", "Date is Not Valid");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }


        }
    }
}
