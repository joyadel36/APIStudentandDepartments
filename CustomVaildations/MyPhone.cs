using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace API_FinalTask.Models
{
    public class MyPhone:ValidationAttribute
    {

        public MyPhone() { }

        public override bool IsValid(object? obj)
        {
            if (obj == null)
                return false;
            else
            {
                if (obj is string)
                {
                    string phone = (string)obj;
                    string pattern = @"(01)[0125]{1}[0-9]{8}";
                    if (Regex.IsMatch(phone, pattern))
                        return true;
                    else
                    {

                        ErrorMessage = "InValid Phone number "; 
                        return false;
                    }
                }
                else
                    return false;
            }
           
        }
    }
}
