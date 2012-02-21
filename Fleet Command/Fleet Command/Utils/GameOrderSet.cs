using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet_Command.Utils {
    class GameOrderSet<T> where T : DGC {
        private SortedSet<T> update, draw;

        public GameOrderSet() {
            update = new SortedSet<T>(new ByUpdateOrder());
            draw = new SortedSet<T>(new ByDrawOrder());
        }

        public GameOrderSet(IEnumerable<T> other)
            : this() {
                this.UnionWith(other);
        }

        public int Count {
            get { return update.Count; }
        }

        public T MaxUpdate {
            get { return update.Max; }
        }

        public T MaxDraw {
            get { return draw.Max; }
        }

        public T MinUpdate {
            get { return update.Min; }
        }

        public T MinDraw {
            get { return draw.Min; }
        }

        public bool Add(T item) {
            return update.Add(item) && draw.Add(item);
        }

        public void Clear() {
            update.Clear();
            draw.Clear();
        }

        public bool Contains(T item) {
            return update.Contains(item);
        }

        public void ExceptWith(IEnumerable<T> other) {
            update.ExceptWith(other);
            draw.ExceptWith(other);
        }

        public SortedSet<T>.Enumerator GetUpdateEnumerator() {
            return update.GetEnumerator();
        }

        public SortedSet<T>.Enumerator GetDrawEnumerator() {
            return draw.GetEnumerator();
        }

        public SortedSet<T> GetUpdateViewBetween(T lowerValue, T upperValue) {
            return update.GetViewBetween(lowerValue, upperValue);
        }

        public SortedSet<T> GetDrawViewBetween(T lowerValue, T upperValue) {
            return draw.GetViewBetween(lowerValue, upperValue);
        }

        public void IntersectWith(IEnumerable<T> other) {
            update.IntersectWith(other);
            draw.IntersectWith(other);
        }

        public bool IsProperSubsetOf(IEnumerable<T> other) {
            return update.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other) {
            return update.IsProperSupersetOf(other);
        }

        public bool IsSubsetOF(IEnumerable<T> other) {
            return update.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other) {
            return update.IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other) {
            return update.Overlaps(other);
        }

        public bool Remove(T item) {
            return update.Remove(item) && draw.Remove(item);
        }

        public int RemoveWhere(Predicate<T> match) {
            update.RemoveWhere(match);
            return draw.RemoveWhere(match);
        }

        public IEnumerable<T> ReverseUpdate() {
            return update.Reverse();
        }

        public IEnumerable<T> ReverseDraw() {
            return draw.Reverse();
        }

        public bool SetEquals(IEnumerable<T> other) {
            return update.SetEquals(other);
        }

        public void SymmetricExceptWith(IEnumerable<T> other) {
            update.SymmetricExceptWith(other);
            draw.SymmetricExceptWith(other);
        }

        public void UnionWith(IEnumerable<T> other) {
            update.UnionWith(other);
            draw.UnionWith(other);
        }
    }
}
