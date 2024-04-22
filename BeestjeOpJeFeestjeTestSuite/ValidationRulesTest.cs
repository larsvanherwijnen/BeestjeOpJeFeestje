using BusinessLogic.Services.BookingValidationService;
using BusinessLogic.Services.BookingValidationService.ValidationRules;
using Data.Enums;
using Data.Interfaces;
using Data.Models;
using Moq;

namespace BeestjeOpJeFeestjeTestSuite;

public class ValidationRulesTest
{
    [Test]
    public void Validate_CustomerWithoutCardAndMoreThanThreeAnimals_ReturnsErrorMessage()
    {
        // Arrange
        var booking = new Booking
        {
            Customer = new Customer { CustomerCard = null },
            Animals = new List<Animal> { new Animal(), new Animal(), new Animal(),new Animal() }
        };
        
        var mockRule = new Mock<CustomerWithoutCardMaxThreeAnimalsRule>();

        // Act
        var result = mockRule.Object.Validate(booking);

        // Assert
        Assert.AreEqual("Customers without a customer card can book max 3 animals", result);
    }
    
    [Test]
    public void Validate_CustomerWithoutCardAndThreeOrFewerAnimals_ReturnsNoErrorMessage()
    {
        // Arrange
        var booking = new Booking
        {
            Customer = new Customer { CustomerCard = null },
            Animals = new List<Animal> { new Animal(), new Animal(), new Animal() }
        };

        var mockRule = new Mock<CustomerWithoutCardMaxThreeAnimalsRule>();

        // Act
        var result = mockRule.Object.Validate(booking);

        // Assert
        Assert.IsEmpty(result);
    }
    
    [Test]
    public void Validate_CustomerWithCard_ReturnsNoErrorMessage()
    {
        // Arrange
        var booking = new Booking
        {
            Customer = new Customer { CustomerCard = CustomerCards.Gold },
            Animals = new List<Animal> { new Animal(), new Animal(), new Animal(), new Animal() }
        };

        var mockRule = new Mock<CustomerWithoutCardMaxThreeAnimalsRule>();

        // Act
        var result = mockRule.Object.Validate(booking);


        // Assert
        Assert.IsEmpty(result);
    }
    
    [Test]
    public void Validate_DesertAnimalInOctoberToFebruary_ReturnsErrorMessage()
    {
        // Arrange
        var booking = new Booking
        {
            Animals = new List<Animal> { new Animal { Type = AnimalTypes.Desert } },
            Date = new DateTime(2024, 1, 15)
        };

        var mockRule = new Mock<NoDesertInOctoberToFebruaryRule>();

        // Act
        var result = mockRule.Object.Validate(booking);


        // Assert
        Assert.AreEqual("Brrrr – Veelste koud voor een woestijn dier.", result);
    }
    
    [Test]
    public void Validate_DesertAnimalInOtherMonths_ReturnsNoErrorMessage()
    {
        // Arrange
        var booking = new Booking
        {
            Animals = new List<Animal> { new Animal { Type = AnimalTypes.Desert } },
            Date = new DateTime(2024, 5, 15)
        };

        var mockRule = new Mock<NoDesertInOctoberToFebruaryRule>();

        // Act
        var result = mockRule.Object.Validate(booking);


        // Assert
        Assert.IsEmpty(result);
    }
    
    [Test]
    public void Validate_LionOrPolarBearWithFarmAnimal_ReturnsErrorMessage()
    {
        // Arrange
        var booking = new Booking
        {
            Animals = new List<Animal>
            {
                new Animal { Name = "Leeuw" },
                new Animal { Type = AnimalTypes.Farm }
            }
        };

        var mockRule = new Mock<NoLionOrPolarBeerWithFarmAnimalRule>();

        // Act
        var result = mockRule.Object.Validate(booking);

        // Assert
        Assert.AreEqual("Nom nom nom", result);
    }

    [Test]
    public void Validate_NonLionOrPolarBearWithFarmAnimal_ReturnsNoErrorMessage()
    {
        // Arrange
        var booking = new Booking
        {
            Animals = new List<Animal>
            {
                new Animal { Type = AnimalTypes.Desert },
                new Animal { Type = AnimalTypes.Farm }
            }
        };

        var mockRule = new Mock<NoLionOrPolarBeerWithFarmAnimalRule>();

        // Act
        var result = mockRule.Object.Validate(booking);

        // Assert
        Assert.IsEmpty(result);
    }
    
    [Test]
    public void Validate_PinguinOnWeekend_ReturnsErrorMessage()
    {
        // Arrange
        var booking = new Booking
        {
            Animals = new List<Animal> { new Animal { Name = "Pinguïn" } },
            Date = new DateTime(2024, 1, 20) // A Saturday
        };

        var mockRule = new Mock<NoPinguinOnWeekendsRule>();

        // Act
        var result = mockRule.Object.Validate(booking);
        // Assert
        Assert.AreEqual("Dieren in pak werken alleen doordeweeks", result);
    }

    [Test]
    public void Validate_NonPinguinOnWeekend_ReturnsNoErrorMessage()
    {
        // Arrange
        var booking = new Booking
        {
            Animals = new List<Animal> { new Animal { Name = "Leeuw" } },
            Date = new DateTime(2024, 1, 21) // A Sunday
        };
        
        var mockRule = new Mock<NoPinguinOnWeekendsRule>();

        // Act
        var result = mockRule.Object.Validate(booking);

        // Assert
        Assert.IsEmpty(result);
    }
    
    [Test]
    public void Validate_SnowAnimalInJuneToAugust_ReturnsErrorMessage()
    {
        // Arrange
        var booking = new Booking
        {
            Animals = new List<Animal> { new Animal { Type = AnimalTypes.Snow } },
            Date = new DateTime(2024, 7, 15)
        };


        var mockRule = new Mock<NoSnowAnimalsInJuneToAugustRule>();

        // Act
        var result = mockRule.Object.Validate(booking);

        // Assert
        Assert.AreEqual("Some People Are Worth Melting For. ~ Olaf", result);
    }

    [Test]
    public void Validate_NonSnowAnimalInJuneToAugust_ReturnsNoErrorMessage()
    {
        // Arrange
        var booking = new Booking
        {
            Animals = new List<Animal> { new Animal { Type = AnimalTypes.Farm } },
            Date = new DateTime(2024, 7, 15)
        };

     
        var mockRule = new Mock<NoSnowAnimalsInJuneToAugustRule>();

        // Act
        var result = mockRule.Object.Validate(booking);

        // Assert
        Assert.IsEmpty(result);
    }
    
    [Test]
    public void Validate_CustomerWithoutPlatinumCardBookingVipAnimal_ReturnsErrorMessage()
    {
        // Arrange
        var booking = new Booking
        {
            Customer = new Customer { CustomerCard = CustomerCards.Gold },
            Animals = new List<Animal> { new Animal { Type = AnimalTypes.Vip } }
        };


        var mockRule = new Mock<NoVipAnimalsForCustomerWithoutAPlatinumCard>();

        // Act
        var result = mockRule.Object.Validate(booking);

        // Assert
        Assert.AreEqual("Only customers with a platinum card can book VIP animals", result);
    }

    [Test]
    public void Validate_CustomerWithPlatinumCardBookingVipAnimal_ReturnsNoErrorMessage()
    {
        // Arrange
        var booking = new Booking
        {
            Customer = new Customer { CustomerCard = CustomerCards.Platinum },
            Animals = new List<Animal> { new Animal { Type = AnimalTypes.Vip } }
        };

        var mockRule = new Mock<NoVipAnimalsForCustomerWithoutAPlatinumCard>();

        // Act
        var result = mockRule.Object.Validate(booking);

        // Assert
        Assert.IsEmpty(result);
    }
    
    [Test]
    public void Validate_CustomerWithoutSilverCardBookingFourAnimals_ReturnsErrorMessage()
    {
        // Arrange
        var booking = new Booking
        {
            Customer = new Customer { CustomerCard = null },
            Animals = new List<Animal> { new Animal(), new Animal(), new Animal(), new Animal(),new Animal()}
        };

        var mockRule = new Mock<SilverCardAllowsOneExtraAnimalRule>();

        // Act
        var result = mockRule.Object.Validate(booking);

        // Assert
        Assert.AreEqual("Silver card only allows 1 extra animal", result);
    }
    
    [Test]
    public void Validate_CustomerWithSilverCardBookingFourAnimals_ReturnsNoErrorMessage()
    {
        // Arrange
        var booking = new Booking
        {
            Customer = new Customer { CustomerCard = CustomerCards.Silver },
            Animals = new List<Animal> { new Animal(), new Animal(), new Animal(), new Animal(),new Animal()}
        };

        var rule = new SilverCardAllowsOneExtraAnimalRule();

        // Act
        var result = rule.Validate(booking);

        // Assert
        Assert.IsEmpty(result);
    }
    
    [Test]
    public void ValidateBooking_AllRulesPass_ReturnsTrueAndEmptyErrorMessages()
    {
        // Arrange
        var booking = new Booking
        {
            Animals = new List<Animal> { new Animal { Type = AnimalTypes.Farm } },
            Customer = new Customer { CustomerCard = CustomerCards.Silver },
            Date = new DateTime(2024, 5, 15)
        };

        var mockManager= new Mock<BookingValidationManager>();

        // Act
        var result = mockManager.Object.ValidateBooking(booking, out var errorMessages);
        
        // Assert
        Assert.IsFalse(result);
        CollectionAssert.IsEmpty(errorMessages);
    }
    
    [Test]
    public void ValidateBooking_OneRuleFails_ReturnsFalseAndContainsErrorMessage()
    {
        
         List<Animal>  animals = new List<Animal>();
         animals.Add(new Animal { Type = AnimalTypes.Farm });
         animals.Add(new Animal { Name = "Leeuw" });
        
        // Arrange
        var booking = new Booking
        {
            Animals = animals,
            Customer = new Customer { CustomerCard = null },
            Date = new DateTime(2024, 1, 20) // A Saturday
        };

        var mockManager= new Mock<BookingValidationManager>();

        // Act
        var result = mockManager.Object.ValidateBooking(booking, out var errorMessages);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(1, errorMessages.Count);
        Assert.AreEqual("Nom nom nom", errorMessages[0]);
    }
    
    
}