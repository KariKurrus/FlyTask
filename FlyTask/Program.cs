namespace FlyTask
{
    public struct Coordinate
    {
        public uint XAxis { get; set; }

        public uint YAxis { get; set; }

        public uint ZAxis { get; set; }

        public Coordinate(uint xAxis, uint yAxis, uint zAxis)
        {
            XAxis = xAxis;
            YAxis = yAxis;
            ZAxis = zAxis;
        }

        public static Coordinate operator +(Coordinate firstSummand, Coordinate secondSummand)
        {
            var sum = new Coordinate();

            sum.XAxis = firstSummand.XAxis + secondSummand.XAxis;
            sum.YAxis = firstSummand.YAxis + secondSummand.YAxis;
            sum.ZAxis = firstSummand.ZAxis + secondSummand.ZAxis;

            return sum;
        }

        public static Coordinate operator -(Coordinate reduced, Coordinate subtractor)
        {
            var dif = new Coordinate();

            if (reduced.XAxis < subtractor.XAxis) dif.XAxis = 0; //below zero protection
            else dif.XAxis = reduced.XAxis - subtractor.XAxis;
            if (reduced.YAxis < subtractor.YAxis) dif.YAxis = 0; //below zero protection
            else dif.YAxis = reduced.YAxis - subtractor.YAxis;
            if (reduced.ZAxis < subtractor.ZAxis) dif.ZAxis = 0; //below zero protection
            else dif.ZAxis = reduced.ZAxis - subtractor.ZAxis;

            return dif;
        }

        public static Coordinate operator *(Coordinate firstMultiplier, Coordinate secondMultiplier)
        {
            var prod = new Coordinate();

            prod.XAxis = firstMultiplier.XAxis * secondMultiplier.XAxis;
            prod.YAxis = firstMultiplier.YAxis * secondMultiplier.YAxis;
            prod.ZAxis = firstMultiplier.ZAxis * secondMultiplier.ZAxis;

            return prod;
        }

        public static Coordinate operator /(Coordinate divisible, Coordinate divisor)
        {
            var quot = new Coordinate();

            if (divisor.XAxis == 0) quot.XAxis = 0; //below zero protection
            else quot.XAxis = divisible.XAxis / divisor.XAxis;
            if (divisor.YAxis == 0) quot.YAxis = 0;  //below zero protection
            else quot.YAxis = divisible.YAxis / divisor.YAxis;
            if (divisor.ZAxis == 0) divisor.ZAxis = 0; //below zero protection
            else quot.ZAxis = divisible.ZAxis / divisor.ZAxis;

            return quot;
        }

        public override string ToString() => $"({XAxis},{YAxis},{ZAxis})";
    }

    public interface IFlyable
    {
        public int Speed { get; set; }

        public double FlyTo(Coordinate beginPoint, Coordinate endPoint);

        public double GetFlyTime(Coordinate beginPoint, Coordinate endPoint);
    }

    public class Bird : IFlyable
    {
        public Coordinate CurrentPosition { get; set; }

        public int Speed { get; set; }

        public Bird(int speed)
        {
            CurrentPosition = new Coordinate(0, 0, 0);
            Speed = speed;
        }

        public double FlyTo(Coordinate beginPoint, Coordinate endPoint)
        {
            double wayDistance = Math.Sqrt(Math.Pow(endPoint.XAxis - beginPoint.XAxis, 2) + Math.Pow(endPoint.YAxis - beginPoint.YAxis, 2) + Math.Pow(endPoint.ZAxis - beginPoint.ZAxis, 2));

            return wayDistance;
        }

        public double GetFlyTime(Coordinate beginPoint, Coordinate endPoint)
        {

            double flyTime = FlyTo(beginPoint, endPoint) / Speed;

            return flyTime;
        }
    }

    public class Airplane : IFlyable
    {
        public Coordinate CurrentPosition { get; set; }
        public int Speed { get; set; }

        public Airplane(int speed)
        {
            CurrentPosition = new Coordinate(0, 0, 0);
            Speed = speed;
        }

        public double FlyTo(Coordinate beginPoint, Coordinate endPoint)
        {
            double wayDistance = Math.Sqrt(Math.Pow(endPoint.XAxis - beginPoint.XAxis, 2) + Math.Pow(endPoint.YAxis - beginPoint.YAxis, 2) + Math.Pow(endPoint.ZAxis - beginPoint.ZAxis, 2));

            return wayDistance;
        }
        public double GetFlyTime(Coordinate beginPoint, Coordinate endPoint)
        {
            double flyTime = 0;

            for (double i = FlyTo(beginPoint, endPoint); i >= 0; i -= 10)
            {
                flyTime = FlyTo(beginPoint, endPoint) / Speed;
                Speed += 10;
            }

            return flyTime;
        }
    }

    public class Drone : IFlyable
    {
        public Coordinate CurrentPosition { get; set; }
        public int Speed { get; set; }

        public Drone(int speed)
        {
            CurrentPosition = new Coordinate(0, 0, 0);
            Speed = speed;
        }
        public double FlyTo(Coordinate beginPoint, Coordinate endPoint)
        {
            double wayDistance = Math.Sqrt(Math.Pow(endPoint.XAxis - beginPoint.XAxis, 2) + Math.Pow(endPoint.YAxis - beginPoint.YAxis, 2) + Math.Pow(endPoint.ZAxis - beginPoint.ZAxis, 2));
            if(wayDistance> 1000)
            {
                return 0;
            }
            else return wayDistance;

        }
        public double GetFlyTime(Coordinate beginPoint, Coordinate endPoint )
        {
            if(FlyTo(beginPoint,endPoint)==0) return 0;
            else
            {
                double tenParts;
                double flyTime = FlyTo(beginPoint, endPoint) / Speed;
                tenParts = Convert.ToInt32(flyTime / 0.10);
                flyTime += tenParts/100;
                return tenParts/100;
            }
            
        }
    }
    internal class Program
    {
        public void BirdRealistaion()
        {
            Random randSpeed = new Random();

            var Bird = new Bird(randSpeed.Next(0, 20));

            var BeginPoint = new Coordinate(230, 130, 600);

            var EndPoint = new Coordinate(620, 201, 745);

            Console.WriteLine($"If bird will fly from point {BeginPoint} to point {EndPoint}");
            Console.WriteLine($"With speed - {Bird.Speed} km/h");
            Console.WriteLine($"Way distance is {Bird.FlyTo(BeginPoint, EndPoint)} km");
            Console.WriteLine($"It will take {Bird.GetFlyTime(BeginPoint, EndPoint)} hours");
        }

        public void AirplaneRealisation()
        {
            var Airplane = new Airplane(200);

            var BeginPoint = new Coordinate(230, 130, 600);

            var EndPoint = new Coordinate(620, 201, 745);

            Console.WriteLine($"If airplane will fly from point {BeginPoint} to point {EndPoint}");
            Console.WriteLine($"With speed - {Airplane.Speed} km/h and increasing it every 10 km for 10km/h");
            Console.WriteLine($"Way distance is {Airplane.FlyTo(BeginPoint, EndPoint)} km");
            Console.WriteLine($"It will take {Airplane.GetFlyTime(BeginPoint, EndPoint)} hours");
        }

        public void DroneRealisation()
        {
            var Drone = new Drone(40);

            var BeginPoint = new Coordinate(230, 130, 600);

            var EndPoint = new Coordinate(620, 201, 745);

            Console.WriteLine($"If drone will fly from point {BeginPoint} to point {EndPoint}");
            Console.WriteLine($"With speed - {Drone.Speed} km/h");
            if (Drone.FlyTo(BeginPoint, EndPoint) != 0)
            {
                Console.WriteLine($"Way distance is {Drone.FlyTo(BeginPoint, EndPoint)} km");
            }
            else Console.WriteLine("Way Distance is more than 1000km");
            if(Drone.GetFlyTime(BeginPoint, EndPoint)!=0)
            {
                Console.WriteLine($"It will take {Drone.GetFlyTime(BeginPoint, EndPoint)} hours");
            }
            else Console.WriteLine("Way Distance is more than 1000km");

        }

        static void Main(string[] args)
        {
            var program = new Program();

            program.BirdRealistaion();
            Console.WriteLine();
            program.AirplaneRealisation();
            Console.WriteLine();
            program.DroneRealisation();
        }
    }
}