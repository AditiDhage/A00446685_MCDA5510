using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Assignment1.ClassFiles;
using CsvHelper;
using CsvHelper.Configuration;

namespace Assignment1
{
    public class DirWalker
    {        
        public void walk(String path)
        {

            string[] directoryList = Directory.GetDirectories(path);


            if (directoryList == null) return;

            foreach (string dirpath in directoryList)
               // foreach (String f : list)
            {
                if (Directory.Exists(dirpath))
                {
                    walk(dirpath);
                    //Console.WriteLine("Dir:" + dirpath);
                }
            }
            string[] fileList = Directory.GetFiles(path);
            foreach (string filepath in fileList)
            {

                Console.WriteLine("File:" + filepath);
            }
        }

        public List<string> getCSVFilesInList(String path)
        {
            List<string> csvList = new List<string>();
            string[] directoryList = Directory.GetDirectories(path);
            Console.WriteLine("Getting & adding files from: " + path);

            if (directoryList == null) return null;

            foreach (string dirpath in directoryList)
            {
                if (Directory.Exists(dirpath))
                {
                    
                    List<string> returnedCSV_Files = getCSVFilesInList(dirpath);
                    if (returnedCSV_Files != null)
                    { 
                        foreach (string filepath in returnedCSV_Files)
                        {
                            csvList.Add(filepath);
                        }
                    }                    
                }
                
            }

            string[] fileList = Directory.GetFiles(path, "*.csv");

            foreach (string filepath in fileList)
            {
                csvList.Add(filepath);
            }
            return csvList;
        }

        public static void Main(String[] args)
        {
            string csvPath = @"C:\Users\Aditi\Desktop\gitproject\A00446685_MCDA5510\Assignment1\InputFiles";
            string outputPath = @"C:\Users\Aditi\Desktop\gitproject\A00446685_MCDA5510\Assignment1\Output\Output.csv";

            File.Create(outputPath).Dispose();

          
            DateTime dateTimeStart, dateTimeEnd;
            int iCntValid=0, iCntInvalid = 0;
            //DirectoryInfo directoryInfo = GetExecutingDirectory();
            //string dir = GetProgramFolder();
            dateTimeStart = DateTime.Now;
            bool bHeaders = true;
            DirWalker fw = new DirWalker();
            List<string> csvList = fw.getCSVFilesInList(csvPath);
            
            if (csvList != null)
            {
                Console.WriteLine("Processing files gathered from folders");
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = false
                };


                foreach (string filepath in csvList)
                {
                    Console.WriteLine("Processing " + filepath + " .....");

                    CsvHeadersHelpers.currentFileNameBeingProcessed = filepath;
                    CsvHeadersHelpers.dateOfFileNameBeingProcessed = GetDate(filepath);


                    using (var reader = new StreamReader(filepath))
                    using (var csvr = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {

                        csvr.Context.RegisterClassMap<CsvHeadersMap>();
                        //var records = csvr.GetRecords<CsvHeaders>();
                        while (csvr.Read())
                        {
                            try
                            {
                                CsvHeaders r = csvr.GetRecord<CsvHeaders>();
                                r.mDate = CsvHeadersHelpers.dateOfFileNameBeingProcessed;

                                using (var writer = new StreamWriter(outputPath, true))
                                {

                                    if (bHeaders)
                                    {

                                        try
                                        {
                                            bHeaders = false;
                                            using (var csvw = new CsvWriter(writer, CultureInfo.InvariantCulture))
                                            {
                                                csvw.WriteHeader<CsvHeaders>();
                                                csvw.NextRecord();
                                                csvw.WriteRecord<CsvHeaders>(r);
                                                csvw.NextRecord();
                                                iCntValid++;
                                            }
                                            //csvw.WriteRecords<CsvHeaders>(records);
                                        }
                                        catch (CsvHelper.ValidationException vex)
                                        {
                                            Logger.Log("Invalid field/data: ");
                                        }
                                        catch (CsvHelperException ex)
                                        {

                                        }


                                    }
                                    else
                                    {
                                        try
                                        {
                                            using (var csvw = new CsvWriter(writer, config))
                                            {
                                                csvw.WriteRecord<CsvHeaders>(r);
                                                csvw.NextRecord();
                                                iCntValid++;
                                            }
                                            //csvw.WriteRecords<CsvHeaders>(records);
                                        }
                                        catch (CsvHelper.ValidationException vex)
                                        {
                                            Logger.Log("Invalid field/data: " + vex.Message);
                                        }
                                        catch (CsvHelperException ex)
                                        { }

                                    }
                                }

                            }
                            catch (CsvHelper.ValidationException vex)
                            {                                
                                iCntInvalid++;
                            }
                            catch (CsvHelperException ex)
                            {
                                if (ex != null)
                                { 
                                    if (ex.InnerException != null)
                                        Logger.Log(filepath +" => "+ ex.InnerException.Message);                                
                                }
                                iCntInvalid++;
                            }
                        }
                    }
                }                   
                
            }

            dateTimeEnd = DateTime.Now;
            TimeSpan timeSpan = dateTimeEnd.Subtract(dateTimeStart);
            Logger.Log("Time taken for parsing: " + Math.Round(timeSpan.TotalHours).ToString() +" Hrs " + Math.Round(timeSpan.TotalMinutes).ToString() + " Mins " + Math.Round(timeSpan.TotalSeconds).ToString() + " Secs");
            Logger.Log("Total number of valid rows: " + iCntValid.ToString());
            Logger.Log("Total number of skipped rows: "+ iCntInvalid.ToString());
            Logger.FinishWriting();
        }


        public static string GetDate(string fileName)
        {
            string[] separatingStrings = { "\\" };

            string[] words = fileName.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
            //System.Console.WriteLine($"{words.Length} substrings in text:");
            //System.Console.WriteLine(words[words.Length - 4] + "/" + words[words.Length - 3] + "/" + words[words.Length - 2] + "/");


            return words[words.Length - 4] + "/" + words[words.Length - 3] + "/" + words[words.Length - 2] + "/";
        }


        public static DirectoryInfo GetExecutingDirectory()
        {
            var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
            return new FileInfo(location.AbsolutePath).Directory;
        }

        public static string GetProgramFolder()
        {
            //string fullPath = System.Reflection.Assembly.GetAssembly(typeof(DirWalker)).Location;
            //string fullPath = System.Reflection.Assembly.GetEntryAssembly().Location;


            //string theDirectory = Path.GetDirectoryName(fullPath);
            string theDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;

            return theDirectory;
        }

    }
}
