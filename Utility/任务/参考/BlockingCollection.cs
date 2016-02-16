using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utility.任务.参考
{

    /*
     BlockingCollection<T> 是一个线程安全集合类，可提供以下功能：

        实现制造者-使用者模式。
        同时在多个线程中添加和取出项。
        可选的最大容量。
        可在集合为空或已满时发生阻塞的插入和移除操作。
        不会发生阻塞或只在指定的时间段内发生阻塞的“尝试”插入和移除操作。
        封装实现 IProducerConsumerCollection<T> 的任何集合类型
        使用取消标记执行取消操作。
        使用 foreach（在 Visual Basic 中为 For Each）的两类枚举：
            只读枚举。
            在枚举项时将项移除的枚举。

     默认情况下， System.Collections.Concurrent.BlockingCollection<T> 的存储为 System.Collections.Concurrent.ConcurrentQueue<T>。
     */

    /// <summary>
    /// 如何：在 BlockingCollection 中逐个添加和取出项
    /// 
    /// 此第一个示例演示如何添加和取出项，以便相关操作在以下情况下发生阻塞：集合临时为空（在取出项时）或达到最大容量（在添加项时），或者已超过指定的超时时间。 
    /// 请注意，只有在创建 BlockingCollection 时在构造函数中指定了最大容量的情况下，才支持在达到最大容量时进行阻塞。
    /// </summary>
    public class Test_BlockingCollection
    {
        public static void Main(string[] args)
        {
            // Increase or decrease this value as desired.
            int itemsToAdd = 500;

            // Preserve all the display output for Adds and Takes
            Console.SetBufferSize(80, (itemsToAdd * 2) + 3);

            // A bounded collection. Increase, decrease, or remove the 
            // maximum capacity argument to see how it impacts behavior.
            BlockingCollection<int> numbers = new BlockingCollection<int>(50);


            // A simple blocking consumer with no cancellation.
            Task.Factory.StartNew(() =>
            {
                int i = -1;
                while (!numbers.IsCompleted)
                {
                    try
                    {
                        i = numbers.Take();
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("Adding was compeleted!");
                        break;
                    }
                    Console.WriteLine("Take:{0} ", i);

                    // Simulate a slow consumer. This will cause
                    // collection to fill up fast and thus Adds wil block.
                    Thread.SpinWait(100000);
                }

                Console.WriteLine("\r\nNo more items to take. Press the Enter key to exit.");
            });

            // A simple blocking producer with no cancellation.
            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < itemsToAdd; i++)
                {
                    numbers.Add(i);
                    Console.WriteLine("Add:{0} Count={1}", i, numbers.Count);
                }

                // See documentation for this method.
                numbers.CompleteAdding();
            });

            // Keep the console display open in debug mode.

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 如何：使用 ForEach 移除 BlockingCollection 中的项
    /// 
    /// 除了使用 Take 和 TryTake 方法从 BlockingCollection<T> 中提取项之外，还可以使用foreach（在 Visual Basic 中为 For Each）移除项，直至添加完成并且集合为空。 
    /// 由于与典型的 foreach (For Each) 循环不同，此枚举器通过移除项来修改源集合，因此将其称作“转变枚举”或“使用枚举”。
    /// 此示例将 foreach 循环与使用线程中的 BlockingCollection<T>.GetConsumingEnumerable 方法结合使用，这会导致在枚举每个项时将其从集合中移除。 
    /// System.Collections.Concurrent.BlockingCollection<T> 可限制集合中任意时间所包含的最大项数。按照此方式枚举集合会在没有项可用或集合为空时阻塞使用者线程。 
    /// 在此示例中，由于制造者线程添加项的速度快于使用项的速度，因此不需要考虑阻塞问题。
    /// 不能保证按照制造者线程添加项的顺序来枚举这些项。
    /// 若要枚举集合而不对其进行修改，只需使用 foreach (For Each) 即可，而无需使用 GetConsumingEnumerable 方法。 
    /// 但是，一定要了解，此类枚举表示某个准确时间点的集合快照。 如果其他线程在您执行循环时同时添加或移除项，则循环可能不会表示集合的实际状态。
    /// </summary>
    public class Test_BlockingCollection_GetConsumingEnumerable
    {

        // Limit the collection size to 2000 items
        // at any given time. Set itemsToProduce to >500
        // to hit the limit.
        const int upperLimit = 1000;

        // Adjust this number to see how it impacts
        // the producing-consuming pattern.
        const int itemsToProduce = 100;

        static BlockingCollection<long> collection = new BlockingCollection<long>(upperLimit);

        // Variables for diagnostic output only.
        static Stopwatch sw = new Stopwatch();
        static int totalAdditions = 0;

        //// Counter for synchronizing producers.
        //static int producersStillRunning = 2;

        public static void Main(string[] args)
        {
            // Start the stopwatch.
            sw.Start();

            // Queue the Producer threads. Store in an array
            // for use with ContinueWhenAll
            Task[] producers = new Task[2];
            producers[0] = Task.Factory.StartNew(() => RunProducer("A", 0));
            producers[1] = Task.Factory.StartNew(() => RunProducer("B", itemsToProduce));

            // Create a cleanup task that will call CompleteAdding after
            // all producers are done adding items.
            Task cleanup = Task.Factory.ContinueWhenAll(producers, (p) => collection.CompleteAdding());

            // Queue the Consumer thread. Put this call
            // before Parallel.Invoke to begin consuming as soon as
            // the producers add items.
            Task.Factory.StartNew(() => RunConsumer());

            // Keep the console window open while the
            // consumer thread completes its output.
            Console.ReadKey();

        }

        static void RunProducer(string ID, int start)
        {

            int additions = 0;
            for (int i = start; i < start + itemsToProduce; i++)
            {
                // The data that is added to the collection.
                long ticks = sw.ElapsedTicks;

                // Display additions and subtractions.
                Console.WriteLine("{0} adding tick value {1}. item# {2}", ID, ticks, i);

                if (!collection.IsAddingCompleted)
                    collection.Add(ticks);

                // Counter for demonstration purposes only.
                additions++;

                // Uncomment this line to 
                // slow down the producer threads     ing.
                Thread.SpinWait(100000);
            }


            Interlocked.Add(ref totalAdditions, additions);
            Console.WriteLine("{0} is done adding: {1} items", ID, additions);
        }


        static void RunConsumer()
        {
            // GetConsumingEnumerable returns the enumerator for the 
            // underlying collection.
            int subtractions = 0;
            foreach (var item in collection.GetConsumingEnumerable())
            {
                Console.WriteLine("Consuming tick value {0} : item# {1} : current count = {2}",
                        item.ToString("D18"), subtractions++, collection.Count);
            }

            Console.WriteLine("Total added: {0} Total consumed: {1} Current count: {2} ",
                                totalAdditions, subtractions, collection.Count());
            sw.Stop();

            Console.WriteLine("Press any key to exit");
        }
    }

    /// <summary>
    /// 如何：在 BlockingCollection 中逐个添加和取出项
    /// 
    /// 此第二个示例演示如何添加和取出项，以便相关操作不会发生阻塞。 
    /// 如果集合中不存在任何项、已达到有限集合的最大容量或超时时间已到，则 TryAdd 或 TryTake 操作将返回 false。 
    /// 这将允许线程执行一段时间的其他有用工作，过一会再重新尝试检索新项或尝试添加之前未能添加的相同项。 
    /// 该程序还演示如何在访问 BlockingCollection<T> 时实现取消。
    /// </summary>
    public class Test_BlockingCollection_CancellationTokenSource
    {
        static int inputs = 2000;

        public static void Main(string[] args)
        {
            // The token source for issuing the cancelation request.
            CancellationTokenSource cts = new CancellationTokenSource();

            // A blocking collection that can hold no more than 100 items at a time.
            BlockingCollection<int> numberCollection = new BlockingCollection<int>(100);

            // Set console buffer to hold our prodigious output.
            Console.SetBufferSize(80, 2000);

            // The simplest UI thread ever invented.
            Task.Factory.StartNew(() =>
            {
                if (Console.ReadKey().KeyChar == 'c')
                    cts.Cancel();
            });

            // Start one producer and one consumer.
            Task.Factory.StartNew(() => NonBlockingConsumer(numberCollection, cts.Token));
            Task.Factory.StartNew(() => NonBlockingProducer(numberCollection, cts.Token));


            Console.WriteLine("Press the Enter key to exit.");
            Console.ReadLine();
        }

        static void NonBlockingConsumer(BlockingCollection<int> bc, CancellationToken ct)
        {
            // IsCompleted == (IsAddingCompleted && Count == 0)
            while (!bc.IsCompleted)
            {
                int nextItem = 0;
                try
                {
                    if (!bc.TryTake(out nextItem, 0, ct))
                    {
                        Console.WriteLine(" Take Blocked");
                    }
                    else
                    {
                        Console.WriteLine(" Take:{0}", nextItem);
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Taking canceled.");
                    break;
                }

                // Slow down consumer just a little to cause
                // collection to fill up faster, and lead to "AddBlocked"
                Thread.SpinWait(500000);
            }

            Console.WriteLine("\r\nNo more items to take. Press the Enter key to exit.");
        }

        static void NonBlockingProducer(BlockingCollection<int> bc, CancellationToken ct)
        {
            int itemToAdd = 0;
            bool success = false;

            do
            {
                // Cancellation causes OCE. We know how to handle it.
                try
                {
                    // A shorter timeout causes more failures.
                    success = bc.TryAdd(itemToAdd, 2, ct);
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Add loop canceled.");
                    // Let other threads know we're done in case
                    // they aren't monitoring the cancellation token.
                    bc.CompleteAdding();
                    break;
                }

                if (success)
                {
                    Console.WriteLine(" Add:{0}", itemToAdd);
                    itemToAdd++;
                }
                else
                {
                    Console.Write(" AddBlocked:{0} Count = {1}", itemToAdd.ToString(), bc.Count);
                    // Don't increment nextItem. Try again on next iteration.

                    //Do something else useful instead.
                    UpdateProgress(itemToAdd);
                }

            } while (itemToAdd < inputs);

            // No lock required here because only one producer.
            bc.CompleteAdding();
        }

        static void UpdateProgress(int i)
        {
            double percent = ((double)i / inputs) * 100;
            Console.WriteLine("Percent complete: {0}", percent);
        }
    }

}
