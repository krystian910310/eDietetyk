using System.Collections.Generic;

namespace eDietetyk.Models
{
    public class ProgressModel
    {
        public bool IsData { get; set; }
        public List<int> Weight { get; set; }

        public List<string> Description { get; set; }
    }
}