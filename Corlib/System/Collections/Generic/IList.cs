// Decompiled with JetBrains decompiler
// Type: System.Collections.IList
// Assembly: mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 83B20313-0977-4FC5-B2F8-2F51C3669666
// Assembly location: D:\experiment\TinyCLR2\GlideTinyCLR2\Glidev2\TinyCLR2.Glide\bin\Debug\mscorlib.dll

namespace System.Collections
{
  public interface IList : ICollection, IEnumerable
  {
    object this[int index] { get; set; }

    int Add(object value);

    bool Contains(object value);

    void Clear();

    bool IsReadOnly { get; }

    bool IsFixedSize { get; }

    int IndexOf(object value);

    void Insert(int index, object value);

    void Remove(object value);

    void RemoveAt(int index);
  }
}
