using CsvHelper.Configuration;
using System;
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

    public sealed class CsvHeadersMap : ClassMap<CsvHeaders>
    {
        public CsvHeadersMap()
        {
            Map(m => m.Firstname).Name("First Name")
                .TypeConverterOption.NullValues(string.Empty)
                .Validate(field => {
                    if (string.IsNullOrEmpty(field.ToString()))
                    {
                        string errorMsg = ("First Name is null in file:\n  " + CsvHeadersHelpers.currentFileNameBeingProcessed + " \nat row : " + CsvHeadersHelpers.currentRowBeingProcessed + ".");
                        Logger.Log($"An exception occurred in validating First Name:\nMessage: {errorMsg}");
                        return false;
                    }
                    else return true;
                });


            Map(m => m.Lastname).Name("Last Name")
                .TypeConverterOption.NullValues(string.Empty)
                .Validate(field => {
                    if (string.IsNullOrEmpty(field.ToString()))
                    {
                        string errorMsg = ("Last Name is null in file:\n  " + CsvHeadersHelpers.currentFileNameBeingProcessed + " \nat row : " + CsvHeadersHelpers.currentRowBeingProcessed + ".");
                        Logger.Log($"An exception occurred in validating Last Name:\nMessage: {errorMsg}");
                        return false;
                    }
                    else return true;
                });

            Map(m => m.Streetnumber).Name("Street Number")
                .TypeConverterOption.NullValues(string.Empty)
                .Validate(field => {
                    if (string.IsNullOrEmpty(field.ToString()))
                    {
                        string errorMsg = ("Street Number is null in file:\n  " + CsvHeadersHelpers.currentFileNameBeingProcessed + " \nat row : " + CsvHeadersHelpers.currentRowBeingProcessed + ".");
                        Logger.Log($"An exception occurred in validating Street Number:\nMessage: {errorMsg}");
                        return false;
                    }
                    else
                        return true;
                });

            Map(m => m.Street).Name("Street")
                .TypeConverterOption.NullValues(string.Empty)
                .Validate(field => {
                    if (string.IsNullOrEmpty(field.ToString()))
                    {
                        string errorMsg = ("Street name is null in file:\n  " + CsvHeadersHelpers.currentFileNameBeingProcessed + " \nat row : " + CsvHeadersHelpers.currentRowBeingProcessed + "");
                        Logger.Log($"An exception occurred in validating Street:\nMessage: {errorMsg}");
                        return false;
                    }
                    else
                        return true;
                });


            Map(m => m.City).Name("City")
                .TypeConverterOption.NullValues(string.Empty)
                .Validate(field => {
                    if (string.IsNullOrEmpty(field.ToString()))//|| validalphanumericwithsplchars(field.ToString()))
                    {
                        string errorMsg = ("City is null in file:\n  " + CsvHeadersHelpers.currentFileNameBeingProcessed + " \nat row : " + CsvHeadersHelpers.currentRowBeingProcessed + ".");
                        Logger.Log($"An exception occurred in validating City:\nMessage: {errorMsg}");
                        return false;
                    }
                    else return true;
                });

            Map(m => m.Province).Name("Province")
                .TypeConverterOption.NullValues(string.Empty)
                .Validate(field => {
                    if (string.IsNullOrEmpty(field.ToString()))
                    {
                        string errorMsg = ("Province is null in file:\n  " + CsvHeadersHelpers.currentFileNameBeingProcessed + " \nat row : " + CsvHeadersHelpers.currentRowBeingProcessed + ".");
                        Logger.Log($"An exception occurred in validating Province:\nMessage: {errorMsg}");
                        return false;
                    }
                    else return true;
                });

            Map(m => m.Country).Name("Country")
                .TypeConverterOption.NullValues(string.Empty)
                .Validate(field => {
                    if (string.IsNullOrEmpty(field.ToString()))
                    {
                        string errorMsg = ("Country is null in file:\n  " + CsvHeadersHelpers.currentFileNameBeingProcessed + " \nat row : " + CsvHeadersHelpers.currentRowBeingProcessed + ".");
                        Logger.Log($"An exception occurred in validating Country:\nMessage: {errorMsg}");
                        return false;
                    }
                    else
                        return true;
                });

            Map(m => m.Postalcode).Name("Postal Code")
                .TypeConverterOption.NullValues(string.Empty)
                .Validate(field => {
                    if (string.IsNullOrEmpty(field.ToString()))
                    {
                        string errorMsg = ("Postal code is null in file:\n  " + CsvHeadersHelpers.currentFileNameBeingProcessed + " \nat row : " + CsvHeadersHelpers.currentRowBeingProcessed + ".");
                        Logger.Log($"An exception occurred in validating Postal code:\nMessage: {errorMsg}");
                        return false;
                    }
                    else return true;
                });

            Map(m => m.Phonenumber).Name("Phone Number")
                .TypeConverterOption.NullValues(string.Empty)
                .Validate(field => {
                    if (string.IsNullOrEmpty(field.ToString()))
                    {
                        string errorMsg = ("Phone Number is null in file:\n  " + CsvHeadersHelpers.currentFileNameBeingProcessed + " \nat row : " + CsvHeadersHelpers.currentRowBeingProcessed + ".");
                        Logger.Log($"An exception occurred in validating Phone Number:\nMessage: {errorMsg}");
                        return false;
                    }
                    else return true;
                });

            Map(m => m.Email).Name("email Address")
                .TypeConverterOption.NullValues(string.Empty)
                .Validate(field => {
                    if (field.ToString().Contains("null"))
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
