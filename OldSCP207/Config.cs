using Exiled.API.Interfaces;
using System.ComponentModel;

namespace OldCola
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public float DamageMultiplier { get; set; } = 0.5f;
        public int SpeedBoostPerCola { get; set; } = 10;
    }
}