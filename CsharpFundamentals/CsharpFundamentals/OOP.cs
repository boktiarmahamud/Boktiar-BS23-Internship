using System;

namespace CsharpFundamentals
{
    // CUSTOM EXCEPTION (Defensive Programming - Requirement 5)
    public class InvalidTripException : Exception
    {
        public InvalidTripException(string message) : base(message) { }
    }

    // ENUMS & INTERFACES
    public enum TripStatus { Pending, Paid, Failed }

    // Polymorphism: any class implementing this can be used as a promotion
    public interface IPromotion
    {
        decimal ApplyDiscount(decimal currentFare);
    }

    // Abstraction: Trip uses this interface; it never sees the real payment class
    public interface IPaymentService
    {
        bool ProcessPayment(string passengerId, decimal amount);
    }
    // PASSENGER - modern C# record (immutable value type)
    public record Passenger(string Id, string Name);

    // VEHICLE - abstract base class (Encapsulation + Inheritance)
    public abstract class Vehicle
    {
        public string LicensePlate { get; init; }

        public decimal BaseFare { get; private set; }

        // Abstract properties: subclasses MUST provide their own rate values
        public abstract decimal PerKmRate { get; }
        public abstract decimal PerMinuteRate { get; }

        protected Vehicle(string licensePlate, decimal baseFare)
        {
            // Validate before allowing any data in — fail fast
            if (string.IsNullOrWhiteSpace(licensePlate))
                throw new ArgumentException("License plate cannot be empty.", nameof(licensePlate));
            if (baseFare <= 0)
                throw new ArgumentException("Base fare must be positive.", nameof(baseFare));

            LicensePlate = licensePlate;
            BaseFare = baseFare;
        }
    }

    public class StandardCar : Vehicle
    {
        // Fixed, flat rates — override the abstract properties
        public override decimal PerKmRate => 1.20m;
        public override decimal PerMinuteRate => 0.15m;

        // Passes plate and a hard-coded base fare up to the parent constructor
        public StandardCar(string licensePlate)
            : base(licensePlate, baseFare: 5.00m) { }
    }

    // LUXURY SEDAN (Inheritance + extra LuxuryTax behavior)
    public class LuxurySedan : Vehicle
    {
        // Premium rates
        public override decimal PerKmRate => 2.50m;
        public override decimal PerMinuteRate => 0.45m;

        public decimal LuxuryTax => 10.00m;

        public LuxurySedan(string licensePlate)
            : base(licensePlate, baseFare: 15.00m) { }
    }

    // Deducts a percentage (10 means 10% off)
    public class PercentageDiscount : IPromotion
    {
        private readonly decimal _percentage;

        public PercentageDiscount(decimal percentage)
        {
            if (percentage < 0 || percentage > 100)
                throw new ArgumentException("Percentage must be between 0 and 100.");
            _percentage = percentage;
        }

        public decimal ApplyDiscount(decimal currentFare)
            => currentFare * (1 - _percentage / 100m);
    }

    // Deducts a flat amount but never below the vehicle's base fare
    public class FlatDiscount : IPromotion
    {
        private readonly decimal _amount;
        private readonly Vehicle _vehicle;

        public FlatDiscount(decimal amount, Vehicle vehicle)
        {
            if (amount < 0)
                throw new ArgumentException("Discount amount cannot be negative.");
            _amount = amount;
            _vehicle = vehicle;
        }

        public decimal ApplyDiscount(decimal currentFare)
            => Math.Max(currentFare - _amount, _vehicle.BaseFare);
    }

    // PAYMENT SERVICE (Abstraction - Requirement 4)
    public class CreditCardPaymentService : IPaymentService
    {
        public bool ProcessPayment(string passengerId, decimal amount)
        {
            Console.WriteLine($"[CreditCard] Charging passenger '{passengerId}': ${amount:F2}");
            return true; 
        }
    }
    // TRIP CLASS (Ties everything together - Requirements 2, 3, 4, 5)
    public class Trip
    {
        private readonly Vehicle _vehicle;
        private readonly Passenger _passenger;
        private readonly IPromotion? _promotion; 

        public decimal DistanceKms { get; }
        public decimal DurationMinutes { get; }
        public TripStatus Status { get; private set; } = TripStatus.Pending;

        public Trip(Vehicle vehicle, Passenger passenger,
                    decimal distanceKms, decimal durationMinutes,
                    IPromotion? promotion = null)
        {
            if (vehicle is null)
                throw new InvalidTripException("A trip must have a vehicle assigned.");
            if (passenger is null)
                throw new ArgumentNullException(nameof(passenger));
            if (distanceKms <= 0)
                throw new InvalidTripException("Distance must be greater than zero.");
            if (durationMinutes <= 0)
                throw new InvalidTripException("Duration must be greater than zero.");

            _vehicle = vehicle;
            _passenger = passenger;
            DistanceKms = distanceKms;
            DurationMinutes = durationMinutes;
            _promotion = promotion;
        }

        // Polymorchism work
        
        public decimal CalculateFinalFare()
        {
            decimal fare = (_vehicle.PerKmRate * DistanceKms)
                         + (_vehicle.PerMinuteRate * DurationMinutes);

            if (_vehicle is LuxurySedan luxury)
                fare += luxury.LuxuryTax;

        
            if (_promotion is not null)
                fare = _promotion.ApplyDiscount(fare);

            return Math.Max(fare, _vehicle.BaseFare);
        }

        // ABSTRACTION at work
        public void CompleteTrip(IPaymentService paymentService)
        {
            if (Status == TripStatus.Paid)
                throw new InvalidOperationException("Trip is already paid.");

            decimal fare = CalculateFinalFare();
            bool success = paymentService.ProcessPayment(_passenger.Id, fare);
            Status = success ? TripStatus.Paid : TripStatus.Failed;

            Console.WriteLine($"[Trip] Status updated → {Status}");
        }
    }

    // PROGRAM — test suite demonstrating all scenarios
    class OOP
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Ride-Sharing Fare Engine Tests ===\n");

            var passenger = new Passenger("P001", "Alice");
            IPaymentService payment = new CreditCardPaymentService();

            // Test 1: StandardCar, no promotion 
            PrintHeader("Test 1: StandardCar — 10 km, 20 min, no promo");
            var stdCar = new StandardCar("ABC-123");
            var trip1 = new Trip(stdCar, passenger, distanceKms: 10, durationMinutes: 20);
            // Expected: (1.20 × 10) + (0.15 × 20) = 12.00 + 3.00 = $15.00
            Console.WriteLine($"Final Fare: ${trip1.CalculateFinalFare():F2}");
            trip1.CompleteTrip(payment);
            Console.WriteLine();

            // Test 2: LuxurySedan with luxury tax
            PrintHeader("Test 2: LuxurySedan — 8 km, 15 min, no promo");
            var luxury = new LuxurySedan("LUX-999");
            var trip2 = new Trip(luxury, passenger, distanceKms: 8, durationMinutes: 15);
            // Expected: (2.50 × 8) + (0.45 × 15) + 10 = 20 + 6.75 + 10 = $36.75
            Console.WriteLine($"Final Fare: ${trip2.CalculateFinalFare():F2}");
            trip2.CompleteTrip(payment);
            Console.WriteLine();

            // ── Test 3: StandardCar with 10% percentage discount ─
            PrintHeader("Test 3: StandardCar — 10 km, 20 min, 10% off");
            var promo10 = new PercentageDiscount(10);
            var trip3 = new Trip(stdCar, passenger, 10, 20, promotion: promo10);
            // Raw = $15.00, 10% off = $13.50
            Console.WriteLine($"Final Fare: ${trip3.CalculateFinalFare():F2}");
            trip3.CompleteTrip(payment);
            Console.WriteLine();

            // ── Test 4: StandardCar with $5 flat discount ────────
            PrintHeader("Test 4: StandardCar — 10 km, 20 min, $5 flat off");
            var flat5 = new FlatDiscount(5, stdCar);
            var trip4 = new Trip(stdCar, passenger, 10, 20, promotion: flat5);
            // Raw = $15.00, minus $5 = $10.00
            Console.WriteLine($"Final Fare: ${trip4.CalculateFinalFare():F2}");
            trip4.CompleteTrip(payment);
            Console.WriteLine();

            // Test 5: BaseFare floor — very short trip
            PrintHeader("Test 5: StandardCar — 1 km, 1 min (should hit $5 floor)");
            var trip5 = new Trip(stdCar, passenger, 1, 1);
            
            Console.WriteLine($"Final Fare: ${trip5.CalculateFinalFare():F2}  (floor applied)");
            Console.WriteLine();

            // Test 6: Defensive — negative distance
            PrintHeader("Test 6: Defensive — negative distance throws exception");
            try
            {
                var bad = new Trip(stdCar, passenger, distanceKms: -5, durationMinutes: 10);
            }
            catch (InvalidTripException ex)
            {
                Console.WriteLine($"Caught InvalidTripException: {ex.Message}");
            }
            Console.WriteLine();

            // Test 7: Defensive — zero duration 
            PrintHeader("Test 7: Defensive — zero duration throws exception");
            try
            {
                var bad = new Trip(stdCar, passenger, distanceKms: 5, durationMinutes: 0);
            }
            catch (InvalidTripException ex)
            {
                Console.WriteLine($"Caught InvalidTripException: {ex.Message}");
            }
        }

        static void PrintHeader(string msg)
        {
            Console.WriteLine($"-{msg}");
        }
    }
}