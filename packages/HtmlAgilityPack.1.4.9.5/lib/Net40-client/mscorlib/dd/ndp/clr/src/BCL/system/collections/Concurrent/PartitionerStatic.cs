// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.Partitioner
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
  /// <summary>提供针对数组、列表和可枚举项的常见分区策略。</summary>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public static class Partitioner
  {
    private const int DEFAULT_BYTES_PER_CHUNK = 512;

    /// <summary>从 <see cref="T:System.Collections.Generic.IList`1" /> 实例创建一个可排序分区程序。</summary>
    /// <returns>基于输入列表的可排序分区程序。</returns>
    /// <param name="list">要进行分区的列表。</param>
    /// <param name="loadBalance">一个布尔值，该值指示创建的分区程序是否应在各分区之间保持动态负载平衡，而不是静态负载平衡。</param>
    /// <typeparam name="TSource">源列表中的元素的类型。</typeparam>
    [__DynamicallyInvokable]
    public static OrderablePartitioner<TSource> Create<TSource>(IList<TSource> list, bool loadBalance)
    {
      if (list == null)
        throw new ArgumentNullException("list");
      if (loadBalance)
        return (OrderablePartitioner<TSource>) new Partitioner.DynamicPartitionerForIList<TSource>(list);
      return (OrderablePartitioner<TSource>) new Partitioner.StaticIndexRangePartitionerForIList<TSource>(list);
    }

    /// <summary>从 <see cref="T:System.Array" /> 实例创建一个可排序分区程序。</summary>
    /// <returns>基于输入数组的可排序分区程序。</returns>
    /// <param name="array">要进行分区的数组。</param>
    /// <param name="loadBalance">一个布尔值，该值指示创建的分区程序是否应在各分区之间保持动态负载平衡，而不是静态负载平衡。</param>
    /// <typeparam name="TSource">源数组中的元素的类型。</typeparam>
    [__DynamicallyInvokable]
    public static OrderablePartitioner<TSource> Create<TSource>(TSource[] array, bool loadBalance)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (loadBalance)
        return (OrderablePartitioner<TSource>) new Partitioner.DynamicPartitionerForArray<TSource>(array);
      return (OrderablePartitioner<TSource>) new Partitioner.StaticIndexRangePartitionerForArray<TSource>(array);
    }

    /// <summary>从 <see cref="T:System.Collections.Generic.IEnumerable`1" /> 实例创建一个可排序分区程序。</summary>
    /// <returns>基于输入数组的可排序分区程序。</returns>
    /// <param name="source">要进行分区的可枚举项。</param>
    /// <typeparam name="TSource">源可枚举项中的元素的类型。</typeparam>
    [__DynamicallyInvokable]
    public static OrderablePartitioner<TSource> Create<TSource>(IEnumerable<TSource> source)
    {
      return Partitioner.Create<TSource>(source, EnumerablePartitionerOptions.None);
    }

    /// <summary>从 <see cref="T:System.Collections.Generic.IEnumerable`1" /> 实例创建一个可排序分区程序。</summary>
    /// <returns>基于输入数组的可排序分区程序。</returns>
    /// <param name="source">要进行分区的可枚举项。</param>
    /// <param name="partitionerOptions">控制分区缓冲行为的选项。</param>
    /// <typeparam name="TSource">源可枚举项中的元素的类型。</typeparam>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="partitionerOptions" /> 参数为 <see cref="T:System.Collections.Concurrent.EnumerablePartitionerOptions" /> 指定无效值。</exception>
    [__DynamicallyInvokable]
    public static OrderablePartitioner<TSource> Create<TSource>(IEnumerable<TSource> source, EnumerablePartitionerOptions partitionerOptions)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if ((partitionerOptions & ~EnumerablePartitionerOptions.NoBuffering) != EnumerablePartitionerOptions.None)
        throw new ArgumentOutOfRangeException("partitionerOptions");
      return (OrderablePartitioner<TSource>) new Partitioner.DynamicPartitionerForIEnumerable<TSource>(source, partitionerOptions);
    }

    /// <summary>创建一个按用户指定的范围划分区块的分区程序。</summary>
    /// <returns>一个分区程序。</returns>
    /// <param name="fromInclusive">范围下限（含）。</param>
    /// <param name="toExclusive">范围上限（不含）。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="toExclusive" /> 参数小于或等于 <paramref name="fromInclusive" /> 参数。</exception>
    [__DynamicallyInvokable]
    public static OrderablePartitioner<Tuple<long, long>> Create(long fromInclusive, long toExclusive)
    {
      int num = 3;
      if (toExclusive <= fromInclusive)
        throw new ArgumentOutOfRangeException("toExclusive");
      long rangeSize = (toExclusive - fromInclusive) / (long) (PlatformHelper.ProcessorCount * num);
      if (rangeSize == 0L)
        rangeSize = 1L;
      return Partitioner.Create<Tuple<long, long>>(Partitioner.CreateRanges(fromInclusive, toExclusive, rangeSize), EnumerablePartitionerOptions.NoBuffering);
    }

    /// <summary>创建一个按用户指定的范围划分区块的分区程序。</summary>
    /// <returns>一个分区程序。</returns>
    /// <param name="fromInclusive">范围下限（含）。</param>
    /// <param name="toExclusive">范围上限（不含）。</param>
    /// <param name="rangeSize">每个子范围的大小。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="toExclusive" /> 参数小于或等于 <paramref name="fromInclusive" /> 参数。- 或 -<paramref name="rangeSize" /> 参数小于或等于 0。</exception>
    [__DynamicallyInvokable]
    public static OrderablePartitioner<Tuple<long, long>> Create(long fromInclusive, long toExclusive, long rangeSize)
    {
      if (toExclusive <= fromInclusive)
        throw new ArgumentOutOfRangeException("toExclusive");
      if (rangeSize <= 0L)
        throw new ArgumentOutOfRangeException("rangeSize");
      return Partitioner.Create<Tuple<long, long>>(Partitioner.CreateRanges(fromInclusive, toExclusive, rangeSize), EnumerablePartitionerOptions.NoBuffering);
    }

    private static IEnumerable<Tuple<long, long>> CreateRanges(long fromInclusive, long toExclusive, long rangeSize)
    {
      bool shouldQuit = false;
      long i = fromInclusive;
      while (i < toExclusive && !shouldQuit)
      {
        long num1 = i;
        long num2;
        try
        {
          num2 = checked (i + rangeSize);
        }
        catch (OverflowException ex)
        {
          num2 = toExclusive;
          shouldQuit = true;
        }
        if (num2 > toExclusive)
          num2 = toExclusive;
        yield return new Tuple<long, long>(num1, num2);
        i += rangeSize;
      }
    }

    /// <summary>创建一个按用户指定的范围划分区块的分区程序。</summary>
    /// <returns>一个分区程序。</returns>
    /// <param name="fromInclusive">范围下限（含）。</param>
    /// <param name="toExclusive">范围上限（不含）。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="toExclusive" /> 参数小于或等于 <paramref name="fromInclusive" /> 参数。</exception>
    [__DynamicallyInvokable]
    public static OrderablePartitioner<Tuple<int, int>> Create(int fromInclusive, int toExclusive)
    {
      int num = 3;
      if (toExclusive <= fromInclusive)
        throw new ArgumentOutOfRangeException("toExclusive");
      int rangeSize = (toExclusive - fromInclusive) / (PlatformHelper.ProcessorCount * num);
      if (rangeSize == 0)
        rangeSize = 1;
      return Partitioner.Create<Tuple<int, int>>(Partitioner.CreateRanges(fromInclusive, toExclusive, rangeSize), EnumerablePartitionerOptions.NoBuffering);
    }

    /// <summary>创建一个按用户指定的范围划分区块的分区程序。</summary>
    /// <returns>一个分区程序。</returns>
    /// <param name="fromInclusive">范围下限（含）。</param>
    /// <param name="toExclusive">范围上限（不含）。</param>
    /// <param name="rangeSize">每个子范围的大小。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="toExclusive" /> 参数小于或等于 <paramref name="fromInclusive" /> 参数。- 或 -<paramref name="rangeSize" /> 参数小于或等于 0。</exception>
    [__DynamicallyInvokable]
    public static OrderablePartitioner<Tuple<int, int>> Create(int fromInclusive, int toExclusive, int rangeSize)
    {
      if (toExclusive <= fromInclusive)
        throw new ArgumentOutOfRangeException("toExclusive");
      if (rangeSize <= 0)
        throw new ArgumentOutOfRangeException("rangeSize");
      return Partitioner.Create<Tuple<int, int>>(Partitioner.CreateRanges(fromInclusive, toExclusive, rangeSize), EnumerablePartitionerOptions.NoBuffering);
    }

    private static IEnumerable<Tuple<int, int>> CreateRanges(int fromInclusive, int toExclusive, int rangeSize)
    {
      bool shouldQuit = false;
      int i = fromInclusive;
      while (i < toExclusive && !shouldQuit)
      {
        int num1 = i;
        int num2;
        try
        {
          num2 = checked (i + rangeSize);
        }
        catch (OverflowException ex)
        {
          num2 = toExclusive;
          shouldQuit = true;
        }
        if (num2 > toExclusive)
          num2 = toExclusive;
        yield return new Tuple<int, int>(num1, num2);
        i += rangeSize;
      }
    }

    private static int GetDefaultChunkSize<TSource>()
    {
      return !typeof (TSource).IsValueType ? 512 / IntPtr.Size : (typeof (TSource).StructLayoutAttribute.Value != LayoutKind.Explicit ? 128 : Math.Max(1, 512 / Marshal.SizeOf(typeof (TSource))));
    }

    private abstract class DynamicPartitionEnumerator_Abstract<TSource, TSourceReader> : IEnumerator<KeyValuePair<long, TSource>>, IDisposable, IEnumerator
    {
      protected static int s_defaultMaxChunkSize = Partitioner.GetDefaultChunkSize<TSource>();
      protected readonly TSourceReader m_sharedReader;
      protected Partitioner.SharedInt m_currentChunkSize;
      protected Partitioner.SharedInt m_localOffset;
      private const int CHUNK_DOUBLING_RATE = 3;
      private int m_doublingCountdown;
      protected readonly int m_maxChunkSize;
      protected readonly Partitioner.SharedLong m_sharedIndex;

      protected abstract bool HasNoElementsLeft { get; set; }

      public abstract KeyValuePair<long, TSource> Current { get; }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.Current;
        }
      }

      protected DynamicPartitionEnumerator_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex)
        : this(sharedReader, sharedIndex, false)
      {
      }

      protected DynamicPartitionEnumerator_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex, bool useSingleChunking)
      {
        this.m_sharedReader = sharedReader;
        this.m_sharedIndex = sharedIndex;
        this.m_maxChunkSize = useSingleChunking ? 1 : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, TSourceReader>.s_defaultMaxChunkSize;
      }

      protected abstract bool GrabNextChunk(int requestedChunkSize);

      public abstract void Dispose();

      public void Reset()
      {
        throw new NotSupportedException();
      }

      public bool MoveNext()
      {
        if (this.m_localOffset == null)
        {
          this.m_localOffset = new Partitioner.SharedInt(-1);
          this.m_currentChunkSize = new Partitioner.SharedInt(0);
          this.m_doublingCountdown = 3;
        }
        if (this.m_localOffset.Value < this.m_currentChunkSize.Value - 1)
        {
          ++this.m_localOffset.Value;
          return true;
        }
        int requestedChunkSize;
        if (this.m_currentChunkSize.Value == 0)
          requestedChunkSize = 1;
        else if (this.m_doublingCountdown > 0)
        {
          requestedChunkSize = this.m_currentChunkSize.Value;
        }
        else
        {
          requestedChunkSize = Math.Min(this.m_currentChunkSize.Value * 2, this.m_maxChunkSize);
          this.m_doublingCountdown = 3;
        }
        this.m_doublingCountdown = this.m_doublingCountdown - 1;
        if (!this.GrabNextChunk(requestedChunkSize))
          return false;
        this.m_localOffset.Value = 0;
        return true;
      }
    }

    private class DynamicPartitionerForIEnumerable<TSource> : OrderablePartitioner<TSource>
    {
      private IEnumerable<TSource> m_source;
      private readonly bool m_useSingleChunking;

      public override bool SupportsDynamicPartitions
      {
        get
        {
          return true;
        }
      }

      internal DynamicPartitionerForIEnumerable(IEnumerable<TSource> source, EnumerablePartitionerOptions partitionerOptions)
        : base(true, false, true)
      {
        this.m_source = source;
        this.m_useSingleChunking = (uint) (partitionerOptions & EnumerablePartitionerOptions.NoBuffering) > 0U;
      }

      public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
      {
        if (partitionCount <= 0)
          throw new ArgumentOutOfRangeException("partitionCount");
        IEnumerator<KeyValuePair<long, TSource>>[] enumeratorArray = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
        IEnumerable<KeyValuePair<long, TSource>> keyValuePairs = (IEnumerable<KeyValuePair<long, TSource>>) new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable(this.m_source.GetEnumerator(), this.m_useSingleChunking, true);
        for (int index = 0; index < partitionCount; ++index)
          enumeratorArray[index] = keyValuePairs.GetEnumerator();
        return (IList<IEnumerator<KeyValuePair<long, TSource>>>) enumeratorArray;
      }

      public override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
      {
        return (IEnumerable<KeyValuePair<long, TSource>>) new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable(this.m_source.GetEnumerator(), this.m_useSingleChunking, false);
      }

      private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable, IDisposable
      {
        private readonly IEnumerator<TSource> m_sharedReader;
        private Partitioner.SharedLong m_sharedIndex;
        private volatile KeyValuePair<long, TSource>[] m_FillBuffer;
        private volatile int m_FillBufferSize;
        private volatile int m_FillBufferCurrentPosition;
        private volatile int m_activeCopiers;
        private Partitioner.SharedBool m_hasNoElementsLeft;
        private Partitioner.SharedBool m_sourceDepleted;
        private object m_sharedLock;
        private bool m_disposed;
        private Partitioner.SharedInt m_activePartitionCount;
        private readonly bool m_useSingleChunking;

        internal InternalPartitionEnumerable(IEnumerator<TSource> sharedReader, bool useSingleChunking, bool isStaticPartitioning)
        {
          this.m_sharedReader = sharedReader;
          this.m_sharedIndex = new Partitioner.SharedLong(-1L);
          this.m_hasNoElementsLeft = new Partitioner.SharedBool(false);
          this.m_sourceDepleted = new Partitioner.SharedBool(false);
          this.m_sharedLock = new object();
          this.m_useSingleChunking = useSingleChunking;
          if (!this.m_useSingleChunking)
            this.m_FillBuffer = new KeyValuePair<long, TSource>[(PlatformHelper.ProcessorCount > 4 ? 4 : 1) * Partitioner.GetDefaultChunkSize<TSource>()];
          if (isStaticPartitioning)
            this.m_activePartitionCount = new Partitioner.SharedInt(0);
          else
            this.m_activePartitionCount = (Partitioner.SharedInt) null;
        }

        public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
        {
          if (this.m_disposed)
            throw new ObjectDisposedException(Environment.GetResourceString("PartitionerStatic_CanNotCallGetEnumeratorAfterSourceHasBeenDisposed"));
          return (IEnumerator<KeyValuePair<long, TSource>>) new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerator(this.m_sharedReader, this.m_sharedIndex, this.m_hasNoElementsLeft, this.m_sharedLock, this.m_activePartitionCount, this, this.m_useSingleChunking);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
          return (IEnumerator) this.GetEnumerator();
        }

        private void TryCopyFromFillBuffer(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
        {
          actualNumElementsGrabbed = 0;
          KeyValuePair<long, TSource>[] keyValuePairArray = this.m_FillBuffer;
          if (keyValuePairArray == null || this.m_FillBufferCurrentPosition >= this.m_FillBufferSize)
            return;
          Interlocked.Increment(ref this.m_activeCopiers);
          int num = Interlocked.Add(ref this.m_FillBufferCurrentPosition, requestedChunkSize);
          int sourceIndex = num - requestedChunkSize;
          if (sourceIndex < this.m_FillBufferSize)
          {
            actualNumElementsGrabbed = num < this.m_FillBufferSize ? num : this.m_FillBufferSize - sourceIndex;
            Array.Copy((Array) keyValuePairArray, sourceIndex, (Array) destArray, 0, actualNumElementsGrabbed);
          }
          Interlocked.Decrement(ref this.m_activeCopiers);
        }

        internal bool GrabChunk(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
        {
          actualNumElementsGrabbed = 0;
          if (this.m_hasNoElementsLeft.Value)
            return false;
          if (this.m_useSingleChunking)
            return this.GrabChunk_Single(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
          return this.GrabChunk_Buffered(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
        }

        internal bool GrabChunk_Single(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
        {
          lock (this.m_sharedLock)
          {
            if (this.m_hasNoElementsLeft.Value)
              return false;
            try
            {
              if (this.m_sharedReader.MoveNext())
              {
                this.m_sharedIndex.Value = checked (this.m_sharedIndex.Value + 1L);
                destArray[0] = new KeyValuePair<long, TSource>(this.m_sharedIndex.Value, this.m_sharedReader.Current);
                actualNumElementsGrabbed = 1;
                return true;
              }
              this.m_sourceDepleted.Value = true;
              this.m_hasNoElementsLeft.Value = true;
              return false;
            }
            catch
            {
              this.m_sourceDepleted.Value = true;
              this.m_hasNoElementsLeft.Value = true;
              throw;
            }
          }
        }

        internal bool GrabChunk_Buffered(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
        {
          this.TryCopyFromFillBuffer(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
          if (actualNumElementsGrabbed == requestedChunkSize)
            return true;
          if (this.m_sourceDepleted.Value)
          {
            this.m_hasNoElementsLeft.Value = true;
            this.m_FillBuffer = (KeyValuePair<long, TSource>[]) null;
            return actualNumElementsGrabbed > 0;
          }
          lock (this.m_sharedLock)
          {
            if (this.m_sourceDepleted.Value)
              return actualNumElementsGrabbed > 0;
            try
            {
              if (this.m_activeCopiers > 0)
              {
                SpinWait local_4 = new SpinWait();
                while (this.m_activeCopiers > 0)
                  local_4.SpinOnce();
              }
              while (actualNumElementsGrabbed < requestedChunkSize)
              {
                if (this.m_sharedReader.MoveNext())
                {
                  this.m_sharedIndex.Value = checked (this.m_sharedIndex.Value + 1L);
                  destArray[actualNumElementsGrabbed] = new KeyValuePair<long, TSource>(this.m_sharedIndex.Value, this.m_sharedReader.Current);
                  ++actualNumElementsGrabbed;
                }
                else
                {
                  this.m_sourceDepleted.Value = true;
                  break;
                }
              }
              KeyValuePair<long, TSource>[] local_3 = this.m_FillBuffer;
              if (!this.m_sourceDepleted.Value)
              {
                if (local_3 != null)
                {
                  if (this.m_FillBufferCurrentPosition >= local_3.Length)
                  {
                    for (int local_5 = 0; local_5 < local_3.Length; ++local_5)
                    {
                      if (this.m_sharedReader.MoveNext())
                      {
                        this.m_sharedIndex.Value = checked (this.m_sharedIndex.Value + 1L);
                        local_3[local_5] = new KeyValuePair<long, TSource>(this.m_sharedIndex.Value, this.m_sharedReader.Current);
                      }
                      else
                      {
                        this.m_sourceDepleted.Value = true;
                        this.m_FillBufferSize = local_5;
                        break;
                      }
                    }
                    this.m_FillBufferCurrentPosition = 0;
                  }
                }
              }
            }
            catch
            {
              this.m_sourceDepleted.Value = true;
              this.m_hasNoElementsLeft.Value = true;
              throw;
            }
          }
          return actualNumElementsGrabbed > 0;
        }

        public void Dispose()
        {
          if (this.m_disposed)
            return;
          this.m_disposed = true;
          this.m_sharedReader.Dispose();
        }
      }

      private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, IEnumerator<TSource>>
      {
        private KeyValuePair<long, TSource>[] m_localList;
        private readonly Partitioner.SharedBool m_hasNoElementsLeft;
        private readonly object m_sharedLock;
        private readonly Partitioner.SharedInt m_activePartitionCount;
        private Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable m_enumerable;

        protected override bool HasNoElementsLeft
        {
          get
          {
            return this.m_hasNoElementsLeft.Value;
          }
          set
          {
            this.m_hasNoElementsLeft.Value = true;
          }
        }

        public override KeyValuePair<long, TSource> Current
        {
          get
          {
            if (this.m_currentChunkSize == null)
              throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
            return this.m_localList[this.m_localOffset.Value];
          }
        }

        internal InternalPartitionEnumerator(IEnumerator<TSource> sharedReader, Partitioner.SharedLong sharedIndex, Partitioner.SharedBool hasNoElementsLeft, object sharedLock, Partitioner.SharedInt activePartitionCount, Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable enumerable, bool useSingleChunking)
          : base(sharedReader, sharedIndex, useSingleChunking)
        {
          this.m_hasNoElementsLeft = hasNoElementsLeft;
          this.m_sharedLock = sharedLock;
          this.m_enumerable = enumerable;
          this.m_activePartitionCount = activePartitionCount;
          if (this.m_activePartitionCount == null)
            return;
          Interlocked.Increment(ref this.m_activePartitionCount.Value);
        }

        protected override bool GrabNextChunk(int requestedChunkSize)
        {
          if (this.HasNoElementsLeft)
            return false;
          if (this.m_localList == null)
            this.m_localList = new KeyValuePair<long, TSource>[this.m_maxChunkSize];
          return this.m_enumerable.GrabChunk(this.m_localList, requestedChunkSize, ref this.m_currentChunkSize.Value);
        }

        public override void Dispose()
        {
          if (this.m_activePartitionCount == null || Interlocked.Decrement(ref this.m_activePartitionCount.Value) != 0)
            return;
          this.m_enumerable.Dispose();
        }
      }
    }

    private abstract class DynamicPartitionerForIndexRange_Abstract<TSource, TCollection> : OrderablePartitioner<TSource>
    {
      private TCollection m_data;

      public override bool SupportsDynamicPartitions
      {
        get
        {
          return true;
        }
      }

      protected DynamicPartitionerForIndexRange_Abstract(TCollection data)
        : base(true, false, true)
      {
        this.m_data = data;
      }

      protected abstract IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(TCollection data);

      public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
      {
        if (partitionCount <= 0)
          throw new ArgumentOutOfRangeException("partitionCount");
        IEnumerator<KeyValuePair<long, TSource>>[] enumeratorArray = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
        IEnumerable<KeyValuePair<long, TSource>> partitionsFactory = this.GetOrderableDynamicPartitions_Factory(this.m_data);
        for (int index = 0; index < partitionCount; ++index)
          enumeratorArray[index] = partitionsFactory.GetEnumerator();
        return (IList<IEnumerator<KeyValuePair<long, TSource>>>) enumeratorArray;
      }

      public override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
      {
        return this.GetOrderableDynamicPartitions_Factory(this.m_data);
      }
    }

    private abstract class DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, TSourceReader> : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, TSourceReader>
    {
      protected int m_startIndex;

      protected abstract int SourceCount { get; }

      protected override bool HasNoElementsLeft
      {
        get
        {
          return Volatile.Read(ref this.m_sharedIndex.Value) >= (long) (this.SourceCount - 1);
        }
        set
        {
        }
      }

      protected DynamicPartitionEnumeratorForIndexRange_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex)
        : base(sharedReader, sharedIndex)
      {
      }

      protected override bool GrabNextChunk(int requestedChunkSize)
      {
        while (!this.HasNoElementsLeft)
        {
          long comparand = Volatile.Read(ref this.m_sharedIndex.Value);
          if (this.HasNoElementsLeft)
            return false;
          long num = Math.Min((long) (this.SourceCount - 1), comparand + (long) requestedChunkSize);
          if (Interlocked.CompareExchange(ref this.m_sharedIndex.Value, num, comparand) == comparand)
          {
            this.m_currentChunkSize.Value = (int) (num - comparand);
            this.m_localOffset.Value = -1;
            this.m_startIndex = (int) (comparand + 1L);
            return true;
          }
        }
        return false;
      }

      public override void Dispose()
      {
      }
    }

    private class DynamicPartitionerForIList<TSource> : Partitioner.DynamicPartitionerForIndexRange_Abstract<TSource, IList<TSource>>
    {
      internal DynamicPartitionerForIList(IList<TSource> source)
        : base(source)
      {
      }

      protected override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(IList<TSource> m_data)
      {
        return (IEnumerable<KeyValuePair<long, TSource>>) new Partitioner.DynamicPartitionerForIList<TSource>.InternalPartitionEnumerable(m_data);
      }

      private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable
      {
        private readonly IList<TSource> m_sharedReader;
        private Partitioner.SharedLong m_sharedIndex;

        internal InternalPartitionEnumerable(IList<TSource> sharedReader)
        {
          this.m_sharedReader = sharedReader;
          this.m_sharedIndex = new Partitioner.SharedLong(-1L);
        }

        public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
        {
          return (IEnumerator<KeyValuePair<long, TSource>>) new Partitioner.DynamicPartitionerForIList<TSource>.InternalPartitionEnumerator(this.m_sharedReader, this.m_sharedIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
          return (IEnumerator) this.GetEnumerator();
        }
      }

      private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, IList<TSource>>
      {
        protected override int SourceCount
        {
          get
          {
            return this.m_sharedReader.Count;
          }
        }

        public override KeyValuePair<long, TSource> Current
        {
          get
          {
            if (this.m_currentChunkSize == null)
              throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
            return new KeyValuePair<long, TSource>((long) (this.m_startIndex + this.m_localOffset.Value), this.m_sharedReader[this.m_startIndex + this.m_localOffset.Value]);
          }
        }

        internal InternalPartitionEnumerator(IList<TSource> sharedReader, Partitioner.SharedLong sharedIndex)
          : base(sharedReader, sharedIndex)
        {
        }
      }
    }

    private class DynamicPartitionerForArray<TSource> : Partitioner.DynamicPartitionerForIndexRange_Abstract<TSource, TSource[]>
    {
      internal DynamicPartitionerForArray(TSource[] source)
        : base(source)
      {
      }

      protected override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(TSource[] m_data)
      {
        return (IEnumerable<KeyValuePair<long, TSource>>) new Partitioner.DynamicPartitionerForArray<TSource>.InternalPartitionEnumerable(m_data);
      }

      private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable
      {
        private readonly TSource[] m_sharedReader;
        private Partitioner.SharedLong m_sharedIndex;

        internal InternalPartitionEnumerable(TSource[] sharedReader)
        {
          this.m_sharedReader = sharedReader;
          this.m_sharedIndex = new Partitioner.SharedLong(-1L);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
          return (IEnumerator) this.GetEnumerator();
        }

        public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
        {
          return (IEnumerator<KeyValuePair<long, TSource>>) new Partitioner.DynamicPartitionerForArray<TSource>.InternalPartitionEnumerator(this.m_sharedReader, this.m_sharedIndex);
        }
      }

      private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, TSource[]>
      {
        protected override int SourceCount
        {
          get
          {
            return this.m_sharedReader.Length;
          }
        }

        public override KeyValuePair<long, TSource> Current
        {
          get
          {
            if (this.m_currentChunkSize == null)
              throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
            return new KeyValuePair<long, TSource>((long) (this.m_startIndex + this.m_localOffset.Value), this.m_sharedReader[this.m_startIndex + this.m_localOffset.Value]);
          }
        }

        internal InternalPartitionEnumerator(TSource[] sharedReader, Partitioner.SharedLong sharedIndex)
          : base(sharedReader, sharedIndex)
        {
        }
      }
    }

    private abstract class StaticIndexRangePartitioner<TSource, TCollection> : OrderablePartitioner<TSource>
    {
      protected abstract int SourceCount { get; }

      protected StaticIndexRangePartitioner()
        : base(true, true, true)
      {
      }

      protected abstract IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex);

      public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
      {
        if (partitionCount <= 0)
          throw new ArgumentOutOfRangeException("partitionCount");
        int result;
        int num = Math.DivRem(this.SourceCount, partitionCount, out result);
        IEnumerator<KeyValuePair<long, TSource>>[] enumeratorArray = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
        int endIndex = -1;
        for (int index = 0; index < partitionCount; ++index)
        {
          int startIndex = endIndex + 1;
          endIndex = index >= result ? startIndex + num - 1 : startIndex + num;
          enumeratorArray[index] = this.CreatePartition(startIndex, endIndex);
        }
        return (IList<IEnumerator<KeyValuePair<long, TSource>>>) enumeratorArray;
      }
    }

    private abstract class StaticIndexRangePartition<TSource> : IEnumerator<KeyValuePair<long, TSource>>, IDisposable, IEnumerator
    {
      protected readonly int m_startIndex;
      protected readonly int m_endIndex;
      protected volatile int m_offset;

      public abstract KeyValuePair<long, TSource> Current { get; }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.Current;
        }
      }

      protected StaticIndexRangePartition(int startIndex, int endIndex)
      {
        this.m_startIndex = startIndex;
        this.m_endIndex = endIndex;
        this.m_offset = startIndex - 1;
      }

      public void Dispose()
      {
      }

      public void Reset()
      {
        throw new NotSupportedException();
      }

      public bool MoveNext()
      {
        if (this.m_offset < this.m_endIndex)
        {
          this.m_offset = this.m_offset + 1;
          return true;
        }
        this.m_offset = this.m_endIndex + 1;
        return false;
      }
    }

    private class StaticIndexRangePartitionerForIList<TSource> : Partitioner.StaticIndexRangePartitioner<TSource, IList<TSource>>
    {
      private IList<TSource> m_list;

      protected override int SourceCount
      {
        get
        {
          return this.m_list.Count;
        }
      }

      internal StaticIndexRangePartitionerForIList(IList<TSource> list)
      {
        this.m_list = list;
      }

      protected override IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex)
      {
        return (IEnumerator<KeyValuePair<long, TSource>>) new Partitioner.StaticIndexRangePartitionForIList<TSource>(this.m_list, startIndex, endIndex);
      }
    }

    private class StaticIndexRangePartitionForIList<TSource> : Partitioner.StaticIndexRangePartition<TSource>
    {
      private volatile IList<TSource> m_list;

      public override KeyValuePair<long, TSource> Current
      {
        get
        {
          if (this.m_offset < this.m_startIndex)
            throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
          return new KeyValuePair<long, TSource>((long) this.m_offset, this.m_list[this.m_offset]);
        }
      }

      internal StaticIndexRangePartitionForIList(IList<TSource> list, int startIndex, int endIndex)
        : base(startIndex, endIndex)
      {
        this.m_list = list;
      }
    }

    private class StaticIndexRangePartitionerForArray<TSource> : Partitioner.StaticIndexRangePartitioner<TSource, TSource[]>
    {
      private TSource[] m_array;

      protected override int SourceCount
      {
        get
        {
          return this.m_array.Length;
        }
      }

      internal StaticIndexRangePartitionerForArray(TSource[] array)
      {
        this.m_array = array;
      }

      protected override IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex)
      {
        return (IEnumerator<KeyValuePair<long, TSource>>) new Partitioner.StaticIndexRangePartitionForArray<TSource>(this.m_array, startIndex, endIndex);
      }
    }

    private class StaticIndexRangePartitionForArray<TSource> : Partitioner.StaticIndexRangePartition<TSource>
    {
      private volatile TSource[] m_array;

      public override KeyValuePair<long, TSource> Current
      {
        get
        {
          if (this.m_offset < this.m_startIndex)
            throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
          return new KeyValuePair<long, TSource>((long) this.m_offset, this.m_array[this.m_offset]);
        }
      }

      internal StaticIndexRangePartitionForArray(TSource[] array, int startIndex, int endIndex)
        : base(startIndex, endIndex)
      {
        this.m_array = array;
      }
    }

    private class SharedInt
    {
      internal volatile int Value;

      internal SharedInt(int value)
      {
        this.Value = value;
      }
    }

    private class SharedBool
    {
      internal volatile bool Value;

      internal SharedBool(bool value)
      {
        this.Value = value;
      }
    }

    private class SharedLong
    {
      internal long Value;

      internal SharedLong(long value)
      {
        this.Value = value;
      }
    }
  }
}
