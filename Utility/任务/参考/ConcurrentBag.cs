using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Utility.任务.参考
{
    public class Test_ConcurrentBag
    {
        // Demonstrates:
        //      ConcurrentBag<T>.Add()
        //      ConcurrentBag<T>.IsEmpty
        //      ConcurrentBag<T>.TryTake()
        //      ConcurrentBag<T>.TryPeek()
        public static void Main()
        {
            // Construct and populate the ConcurrentBag
            ConcurrentBag<int> cb = new ConcurrentBag<int>();
            cb.Add(1);
            cb.Add(2);
            cb.Add(3);

            // Consume the items in the bag
            int item;
            while (!cb.IsEmpty)
            {
                if (cb.TryTake(out item))
                    Console.WriteLine(item);
                else
                    Console.WriteLine("TryTake failed for non-empty bag");
            }

            // Bag should be empty at this point
            if (cb.TryPeek(out item))
                Console.WriteLine("TryPeek succeeded for empty bag!");
        }

    }
}
