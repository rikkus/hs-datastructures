using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace HS.DataStructures.Tests
{
    [TestFixture]
    public class LruCacheTestFixture
    {
        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CacheRefusesMaxSizeOfLessThanOne()
        {
            new LruCache<int, int>(0);
        }

        [Test]
        public void CacheWithSizeOfOneOnlyHoldsOneItem()
        {
            var cache = new LruCache<int, int>(1);
            cache.Put(0, 42);
            cache.Put(1, 43);
            Assert.AreEqual(new KeyValuePair<int, int>(1, 43), cache.Items().Single());
        }

        [Test]
        public void ItemsOverwriteOthersWithSameKey()
        {
            var cache = new LruCache<int, int>(2);
            cache.Put(0, 42);
            cache.Put(0, 43);
            Assert.AreEqual(new KeyValuePair<int, int>(0, 43), cache.Items().Single());
        }

        [Test]
        public void ContainsSucceedsWithExistingItem()
        {
            var cache = new LruCache<int, int>(1);
            cache.Put(42, 0);
            Assert.IsTrue(cache.Contains(42));
        }

        [Test]
        public void ContainsFailsWithNonExistentItem()
        {
            var cache = new LruCache<int, int>(1);
            Assert.IsFalse(cache.Contains(42));
        }

        [Test]
        public void ContainsFailsAfterItemExpelled()
        {
            var cache = new LruCache<int, int>(1);
            cache.Put(0, 42);
            cache.Put(1, 43);
            Assert.IsFalse(cache.Contains(42));
        }

        [Test]
        public void AddingTwoItemsToCacheWithMaxSizeOfTwoSucceeds()
        {
            var cache = new LruCache<int, int>(2);
            cache.Put(0, 42);
            cache.Put(1, 43);
            Assert.AreEqual
                (
                new[]
                    {
                        new KeyValuePair<int, int>(0, 42),
                        new KeyValuePair<int, int>(1, 43)
                    },
                cache.Items().ToArray()
                );
        }

        [Test]
        public void AddingItemThenDeletingLeavesCacheEmpty()
        {
            var cache = new LruCache<int, int>(1);

            cache.Put(0, 42);

            cache.Delete(0);

            Assert.AreEqual(0, cache.Count);
        }

        [Test]
        public void CountIsZeroBeforeAdding()
        {
            Assert.AreEqual(0, new LruCache<int, int>(1).Count);
        }

        [Test]
        public void CapacityMatchesSpecified()
        {
            Assert.AreEqual(42, new LruCache<int, int>(42).Capacity);
        }

        [Test]
        public void AddingTwoItemsAndDeletingOneLeavesOther()
        {
            var cache = new LruCache<int, int>(2);

            cache.Put(0, 42);
            cache.Put(1, 43);

            cache.Delete(0);

            Assert.AreEqual
                (
                new KeyValuePair<int, int>(1, 43),
                cache.Items().Single()
                );
        }

        [Test]
        public void AddingTwoItemsAndDeletingFirstLeavesSecond()
        {
            var cache = new LruCache<int, int>(2);

            cache.Put(0, 42);
            cache.Put(1, 43);

            cache.Delete(0);

            Assert.AreEqual
                (
                new KeyValuePair<int, int>(1, 43),
                cache.Items().Single()
                );
        }

        [Test]
        public void ClearClears()
        {
            var cache = new LruCache<int, int>(2);

            cache.Put(0, 42);
            cache.Put(1, 43);

            cache.Clear();

            Assert.AreEqual(0, cache.Count);
        }

        [Test, ExpectedException(typeof(KeyNotFoundException))]
        public void GetFailsWhenKeyNotInCache()
        {
            new LruCache<int, int>(1).Get(42);
        }
    }
}
