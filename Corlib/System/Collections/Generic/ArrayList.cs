// Decompiled with JetBrains decompiler
// Type: System.Collections.ArrayList
// Assembly: mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 83B20313-0977-4FC5-B2F8-2F51C3669666
// Assembly location: D:\experiment\TinyCLR2\GlideTinyCLR2\Glidev2\TinyCLR2.Glide\bin\Debug\mscorlib.dll

using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections
{
  public class ArrayList :  ICloneable//IEnumerable,IList,ICollection
    {
    private object[] _items;
    private int _size;
    private const int _defaultCapacity = 4;

    public ArrayList() => this._items = new object[4];

    public virtual int Capacity
    {
      get => this._items.Length;
      set => this.SetCapacity(value);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void SetCapacity(int capacity);

    public virtual int Count => this._size;

    public virtual bool IsFixedSize => false;

    public virtual bool IsReadOnly => false;

    public virtual bool IsSynchronized => false;

    public virtual object SyncRoot => (object) this;

    public virtual extern object this[int index] { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

    [MethodImpl(MethodImplOptions.InternalCall)]
    public virtual extern int Add(object value);

    //public virtual int BinarySearch(object value, IComparer comparer) => Array.BinarySearch((Array) this._items, 0, this._size, value, comparer);

    [MethodImpl(MethodImplOptions.InternalCall)]
    public virtual extern void Clear();

    public virtual object Clone()
    {
      ArrayList arrayList = new ArrayList();
      if (this._size > 4)
        arrayList._items = new object[this._size];
      arrayList._size = this._size;
      //System.Array.Copy((Array) this._items, 0, (Array) arrayList._items, 0, this._size);
      System.Array.Copy( this._items, ref arrayList._items);
      return (object) arrayList;
    }

    public virtual bool Contains(object item) => System.Array.IndexOf((Array) this._items, item) >= 0;

    public virtual void CopyTo(Array array) => this.CopyTo(array, 0);

    public virtual void CopyTo(Array array, int arrayIndex) => Array.Copy((Array) this._items,ref array);

    //public virtual IEnumerator GetEnumerator() => (IEnumerator) new System.Array.SZArrayEnumerator((Array) this._items, 0, this._size);

    public virtual int IndexOf(object value) => Array.IndexOf((Array) this._items, value);

    [MethodImpl(MethodImplOptions.InternalCall)]
    public virtual extern void Insert(int index, object value);

    public virtual void Remove(object obj)
    {
      int index = Array.IndexOf((Array) this._items, obj);
      if (index < 0)
        return;
      this.RemoveAt(index);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    public virtual extern void RemoveAt(int index);

    public virtual object[] ToArray() => (object[]) this.ToArray();

    //public virtual Array ToArray()
    //{
    //  Array instance = Array.CreateInstance(type, this._size);
    //  Array.Copy((Array) this._items, ref instance);
    //  return instance;
    //}
  }
}
