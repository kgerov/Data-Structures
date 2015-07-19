using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _03.ImplementAnArrayBasedStack;

[TestClass]
public class ArrayStackTester
{
    [TestMethod]
    public void Initialize_EmptyStack_ShouldHaveZeroCount()
    {
        // Arrange
        ArrayStack<int> nums = new ArrayStack<int>();

        // Act
        int count = nums.Count;

        // Assert
        Assert.AreEqual(0, count);
    }

    [TestMethod]
    public void AddElementTo_EmptyStack_ShouldHaveOneCount()
    {
        // Arrange
        ArrayStack<int> nums = new ArrayStack<int>();
        nums.Push(3);

        // Act
        int count = nums.Count;

        // Assert
        Assert.AreEqual(1, count);
    }

    [TestMethod]
    public void CheckCountWhenPopElement_ShouldSameAsBeforePush()
    {
        // Arrange
        ArrayStack<int> nums = new ArrayStack<int>();
        nums.Push(4);
        nums.Pop();

        // Act
        int count = nums.Count;

        // Assert
        Assert.AreEqual(0, count);
    }

    [TestMethod]
    public void TestAutoGrowFunctionality_With1000Elemenets()
    {
        // Arrange
        ArrayStack<string> words = new ArrayStack<string>();
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

            words.Push(result);
            Assert.AreEqual(i+1, words.Count);
        }

        while (words.Count > 0)
        {
            Assert.AreEqual(boundary, words.Count);
            words.Pop();
            boundary--;
        }
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void PopElement_FromEmptyStack_ShouldThrowException()
    {
        // Arrange
        ArrayStack<int> nums = new ArrayStack<int>();

        // Act
        nums.Pop();

        // Assert: expected exception
    }

    [TestMethod]
    public void PushPopElement_FromStackWithCapacityOne_CheckCount()
    {
        // Arrange
        ArrayStack<int> nums = new ArrayStack<int>(1);

        // Act and Assert
        Assert.AreEqual(0, nums.Count);

        int expectedElementSecond = 1;
        nums.Push(expectedElementSecond);
        Assert.AreEqual(1, nums.Count);

        int expectedElementFirst = 3;
        nums.Push(expectedElementFirst);
        Assert.AreEqual(2, nums.Count);

        Assert.AreEqual(expectedElementFirst, nums.Pop());
        Assert.AreEqual(1, nums.Count);

        Assert.AreEqual(expectedElementSecond, nums.Pop());
        Assert.AreEqual(0, nums.Count);
    }

    [TestMethod]
    public void CheckToArrayMethod_ForStack_ShouldReturnArrayWithReversedNumebrs()
    {
        // Arrange
        int[] arr = new int[] {1, 2, 3, 4};
        ArrayStack<int> stack = new ArrayStack<int>();

        // Act
        for (int i = 0; i < arr.Length; i++)
        {
            stack.Push(arr[i]);
        }

        int[] stackArr = stack.ToArray();


        // Assert
        for (int i = 0, j = arr.Length - 1; i < stackArr.Length; i++, j--)
        {
            Assert.AreEqual(arr[j], stackArr[i]);
        }
    }

    [TestMethod]
    public void CheckEmptyStack_ToArrayMethod_ShouldReturnEmptyArray()
    {
        // Arrange
        ArrayStack<int> stack = new ArrayStack<int>();

        // Act
        int[] stackArr = stack.ToArray();

        // Assert
        Assert.AreEqual(0, stackArr.Length);
    }
}