
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _03.ImplementAnArrayBasedStack;

[TestClass]
public class LinkedQueue
{
    [TestMethod]
    public void Initialize_EmptyStack_ShouldHaveZeroCount()
    {
        // Arrange
        LinkedQueue<int> nums = new LinkedQueue<int>();

        // Act
        int count = nums.Count;

        // Assert
        Assert.AreEqual(0, count);
    }

    [TestMethod]
    public void AddElementTo_EmptyStack_ShouldHaveOneCount()
    {
        // Arrange
        LinkedQueue<int> nums = new LinkedQueue<int>();
        nums.Enqueue(3);

        // Act
        int count = nums.Count;

        // Assert
        Assert.AreEqual(1, count);
    }

    [TestMethod]
    public void CheckCountWhenPopElement_ShouldSameAsBeforePush()
    {
        // Arrange
        LinkedQueue<int> nums = new LinkedQueue<int>();
        nums.Enqueue(4);
        nums.Dequeue();

        // Act
        int count = nums.Count;

        // Assert
        Assert.AreEqual(0, count);
    }

    [TestMethod]
    public void TestAutoGrowFunctionality_With1000Elemenets()
    {
        // Arrange
        LinkedQueue<string> words = new LinkedQueue<string>();
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        int boundary = 1000;

        // Act and Assert
        Assert.AreEqual(0, words.Count);

        for (int i = 0; i < boundary; i++)
        {
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            words.Enqueue(result);
            Assert.AreEqual(i + 1, words.Count);
        }

        while (words.Count > 0)
        {
            Assert.AreEqual(boundary, words.Count);
            words.Dequeue();
            boundary--;
        }
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void PopElement_FromEmptyStack_ShouldThrowException()
    {
        // Arrange
        LinkedQueue<int> nums = new LinkedQueue<int>();

        // Act
        nums.Dequeue();

        // Assert: expected exception
    }

    [TestMethod]
    public void PushPopElement_FromStackWithCapacityOne_CheckCount()
    {
        // Arrange
        LinkedQueue<int> nums = new LinkedQueue<int>();

        // Act and Assert
        Assert.AreEqual(0, nums.Count);

        int expectedElementSecond = 1;
        nums.Enqueue(expectedElementSecond);
        Assert.AreEqual(1, nums.Count);

        int expectedElementFirst = 3;
        nums.Enqueue(expectedElementFirst);
        Assert.AreEqual(2, nums.Count);

        Assert.AreEqual(expectedElementSecond, nums.Dequeue());
        Assert.AreEqual(1, nums.Count);

        Assert.AreEqual(expectedElementFirst, nums.Dequeue());
        Assert.AreEqual(0, nums.Count);
    }

    [TestMethod]
    public void CheckToArrayMethod_ForStack_ShouldReturnArrayWithReversedNumebrs()
    {
        // Arrange
        int[] arr = new int[] { 1, 2, 3, 4 };
        LinkedQueue<int> stack = new LinkedQueue<int>();

        // Act
        for (int i = 0; i < arr.Length; i++)
        {
            stack.Enqueue(arr[i]);
        }

        int[] stackArr = stack.ToArray();


        // Assert
        for (int i = 0; i < stackArr.Length; i++)
        {
            Assert.AreEqual(arr[i], stackArr[i]);
        }
    }

    [TestMethod]
    public void CheckEmptyStack_ToArrayMethod_ShouldReturnEmptyArray()
    {
        // Arrange
        LinkedQueue<int> stack = new LinkedQueue<int>();

        // Act
        int[] stackArr = stack.ToArray();

        // Assert
        Assert.AreEqual(0, stackArr.Length);
    }
}