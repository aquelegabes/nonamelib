using System.ComponentModel.DataAnnotations;

namespace NoNameLib.Domain.Tests;

public class ValidationHandlerTest
{
    [Fact]
    public void PassingTest_ValidateObject()
    {
        // arrange
        var objectToValidate =
            new TestDomain("Gabriel Santos", new DateTime(year: 1998, month: 9, day: 4));

        ValidationHandler.Validate(objectToValidate);

        // assert
        Assert.True(!string.IsNullOrWhiteSpace(objectToValidate.FullName));
        Assert.True(objectToValidate.BirthDate != default);
    }

    [Fact]
    public void PassingTest_ValidateObject_Init()
    {
        // arrange
        var objectToValidate = new TestDomain()
        {
            FullName = "Gabriel Santos",
            BirthDate = DateTime.Now
        };

        ValidationHandler.Validate(objectToValidate);

        Assert.True(!string.IsNullOrWhiteSpace(objectToValidate.FullName));
        Assert.True(objectToValidate.BirthDate != default);
    }

    [Fact]
    public void PassingTest_ValidateObject_PersonalIdentificationCPF()
    {
        // arrange
        var objectToValidate = new TestDomain()
        {
            FullName = "Gabriel Santos",
            BirthDate = DateTime.Now,
            CPF = "31146807040",
        };

        ValidationHandler.Validate(objectToValidate);

        Assert.True(!string.IsNullOrWhiteSpace(objectToValidate.FullName));
        Assert.True(objectToValidate.BirthDate != default);
        Assert.True(objectToValidate.CPF != null);
    }

    [Fact]
    public void FailingTest_ValidateObject_NullObject()
    {
        // arrange
        var objectToValidate = new TestDomain("Gabriel", default);

        // assert
        Assert.Throws<ValidationException>(() => ValidationHandler.Validate(objectToValidate));
    }

    [Fact]
    public void FailingTest_ValidateObject_MultipleNullValues()
    {
        // arrange
        var objectToValidate = new TestDomain("", default);

        // assert
        var exc = Assert.Throws<AggregateException>(() => ValidationHandler.Validate(objectToValidate));
        Assert.True(exc.InnerExceptions.Count > 1);
    }

    [Fact]
    public void FailingTest_ValidateObject_NegativeNumber()
    {
        // arrange
        var objectToValidate = new TestDomain()
        {
            FullName = "Gabriel Santos",
            BirthDate = DateTime.Now,
            IntValue = -2
        };

        // assert
        Assert.Throws<ValidationException>(() => ValidationHandler.Validate(objectToValidate));
    }

    [Fact]
    public void FailingTest_ValidateObject_PersonalIdentificationCPF()
    {
        // arrange
        var objectToValidate = new TestDomain()
        {
            FullName = "Gabriel Santos",
            BirthDate = DateTime.Now,
            CPF = "31146807041",
        };

        Assert.Throws<ValidationException>(() => ValidationHandler.Validate(objectToValidate));

        Assert.True(!string.IsNullOrWhiteSpace(objectToValidate.FullName));
        Assert.True(objectToValidate.BirthDate != default);
        Assert.True(objectToValidate.CPF != null);
    }
}