using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TextFileChallenge
{
    public class MyData 
    {
        BindingList<UserModel> users = new BindingList<UserModel>();
        UserModel currentUser = new UserModel();
        
        public BindingList<UserModel> GetUsers()
        {
            
            string[] lines = File.ReadAllLines("StandardDataSet.csv");
            // Add error handing to make sure all lines have four enteris 
            foreach (string line in lines.Skip(1))
            {
                //I am not sure how to handle the first line that is a header
                string [] values = line.Split(',').Select(x => x.Trim()).ToArray();
                currentUser.FirstName = values[0];
                currentUser.LastName = values[1];
                int.TryParse(values[2], out int age);
                currentUser.Age = age;
                bool.TryParse(values[3], out bool alive);
                currentUser.IsAlive = alive;
                users.Add(currentUser);
            }
            return users;
        }
        public enum dataHeadings { FirstName, LastName, Age, IsAlive}
        IDictionary<dataHeadings, Action> matchWith = new Dictionary<dataHeadings, Action> {
             
        };
        private string[] data;

        private static T DictionaryToObject<T>(IDictionary<string, object> dict) where T : new()
        {
            T t = new T();
            PropertyInfo[] properties = t.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (!dict.Any(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
                    continue;
                KeyValuePair<string, object> item = dict.First(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));
                Type tPropertyType = t.GetType().GetProperty(property.Name).PropertyType;
                Type newT = Nullable.GetUnderlyingType(tPropertyType) ?? tPropertyType;
                //if (item.Value is '0') Convert.ToBoolean(Convert.ToInt32(item.Value));
                object newA = Convert.ChangeType(item.Value, newT);
                t.GetType().GetProperty(property.Name).SetValue(t, newA, null);
            }
            return t;
        }
        //private static T HashTableToObject<T>(Hashtable dict) where T : new()
        //{
        //    T t = new T();
        //    PropertyInfo[] properties = t.GetType().GetProperties();

        //    foreach (PropertyInfo property in properties)
        //    {
        //        if (!dict.Any(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
        //            continue;
        //        KeyValuePair<string, object> item = dict.First(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));
        //        Type tPropertyType = t.GetType().GetProperty(property.Name).PropertyType;
        //        Type newT = Nullable.GetUnderlyingType(tPropertyType) ?? tPropertyType;
        //        //if (item.Value is '0') Convert.ToBoolean(Convert.ToInt32(item.Value));
        //        object newA = Convert.ChangeType(item.Value, newT);
        //        t.GetType().GetProperty(property.Name).SetValue(t, newA, null);
        //    }
        //    return t;
        //}
        public static BindingList<dynamic> ListDictionaryToListDynamic(List<Dictionary<string, object>> dbRecords)
        {
            var eRecords = new BindingList<dynamic>();
            foreach (var record in dbRecords)
            {
                var eRecord = new ExpandoObject() as IDictionary<string, object>;
                foreach (var kvp in record)
                {
                    eRecord.Add(kvp);
                }
                eRecords.Add(eRecord);
            }
            return eRecords;
        }
        //public BindingList<UserModel> GetUsersAdvanced()
        //{
        //    users = new BindingList<UserModel>();
        //    string[] lines = File.ReadAllLines("AdvancedDataSet.csv");
        //    var cols = lines.First().Split(',').ToList();

        //    // validate column name duplication here!

        //    foreach (var line in lines.Skip(1))
        //    {
        //        if (line != null) data = line.Split(',');

        //        for (var i = 0; i < cols.Count(); i++)
        //        {
        //            var key = cols[i];
        //            var dict = new Dictionary<string, object>();

        //            if (data[i] == "0" || data[i] == "1")
        //            {
        //                Boolean IsAlive = Extensions.ToBoolean(data[i]);
        //                dict.Add(key, IsAlive);
        //            }
        //            else
        //                dict.Add(key, data[i]);

        //            var currentUser = DictionaryToObject<UserModel>(dict);
        //            users.Add(currentUser);
        //        }

        //        return users;
        //    }
        //    return users;
        //}
        //public BindingList<UserModel> GetUsersAdvanced3()
        //{
        //    string[] lines = File.ReadAllLines("AdvancedDataSet.csv");
        //    UserModel user = new UserModel();
        //    user.Headers = lines.First().Split(',');
        //    var data = lines.Skip(1).First().Split(',');
        //    foreach (var h in user.Headers)
        //    {
        //      //I tried adding a field to UserModel called Headers but the problem still seems to be implementing 
        //      //if the header is FirstName, then put the firstname there. The best choice seems to be saving the 
        //      //the first object along with the headers in a list, then read the rest of the records in the same order
        //    }
        //}
        //public BindingList<UserModel> GetUsersAdvanced() {
        //    users = new BindingList<UserModel>();
        //    var userHashTable = new Hashtable();
        //    string[] lines = File.ReadAllLines("AdvancedDataSet.csv");
        //    var header = lines.First().Split(',');
        //    var data = lines.Skip(1).First().Split(',');
        //    for (int i = 0; i < header.Length; i++)
        //    {
        //        if (data[i] == "0" || data[i] == "1")
        //        {
        //            Boolean IsAlive = Extensions.ToBoolean(data[i]);
        //            userHashTable.Add(header[i], IsAlive);
        //        }
        //        else
        //            //if (data[i]!= "0" || data[i]!="1")
        //            userHashTable.Add(header[i], data[i]);
        //    }
        //    currentUser = HashTableToObject<UserModel>(userHashTable);
        //}
        public BindingList<UserModel> GetUsersAdvanced()
        {
            users = new BindingList<UserModel>();
            var dict= new Dictionary<string,object>();
            string[] lines = File.ReadAllLines("AdvancedDataSet.csv");
            var header = lines.First().Split(',');
            List<string> listHeaders = new List<string>(header);
            var data = lines.Skip(1).First().Split(',');


            for (int i = 0; i < header.Length; i++)
                {
                    if (data[i] == "0" || data[i] == "1")
                        {
                            Boolean IsAlive = Extensions.ToBoolean(data[i]);
                            dict.Add(header[i], IsAlive);
                        }
                    else
                    //if (data[i]!= "0" || data[i]!="1")
                    dict.Add(header[i], data[i]);
                Hashtable userHashTable = new Hashtable(dict);
            }   
            currentUser = DictionaryToObject<UserModel>(dict);
            //var dictCopy = new Dictionary<string, object>(dict);
            var dictConcurrent = new ConcurrentDictionary<string, object>(dict);

            //write a method that takes the same dict and updates it with the next line of data
        foreach (var line in lines.Skip(1))
        {
        if (line != null)  data = line.Split(',');
               int i = 0;
                    foreach (var key in listHeaders)
                    {
                    
                    
                    if (data[i] == "0" || data[i] == "1")
                        {
                            Boolean IsAlive = Extensions.ToBoolean(data[i]);
                            dict[key] = IsAlive;
                            i++;
                            continue;
                        }
                        else
                        dict[key] = data[i];
                        i++;
                    
                    }
                    currentUser = DictionaryToObject<UserModel>(dict);
                    users.Add(currentUser);
                    currentUser = new UserModel();
                 
        }
        
            //List<UserModel> AdvancedUsers = new List<UserModel>();
            //foreach (var i in dict.Keys)
            //{
            //    Console.WriteLine("Key: {0} value: {1}", i, dict[i]);
                
                
            //}
            return users;
            //List<string> dataLines = File.ReadAllLines("StandardDataSet.csv").ToList();
            //var enumerator = lines.GetEnumerator();
            //var listEnumerator = dataLines.GetEnumerator();
            //foreach (string line in lines)
            //{ 
            //    string[] values = line.Split(',').Select(x => x.Trim()).ToArray();
                
            //    //value[0] = currentFormat.dataHeading[0],...then you can write a switch statement to process data based on 
            //    //the currentFormat
            //        for (int i = 0; i < 4; i++)
            //        {
            //            currentFormat.Add(values[i], null);
            //        }
            //}
            //foreach (var line in dataLines)
            //{
            //    List<string> dataStrings = line.Split(',').Select(x => x.Trim()).ToList();
            //    foreach (var ds in dataStrings)
            //    {
            //        currentFormat.Add(ds, null);
            //    }
            //}

        }

        public void WriteUser(BindingList<UserModel> updatedUserList)
        {
            //Just overwrite the file with the new list
            StringBuilder sb = new StringBuilder("FirstName,LastName,Age,IsAlive");
            sb.Append("\r\n");
            //System.IO.File.WriteAllText(@"StandardDataSet.csv", "FirstName,LastName,Age,IsAlive");
            foreach (UserModel user in updatedUserList)
            {
                
                sb.Append(user.FirstName);
                sb.Append(',');
                sb.Append(user.LastName);
                sb.Append(',');
                sb.Append(user.Age);
                sb.Append(',');
                if (user.IsAlive == true)
            {
                
                sb.Append('1');
            }
            else sb.Append('0');
                sb.Append("\r\n");
            }
            // Got it from https://stackoverflow.com/questions/10745542/object-to-string-array Don't fully understand it
            // Have to read more about how it works
            //string[] lines = ((IEnumerable)userUpdate).Cast<object>().Select(x => x.ToString()).ToArray();
            System.IO.File.WriteAllText(@"StandardDataSet.csv", sb.ToString());
            
            //System.IO.File.WriteAllLines(@"StandardDataSet.csv", lines);
            
        }
    }

    public static class Extensions
    {
        public static Boolean ToBoolean(this string str)
        {
            try { return Convert.ToBoolean(str); }
            catch
            {

            }
            try
            {
                return Convert.ToBoolean(Convert.ToInt32(str));
            }
            catch
            {

            }
            return false;
        }
    }
}
