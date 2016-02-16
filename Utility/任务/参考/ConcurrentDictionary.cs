using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Utility.任务.参考
{
    /*
     ConcurrentDictionary<TKey, TValue> 提供了多个便捷的方法，这些方法使代码在尝试添加或移除数据之前无需先检查键是否存在。下表列出了这些便捷的方法，并说明在何种情况下使用这些方法。
        方法                                                 在以下情况下使用
        AddOrUpdate                                          您需要为指定的键添加新值，如果此键已存在，则需要替换其值。
        GetOrAdd                                             您需要检索指定的键的现有值，如果此键不存在，则需要指定一个键/值对。
        TryAdd, TryGetValue , TryUpdate , TryRemove          您需要添加、获取、更新或移除键/值对，如果此键已存在或因任何其他原因导致尝试失败，则需要执行一些备选操作。

     * 
    ConcurrentDictionary<TKey, TValue> 是为多线程方案而设计的。无需在代码中使用锁定即可在集合中添加或移除项。 
    但始终有可能出现以下情况：一个线程检索一个值，而另一个线程通过为同一个键赋予一个新值来立即更新集合。
    而且，尽管 ConcurrentDictionary<TKey, TValue> 的所有方法都是线程安全的，但并非所有方法都是原子的，尤其是 GetOrAdd 和 AddOrUpdate。 
    传递给这些方法的用户委托将在词典的内部锁之外调用。 （这样做是为了防止未知代码阻止所有线程。）因此，可能发生以下事件序列：

    1) threadA 调用 GetOrAdd，未找到项，通过调用 valueFactory 委托创建要添加的新项。
    2) threadB 并发调用 GetOrAdd，其 valueFactory 委托受到调用，并且它在 threadA 之前到达内部锁，并将它的新键-值对添加到词典中。
    3) threadA 的用户委托完成，线程到达锁，但现在发现该项已存在
    4) threadA 执行“Get”，返回之前由 threadB 添加的数据。

    因此，无法保证 GetOrAdd 返回的数据与线程的 valueFactory 创建的数据相同。 调用 AddOrUpdate 时可能发生相似的事件序列。
     */

    /// <summary>
    /// 下面的示例使用两个 Task 实例同时向 ConcurrentDictionary<TKey, TValue> 添加一些元素，然后输出所有内容以说明已成功添加元素。
    /// 此示例还演示如何使用 AddOrUpdate、 TryGetValue、 GetOrAdd 和 TryRemove 方法在集合中添加、更新、检索和移除项。
    /// </summary>
    public class Test_ConcurrentDictionary
    {
        class CityInfo : IEqualityComparer<CityInfo>
        {
            public string Name { get; set; }
            public DateTime lastQueryDate { get; set; }
            public decimal Longitude { get; set; }
            public decimal Latitude { get; set; }
            public int[] RecentHighTemperatures { get; set; }

            public CityInfo(string name, decimal longitude, decimal latitude, int[] temps)
            {
                Name = name;
                lastQueryDate = DateTime.Now;
                Longitude = longitude;
                Latitude = latitude;
                RecentHighTemperatures = temps;
            }

            public CityInfo()
            {
            }

            public CityInfo(string key)
            {
                Name = key;
                // MaxValue means "not initialized"
                Longitude = Decimal.MaxValue;
                Latitude = Decimal.MaxValue;
                lastQueryDate = DateTime.Now;
                RecentHighTemperatures = new int[] { 0 };

            }
            public bool Equals(CityInfo x, CityInfo y)
            {
                return x.Name == y.Name && x.Longitude == y.Longitude && x.Latitude == y.Latitude;
            }

            public int GetHashCode(CityInfo obj)
            {
                CityInfo ci = (CityInfo)obj;
                return ci.Name.GetHashCode();
            }
        }

        // Create a new concurrent dictionary.
        static ConcurrentDictionary<string, CityInfo> cities = new ConcurrentDictionary<string, CityInfo>();

        public static void Main(string[] args)
        {
            CityInfo[] data = 
            {
                new CityInfo(){ Name = "Boston", Latitude = 42.358769M, Longitude = -71.057806M, RecentHighTemperatures = new int[] {56, 51, 52, 58, 65, 56,53}},
                new CityInfo(){ Name = "Miami", Latitude = 25.780833M, Longitude = -80.195556M, RecentHighTemperatures = new int[] {86,87,88,87,85,85,86}},
                new CityInfo(){ Name = "Los Angeles", Latitude = 34.05M, Longitude = -118.25M, RecentHighTemperatures =   new int[] {67,68,69,73,79,78,78}},
                new CityInfo(){ Name = "Seattle", Latitude = 47.609722M, Longitude =  -122.333056M, RecentHighTemperatures =   new int[] {49,50,53,47,52,52,51}},
                new CityInfo(){ Name = "Toronto", Latitude = 43.716589M, Longitude = -79.340686M, RecentHighTemperatures =   new int[] {53,57, 51,52,56,55,50}},
                new CityInfo(){ Name = "Mexico City", Latitude = 19.432736M, Longitude = -99.133253M, RecentHighTemperatures =   new int[] {72,68,73,77,76,74,73}},
                new CityInfo(){ Name = "Rio de Janiero", Latitude = -22.908333M, Longitude = -43.196389M, RecentHighTemperatures =   new int[] {72,68,73,82,84,78,84}},
                new CityInfo(){ Name = "Quito", Latitude = -0.25M, Longitude = -78.583333M, RecentHighTemperatures =   new int[] {71,69,70,66,65,64,61}}
            };

            // Add some key/value pairs from multiple threads.
            Task[] tasks = new Task[2];

            tasks[0] = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 2; i++)
                {
                    if (cities.TryAdd(data[i].Name, data[i]))
                        Console.WriteLine("Added {0} on thread {1}", data[i],
                            Thread.CurrentThread.ManagedThreadId);
                    else
                        Console.WriteLine("Could not add {0}", data[i]);
                }
            });

            tasks[1] = Task.Factory.StartNew(() =>
            {
                for (int i = 2; i < data.Length; i++)
                {
                    if (cities.TryAdd(data[i].Name, data[i]))
                        Console.WriteLine("Added {0} on thread {1}", data[i],
                            Thread.CurrentThread.ManagedThreadId);
                    else
                        Console.WriteLine("Could not add {0}", data[i]);
                }
            });

            // Output results so far.
            Task.WaitAll(tasks);

            // Enumerate collection from the app main thread.
            // Note that ConcurrentDictionary is the one concurrent collection
            // that does not support thread-safe enumeration.
            foreach (var city in cities)
            {
                Console.WriteLine("{0} has been added.", city.Key);
            }

            AddOrUpdateWithoutRetrieving();
            RetrieveValueOrAdd();
            RetrieveAndUpdateOrAdd();

            Console.WriteLine("Press any key.");
            Console.ReadKey();
        }

        // This method shows how to add key-value pairs to the dictionary
        // in scenarios where the key might already exist.
        private static void AddOrUpdateWithoutRetrieving()
        {
            // Sometime later. We receive new data from some source.
            CityInfo ci = new CityInfo()
            {
                Name = "Toronto",
                Latitude = 43.716589M,
                Longitude = -79.340686M,
                RecentHighTemperatures = new int[] { 54, 59, 67, 82, 87, 55, -14 }
            };

            // Try to add data. If it doesn't exist, the object ci is added. If it does
            // already exist, update existingVal according to the custom logic in the 
            // delegate.
            cities.AddOrUpdate(ci.Name, ci,
                (key, existingVal) =>
                {
                    // If this delegate is invoked, then the key already exists.
                    // Here we make sure the city really is the same city we already have.
                    // (Support for multiple cities of the same name is left as an exercise for the reader.)
                    if (ci != existingVal)
                        throw new ArgumentException("Duplicate city names are not allowed: {0}.", ci.Name);

                    // The only updatable fields are the temerature array and lastQueryDate.
                    existingVal.lastQueryDate = DateTime.Now;
                    existingVal.RecentHighTemperatures = ci.RecentHighTemperatures;
                    return existingVal;
                });

            // Verify that the dictionary contains the new or updated data.
            Console.Write("Most recent high temperatures for {0} are: ", cities[ci.Name].Name);
            int[] temps = cities[ci.Name].RecentHighTemperatures;
            foreach (var temp in temps) Console.Write("{0}, ", temp);
            Console.WriteLine();
        }

        // This method shows how to use data and ensure that it has been
        // added to the dictionary.
        private static void RetrieveValueOrAdd()
        {
            string searchKey = "Caracas";
            CityInfo retrievedValue = null;

            try
            {
                retrievedValue = cities.GetOrAdd(searchKey, GetDataForCity(searchKey));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            // Use the data.
            if (retrievedValue != null)
            {
                Console.Write("Most recent high temperatures for {0} are: ", retrievedValue.Name);
                int[] temps = cities[retrievedValue.Name].RecentHighTemperatures;
                foreach (var temp in temps) Console.Write("{0}, ", temp);
            }
            Console.WriteLine();
        }

        // This method shows how to retrieve a value from the dictionary,
        // when you expect that the key/value pair already exists,
        // and then possibly update the dictionary with a new value for the key.
        private static void RetrieveAndUpdateOrAdd()
        {
            CityInfo retrievedValue;
            string searchKey = "Buenos Aires";

            if (cities.TryGetValue(searchKey, out retrievedValue))
            {
                // use the data
                Console.Write("Most recent high temperatures for {0} are: ", retrievedValue.Name);
                int[] temps = retrievedValue.RecentHighTemperatures;
                foreach (var temp in temps) Console.Write("{0}, ", temp);

                // Make a copy of the data. Our object will update its lastQueryDate automatically.
                CityInfo newValue = new CityInfo(retrievedValue.Name,
                                                retrievedValue.Longitude,
                                                retrievedValue.Latitude,
                                                retrievedValue.RecentHighTemperatures);

                // Replace the old value with the new value.
                if (!cities.TryUpdate(searchKey, retrievedValue, newValue))
                {
                    //The data was not updated. Log error, throw exception, etc.
                    Console.WriteLine("Could not update {0}", retrievedValue.Name);
                }
            }
            else
            {
                // Add the new key and value. Here we call a method to retrieve
                // the data. Another option is to add a default value here and 
                // update with real data later on some other thread.
                CityInfo newValue = GetDataForCity(searchKey);
                if (cities.TryAdd(searchKey, newValue))
                {
                    // use the data
                    Console.Write("Most recent high temperatures for {0} are: ", newValue.Name);
                    int[] temps = newValue.RecentHighTemperatures;
                    foreach (var temp in temps) Console.Write("{0}, ", temp);
                }
                else
                    Console.WriteLine("Unable to add data for {0}", searchKey);
            }
        }

        //Assume this method knows how to find long/lat/temp info for any specified city.
        static CityInfo GetDataForCity(string name)
        {
            // Real implementation left as exercise for the reader.
            if (String.CompareOrdinal(name, "Caracas") == 0)
                return new CityInfo()
                {
                    Name = "Caracas",
                    Longitude = 10.5M,
                    Latitude = -66.916667M,
                    RecentHighTemperatures = new int[] { 91, 89, 91, 91, 87, 90, 91 }
                };
            else if (String.CompareOrdinal(name, "Buenos Aires") == 0)
                return new CityInfo()
                {
                    Name = "Buenos Aires",
                    Longitude = -34.61M,
                    Latitude = -58.369997M,
                    RecentHighTemperatures = new int[] { 80, 86, 89, 91, 84, 86, 88 }
                };
            else
                throw new ArgumentException("Cannot find any data for {0}", name);
        }
    }
}
