using NUnit.Framework;

namespace HS.DataStructures.Tests
{
    [TestFixture]
    public class SetTestFixture
    {
        [Test]
        public void EmptySetCountIsZero()
        {
            Assert.AreEqual(0, new Set<int>().Count);
        }

        [Test]
        public void CountCorrectAfterAdd()
        {
            Assert.AreEqual(1, new Set<int> { 0 }.Count);
        }

        [Test]
        public void NonExistentMemberNotFound()
        {
            Assert.IsFalse(new Set<int>().Contains(0));
        }

        [Test]
        public void ExistingMemberFound()
        {
            Assert.IsTrue(new Set<int> { 0 }.Contains(0));
        }

        [Test]
        public void RemoveLowersCount()
        {
            var set = new Set<int> {0};
            set.Remove(0);
            Assert.AreEqual(0, set.Count);
        }

        [Test]
        public void ContainsFalseAfterRemove()
        {
            var set = new Set<int> {0};
            set.Remove(0);
            Assert.IsFalse(set.Contains(0));
        }

        [Test]
        public void ClearLowersCountToZero()
        {
            var set = new Set<int> {0};
            set.Clear();
            Assert.AreEqual(0, set.Count);
            Assert.IsFalse(set.Contains(0));
        }

        [Test]
        public void ClearRemovesAllMembers()
        {
            var set = new Set<int> {0, 1};
            set.Clear();
            Assert.IsFalse(set.Contains(0));
            Assert.IsFalse(set.Contains(1));
        }

        [Test]
        public void EmptySetsEqual()
        {
            Assert.AreEqual(new Set<int>(), new Set<int>());
        }

        [Test]
        public void SetsOfDifferentSizesUnequal()
        {
            Assert.AreNotEqual(new Set<int>(), new Set<int> { 0 });
        }

        [Test]
        public void SetsWithDifferentMembersUnequal()
        {
            Assert.AreNotEqual(new Set<int> { 0 }, new Set<int> { 1 });
        }

        [Test]
        public void SetsWithIdenticalMembersEqual()
        {
            Assert.AreEqual(new Set<int> { 0 }, new Set<int> { 0 });
        }

        [Test]
        public void UnionWithEmptySetIsOriginalSet()
        {
            Assert.AreEqual(new Set<int> { 0 }, new Set<int> { 0 }.Union(new Set<int>()));
        }

        [Test]
        public void UnionWithOtherSetIsUnion()
        {
            Assert.AreEqual(new Set<int> { 0, 1 }, new Set<int> { 0 }.Union(new Set<int> { 1 }));
        }

        [Test]
        public void IntersectionWithEmptySetIsEmptySet()
        {
            Assert.AreEqual(new Set<int>(), new Set<int> { 0 }.Intersection(new Set<int>()));
        }

        [Test]
        public void IntersectionWithDisjointSetIsEmptySet()
        {
            Assert.AreEqual(new Set<int>(), new Set<int> { 0 }.Intersection(new Set<int> { 1 }));
        }

        [Test]
        public void IntersectionWithOtherSetIsIntersection()
        {
            Assert.AreEqual(new Set<int> { 0 }, new Set<int> { 0 }.Intersection(new Set<int> { 0, 1 }));
        }

        [Test]
        public void DifferenceWithIdenticalSetIsEmptySet()
        {
            Assert.AreEqual(new Set<int>(), new Set<int> { 0 }.Difference(new Set<int> { 0 }));
        }

        [Test]
        public void DifferenceWithDifferentSetIsDifference()
        {
            Assert.AreEqual(new Set<int> { 1 }, new Set<int> { 0, 1 }.Difference(new Set<int> { 0 }));
        }

        [Test]
        public void DifferenceWithDifferentSetIsEmptySetWhenOtherSetContainsFewer()
        {
            Assert.AreEqual(new Set<int>(), new Set<int> { 0 }.Difference(new Set<int> { 0, 1 }));
        }
    }
}
