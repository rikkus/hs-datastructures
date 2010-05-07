using System;
using NUnit.Framework;

namespace HS.DataStructures.Tests
{
    [TestFixture]
    public class CircularBufferFixture
    {
        [Test]
        public void AddIncreasesSize()
        {
            var buffer = new CircularBuffer<int>(1) {42};
            Assert.AreEqual(1, buffer.Size);
        }

        [Test]
        public void AddTwiceIncreasesSizeTwice()
        {
            var buffer = new CircularBuffer<int>(2) {42, 42};
            Assert.AreEqual(2, buffer.Size);
        }

        [Test]
        public void CapacityUnchangedAfterRemove()
        {
            var buffer = new CircularBuffer<int>(1) {42};
            buffer.Remove();
            Assert.AreEqual(1, buffer.Capacity);
        }

        [Test]
        public void ClearLeavesBufferEmpty()
        {
            var buffer = new CircularBuffer<int>(1) {42};
            buffer.Clear();
            Assert.IsTrue(buffer.Empty);
        }

        [Test]
        public void ClearLeavesBufferEmptyAfterAddingTwice()
        {
            var buffer = new CircularBuffer<int>(2) {42, 123};
            buffer.Clear();
            Assert.IsTrue(buffer.Empty);
        }

        [Test]
        public void CtorCreatesBufferWithCorrectCapacity()
        {
            Assert.AreEqual(1, new CircularBuffer<int>(1).Capacity);
        }

        [Test]
        public void CtorCreatesBufferWithCorrectSize()
        {
            Assert.AreEqual(0, new CircularBuffer<int>(1).Size);
        }

        [Test]
        public void CtorCreatesEmptyBuffer()
        {
            Assert.IsTrue(new CircularBuffer<int>(1).Empty);
        }

        [Test]
        public void FirstLeavesBufferAtExpectedSize()
        {
            var buffer = new CircularBuffer<int>(1) {42};
            buffer.First();
            Assert.AreEqual(1, buffer.Size);
        }

        [Test, ExpectedException(typeof (InvalidOperationException))]
        public void FirstOfEmptyBufferThrowsBufferEmptyException()
        {
            (new CircularBuffer<int>(1)).First();
        }

        [Test]
        public void FirstReturnsExpectedValue()
        {
            var buffer = new CircularBuffer<int>(1) {42};
            Assert.AreEqual(42, buffer.First());
        }

        [Test]
        public void FirstReturnsExpectedValueAfterAddingTwice()
        {
            var buffer = new CircularBuffer<int>(2) {42, 123};
            Assert.AreEqual(42, buffer.First());
        }

        [Test]
        public void IndexerWorksAfterMultipleItemFullWrap()
        {
            var buffer = new CircularBuffer<int>(2) {1, 2, 3, 4};
            Assert.AreEqual(3, buffer[0]);
            Assert.AreEqual(4, buffer[1]);
        }

        [Test]
        public void IndexerWorksAfterMultipleItemWrap()
        {
            var buffer = new CircularBuffer<int>(2) {1, 2, 3};
            Assert.AreEqual(2, buffer[0]);
            Assert.AreEqual(3, buffer[1]);
        }

        [Test]
        public void IndexerWorksAfterWrap()
        {
            var buffer = new CircularBuffer<int>(1) {1, 2};
            Assert.AreEqual(2, buffer[0]);
        }

        [Test]
        public void IndexerWorksWithMoreThanOneItem()
        {
            var buffer = new CircularBuffer<int>(2) {1, 2};
            Assert.AreEqual(1, buffer[0]);
            Assert.AreEqual(2, buffer[1]);
        }

        [Test]
        public void IndexerWorksWithNoWrap()
        {
            var buffer = new CircularBuffer<int>(1) {1};
            Assert.AreEqual(1, buffer[0]);
        }

        [Test, ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void InvalidCapacityGivenToCtorThrowsArgumentOfOfRangeException()
        {
            new CircularBuffer<int>(0);
        }

        [Test]
        public void ItemCorrectAfterWrapWithSingleItemBuffer()
        {
            var buffer = new CircularBuffer<int>(1) {1, 2};
            Assert.AreEqual(2, buffer.First());
        }

        [Test, ExpectedException(typeof (InvalidOperationException))]
        public void RemoveFromEmptyBufferThrowsBufferEmptyException()
        {
            (new CircularBuffer<int>(1)).Remove();
        }

        [Test]
        public void RemoveLeavesBufferEmpty()
        {
            var buffer = new CircularBuffer<int>(1) {42};
            buffer.Remove();
            Assert.IsTrue(buffer.Empty);
        }

        [Test]
        public void RemoveLeavesOneItemAfterAddingTwice()
        {
            var buffer = new CircularBuffer<int>(2) {42, 123};
            buffer.Remove();
            Assert.AreEqual(1, buffer.Size);
        }

        [Test]
        public void RemoveReturnsExpectedValue()
        {
            var buffer = new CircularBuffer<int>(1) {42};
            Assert.AreEqual(42, buffer.Remove());
        }

        [Test]
        public void RemoveReturnsExpectedValueAfterAddingTwice()
        {
            var buffer = new CircularBuffer<int>(2) {42, 123};
            Assert.AreEqual(42, buffer.Remove());
        }

        [Test]
        public void SizeCorrectAfterWrap()
        {
            var buffer = new CircularBuffer<int>(1) {1, 2};
            Assert.AreEqual(1, buffer.Size);
        }

        [Test]
        public void ToArrayCorrectAfterFullWrapWithDoubleItemBuffer()
        {
            var buffer = new CircularBuffer<int>(2) {1, 2, 3, 4};
            Assert.AreEqual(new[] {3, 4}, buffer.ToArray());
        }

        [Test]
        public void ToArrayCorrectAfterWrapWithDoubleItemBuffer()
        {
            var buffer = new CircularBuffer<int>(2) {1, 2, 3};
            Assert.AreEqual(new[] {2, 3}, buffer.ToArray());
        }

        [Test]
        public void ToArrayReturnsEmptyArrayForEmptyBuffer()
        {
            Assert.AreEqual(new int[0], new CircularBuffer<int>(1).ToArray());
        }

        [Test]
        public void ToStringWorksWithElevenItems()
        {
            Assert.AreEqual
                (
                "{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, [...] }",
                new CircularBuffer<int>(11) {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11}.ToString()
                );
        }

        [Test]
        public void ToStringWorksWithNoItems()
        {
            Assert.AreEqual("{ }", new CircularBuffer<int>(1).ToString());
        }

        [Test]
        public void ToStringWorksWithOneItem()
        {
            Assert.AreEqual("{ 1 }", new CircularBuffer<int>(1) {1}.ToString());
        }

        [Test]
        public void ToStringWorksWithTwoItems()
        {
            Assert.AreEqual("{ 1, 2 }", new CircularBuffer<int>(2) {1, 2}.ToString());
        }
    }
}