using CsvHelper.Configuration;
using System.Text.RegularExpressions;

namespace Assignment1.ClassFiles
{
    public class CsvHeaders
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public int Streetnumber { get; set; }
        public string Street { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        public string Postalcode { get; set; }

        public string Phonenumber { get; set; }

        public string Email { get; set; }

        public string Date { get { return mDate; } }
        public string mDate = "234-43-2";
    }


    public class CsvHeadersHelpers
    {
        public static int currentRowBeingProcessed = -1;
        public static string currentFileNameBeingProcessed = "";
        public static string dateOfFileNameBeingProcessed = "";
    }

    public sealed class CsvHeadersMap : ClassMap <CsvHeaders>
    {
        public CsvHeadersMap()
        {
            Map(m => m.Firstname).Name("First Name").Validate(field => {
                if (string.IsNullOrEmpty(field))
                {
                    string errorMsg = ("First Name is null in file:\n  " + CsvHeadersHelpers.currentFileNameBeingProcessed + " \nat row : " + CsvHeadersHelpers.currentRowBeingProcessed + ".");
                    Logger.Log($"An exception occurred in validating First Name:\nMessage: {errorMsg}");
                    return false;
                }
                else return true;
            });


            Map(m => m.Lastname).Name("Last Name").Validate(field => {
                if (string.IsNullOrEmpty(field))
                {
                    string errorMsg = ("Last Name is null in file:\n  " + CsvHeadersHelpers.currentFileNameBeingProcessed + " \nat row : " + CsvHeadersHelpers.currentRowBeingProcessed + ".");
                    Logger.Log($"An exception occurred in validating Last Name:\nMessage: {errorMsg}");
                    return false;
                }
                else return true;
            });

            Map(m => m.Streetnumber).Name("Street Number").Validate(field => {
                if (string.IsNullOrEmpty(field))
                {
                    string errorMsg = ("Street Number is null in file:\n  " + CsvHeadersHelpers.currentFileNameBeingProcessed + " \nat row : " + CsvHeadersHelpers.currentRowBeingProcessed + ".");
                    Logger.Log($"An exception occurred in validating Street Number:\nMessage: {errorMsg}");
                    return false;
                }
                else
                    return true;
            });

            Map(m => m.Street).Name("Street").Validate(field => {
                if (string.IsNullOrEmpty(field))
                {
                    string errorMsg = ("Street name is null in file:\n  " + CsvHeadersHelpers.currentFileNameBeingProcessed + " \nat row : " + CsvHeadersHelpers.currentRowBeingProcessed + "");
                    Logger.Log($"An exception occurred in validating Street:\nMessage: {errorMsg}");
                    return false;
                }
                else
                    return true;
            });


            Map(m => m.City).Name("City").Validate(field => {
                if (string.IsNullOrEmpty(field) )//|| validalphanumericwithsplchars(field))
                {
                    string errorMsg = ("City is null in file:\n  " + CsvHeadersHelpers.currentFileNameBeingProcessed + " \nat row : " + CsvHeadersHelpers.currentRowBeingProcessed + ".");
                    Logger.Log($"An exception occurred in validating City:\nMessage: {errorMsg}");
                    return false;
                }
                else return true;
            });

            Map(m => m.Province).Name("Province").Validate(field => {
                if (string.IsNullOrEmpty(field))
                {
                    string errorMsg = ("Province is null in file:\n  " + CsvHeadersHelpers.currentFileNameBeingProcessed + " \nat row : " + CsvHeadersHelpers.currentRowBeingProcessed + ".");
                    Logger.Log($"An exception occurred in validating Province:\nMessage: {errorMsg}");
                    return false;
                }
                else return true;
            });

            Map(m => m.Country).Name("Country").Validate(field => {
                if (string.IsNullOrEmpty(field))
                {
                    string errorMsg = ("Country is null in file:\n  " + CsvHeadersHelpers.currentFileNameBeingProcessed + " \nat row : " + CsvHeadersHelpers.currentRowBeingProcessed + ".");
                    Logger.Log($"An exception occurred in validating Country:\nMessage: {errorMsg}");
                    return false;
                }
                else
                    return true;
            });

            Map(m => m.Postalcode).Name("Postal Code").Validate(field => {
                if (string.IsNullOrEmpty(field))
                {
                    string errorMsg = ("Postal code is null in file:\n  " + CsvHeadersHelpers.currentFileNameBeingProcessed + " \nat row : " + CsvHeadersHelpers.currentRowBeingProcessed + ".");
                    Logger.Log($"An exception occurred in validating Postal code:\nMessage: {errorMsg}");
                    return false;
                }
                else return true;
            });

            Map(m => m.Phonenumber).Name("Phone Number").Validate(field => {
                if (string.IsNullOrEmpty(field))
                {
                    string errorMsg = ("Phone Number is null in file:\n  " + CsvHeadersHelpers.currentFileNameBeingProcessed + " \nat row : " + CsvHeadersHelpers.currentRowBeingProcessed + ".");
                    Logger.Log($"An exception occurred in validating Phone Number:\nMessage: {errorMsg}");
                    return false;
                }
                else return true;
            });

            Map(m => m.Email).Name("email Address").Validate(field => {
                if (field.Contains("null"))
                {
                    string errorMsg = ("email Address is null in file:\n  " + CsvHeadersHelpers.currentFileNameBeingProcessed + " \nat row : " + CsvHeadersHelpers.currentRowBeingProcessed + ".");
                    Logger.Log($"An exception occurred in validating email Address:\nMessage: {errorMsg}");
                    return false;
                }
                else return true;
            });

        }

        bool validalphanumericwithsplchars(string text)
        {
            Regex r = new Regex(@"^[a-zA-Z0-9 -] +$");

            return r.IsMatch(text);
        }
    }
}
