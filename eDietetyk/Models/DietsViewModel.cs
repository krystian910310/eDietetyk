namespace eDietetyk.Models
{
    public class DietsViewModel
    {
        public bool IsData { get; set; }
        public int TargetCalories { get; set; }
        public int CurrentCalories { get; set; }

        public int LeftCalories { get; set; }

        public bool IsExceededCalories { get; set; }
        public double Bmi { get; set; }
        public string Info { get; set; }
        public string BmiInfo { get; set; }
    }
}