using BusinessLogic.Services.PriceServices;
using BusinessLogic.Services.PriceServices.DiscountRules;
using Data.Enums;
using Data.Models;
using Moq;

namespace BeestjeOpJeFeestjeTestSuite;

public class DiscountRulesTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void SameTypeDiscountRule_AppliesDiscountForThreeAnimalsOfSameType()
    {
        //Arrange
        var booking = new Booking
        {
            Animals = new List<Animal>
            {
                new Animal { Type = AnimalTypes.Desert },
                new Animal { Type = AnimalTypes.Desert },
                new Animal { Type = AnimalTypes.Desert }
            }
        };

        //Act
        var mockRule = new Mock<SameTypeDiscountRule>();
        double discount = mockRule.Object.CalculateDiscount(booking);

        //Assert
        Assert.That(discount, Is.EqualTo(0.1)); // 10% discount for 3 animals of the same type
    }
    
    [Test]
    public void EendDiscountRule_Applies50PercentDiscountForEendWith50PercentChance()
    {
        //Arrange
        var booking = new Booking
        {
            Animals = new List<Animal>
            {
                new Animal { Name = "Eend" }
            }
        };

        //Act
        var mockRule = new Mock<EendDiscountRule>();
        double discount = mockRule.Object.CalculateDiscount(booking);

        //Assert
        Assert.IsTrue(discount == 0.5 || discount == 0.0);
    }
    
    
    [Test]
    public void DayOfWeekDiscountRule_Applies15PercentDiscountForMondayOrTuesday()
    {
        var bookingMonday = new Booking { Date = new DateTime(2024, 1, 1) }; // Monday
        var bookingTuesday = new Booking { Date = new DateTime(2024, 1, 2) }; // Tuesday
        var bookingOtherDay = new Booking { Date = new DateTime(2024, 1, 3) }; // Wednesday

        var mockRule = new  Mock<DayOfWeekDiscountRule>();

        Assert.That(mockRule.Object.CalculateDiscount(bookingMonday), Is.EqualTo(0.15));
        Assert.That(mockRule.Object.CalculateDiscount(bookingTuesday), Is.EqualTo(0.15));
        Assert.That(mockRule.Object.CalculateDiscount(bookingOtherDay), Is.EqualTo(0.0));
    }
    
    
    [Test]
    public void LetterDiscountRule_AppliesAdditionalDiscountBasedOnLettersInAnimalName()
    {
        //Arrange
        var booking = new Booking
        {
            Animals = new List<Animal>
            {
                new Animal { Name = "ABc" },
                new Animal { Name = "DE" }
            }
        };

        //Act
        var mockRule = new Mock<LetterDiscountRule>();
        double discount = mockRule.Object.CalculateDiscount(booking);

        //Assert
        // Additional 2% for each occurrence of letters A, B, C, D, E, F
        Assert.That(discount, Is.EqualTo(0.06));
    }
    
    [Test]
    public void LetterDiscountRule_AppliesAdditionalDiscountBasedOnLettersInAnimalNameNonAlphabetical()
    {
        //Arrange
        var booking = new Booking
        {
            Animals = new List<Animal>
            {
                new Animal { Name = "t" },
                new Animal { Name = "t" }
            }
        };

        //Act
        var mockRule = new Mock<LetterDiscountRule>();
        double discount = mockRule.Object.CalculateDiscount(booking);

        //Assert
        Assert.That(discount, Is.EqualTo(0.0));
    }
    
    
    [Test]
    public void CustomerCardDiscountRule_Applies10PercentDiscountForCustomerWithCard()
    {
        var CustomerWithCard = new Customer { CustomerCard = CustomerCards.Gold };
        var CustomerWithOutCard = new Customer { CustomerCard = null };
        
        var bookingWithCard = new Booking { Customer = CustomerWithCard };
        var bookingWithoutCard = new Booking { Customer = CustomerWithOutCard };

        var mockRule = new Mock<CustomerCardDiscountRule>();
        
        Assert.That(mockRule.Object.CalculateDiscount(bookingWithCard), Is.EqualTo(0.1));
        Assert.That(mockRule.Object.CalculateDiscount(bookingWithoutCard), Is.EqualTo(0.0));
    }
    
    
    [Test]
    public void PriceManager_CalculatesCorrectDiscount()
    {
        //Arrange
        var booking = new Booking
        {
            Animals = new List<Animal>
            {
                new Animal {Name = "t", Type = AnimalTypes.Desert },
                new Animal {Name = "t", Type = AnimalTypes.Desert },
                new Animal {Name = "t", Type = AnimalTypes.Desert }
            },
            Customer = new Customer { CustomerCard = CustomerCards.Gold },
            Date = new DateTime(2024, 1, 1)
        };

        //Act
        var mockPriceManager = new Mock<PriceManager>();
        double discount = mockPriceManager.Object.CalculateDiscount(booking);

        //Assert
        // 10% discount for 3 animals of the same type
        // 10% discount for customer with gold card
        // 15% discount for booking on Monday
        Assert.That(discount, Is.EqualTo(0.35).Within(0.0000001));
    }

}